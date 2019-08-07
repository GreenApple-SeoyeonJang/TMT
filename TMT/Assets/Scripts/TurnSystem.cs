using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystem : Photon.MonoBehaviour {
    public static int CurrentTurnNumber = 1; //1인 사람이 먼저 시작함
    public static int MyTurnNumber; //Turn Choice 이후 정해짐
    public static bool TurnGoingOn = false;
    public GameObject RoulettePanel;
    public static int TurnProcess; //1:턴 시작.룰렛판 생김, 2:룰렛 완료.움직임 시작, 3:움직임 완료, 보드 이펙트 적용 시작 4:피로도 체크하고 넘었으면 병원감 5:끝
    public int WhoAmI;
    public GameObject Player1Cover;
    public GameObject Player2Cover;
    public bool FoundOtherPlayer;
    public GameObject Player1;
    public GameObject Player2;
    private PhotonView pv;
    public AudioClip MyTurnSound;
    public Image RouletteImage;
    public Button RouletteButton;
    public Image RouletteArrowImage;
    public GameObject StillHospitalNotice;
    public Text StillHospitalNoticeText;
    public Text Player1Name;
    public Text Player2Name;
    public GameObject HospitalOutNotice;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        CurrentTurnNumber = 1; //플레이어1부터 시작
        MyTurnNumber = 0;
    }

    void Start () {
        StillHospitalNotice.SetActive(false);
        HospitalOutNotice.SetActive(false);
        FoundOtherPlayer = false;
        TurnGoingOn = false;
        TurnProcess = 0;
        if (PhotonNetwork.isMasterClient)
            WhoAmI = 1;

        else
            WhoAmI = 2;
    }

    void Update()
    {
        if (!TurnChoice.TurnChoiceGoingOn) //(젤 처음)선 고르기가 끝났다면
        {
            if (FoundOtherPlayer == false)
            {
                FoundOtherPlayer = true;
                Player1 = GameObject.Find("Player1");
                Player2 = GameObject.Find("Player2");
            }

            if ((CurrentTurnNumber == MyTurnNumber) && (TurnGoingOn == false)) //자신의 턴 차례
            {
                if (HospitalSystem.InTheHospital == true)
                {
                    pv.RPC("StillHospitalNoticeOn", PhotonTargets.All, WhoAmI);
                }
                   
                else
                {
                    this.GetComponent<AudioSource>().clip = MyTurnSound;
                    this.GetComponent<AudioSource>().Play();
                    if (HospitalSystem.OkComeOut == true) //병원에 입원해있었던 경우 다시 제자리로 돌아가고 시작함
                    {
                        pv.RPC("HospitalOutNoticeStart", PhotonTargets.All, null);
                        HospitalSystem.OkComeOut = false;
                        if (WhoAmI == 1)
                            GameObject.Find("Player1").transform.position = MovementSystem.BoardLocation[MovementSystem.MyCurrentBoard];
                        else
                            GameObject.Find("Player2").transform.position = MovementSystem.BoardLocation[MovementSystem.MyCurrentBoard];
                    }
                    TurnGoingOn = true;
                    TurnStart();
                }   
            }

            if (TurnProcess == 5) //자신의 턴일때 해야할 일이 다 끝났다면
            {
                if (MyTurnNumber == 1) //플레이어1인 경우 플레이어2의 턴으로 바뀜
                    pv.RPC("CurrentTurnNumberSync", PhotonTargets.All, 2);
                else
                    pv.RPC("CurrentTurnNumberSync", PhotonTargets.All, 1);

                TurnProcess = 0;
                TurnGoingOn = false;
            } 

            if(CurrentTurnNumber == 1) //1가진 사람의 턴일때
            {
                if(TurnChoice.MasterChoice == 1) //플레이어 1이 1을 가진 경우
                {
                    Player1Cover.SetActive(false);
                    Player2Cover.SetActive(true);
                    Player1.GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 1f, 1f);
                    Player2.GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 1f, 0.5f);
                }
                else //플레이어 2가 1을 가진 경우
                {
                    Player1Cover.SetActive(true);
                    Player2Cover.SetActive(false);
                    Player1.GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 1f, 0.5f);
                    Player2.GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 1f, 1f);
                }
                
            }
            else //2가진 사람의 턴일때
            {
                if (TurnChoice.MasterChoice == 1) //플레이어 1이 1을 가진 경우
                {
                    Player1Cover.SetActive(true);
                    Player2Cover.SetActive(false);
                    Player1.GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 1f, 0.5f);
                    Player2.GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 1f, 1f);
                }
                else //플레이어 2가 1을 가진 경우
                {
                    Player1Cover.SetActive(false);
                    Player2Cover.SetActive(true);
                    Player1.GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 1f, 1f);
                    Player2.GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 1f, 0.5f);
                }
            }
        }
    }

    [PunRPC]
    void StillHospitalNoticeOn(int Who)
    {
        StartCoroutine(HospitalNoticeOn(Who));
    }

    IEnumerator HospitalNoticeOn(int Who)
    {
        if (Who == 1)
            StillHospitalNoticeText.text = Player1Name.text + "님 아직 퇴원하시면 안돼요!\n한 턴 더 쉬고 가실게요~";
        else
            StillHospitalNoticeText.text = Player2Name.text + "님 아직 퇴원하시면 안돼요!\n한 턴 더 쉬고 가실게요~";

        StillHospitalNotice.SetActive(true);
        yield return new WaitForSeconds(3f);
        StillHospitalNotice.SetActive(false);
        if (Who == WhoAmI)
        {
            TurnGoingOn = true;
            TurnProcess = 5;
            HospitalSystem.InTheHospital = false; //병원 입원으로 인해 한 턴 넘어감
            HospitalSystem.OkComeOut = true;
        }
    }

    [PunRPC]
    void HospitalOutNoticeStart()
    {
        StartCoroutine(HospitalOutNoticeOn());
    }

    IEnumerator HospitalOutNoticeOn()
    {
        HospitalOutNotice.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        HospitalOutNotice.SetActive(false);
        yield return new WaitForSeconds(0.7f);
        HospitalOutNotice.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        HospitalOutNotice.SetActive(false);
        yield return new WaitForSeconds(0.7f);
        HospitalOutNotice.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        HospitalOutNotice.SetActive(false);
    }
    public void TurnStart()
    {
        pv.RPC("ShowRoulettePanel", PhotonTargets.All, null);
        TurnProcess = 1;
    }

    [PunRPC]
    void ShowRoulettePanel()
    {
        if(CurrentTurnNumber == MyTurnNumber)
        {
            RouletteImage.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            RouletteButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            RouletteArrowImage.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        else
        {
            RouletteImage.GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f, 0.7f);
            RouletteButton.GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f, 0.7f);
            RouletteArrowImage.GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f, 0.7f);
        }
        RoulettePanel.SetActive(true);
    }

    [PunRPC] //원격 호출용 함수
    void CurrentTurnNumberSync(int TurnNumber)
    {
        CurrentTurnNumber = TurnNumber;
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) //스트림 송수신 (데이터 동기화 콜백함수)
    { }
    
}
