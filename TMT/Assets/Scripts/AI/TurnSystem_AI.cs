using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystem_AI : MonoBehaviour
{
    public static int CurrentTurnNumber = 1; //1인 사람이 먼저 시작함
    public static int Player1TurnNumber; //Turn Choice 이후 정해짐(사용자 턴 넘버)
    public static int Player2TurnNumber; //Turn Choice 이후 정해짐(AI 턴 넘버)
    public static bool TurnGoingOn = false;
    public GameObject RoulettePanel;
    public static int TurnProcess; //1:턴 시작.룰렛판 생김, 2:룰렛 완료.움직임 시작, 3:움직임 완료, 보드 이펙트 적용 시작 4:피로도 체크하고 넘었으면 병원감 5:끝
    public GameObject Player1Cover;
    public GameObject Player2Cover;
    public GameObject Player1;
    public GameObject Player2;
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
        CurrentTurnNumber = 1; //플레이어1부터 시작
    }

    void Start()
    {
        StillHospitalNotice.SetActive(false);
        HospitalOutNotice.SetActive(false);
        TurnGoingOn = false;
        TurnProcess = 0;
        Player1 = GameObject.Find("Player1");
        Player2 = GameObject.Find("Player2");
    }

    void Update()
    {
        if (!TurnChoice_AI.TurnChoiceGoingOn) //(젤 처음)선 고르기가 끝났다면
        {
            if( (CurrentTurnNumber == Player1TurnNumber) && (TurnGoingOn == false) ) //사용자(Player1)의 턴 차례
            {
                TurnGoingOn = true;
                if(HospitalSystem_AI.Player1InTheHospital == true)
                {
                    StartCoroutine(HospitalNoticeOn(1));
                }
                else
                {
                    this.GetComponent<AudioSource>().clip = MyTurnSound;
                    this.GetComponent<AudioSource>().Play();
                    if (HospitalSystem_AI.Player1OkComeOut == true) //병원에 입원해있었던 경우 다시 제자리로 돌아가고 시작함
                    {
                        StartCoroutine(HospitalOutNoticeOn());
                        HospitalSystem_AI.Player1OkComeOut = false;
                        Player1.transform.position = MovementSystem_AI.Player1BoardLocation[MovementSystem_AI.Player1CurrentBoard];
                    }
                    TurnStart();
                }
            }
            else if(( CurrentTurnNumber == Player2TurnNumber) && (TurnGoingOn == false) ) //AI(Player2) 턴 차례
            {
                TurnGoingOn = true;
                if (HospitalSystem_AI.Player2InTheHospital == true)
                {
                    StartCoroutine(HospitalNoticeOn(2));
                }
                else
                {
                    if (HospitalSystem_AI.Player2OkComeOut == true) //병원에 입원해있었던 경우 다시 제자리로 돌아가고 시작함
                    {
                        StartCoroutine(HospitalOutNoticeOn());
                        HospitalSystem_AI.Player2OkComeOut = false;
                        Player2.transform.position = MovementSystem_AI.Player2BoardLocation[MovementSystem_AI.Player2CurrentBoard];
                    }
                    TurnStart();
                }
            }

            if(TurnProcess == 5)
            {
                if (CurrentTurnNumber == 1)
                    CurrentTurnNumber = 2;
                else
                    CurrentTurnNumber = 1;

                TurnProcess = 0;
                TurnGoingOn = false;
            }

            if (CurrentTurnNumber == Player1TurnNumber) //사용자(플레이어1) 턴일때
            {
                Player1Cover.SetActive(false);
                Player2Cover.SetActive(true);
                Player1.GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 1f, 1f);
                Player2.GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 1f, 0.5f);
            }
            else //AI(플레이어2) 턴일때
            {
                Player1Cover.SetActive(true);
                Player2Cover.SetActive(false);
                Player1.GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 1f, 0.5f);
                Player2.GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 1f, 1f);
            }
        }
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
        if (Who == 1) //사용자가 병원에 있는 경우
        {
            HospitalSystem_AI.Player1InTheHospital = false; //병원 입원으로 인해 한 턴 넘어감
            HospitalSystem_AI.Player1OkComeOut = true;
        }
        else //AI가 병원에 있는 경우
        {
            HospitalSystem_AI.Player2InTheHospital = false; //병원 입원으로 인해 한 턴 넘어감
            HospitalSystem_AI.Player2OkComeOut = true;
        }
        TurnProcess = 5;
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
        ShowRoulettePanel();
        TurnProcess = 1;
    }

    void ShowRoulettePanel()
    {
        if (CurrentTurnNumber == Player1TurnNumber) //사용자(플레이어1) 턴인 경우
        {
            RouletteImage.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            RouletteButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            RouletteArrowImage.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        else //AI(플레이어2) 턴인 경우
        {
            RouletteImage.GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f, 0.7f);
            RouletteButton.GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f, 0.7f);
            RouletteArrowImage.GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f, 0.7f);
        }
        RoulettePanel.SetActive(true);
    }
}
