using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardEffect : MonoBehaviour
{
    public static bool EffectApplyGoingOn;
    public AudioClip CardPopUpSound;
    public AudioClip ParentBonusSound;
    public AudioClip StudySound;
    public AudioClip BadChanceCardSound;
    public AudioClip GoodChcanceCardSound;
    
    public int[,] RedEffectArray = new int[7, 2]; //(식사) 0돈, 1스트레스
    public int[,] RedEffect2Array = new int[7, 2]; //(간식) 0돈, 1스트레스

    public int[,] OrangeEffectArray = new int[7, 2]; //(문화생활) 0돈, 1스트레스
    public int[,] OrangeEffect2Array = new int[7, 2]; //(여행) 0돈, 1스트레스

    public int[,] YellowEffectArray = new int[7, 2]; //(사회생활) 0돈, 1스트레스

    public int[,] GreenEffectArray = new int[7, 2]; //(백화점) 0돈, 1스트레스
    public int[,] GreenEffect2Array = new int[7, 2]; //(마트) 0돈, 1스트레스

    public int[,] BlueEffectArray = new int[10, 2]; //(아르바이트) 0돈, 1스트레스

    public int[] PinkEffectArray = new int[4]; //강의, 공부, 과제, 시험

    public int[,] PurpleEffectArray = new int[21, 2]; //(찬스) 0돈, 1스트레스


    public Sprite[] RedCardImages = new Sprite[7]; //식사
    public Sprite[] RedCard2Images = new Sprite[7]; //간식

    public Sprite[] OrangeCardImages = new Sprite[7]; //문화생활
    public Sprite[] OrangeCard2Images = new Sprite[7]; //여행

    public Sprite[] YellowCardImages = new Sprite[7]; //사회생활

    public Sprite[] GreenCardImages = new Sprite[7]; //백화점
    public Sprite[] GreenCard2Images = new Sprite[7]; //마트

    public Sprite[] BlueCardImages = new Sprite[10]; //아르바이트

    public Sprite[] PinkCardImages = new Sprite[4]; //상금

    public Sprite[] PurpleCardImages = new Sprite[21]; //찬스

    public Sprite BonusCardImage; //용돈(스타트 지점 도착할 때)

    public Sprite[] BoardTitleImages = new Sprite[8]; //0제외 1 ~ 8까지 빨강~보라

    public GameObject SelectionMent;
    public Text SelectionMentText;
    public GameObject CardChoice1;
    public GameObject CardChoice2;
    public GameObject CardRandomChoice;
    public GameObject BoardTitleObject;
    public Button Choice1OkButton;
    public Button Choice2OkButton;
    public Button RandomChoiceOkButton;
    public Image Choice1CardImage;
    public Image Choice2CardImage;
    public Image RandomCardImage;
    public Image BoardTitleImage;
    public GameObject Selection1BlueSquare;
    public GameObject Selection2BlueSquare;

    public GameObject CardChoiceTimer;
    public Text CardChoiceTimerText;
    public Text BoardTitleText;

    public static bool BonusGoingOn = false;

    public static int Choice1, Choice2, RandomChoice;
    public int WhoAmI;
    private PhotonView pv;

    void Start()
    {
        pv = GetComponent<PhotonView>();
        EffectApplyGoingOn = false;
        CardChoiceTimer.SetActive(false);
        CardChoice1.SetActive(false);
        CardChoice2.SetActive(false);
        BoardTitleObject.SetActive(false);
        CardRandomChoice.SetActive(false);
        SelectionMent.SetActive(false);
        Selection1BlueSquare.SetActive(false);
        Selection2BlueSquare.SetActive(false);

        if (PhotonNetwork.isMasterClient)
            WhoAmI = 1;
        else
            WhoAmI = 2;

        RedEffectArray[0, 0] = -15000; //초밥(일식)
        RedEffectArray[0, 1] = 45;
        RedEffectArray[1, 0] = -20000; //야식배달
        RedEffectArray[1, 1] = 50;
        RedEffectArray[2, 0] = -10000; //학교 앞에서 점심 먹기
        RedEffectArray[2, 1] = 40;
        RedEffectArray[3, 0] = -18000; //파스타   여기까지 그룹 1
        RedEffectArray[3, 1] = 45;
        RedEffectArray[4, 0] = -60000; //스테이크
        RedEffectArray[4, 1] = 130;
        RedEffectArray[5, 0] = -40000; //소고기
        RedEffectArray[5, 1] = 85;
        RedEffectArray[6, 0] = -35000; //한식 정식
        RedEffectArray[6, 1] = 80;

        RedEffect2Array[0, 0] = -10000; //분식
        RedEffect2Array[0, 1] = 80;
        RedEffect2Array[1, 0] = -20000; //케이크
        RedEffect2Array[1, 1] = 95;
        RedEffect2Array[2, 0] = -14000; //아이스크림
        RedEffect2Array[2, 1] = 85;
        RedEffect2Array[3, 0] = -15000; //티타임    여기까지 그룹 1
        RedEffect2Array[3, 1] = 85;
        RedEffect2Array[4, 0] = -5000; //편의점 간식
        RedEffect2Array[4, 1] = 30;
        RedEffect2Array[5, 0] = -8000; //과일
        RedEffect2Array[5, 1] = 45;
        RedEffect2Array[6, 0] = -6000; //마카롱
        RedEffect2Array[6, 1] = 40;

        OrangeEffectArray[0, 0] = -15000; //영화보기
        OrangeEffectArray[0, 1] = 50;
        OrangeEffectArray[1, 0] = -20000; //전시회
        OrangeEffectArray[1, 1] = 55;
        OrangeEffectArray[2, 0] = -25000; //스포츠 경기 관람
        OrangeEffectArray[2, 1] = 65;
        OrangeEffectArray[3, 0] = -30000; //좋아하는 악기 레슨  여기까지 그룹 1
        OrangeEffectArray[3, 1] = 70;
        OrangeEffectArray[4, 0] = -50000; //놀이공원  
        OrangeEffectArray[4, 1] = 110;
        OrangeEffectArray[5, 0] = -110000; //콘서트
        OrangeEffectArray[5, 1] = 200;
        OrangeEffectArray[6, 0] = -150000; //뮤지컬
        OrangeEffectArray[6, 1] = 240;

        OrangeEffect2Array[0, 0] = -50000; //서울 투어
        OrangeEffect2Array[0, 1] = 60;
        OrangeEffect2Array[1, 0] = -200000; //제주도
        OrangeEffect2Array[1, 1] = 140;
        OrangeEffect2Array[2, 0] = -150000; //부산 바다
        OrangeEffect2Array[2, 1] = 90;
        OrangeEffect2Array[3, 0] = -250000; //홍콩   여기까지 그룹 1
        OrangeEffect2Array[3, 1] = 180;
        OrangeEffect2Array[4, 0] = -700000; //파리
        OrangeEffect2Array[4, 1] = 450;
        OrangeEffect2Array[5, 0] = -900000; //미국
        OrangeEffect2Array[5, 1] = 500;
        OrangeEffect2Array[6, 0] = -500000; //푸켓
        OrangeEffect2Array[6, 1] = 400;

        YellowEffectArray[0, 0] = -20000; //동아리 전체 회식
        YellowEffectArray[0, 1] = 45;
        YellowEffectArray[1, 0] = -10000; //친구랑 카페에서 수다 떨기
        YellowEffectArray[1, 1] = 50;
        YellowEffectArray[2, 0] = -25000; //집들이 선물
        YellowEffectArray[2, 1] = 50;
        YellowEffectArray[3, 0] = -30000; //졸업식 꽃다발   여기까지 그룹 1
        YellowEffectArray[3, 1] = 55;
        YellowEffectArray[4, 0] = -150000; //기부하기
        YellowEffectArray[4, 1] = 150;
        YellowEffectArray[5, 0] = -100000; //결혼식 축의금
        YellowEffectArray[5, 1] = 160;
        YellowEffectArray[6, 0] = -60000; //친구 생일선물
        YellowEffectArray[6, 1] = 100;

        GreenEffectArray[0, 0] = -80000; //신발사기
        GreenEffectArray[0, 1] = 110;
        GreenEffectArray[1, 0] = -45000; //피규어 구매하기
        GreenEffectArray[1, 1] = 60;
        GreenEffectArray[2, 0] = -70000; //새 옷 장만
        GreenEffectArray[2, 1] = 100;
        GreenEffectArray[3, 0] = -30000; //모자 사기    여기까지 그룹 1
        GreenEffectArray[3, 1] = 55;
        GreenEffectArray[4, 0] = -250000; //신상 게임기 구매
        GreenEffectArray[4, 1] = 230;
        GreenEffectArray[5, 0] = -180000; //가방 사기
        GreenEffectArray[5, 1] = 180;
        GreenEffectArray[6, 0] = -300000; //가전제품
        GreenEffectArray[6, 1] = 260;

        GreenEffect2Array[0, 0] = -6000; //주방용품
        GreenEffect2Array[0, 1] = 60;
        GreenEffect2Array[1, 0] = -9000; //곡물
        GreenEffect2Array[1, 1] = 65;
        GreenEffect2Array[2, 0] = -4000; //반찬
        GreenEffect2Array[2, 1] = 50;
        GreenEffect2Array[3, 0] = -5000; //음료    여기까지 그룹 1
        GreenEffect2Array[3, 1] = 55;
        GreenEffect2Array[4, 0] = -15000; //위생용품
        GreenEffect2Array[4, 1] = 90;
        GreenEffect2Array[5, 0] = -14000; //욕실용품
        GreenEffect2Array[5, 1] = 90;
        GreenEffect2Array[6, 0] = -20000; //사무용품
        GreenEffect2Array[6, 1] = 130;

        BlueEffectArray[0, 0] = 40000; //카페 알바
        BlueEffectArray[0, 1] = -160;
        BlueEffectArray[1, 0] = 45000; //빵집 알바
        BlueEffectArray[1, 1] = -160;
        BlueEffectArray[2, 0] = 60000; //영화관 알바
        BlueEffectArray[2, 1] = -210;
        BlueEffectArray[3, 0] = 60000; //주차요원 알바
        BlueEffectArray[3, 1] = -210;
        BlueEffectArray[4, 0] = 45000; //편의점 알바    여기까지 그룹 1
        BlueEffectArray[4, 1] = -160;
        BlueEffectArray[5, 0] = 100000; //뷔페 서빙 알바     
        BlueEffectArray[5, 1] = -260;
        BlueEffectArray[6, 0] = 120000; //포장 알바
        BlueEffectArray[6, 1] = -280;
        BlueEffectArray[7, 0] = 80000; //전단지 알바
        BlueEffectArray[7, 1] = -240;
        BlueEffectArray[8, 0] = 85000; //번역 알바
        BlueEffectArray[8, 1] = -240;
        BlueEffectArray[9, 0] = 150000; //공사장 알바
        BlueEffectArray[9, 1] = -300;

        PinkEffectArray[0] = -100;
        PinkEffectArray[1] = -100;
        PinkEffectArray[2] = -200;
        PinkEffectArray[3] = -200;

        //-----------------------------찬스-----------------------------------
        PurpleEffectArray[0, 0] = -100000; //노트북 고장
        PurpleEffectArray[0, 1] = -80;
        PurpleEffectArray[1, 0] = -150000; //핸드폰 고장
        PurpleEffectArray[1, 1] = -110;
        PurpleEffectArray[2, 0] = -35000; //지갑 잃어버림
        PurpleEffectArray[2, 1] = -50;
        PurpleEffectArray[3, 0] = 10000; //돈 줍기
        PurpleEffectArray[3, 1] = 70;
        PurpleEffectArray[4, 0] = 30000; //비상금 발견
        PurpleEffectArray[4, 1] = 130;
        PurpleEffectArray[5, 0] = 70000; //내 생일
        PurpleEffectArray[5, 1] = 200;
        PurpleEffectArray[6, 0] = -100000; //미용실 기분전환
        PurpleEffectArray[6, 1] = 100;
        PurpleEffectArray[7, 0] = 40000; //알바 대타
        PurpleEffectArray[7, 1] = -60;
        PurpleEffectArray[8, 0] = -180000; //이어폰 잃어버림
        PurpleEffectArray[8, 1] = -130;
        PurpleEffectArray[9, 0] = -20000; //게임 현질
        PurpleEffectArray[9, 1] = 100;
        PurpleEffectArray[10, 0] = -20000; //소개팅
        PurpleEffectArray[10, 1] = -50;
        PurpleEffectArray[11, 0] = -15000; //증명사진 찍기
        PurpleEffectArray[11, 1] = -10;
        PurpleEffectArray[12, 0] = -15000; //세탁소 비용
        PurpleEffectArray[12, 1] = -20;
        PurpleEffectArray[13, 0] = -35000; //전공책 사기
        PurpleEffectArray[13, 1] = -45;
        PurpleEffectArray[14, 0] = -20000; //지각 택시비
        PurpleEffectArray[14, 1] = -30;
        PurpleEffectArray[15, 0] = 300000; //성적 장학금
        PurpleEffectArray[15, 1] = -200;
        PurpleEffectArray[16, 0] = 200000; //프로젝트 공모전 우승
        PurpleEffectArray[16, 1] = -150;
        PurpleEffectArray[17, 0] = 50000; //로또 당첨
        PurpleEffectArray[17, 1] = 30;
        PurpleEffectArray[18, 0] = 100000; //경품 응모 당첨
        PurpleEffectArray[18, 1] = 55;
        PurpleEffectArray[19, 0] = 1000000; //논문 대회 우승
        PurpleEffectArray[19, 1] = -200;
        PurpleEffectArray[20, 0] = 70000; //친척 용돈
        PurpleEffectArray[20, 1] = 100;
    }

    void Update()
    {
        if ((TurnSystem.TurnProcess == 3) && (EffectApplyGoingOn == false))
        {
            EffectApplyGoingOn = true;
            if (MovementSystem.MyCurrentBoard == 0) //스타트 칸은 보드 효과 없음
            {
                EffectApplyGoingOn = false;
                TurnSystem.TurnProcess = 4;
            }
            else
            {
                CardChoice(RouletteManager.RouletteColor);
            }
        }
    }

    public void CardChoice(int Color)
    {
        if (Color <= 4) //빨강(식비,간식) 주황(문화생활,여행) 노랑(사회생활) 초록(백화점,마트)
        {
            Choice1 = Random.Range(0, 4);
            Choice2 = Random.Range(4, 7);
            pv.RPC("CardChoicePopUp", PhotonTargets.All, Color, MovementSystem.MyCurrentLineNumber, Choice1, Choice2, WhoAmI);
        }
        else if (Color == 5) //파랑(아르바이트)
        {
            Choice1 = Random.Range(0, 5);
            Choice2 = Random.Range(5, 10);
            pv.RPC("CardChoicePopUp", PhotonTargets.All, Color, MovementSystem.MyCurrentLineNumber, Choice1, Choice2, WhoAmI);
        }
        else if (Color == 6) //분홍(상금, 용돈)
        {
            RandomChoice = MovementSystem.MyCurrentBoard;
            if (RandomChoice == 6)
            {
                RandomChoice = 0;
                pv.RPC("RandomChoicePopUp", PhotonTargets.All, Color, 1, 0, WhoAmI);
            }
            else if (RandomChoice == 13)
            {
                RandomChoice = 1;
                pv.RPC("RandomChoicePopUp", PhotonTargets.All, Color, 2, 1, WhoAmI);
            }  
            else if (RandomChoice == 20)
            {
                RandomChoice = 2;
                pv.RPC("RandomChoicePopUp", PhotonTargets.All, Color, 3, 2, WhoAmI);
            }
            else if (RandomChoice == 27)
            {
                RandomChoice = 3;
                pv.RPC("RandomChoicePopUp", PhotonTargets.All, Color, 4, 3, WhoAmI);
            }    
        }
        else if (Color == 7) //보라(찬스)
        {
            RandomChoice = Random.Range(0, 21);
            pv.RPC("RandomChoicePopUp", PhotonTargets.All, Color, MovementSystem.MyCurrentLineNumber, RandomChoice, WhoAmI);
        }
        else if (Color == 10) //스타트 지점 돌 때 용돈
        {
            RandomChoice = 0;
            pv.RPC("RandomChoicePopUp", PhotonTargets.All, Color, MovementSystem.MyCurrentLineNumber, RandomChoice, WhoAmI);
        }
    }

    [PunRPC]
    public void RandomChoicePopUp(int Color, int LineNumber, int RandomChoice, int Who)
    {
        BoardTitleObject.SetActive(true);
        if (Color == 6) //분홍
        {
            this.GetComponent<AudioSource>().clip = StudySound;
            this.GetComponent<AudioSource>().Play();
            if (LineNumber == 1)
                BoardTitleText.text = "강 의";
            else if (LineNumber == 2)
                BoardTitleText.text = "공 부";
            else if (LineNumber == 3)
                BoardTitleText.text = "과 제";
            else
                BoardTitleText.text = "시 험";
            BoardTitleImage.GetComponent<Image>().sprite = BoardTitleImages[Color];
            RandomCardImage.GetComponent<Image>().sprite = PinkCardImages[RandomChoice];
        }
        else if (Color == 7) //보라(찬스)
        {
            if ((RandomChoice == 3) || (RandomChoice == 4) || (RandomChoice == 5) || (RandomChoice == 17) || (RandomChoice == 18) || (RandomChoice == 20))
                this.GetComponent<AudioSource>().clip = GoodChcanceCardSound;
            else
                this.GetComponent<AudioSource>().clip = BadChanceCardSound;

            this.GetComponent<AudioSource>().Play();
            BoardTitleText.text = "찬 스";
            BoardTitleImage.GetComponent<Image>().sprite = BoardTitleImages[Color];
            RandomCardImage.GetComponent<Image>().sprite = PurpleCardImages[RandomChoice];
        }
        else //용돈
        {
            this.GetComponent<AudioSource>().clip = ParentBonusSound;
            this.GetComponent<AudioSource>().Play();
            BoardTitleText.text = "용 돈";
            BoardTitleImage.GetComponent<Image>().sprite = BoardTitleImages[7];
            RandomCardImage.GetComponent<Image>().sprite = BonusCardImage;
        }

        StartCoroutine(RandomCardPopUp());

        if (WhoAmI == Who) //호출한 사람이라면
            RandomChoiceOkButton.interactable = true;

        else //나머지 플레이어는 버튼 선택 비활성화
            RandomChoiceOkButton.interactable = false;

        if(Who == WhoAmI)
            StartCoroutine(RandomCardChoiceTimerStart(Who, RandomChoice));
    }


    [PunRPC]
    public void CardChoicePopUp(int Color, int LineNumber, int Choice1, int Choice2, int Who) //카드 두개 중에 선택할 수 있는 경우
    {
        BoardTitleObject.SetActive(true);
        BoardTitleImage.GetComponent<Image>().sprite = BoardTitleImages[Color];
        if (Color == 1) //빨강
        {
            if ((LineNumber % 2) != 0) //1,3라인
            {
                BoardTitleText.text = "식 사";
                Choice1CardImage.GetComponent<Image>().sprite = RedCardImages[Choice1];
                Choice2CardImage.GetComponent<Image>().sprite = RedCardImages[Choice2];
            }
            else
            {
                BoardTitleText.text = "간 식";
                Choice1CardImage.GetComponent<Image>().sprite = RedCard2Images[Choice1];
                Choice2CardImage.GetComponent<Image>().sprite = RedCard2Images[Choice2];
            }

        }
        else if (Color == 2) //주황
        {
            if ((LineNumber % 2) != 0)
            {
                BoardTitleText.text = "문화생활";
                Choice1CardImage.GetComponent<Image>().sprite = OrangeCardImages[Choice1];
                Choice2CardImage.GetComponent<Image>().sprite = OrangeCardImages[Choice2];
            }
            else
            {
                BoardTitleText.text = "여 행";
                Choice1CardImage.GetComponent<Image>().sprite = OrangeCard2Images[Choice1];
                Choice2CardImage.GetComponent<Image>().sprite = OrangeCard2Images[Choice2];
            }

        }
        else if (Color == 3) //노랑
        {
            BoardTitleText.text = "사회생활";
            Choice1CardImage.GetComponent<Image>().sprite = YellowCardImages[Choice1];
            Choice2CardImage.GetComponent<Image>().sprite = YellowCardImages[Choice2];
        }
        else if (Color == 4) //초록
        {
            if ((LineNumber % 2) != 0)
            {
                BoardTitleText.text = "백화점";
                Choice1CardImage.GetComponent<Image>().sprite = GreenCardImages[Choice1];
                Choice2CardImage.GetComponent<Image>().sprite = GreenCardImages[Choice2];
            }
            else
            {
                BoardTitleText.text = "마 트";
                Choice1CardImage.GetComponent<Image>().sprite = GreenCard2Images[Choice1];
                Choice2CardImage.GetComponent<Image>().sprite = GreenCard2Images[Choice2];
            }

        }
        else if (Color == 5) //파랑
        {
            BoardTitleText.text = "아르바이트";
            Choice1CardImage.GetComponent<Image>().sprite = BlueCardImages[Choice1];
            Choice2CardImage.GetComponent<Image>().sprite = BlueCardImages[Choice2];
        }

        StartCoroutine(CardPopUp());

        if (WhoAmI == Who) //호출한 사람이라면
        {
            Choice1OkButton.interactable = true;
            Choice2OkButton.interactable = true;
            SelectionMentText.text = "원하는 활동을 선택하세요";
        }
        else //나머지 플레이어는 버튼 선택 비활성화
        {
            Choice1OkButton.interactable = false;
            Choice2OkButton.interactable = false;
            SelectionMentText.text = "상대방이 선택을 하고있습니다";
        }
        SelectionMent.SetActive(true);
        if(Who == WhoAmI)
            StartCoroutine(CardChoiceTimerStart(Who, Choice1, Choice2));
    }

    IEnumerator CardPopUp() //선택 카드(2개) 점점 커지면서 나타나기
    {
        this.GetComponent<AudioSource>().clip = CardPopUpSound;
        this.GetComponent<AudioSource>().Play();
        if (TurnSystem.TurnProcess != 0) //지금 턴인사람
        {
            Choice1CardImage.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            Choice2CardImage.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            Choice1OkButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            Choice2OkButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        else //턴 아닌 사람은 투명하게
        {
            Choice1CardImage.GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f, 0.7f);
            Choice2CardImage.GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f, 0.7f);
            Choice1OkButton.GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f, 0.7f);
            Choice2OkButton.GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f, 0.7f);
        }
        CardChoice1.SetActive(true);
        CardChoice2.SetActive(true);
        for (float i = 0; i <= 1; i += 0.02f) //보드 카드 커지면서 나타남
        {
            CardChoice1.transform.localScale = new Vector2(i, i);
            CardChoice2.transform.localScale = new Vector2(i, i);
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator RandomCardPopUp() //랜덤 카드(1개) 점점 커지면서 나타나기
    {
        if (TurnSystem.TurnProcess != 0) //지금 턴인사람
        {
            RandomCardImage.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            RandomChoiceOkButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        else //턴 아닌 사람은 투명하게
        {
            RandomCardImage.GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f, 0.7f);
            RandomChoiceOkButton.GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f, 0.7f);
        }
        CardRandomChoice.SetActive(true);
        for (float i = 0; i <= 1; i += 0.02f) //보드 카드 커지면서 나타남
        {
            CardRandomChoice.transform.localScale = new Vector2(i, i);
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator CardChoiceTimerStart(int Who, int Choice1, int Choice2) //타이머 보여줌
    {
        CardChoiceTimer.SetActive(true);
        for (int i = 20; i >= 0; i--)
        {
            CardChoiceTimerText.text = i.ToString();
            yield return new WaitForSeconds(1.0f);
            if (!Choice1OkButton.IsInteractable() || !Choice2OkButton.IsInteractable())
                break;
        }
        if (Choice1OkButton.IsInteractable() || Choice2OkButton.IsInteractable()) //시간 지나도 안골랐다면
            pv.RPC("ChoiceResultShow", PhotonTargets.All, 1, WhoAmI, RouletteManager.RouletteColor, MovementSystem.MyCurrentLineNumber, Choice1, MovementSystem.MyCurrentBoard);
    }

    IEnumerator RandomCardChoiceTimerStart(int Who, int RandomChoice) //타이머 보여줌
    {
        CardChoiceTimer.SetActive(true);
        for (int i = 20; i >= 0; i--)
        {
            CardChoiceTimerText.text = i.ToString();
            yield return new WaitForSeconds(1.0f);
            if (!RandomChoiceOkButton.IsInteractable())
                break;
        }
        if (RandomChoiceOkButton.IsInteractable()) //시간 지나도 안골랐다면
            pv.RPC("RandomChoiceOk", PhotonTargets.All, WhoAmI, RouletteManager.RouletteColor, MovementSystem.MyCurrentLineNumber, RandomChoice, MovementSystem.MyCurrentBoard);

    }

    public void LeftOkButtonClick()
    {
        pv.RPC("ChoiceResultShow", PhotonTargets.All, 1, WhoAmI, RouletteManager.RouletteColor, MovementSystem.MyCurrentLineNumber, Choice1, MovementSystem.MyCurrentBoard);
    }

    public void RightOkButtonClick()
    {
        pv.RPC("ChoiceResultShow", PhotonTargets.All, 2, WhoAmI, RouletteManager.RouletteColor, MovementSystem.MyCurrentLineNumber, Choice2, MovementSystem.MyCurrentBoard);
    }

    public void RandomCardOkButtonClick()
    {
        pv.RPC("RandomChoiceOk", PhotonTargets.All, WhoAmI, RouletteManager.RouletteColor, MovementSystem.MyCurrentLineNumber, RandomChoice, MovementSystem.MyCurrentBoard);
    }



    [PunRPC]
    public void ChoiceResultShow(int LeftOrRight, int Who, int Color, int LineNumber, int Choice, int BoardLocation)
    {
        Choice1OkButton.interactable = false;
        Choice2OkButton.interactable = false;
        CardChoiceTimer.SetActive(false);
        if (TurnSystem.TurnProcess != 0) //지금 턴인사람
        {
            Selection1BlueSquare.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            Selection2BlueSquare.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        else //턴 아닌 사람은 투명하게
        {
            Selection1BlueSquare.GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f, 0.7f);
            Selection2BlueSquare.GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f, 0.7f);
        }
        if (LeftOrRight == 1)
            Selection1BlueSquare.SetActive(true);
        else
            Selection2BlueSquare.SetActive(true);

        var EA = GameObject.Find("BoardEffect").GetComponent<EffectApply>();
        EffectApply.MoneyEffectApplyingGoingOn = true;
        EffectApply.HealthEffectApplyingGoingOn = true;

        if (Color == 1) //빨강
        {
            if ((LineNumber % 2) != 0) //1,3라인
            {
                StartCoroutine(EA.MoneyChangeApply(Who, RedEffectArray[Choice, 0], BoardLocation));
                StartCoroutine(EA.HealthChangeApply(Who, RedEffectArray[Choice, 1]));
            }
            else //2,4라인
            {
                StartCoroutine(EA.MoneyChangeApply(Who, RedEffect2Array[Choice, 0], BoardLocation));
                StartCoroutine(EA.HealthChangeApply(Who, RedEffect2Array[Choice, 1]));
            }
        }
        else if (Color == 2) //주황
        {
            if ((LineNumber % 2) != 0) //1,3라인
            {
                StartCoroutine(EA.MoneyChangeApply(Who, OrangeEffectArray[Choice, 0], BoardLocation));
                StartCoroutine(EA.HealthChangeApply(Who, OrangeEffectArray[Choice, 1]));
            }
            else //2,4라인
            {
                StartCoroutine(EA.MoneyChangeApply(Who, OrangeEffect2Array[Choice, 0], BoardLocation));
                StartCoroutine(EA.HealthChangeApply(Who, OrangeEffect2Array[Choice, 1]));
            }
        }
        else if (Color == 3) //노랑
        {
            StartCoroutine(EA.MoneyChangeApply(Who, YellowEffectArray[Choice, 0], BoardLocation));
            StartCoroutine(EA.HealthChangeApply(Who, YellowEffectArray[Choice, 1]));
        }
        else if (Color == 4) //초록
        {
            if ((LineNumber % 2) != 0) //1,3라인
            {
                StartCoroutine(EA.MoneyChangeApply(Who, GreenEffectArray[Choice, 0], BoardLocation));
                StartCoroutine(EA.HealthChangeApply(Who, GreenEffectArray[Choice, 1]));
            }
            else //2,4라인
            {
                StartCoroutine(EA.MoneyChangeApply(Who, GreenEffect2Array[Choice, 0], BoardLocation));
                StartCoroutine(EA.HealthChangeApply(Who, GreenEffect2Array[Choice, 1]));
            }
        }
        else if (Color == 5) //파랑
        {
            StartCoroutine(EA.MoneyChangeApply(Who, BlueEffectArray[Choice, 0], BoardLocation));
            StartCoroutine(EA.HealthChangeApply(Who, BlueEffectArray[Choice, 1]));
        }
        StartCoroutine(WaitForChangeApply(Who));
    }

    [PunRPC]
    public void RandomChoiceOk(int Who, int Color, int LineNumber, int Choice, int BoardLocation)
    {
        RandomChoiceOkButton.interactable = false;
        CardChoiceTimer.SetActive(false);
        var EA = GameObject.Find("BoardEffect").GetComponent<EffectApply>();
        EffectApply.MoneyEffectApplyingGoingOn = true;
        EffectApply.HealthEffectApplyingGoingOn = true;

        if (Color == 6) //분홍
        {
            EffectApply.MoneyEffectApplyingGoingOn = false;
            StartCoroutine(EA.HealthChangeApply(Who, PinkEffectArray[Choice]));
        }
        else if (BoardLocation == 0) //용돈
        {
            EffectApply.HealthEffectApplyingGoingOn = false;
            StartCoroutine(EA.MoneyChangeApply(Who, 300000, BoardLocation));
        }
        else if (Color == 7) //보라
        {
            StartCoroutine(EA.MoneyChangeApply(Who, PurpleEffectArray[Choice, 0], BoardLocation));
            StartCoroutine(EA.HealthChangeApply(Who, PurpleEffectArray[Choice, 1]));
        }
        StartCoroutine(WaitForChangeApply(Who));
    }


    IEnumerator WaitForChangeApply(int Who)
    {
        yield return new WaitForSeconds(1.5f);
        SelectionMent.SetActive(false);
        CardChoice1.SetActive(false);
        CardChoice2.SetActive(false);
        CardRandomChoice.SetActive(false);
        Selection1BlueSquare.SetActive(false);
        Selection2BlueSquare.SetActive(false);
        BoardTitleObject.SetActive(false);

        while((EffectApply.MoneyEffectApplyingGoingOn == true) || (EffectApply.HealthEffectApplyingGoingOn == true))
        {
            yield return null;
        }
        if (WhoAmI == Who)
        {
            if(TurnSystem.TurnProcess == 2)//용돈의 경우
            {
                BonusGoingOn = false;
            }
            else if(TurnSystem.TurnProcess == 3)//일반 보드이펙트 경우
            {
                EffectApplyGoingOn = false;
                TurnSystem.TurnProcess = 4;
            }
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) //스트림 송수신 (데이터 동기화 콜백함수)
    { }
}
