using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnChoice : Photon.MonoBehaviour {
    public GameObject TurnChoiceObject;
    public static bool TurnChoiceGoingOn = true;
    public bool TurnChoiceStarted = false;
    public static int MasterChoice = 0; //1등 2등
    public static bool MasterChoiceIsLeft = false;
    public GameObject CardLeft;
    public GameObject CardRight;
    public Button CardLeftButton;
    public Button CardRightButton;
    public GameObject TurnChoiceTimer;
    public GameObject FirstResultCard;
    public GameObject SecondResultCard;
    public GameObject MasterChoosesText;
    public GameObject Player1Rank;
    public GameObject Player2Rank;
    public Text Player1RankText, Player2RankText;
    public Vector2 CardToGoPlayer1Location;
    public Vector2 CardToGoPlayer2Location;
    public Text TimerText;
    private PhotonView pv;
    public AudioClip CardPopUpSound;
    public AudioClip CardSelectSound;
    public AudioClip ResultSound;

    void Start () {
        TurnChoiceObject.SetActive(false);
        TurnChoiceGoingOn = true;
        pv = GetComponent<PhotonView>();
        TurnChoiceStarted = false;
        MasterChoice = 0;
        CardToGoPlayer1Location = Player1Rank.transform.position;
        CardToGoPlayer2Location = Player2Rank.transform.position;
        FirstResultCard.SetActive(false);
        SecondResultCard.SetActive(false);
    }
	
	void Update () {
		if((GameObject.Find("Player1") && (GameObject.Find("Player2")) && (TurnChoiceStarted == false))){ //두 플레이어 다 접속 완료했을 경우
            TurnChoiceObject.SetActive(true);
            TurnChoiceStarted = true;
            if (PhotonNetwork.isMasterClient)
                StartCoroutine(MasterTurnChoice());
            else
                StartCoroutine(SubPlayerWait());
        }
	}

    IEnumerator MasterTurnChoice()
    {
        CardLeftButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        CardRightButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

        this.GetComponent<AudioSource>().clip = CardPopUpSound;
        this.GetComponent<AudioSource>().Play();
        MasterChoosesText.SetActive(true);
        CardLeft.SetActive(true);
        CardRight.SetActive(true);
        TurnChoiceTimer.SetActive(true);
       
        for(int i = 5; i >= 0; i--)
        {
            TimerText.text = i.ToString();
            if(MasterChoice != 0)
            {
                break;
            }
            yield return new WaitForSeconds(1.0f);
        }
        if(MasterChoice == 0) //시간 지났는데도 카드 안골랐다면
        {
            MasterChoice = Random.Range(1, 3); //1등이나 2등 랜덤으로 정해짐
            if (MasterChoice == 1)
                pv.RPC("TurnChoiceResultSync", PhotonTargets.All, true, 1, 2);
            else
                pv.RPC("TurnChoiceResultSync", PhotonTargets.All, true, 2, 1);

            StartCoroutine(CardRotation(true, MasterChoice)); //왼쪽카드 자동으로 뒤집힘
        }
    }

    IEnumerator SubPlayerWait()
    {
        CardLeftButton.GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f, 0.7f);
        CardRightButton.GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f, 0.7f);
        this.GetComponent<AudioSource>().clip = CardPopUpSound;
        this.GetComponent<AudioSource>().Play();
        MasterChoosesText.SetActive(true);
        CardLeft.SetActive(true);
        CardLeftButton.interactable = false;
        CardRight.SetActive(true);
        CardRightButton.interactable = false;
        TurnChoiceTimer.SetActive(true);

        for (int i = 5; i >= 0; i--)
        {
            TimerText.text = i.ToString();
            if (MasterChoice != 0)
            {
                break;
            }
            yield return new WaitForSeconds(1.0f);
        }
        while(MasterChoice == 0) //(마스터 연결이 느릴것을 대비) 아직 결과 전달받지 못한 경우 기다림
        {
            yield return null;
        }
        StartCoroutine(CardRotation(MasterChoiceIsLeft,MasterChoice));     
    }

    IEnumerator CardRotation(bool LeftOrRight, int FirstOrSecond) //마스터 결정 기준
    {
        this.GetComponent<AudioSource>().clip = ResultSound;
        this.GetComponent<AudioSource>().Play();
        TurnChoiceTimer.SetActive(false);
        if (FirstOrSecond == 1) //마스터가 고른 카드가 1등
        {
            if(LeftOrRight) // 마스터가 고른 카드가 1등, 그게 왼쪽이라면
            {

                if (PhotonNetwork.isMasterClient) //여기부터 하기***********************************************************************
                    CardRightButton.GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f, 0.7f);
                else
                    CardRightButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                for (int i = 10; i <= 90; i = i + 10) //고른 카드 뒤집기
                {
                    CardLeft.transform.rotation = Quaternion.Euler(0, -i, 0);
                    yield return new WaitForSeconds(0.02f);
                }
                CardLeft.SetActive(false);
                FirstResultCard.transform.position = CardLeft.transform.position;
                FirstResultCard.transform.rotation = Quaternion.Euler(0, 90, 0);
                FirstResultCard.SetActive(true);

                if (PhotonNetwork.isMasterClient) //여기부터 하기***********************************************************************
                    FirstResultCard.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                else
                    FirstResultCard.GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f, 0.7f);


                for (int i = 90; i >= 0; i = i - 10)
                {
                    FirstResultCard.transform.rotation = Quaternion.Euler(0, i, 0);
                    yield return new WaitForSeconds(0.02f);
                }
                CardLeft.transform.rotation = Quaternion.Euler(0, 0, 0);
                
                yield return new WaitForSeconds(0.5f);

                for (int i = 10; i <= 90; i = i + 10) //나머지 카드 보여주기
                {
                    CardRight.transform.rotation = Quaternion.Euler(0, -i, 0);
                    yield return new WaitForSeconds(0.01f);
                }
                CardRight.SetActive(false);
                CardRight.transform.rotation = Quaternion.Euler(0, 0, 0);
                SecondResultCard.transform.position = CardRight.transform.position;
                SecondResultCard.transform.rotation = Quaternion.Euler(0, 90, 0);
                SecondResultCard.SetActive(true);


                if (PhotonNetwork.isMasterClient) //여기부터 하기***********************************************************************
                    SecondResultCard.GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f, 0.7f);
                else
                    SecondResultCard.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                for (int i = 90; i >= 0; i = i - 10)
                {
                    SecondResultCard.transform.rotation = Quaternion.Euler(0, i, 0);
                    yield return new WaitForSeconds(0.01f);
                }
                yield return new WaitForSeconds(1.0f);
                MasterChoosesText.SetActive(false);
                yield return new WaitForSeconds(1.0f);
                
            }

            else //마스터가 고른 카드가 1등, 그게 오른쪽이라면
            {
                if (PhotonNetwork.isMasterClient) //여기부터 하기***********************************************************************
                    CardLeftButton.GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f, 0.7f);
                else
                    CardLeftButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                for (int i = 10; i <= 90; i = i + 10) //고른 카드 뒤집기
                {
                    CardRight.transform.rotation = Quaternion.Euler(0, -i, 0);
                    yield return new WaitForSeconds(0.02f);
                }
                CardRight.SetActive(false);
                FirstResultCard.transform.position = CardRight.transform.position;
                FirstResultCard.transform.rotation = Quaternion.Euler(0, 90, 0);
                FirstResultCard.SetActive(true);

                if (PhotonNetwork.isMasterClient) //여기부터 하기***********************************************************************
                    FirstResultCard.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                else
                    FirstResultCard.GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f, 0.7f);


                for (int i = 90; i >= 0; i = i - 10)
                {
                    FirstResultCard.transform.rotation = Quaternion.Euler(0, i, 0);
                    yield return new WaitForSeconds(0.02f);
                }
                CardRight.transform.rotation = Quaternion.Euler(0, 0, 0);

                yield return new WaitForSeconds(0.5f);

                for (int i = 10; i <= 90; i = i + 10) //나머지 카드 보여주기
                {
                    CardLeft.transform.rotation = Quaternion.Euler(0, -i, 0);
                    yield return new WaitForSeconds(0.01f);
                }
                CardLeft.SetActive(false);
                CardLeft.transform.rotation = Quaternion.Euler(0, 0, 0);
                SecondResultCard.transform.position = CardLeft.transform.position;
                SecondResultCard.transform.rotation = Quaternion.Euler(0, 90, 0);
                SecondResultCard.SetActive(true);

                if (PhotonNetwork.isMasterClient) //여기부터 하기***********************************************************************
                    SecondResultCard.GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f, 0.7f);
                else
                    SecondResultCard.GetComponent<Image>().color = new Color(1, 1, 1, 1);


                for (int i = 90; i >= 0; i = i - 10)
                {
                    SecondResultCard.transform.rotation = Quaternion.Euler(0, i, 0);
                    yield return new WaitForSeconds(0.01f);
                }
                yield return new WaitForSeconds(1.0f);
                MasterChoosesText.SetActive(false);
                yield return new WaitForSeconds(1.0f);
                
            }
            float mxdif = (CardToGoPlayer1Location.x - FirstResultCard.transform.position.x) / 100;
            float mydif = (CardToGoPlayer1Location.y - FirstResultCard.transform.position.y) / 100;
            float sxdif = (CardToGoPlayer2Location.x - SecondResultCard.transform.position.x) / 100;
            float sydif = (CardToGoPlayer2Location.y - SecondResultCard.transform.position.y) / 100;
            for (int i = 0; i < 100; i++) //결과 카드 각자 프로필 쪽으로 이동하기
            {
                FirstResultCard.transform.position = new Vector2((FirstResultCard.transform.position.x + mxdif), (FirstResultCard.transform.position.y + mydif));
                FirstResultCard.transform.localScale = new Vector2((FirstResultCard.transform.localScale.x - 0.01f), (FirstResultCard.transform.localScale.y - 0.01f));
                SecondResultCard.transform.position = new Vector2((SecondResultCard.transform.position.x + sxdif), (SecondResultCard.transform.position.y + sydif));
                SecondResultCard.transform.localScale = new Vector2((SecondResultCard.transform.localScale.x - 0.01f), (SecondResultCard.transform.localScale.y - 0.01f));
                yield return new WaitForSeconds(0.002f);
            }
            FirstResultCard.SetActive(false);
            SecondResultCard.SetActive(false);
            Player1RankText.text = "1등";
            Player2RankText.text = "2등";
        }
        else //마스터가 고른 카드가 2등 카드라면
        {
            if (LeftOrRight) // 마스터가 고른 카드가 2등, 그게 왼쪽이라면
            {

                if (PhotonNetwork.isMasterClient) //여기부터 하기***********************************************************************
                    CardRightButton.GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f, 0.7f);
                else
                    CardRightButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);


                for (int i = 10; i <= 90; i = i + 10) //고른 카드 뒤집기
                {
                    CardLeft.transform.rotation = Quaternion.Euler(0, -i, 0);
                    yield return new WaitForSeconds(0.02f);
                }
                CardLeft.SetActive(false);
                SecondResultCard.transform.position = CardLeft.transform.position;
                SecondResultCard.transform.rotation = Quaternion.Euler(0, 90, 0);
                SecondResultCard.SetActive(true);

                if (PhotonNetwork.isMasterClient) //여기부터 하기***********************************************************************
                    SecondResultCard.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                else
                    SecondResultCard.GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f, 0.7f);

                for (int i = 90; i >= 0; i = i - 10)
                {
                    SecondResultCard.transform.rotation = Quaternion.Euler(0, i, 0);
                    yield return new WaitForSeconds(0.02f);
                }
                CardLeft.transform.rotation = Quaternion.Euler(0, 0, 0);

                yield return new WaitForSeconds(0.5f);

                for (int i = 10; i <= 90; i = i + 10) //나머지 카드 보여주기
                {
                    CardRight.transform.rotation = Quaternion.Euler(0, -i, 0);
                    yield return new WaitForSeconds(0.01f);
                }
                CardRight.SetActive(false);
                CardRight.transform.rotation = Quaternion.Euler(0, 0, 0);
                FirstResultCard.transform.position = CardRight.transform.position;
                FirstResultCard.transform.rotation = Quaternion.Euler(0, 90, 0);
                FirstResultCard.SetActive(true);

                if (PhotonNetwork.isMasterClient) //여기부터 하기***********************************************************************
                    FirstResultCard.GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f, 0.7f);
                else
                    FirstResultCard.GetComponent<Image>().color = new Color(1, 1, 1, 1);


                for (int i = 90; i >= 0; i = i - 10)
                {
                    FirstResultCard.transform.rotation = Quaternion.Euler(0, i, 0);
                    yield return new WaitForSeconds(0.01f);
                }
                yield return new WaitForSeconds(1.0f);
                MasterChoosesText.SetActive(false);
                yield return new WaitForSeconds(1.0f);

            }

            else //마스터가 고른 카드가 2등, 그게 오른쪽이라면
            {

                if (PhotonNetwork.isMasterClient) //여기부터 하기***********************************************************************
                    CardLeftButton.GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f, 0.7f);
                else
                    CardLeftButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);



                for (int i = 10; i <= 90; i = i + 10) //고른 카드 뒤집기
                {
                    CardRight.transform.rotation = Quaternion.Euler(0, -i, 0);
                    yield return new WaitForSeconds(0.02f);
                }
                CardRight.SetActive(false);
                SecondResultCard.transform.position = CardRight.transform.position;
                SecondResultCard.transform.rotation = Quaternion.Euler(0, 90, 0);
                SecondResultCard.SetActive(true);

                if (PhotonNetwork.isMasterClient) //여기부터 하기***********************************************************************
                    SecondResultCard.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                else
                    SecondResultCard.GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f, 0.7f);


                for (int i = 90; i >= 0; i = i - 10)
                {
                    SecondResultCard.transform.rotation = Quaternion.Euler(0, i, 0);
                    yield return new WaitForSeconds(0.02f);
                }
                CardRight.transform.rotation = Quaternion.Euler(0, 0, 0);

                yield return new WaitForSeconds(0.5f);

                for (int i = 10; i <= 90; i = i + 10) //나머지 카드 보여주기
                {
                    CardLeft.transform.rotation = Quaternion.Euler(0, -i, 0);
                    yield return new WaitForSeconds(0.01f);
                }
                CardLeft.SetActive(false);
                CardLeft.transform.rotation = Quaternion.Euler(0, 0, 0);
                FirstResultCard.transform.position = CardLeft.transform.position;
                FirstResultCard.transform.rotation = Quaternion.Euler(0, 90, 0);
                FirstResultCard.SetActive(true);

                if (PhotonNetwork.isMasterClient) //여기부터 하기***********************************************************************
                    FirstResultCard.GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f, 0.7f);
                else
                    FirstResultCard.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                for (int i = 90; i >= 0; i = i - 10)
                {
                    FirstResultCard.transform.rotation = Quaternion.Euler(0, i, 0);
                    yield return new WaitForSeconds(0.01f);
                }
                yield return new WaitForSeconds(1.0f);
                MasterChoosesText.SetActive(false);
                yield return new WaitForSeconds(1.0f);

            }
            float mxdif = (CardToGoPlayer1Location.x - SecondResultCard.transform.position.x) / 100;
            float mydif = (CardToGoPlayer1Location.y - SecondResultCard.transform.position.y) / 100;
            float sxdif = (CardToGoPlayer2Location.x - FirstResultCard.transform.position.x) / 100;
            float sydif = (CardToGoPlayer2Location.y - FirstResultCard.transform.position.y) / 100;
            for (int i = 0; i < 100; i++) //결과 카드 각자 프로필 쪽으로 이동하기
            {
                FirstResultCard.transform.position = new Vector2((FirstResultCard.transform.position.x + sxdif), (FirstResultCard.transform.position.y + sydif));
                FirstResultCard.transform.localScale = new Vector2((FirstResultCard.transform.localScale.x - 0.01f), (FirstResultCard.transform.localScale.y - 0.01f));
                SecondResultCard.transform.position = new Vector2((SecondResultCard.transform.position.x + mxdif), (SecondResultCard.transform.position.y + mydif));
                SecondResultCard.transform.localScale = new Vector2((SecondResultCard.transform.localScale.x - 0.01f), (SecondResultCard.transform.localScale.y - 0.01f));
                yield return new WaitForSeconds(0.002f);
            }
            FirstResultCard.SetActive(false);
            SecondResultCard.SetActive(false);
            Player1RankText.text = "2등";
            Player2RankText.text = "1등";

        }
        yield return new WaitForSeconds(1.0f);
        TurnChoiceGoingOn = false;
    }

    [PunRPC] //원격 호출용 함수
    void TurnChoiceResultSync(bool ChoiceLeft, int Master, int Sub)
    {
        MasterChoiceIsLeft = ChoiceLeft; //True인 경우 왼쪽을 고름
        MasterChoice = Master; //1 혹은 2
        if (PhotonNetwork.isMasterClient)
            TurnSystem.MyTurnNumber = Master;
        else
            TurnSystem.MyTurnNumber = Sub;
    }

    public void CardLeftButtonClick()
    {
        this.GetComponent<AudioSource>().clip = CardSelectSound;
        this.GetComponent<AudioSource>().Play();
        CardRightButton.interactable = false;
        MasterChoice = Random.Range(1, 3); //카드 누르는 위치와 상관없이 1등 2등 랜덤으로 정해짐
        if(MasterChoice == 1)
            pv.RPC("TurnChoiceResultSync", PhotonTargets.All, true, 1, 2);
        else
            pv.RPC("TurnChoiceResultSync", PhotonTargets.All, true, 2, 1);

        StartCoroutine(CardRotation(true, MasterChoice));
    }

    public void CardRightButtonClick()
    {
        this.GetComponent<AudioSource>().clip = CardSelectSound;
        this.GetComponent<AudioSource>().Play();
        CardLeftButton.interactable = false;
        MasterChoice = Random.Range(1, 3); //카드 누르는 위치와 상관없이 1등 2등 랜덤으로 정해짐
        if (MasterChoice == 1)
            pv.RPC("TurnChoiceResultSync", PhotonTargets.All, false, 1, 2);
        else
            pv.RPC("TurnChoiceResultSync", PhotonTargets.All, false, 2, 1);

        StartCoroutine(CardRotation(false, MasterChoice));
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) //스트림 송수신 (데이터 동기화 콜백함수)
    { }
}
