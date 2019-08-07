using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSystem : Photon.MonoBehaviour
{
    public bool MovementGoingOn;
    public static Vector3[] BoardLocationForUpDown = new Vector3[28];
    public static Vector3[] BoardLocation = new Vector3[28];
    public GameObject[] BoardObject = new GameObject[28];
    public static int MyCurrentBoard; //자신의 현재 보드 넘버
    public static int OtherPlayerCurrentBoard; //상대 플레이어의 보드 넘버
    public static int MyCurrentLineNumber; //1 ~ 4라인  스타트 지점은 0라인
    public int MyCurrentBoardColor; //1 ~ 7색깔
    public int BoardToGo;
    public int DestinationBoardNumber;
    public GameObject Player1ToGoArrow;
    public GameObject Player1ToGoArrowSon;
    public GameObject Player2ToGoArrow;
    public GameObject Player2ToGoArrowSon;
    public GameObject Me;
    public bool FoundOtherPlayer;
    private PhotonView pv;
    public int WhoAmI;
    public Vector3 HospitalBoard;

    void Start()
    {
        FoundOtherPlayer = false;
        pv = this.GetComponent<PhotonView>();
        MovementGoingOn = false;
        BoardToGo = 0;
        MyCurrentLineNumber = 1;
        MyCurrentBoardColor = 0;
        OtherPlayerCurrentBoard = 0;
        Player1ToGoArrowSon.SetActive(false);
        Player2ToGoArrowSon.SetActive(false);
        HospitalBoard = GameObject.Find("Hospital Board").transform.position;
        if (PhotonNetwork.isMasterClient) //플레이어1이 더 보드 안쪽 테두리를 돎
        {
            for (int i = 0; i < 28; i++) //초기화
            {
                BoardObject[i] = GameObject.Find("B (" + i.ToString() + ")");
                BoardLocationForUpDown[i] = BoardObject[i].transform.position;
                BoardLocation[i] = GameObject.Find("Player1Board (" + i.ToString() + ")").transform.position;
            }
        }
        else //플레이어2가 보드 바깥 테두리를 돎
        {
            for (int i = 0; i < 28; i++) //초기화
            {
                BoardObject[i] = GameObject.Find("B (" + i.ToString() + ")");
                BoardLocationForUpDown[i] = BoardObject[i].transform.position;
                BoardLocation[i] = GameObject.Find("Player2Board (" + i.ToString() + ")").transform.position;
            }
        }
        MyCurrentBoard = 0;
        if (PhotonNetwork.isMasterClient)
        {
            Me = GameObject.Find("Player1");
            WhoAmI = 1;
        }

        else
        {
            Me = GameObject.Find("Player2");
            WhoAmI = 2;
        }
    }

    void Update()
    {
        if ((TurnSystem.TurnProcess == 2) && (MovementGoingOn == false)) //룰렛 돌린 이후
        {
            MovementGoingOn = true;
            WhichBoardToGo(MyCurrentBoardColor, MyCurrentLineNumber, RouletteManager.RouletteColor);        
        }
    }

    public void WhichBoardToGo(int CurrentBoardColor, int CurrentLineNumber, int RouletteColor)
    {
        if (RouletteColor > CurrentBoardColor)//라인 기준 현재 자신 색깔보다 더 나중 순서의 색깔이 나왔다면 그냥 같은 라인의 그 색깔을 가면 됨
        {
            if ((CurrentLineNumber == 4) && (RouletteColor == 7)) //마지막 라인의 보라 경우 -> 0
            {   DestinationBoardNumber = 0;
                MyCurrentLineNumber = 0;
            }
            else
            {
                DestinationBoardNumber = MyCurrentBoard + (RouletteColor - CurrentBoardColor);
            }
                
        }
        else if (RouletteColor <= CurrentBoardColor)//라인 기준 현재 자신 색깔과 같거나 이전 순서의 색깔이 나왔다면 다음 라인으로 넘어가서 색깔 가야함
        {
            if( (CurrentLineNumber == 3) && (CurrentBoardColor == 7) && (RouletteColor == 7)) //3번째 찬스칸에서 스타트 지점으로 가야하는 경우
            {
                DestinationBoardNumber = 0;
                MyCurrentLineNumber = 0;
            }
            else if (CurrentLineNumber == 4) //지금 4라인인 경우 다시 1라인으로 감
            {
               DestinationBoardNumber = RouletteColor;
               MyCurrentLineNumber = 1;//변화 적용
            }
            else
            {
                DestinationBoardNumber = ((7 * CurrentLineNumber) + RouletteColor);
                MyCurrentLineNumber = CurrentLineNumber + 1;//변화 적용
            }
        }
        if(WhoAmI == 1)
            pv.RPC("DestinationArrowForOtherPlayer", PhotonTargets.All, 1, BoardLocation[DestinationBoardNumber], 1);
        else
            pv.RPC("DestinationArrowForOtherPlayer", PhotonTargets.All, 2, BoardLocation[DestinationBoardNumber], 1);
        StartCoroutine(MovementStart(MyCurrentBoard, DestinationBoardNumber));
    }

    public IEnumerator MovementStart(int CurrentBoard, int Destination)
    {
        int Step = CurrentBoard;
        while (Step != Destination)
        {
            float CurrentX = BoardLocation[Step].x;
            float CurrentZ = BoardLocation[Step].z;
            Step++;
            if (Step == 28) //0(출발지)을 지나가거나 도착하는 경우
                Step = 0; //보드 넘버 28 -> 0으로 다시 초기화  
            if(Step == Destination) // 도착하기 한 칸 전에 위치 알려주는 화살표 사라지기
                pv.RPC("DestinationArrowForOtherPlayer", PhotonTargets.All, WhoAmI, BoardLocation[DestinationBoardNumber], 2);
            float Xdif = (BoardLocation[Step].x - CurrentX) / 14;
            float Ydif = (0.7f - 0.11f) / 7;
            float Zdif = (BoardLocation[Step].z - CurrentZ) / 14;
            this.GetComponent<AudioSource>().Play();
            pv.RPC("JumpSoundForOtherPlayer", PhotonTargets.Others, null);
            for (int i = 0; i < 7; i++) //위로 점프
            {
                Me.transform.position = new Vector3(Me.transform.position.x + Xdif, Me.transform.position.y + Ydif, Me.transform.position.z + Zdif);
                yield return 0.000000001f;
            }
            Xdif = (BoardLocation[Step].x - Me.transform.position.x) / 7;
            Ydif = (0.11f - 0.7f) / 7;
            Zdif = (BoardLocation[Step].z - Me.transform.position.z) / 7;
            for (int i = 0; i < 7; i++) //다시 내려가기
            {
                Me.transform.position = new Vector3(Me.transform.position.x + Xdif, Me.transform.position.y + Ydif, Me.transform.position.z + Zdif);
                yield return 0.000000001f;
            }
            pv.RPC("BoardUpDownForOtherPlayer", PhotonTargets.Others, Step);
            for (int i = 0; i < 8; i++) //보드 내려가기
            {
                BoardObject[Step].transform.position = new Vector3(BoardLocationForUpDown[Step].x, BoardObject[Step].transform.position.y - 0.025f, BoardLocationForUpDown[Step].z);
                yield return 0.00000001f;
            }
            for (int i = 0; i < 8; i++) //보드 올라오기
            {
                BoardObject[Step].transform.position = new Vector3(BoardLocationForUpDown[Step].x, BoardObject[Step].transform.position.y + 0.025f, BoardLocationForUpDown[Step].z);
                yield return 0.00000001f;
            }
            if (Step == 0)
            {   //용돈 받기
                MyCurrentBoard = 0;
                BoardEffect.BonusGoingOn = true;
                var BE = GameObject.Find("BoardEffect").GetComponent<BoardEffect>();
                BE.CardChoice(10); //용돈의 경우 색깔 10
                while (BoardEffect.BonusGoingOn == true)
                {
                    yield return null;
                }
            }
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(1.0f);
        MyCurrentBoard = Step; //변화 적용
        MyCurrentBoardColor = RouletteManager.RouletteColor;//변화 적용
        pv.RPC("OtherPlayerBoardSync", PhotonTargets.Others, MyCurrentBoard);
        if (MyCurrentBoard == OtherPlayerCurrentBoard) //상대방 위치랑 같으면 가위바위보
        {
            if((GameObject.Find("Player1").transform.position != HospitalBoard) && (GameObject.Find("Player2").transform.position != HospitalBoard))
            {
                RockScissorsPaper.RSPGoingOn = true;
                var RSP = GameObject.Find("BoardEffect").GetComponent<RockScissorsPaper>();
                RSP.RockScissorsPaperStart();
                while (RockScissorsPaper.RSPGoingOn == true)
                {
                    yield return null;
                }
            }
        }
        TurnSystem.TurnProcess = 3;
        MovementGoingOn = false;
    }

    [PunRPC]
    public void BoardUpDownForOtherPlayer(int BoardNumber)
    {
        StartCoroutine(BoardUpDown(BoardNumber));
    }

    [PunRPC]
    public void JumpSoundForOtherPlayer()
    {
        this.GetComponent<AudioSource>().Play();
    }

    [PunRPC]
    public void OtherPlayerBoardSync(int OtherPlayerBoardNumber)
    {
        OtherPlayerCurrentBoard = OtherPlayerBoardNumber;
    }

    [PunRPC]
    public void DestinationArrowForOtherPlayer(int Who, Vector3 ArrowLocation, int ShowOrHide)
    {
        if(Who == 1) //플레이어 1이 가야할 곳 알려주는 화살표
        {
            if (ShowOrHide == 1) //생기기
            {
                Player1ToGoArrow.transform.position = ArrowLocation;
                Player1ToGoArrowSon.SetActive(true);
            }
            else //사라지기
            {
                Player1ToGoArrowSon.SetActive(false);
            }
        }
        else //플레이어 2가 가야할 곳 알려주는 화살표
        {
            if (ShowOrHide == 1)
            {
                Player2ToGoArrow.transform.position = ArrowLocation;
                Player2ToGoArrowSon.SetActive(true);
            }
            else
            {
                Player2ToGoArrowSon.SetActive(false);
            }
        }
    }

    IEnumerator BoardUpDown(int BoardNumber)
    {
        for (int i = 0; i < 10; i++) //보드 내려가기
        {
            BoardObject[BoardNumber].transform.position = new Vector3(BoardLocationForUpDown[BoardNumber].x, BoardObject[BoardNumber].transform.position.y - 0.025f, BoardLocationForUpDown[BoardNumber].z);
            yield return 0.00000001f;
        }
        for (int i = 0; i < 10; i++) //보드 올라오기
        {
            BoardObject[BoardNumber].transform.position = new Vector3(BoardLocationForUpDown[BoardNumber].x, BoardObject[BoardNumber].transform.position.y + 0.025f, BoardLocationForUpDown[BoardNumber].z);
            yield return 0.00000001f;
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) //스트림 송수신 (데이터 동기화 콜백함수)
    { }
}
