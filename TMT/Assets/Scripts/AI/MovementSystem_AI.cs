using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSystem_AI : MonoBehaviour
{
    public bool MovementGoingOn;
    public static Vector3[] BoardLocationForUpDown = new Vector3[28];
    public static Vector3[] Player1BoardLocation = new Vector3[28];
    public static Vector3[] Player2BoardLocation = new Vector3[28];
    public GameObject[] BoardObject = new GameObject[28];
    public static int Player1CurrentBoard; //자신의 현재 보드 넘버
    public static int Player2CurrentBoard; //상대 플레이어의 보드 넘버
    public static int Player1CurrentLineNumber; //1 ~ 4라인  스타트 지점은 0라인
    public static int Player2CurrentLineNumber; //1 ~ 4라인  스타트 지점은 0라인
    public int Player1CurrentBoardColor; //1 ~ 7색깔
    public int Player2CurrentBoardColor; //1 ~ 7색깔
    public int BoardToGo;
    public int DestinationBoardNumber;
    public GameObject Player1ToGoArrow;
    public GameObject Player1ToGoArrowSon;
    public GameObject Player2ToGoArrow;
    public GameObject Player2ToGoArrowSon;
    public GameObject Player1;
    public GameObject Player2;


    void Start()
    {
        MovementGoingOn = false;
        BoardToGo = 0;
        Player1CurrentBoard = 0;
        Player2CurrentBoard = 0;
        Player1CurrentLineNumber = 1;
        Player2CurrentLineNumber = 1;
        Player1CurrentBoardColor = 0;
        Player2CurrentBoardColor = 0;
        Player1ToGoArrowSon.SetActive(false);
        Player2ToGoArrowSon.SetActive(false);
        Player1 = GameObject.Find("Player1");
        Player2 = GameObject.Find("Player2");

        for (int i = 0; i < 28; i++) //초기화
        {
            BoardObject[i] = GameObject.Find("B (" + i.ToString() + ")");
            BoardLocationForUpDown[i] = BoardObject[i].transform.position;
            Player1BoardLocation[i] = GameObject.Find("Player1Board (" + i.ToString() + ")").transform.position;
            Player2BoardLocation[i] = GameObject.Find("Player2Board (" + i.ToString() + ")").transform.position;
        }

    }

    void Update()
    {
        if ((TurnSystem_AI.TurnProcess == 2) && (MovementGoingOn == false)) //룰렛 돌린 이후
        {
            MovementGoingOn = true;
            if ((TurnSystem_AI.CurrentTurnNumber == TurnSystem_AI.Player1TurnNumber)) //사용자(플레이어1)
                WhichBoardToGo(Player1CurrentBoardColor, Player1CurrentLineNumber, RouletteManager_AI.RouletteColor, 1);
            else //AI(플레이어2)
                WhichBoardToGo(Player2CurrentBoardColor, Player2CurrentLineNumber, RouletteManager_AI.RouletteColor, 2);
        }
    }

    public void WhichBoardToGo(int CurrentBoardColor, int CurrentLineNumber, int RouletteColor, int who)
    {
        if (RouletteColor > CurrentBoardColor)//라인 기준 현재 자신 색깔보다 더 나중 순서의 색깔이 나왔다면 그냥 같은 라인의 그 색깔을 가면 됨
        {
            if ((CurrentLineNumber == 4) && (RouletteColor == 7)) //마지막 라인의 보라 경우 -> 0
            {
                DestinationBoardNumber = 0;
                if (who == 1)//사용자(플레이어1)
                    Player1CurrentLineNumber = 0;
                else//AI(플레이어2)
                    Player2CurrentLineNumber = 0;
            }
            else
            {
                if(who == 1)//사용자(플레이어1)
                    DestinationBoardNumber = Player1CurrentBoard + (RouletteColor - CurrentBoardColor);
                else//AI(플레이어2)
                    DestinationBoardNumber = Player2CurrentBoard + (RouletteColor - CurrentBoardColor);
            }

        }
        else if (RouletteColor <= CurrentBoardColor)//라인 기준 현재 자신 색깔과 같거나 이전 순서의 색깔이 나왔다면 다음 라인으로 넘어가서 색깔 가야함
        {
            if ((CurrentLineNumber == 3) && (CurrentBoardColor == 7) && (RouletteColor == 7)) //3번째 찬스칸에서 스타트 지점으로 가야하는 경우
            {
                DestinationBoardNumber = 0;
                if (who == 1)//사용자(플레이어1)
                    Player1CurrentLineNumber = 0;
                else//AI(플레이어2)
                    Player2CurrentLineNumber = 0;
            }
            else if (CurrentLineNumber == 4) //지금 4라인인 경우 다시 1라인으로 감
            {
                DestinationBoardNumber = RouletteColor;
                if (who == 1)//사용자(플레이어1)
                    Player1CurrentLineNumber = 1;//변화 적용
                else //AI(플레이어2)
                    Player2CurrentLineNumber = 1;
            }
            else
            {
                DestinationBoardNumber = ((7 * CurrentLineNumber) + RouletteColor);
                if(who == 1)
                    Player1CurrentLineNumber = CurrentLineNumber + 1;//변화 적용
                else
                    Player2CurrentLineNumber = CurrentLineNumber + 1;//변화 적용
            }
        }
        if (who == 1)
        {
            DestinationArrow(1, Player1BoardLocation[DestinationBoardNumber], 1);
            StartCoroutine(MovementStart(Player1CurrentBoard, DestinationBoardNumber, who));
        }
        else
        {
            DestinationArrow(2, Player2BoardLocation[DestinationBoardNumber], 1);
            StartCoroutine(MovementStart(Player2CurrentBoard, DestinationBoardNumber, who));
        }
    }

    public void DestinationArrow(int Who, Vector3 ArrowLocation, int ShowOrHide)
    {
        if (Who == 1) //플레이어 1이 가야할 곳 알려주는 화살표
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

    public IEnumerator MovementStart(int CurrentBoard, int Destination, int who)
    {
        float CurrentX, CurrentZ, Xdif, Ydif, Zdif;
        int Step = CurrentBoard;
        while (Step != Destination)
        {
            if(who == 1)
            {
                CurrentX = Player1BoardLocation[Step].x;
                CurrentZ = Player1BoardLocation[Step].z;
            }
            else
            {
                CurrentX = Player2BoardLocation[Step].x;
                CurrentZ = Player2BoardLocation[Step].z;
            }
            
            Step++;
            if (Step == 28) //0(출발지)을 지나가거나 도착하는 경우
                Step = 0; //보드 넘버 28 -> 0으로 다시 초기화  

            if (Step == Destination) // 도착하기 한 칸 전에 위치 알려주는 화살표 사라지기
            {
                if(who == 1)
                    DestinationArrow(1, Player1BoardLocation[DestinationBoardNumber], 2);
                else
                    DestinationArrow(2, Player2BoardLocation[DestinationBoardNumber], 2);
            }

            if(who == 1)
            {
                Xdif = (Player1BoardLocation[Step].x - CurrentX) / 12;
                Ydif = (0.7f - 0.11f) / 6;
                Zdif = (Player1BoardLocation[Step].z - CurrentZ) / 12;

                this.GetComponent<AudioSource>().Play(); //점프 소리

                for (int i = 0; i < 6; i++) //위로 점프
                {
                    Player1.transform.position = new Vector3(Player1.transform.position.x + Xdif, Player1.transform.position.y + Ydif, Player1.transform.position.z + Zdif);
                    yield return 0.000000001f;
                }
                Xdif = (Player1BoardLocation[Step].x - Player1.transform.position.x) / 6;
                Ydif = (0.11f - 0.7f) / 6;
                Zdif = (Player1BoardLocation[Step].z - Player1.transform.position.z) / 6;
                for (int i = 0; i < 6; i++) //다시 내려가기
                {
                    Player1.transform.position = new Vector3(Player1.transform.position.x + Xdif, Player1.transform.position.y + Ydif, Player1.transform.position.z + Zdif);
                    yield return 0.000000001f;
                }
                for (int i = 0; i < 7; i++) //보드 내려가기
                {
                    BoardObject[Step].transform.position = new Vector3(BoardLocationForUpDown[Step].x, BoardObject[Step].transform.position.y - 0.025f, BoardLocationForUpDown[Step].z);
                    yield return 0.00000001f;
                }
                for (int i = 0; i < 7; i++) //보드 올라오기
                {
                    BoardObject[Step].transform.position = new Vector3(BoardLocationForUpDown[Step].x, BoardObject[Step].transform.position.y + 0.025f, BoardLocationForUpDown[Step].z);
                    yield return 0.00000001f;
                }
                if (Step == 0)
                {   //용돈 받기
                    Player1CurrentBoard = 0;
                    BoardEffect_AI.BonusGoingOn = true;
                    var BE = GameObject.Find("BoardEffect").GetComponent<BoardEffect_AI>();
                    BE.CardChoice(10); //용돈의 경우 색깔 10
                    while (BoardEffect_AI.BonusGoingOn == true)
                    {
                        yield return null;
                    }
                }
                
            }
            else
            {
                Xdif = (Player2BoardLocation[Step].x - CurrentX) / 12;
                Ydif = (0.7f - 0.11f) / 6;
                Zdif = (Player2BoardLocation[Step].z - CurrentZ) / 12;

                this.GetComponent<AudioSource>().Play(); //점프 소리

                for (int i = 0; i < 6; i++) //위로 점프
                {
                    Player2.transform.position = new Vector3(Player2.transform.position.x + Xdif, Player2.transform.position.y + Ydif, Player2.transform.position.z + Zdif);
                    yield return 0.000000001f;
                }
                Xdif = (Player2BoardLocation[Step].x - Player2.transform.position.x) / 6;
                Ydif = (0.11f - 0.7f) / 6;
                Zdif = (Player2BoardLocation[Step].z - Player2.transform.position.z) / 6;
                for (int i = 0; i < 6; i++) //다시 내려가기
                {
                    Player2.transform.position = new Vector3(Player2.transform.position.x + Xdif, Player2.transform.position.y + Ydif, Player2.transform.position.z + Zdif);
                    yield return 0.000000001f;
                }
                for (int i = 0; i < 7; i++) //보드 내려가기
                {
                    BoardObject[Step].transform.position = new Vector3(BoardLocationForUpDown[Step].x, BoardObject[Step].transform.position.y - 0.025f, BoardLocationForUpDown[Step].z);
                    yield return 0.00000001f;
                }
                for (int i = 0; i < 7; i++) //보드 올라오기
                {
                    BoardObject[Step].transform.position = new Vector3(BoardLocationForUpDown[Step].x, BoardObject[Step].transform.position.y + 0.025f, BoardLocationForUpDown[Step].z);
                    yield return 0.00000001f;
                }
                if (Step == 0)
                {   //용돈 받기
                    Player2CurrentBoard = 0;
                    BoardEffect_AI.BonusGoingOn = true;
                    var BE = GameObject.Find("BoardEffect").GetComponent<BoardEffect_AI>();
                    BE.CardChoice(10); //용돈의 경우 색깔 10
                    while (BoardEffect_AI.BonusGoingOn == true)
                    {
                        yield return null;
                    }
                }
            }

            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(1.0f);

        if(who == 1)
        {
            Player1CurrentBoard = Step; //변화 적용
            Player1CurrentBoardColor = RouletteManager_AI.RouletteColor;//변화 적용
        }
        else
        {
            Player2CurrentBoard = Step; //변화 적용
            Player2CurrentBoardColor = RouletteManager_AI.RouletteColor;//변화 적용
        }
           
        if (Player1CurrentBoard == Player2CurrentBoard) //상대방 위치랑 같으면 가위바위보
        {
            if(!HospitalSystem_AI.Player1InTheHospital && !HospitalSystem_AI.Player2InTheHospital)
            {
                RockScissorsPaper_AI.RSPGoingOn = true;
                var RSP = GameObject.Find("BoardEffect").GetComponent<RockScissorsPaper_AI>();
                RSP.RockScissorsPaperStart();
                while (RockScissorsPaper_AI.RSPGoingOn == true)
                {
                    yield return null;
                }
            }
        }

        TurnSystem_AI.TurnProcess = 3;
        MovementGoingOn = false;
    }
}
