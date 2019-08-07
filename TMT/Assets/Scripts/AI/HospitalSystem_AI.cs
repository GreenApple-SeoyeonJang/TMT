using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HospitalSystem_AI : MonoBehaviour
{
    public bool HospitalCheckGoingOn;
    public GameObject HospitalNotice;
    public static bool Player1InTheHospital;
    public static bool Player2InTheHospital;
    public static bool Player1OkComeOut;
    public static bool Player2OkComeOut;
    public Text HospitalNoticeText;
    public GameObject Ambulance;
    public GameObject AmbulanceLight;
    public Vector3 HospitalBoard;

    void Start()
    {
        Player1InTheHospital = false;
        Player2InTheHospital = false;
        Player1OkComeOut = false;
        Player2OkComeOut = false;
        HospitalCheckGoingOn = false;
        HospitalNotice.SetActive(false);
        AmbulanceLight.SetActive(false);
        Ambulance.SetActive(false);
        HospitalBoard = GameObject.Find("Hospital Board").transform.position;
    }

    void Update()
    {
        if ((TurnSystem_AI.TurnProcess == 4) && (HospitalCheckGoingOn == false))
        {
            HospitalCheckGoingOn = true;
            if (TurnSystem_AI.CurrentTurnNumber == TurnSystem_AI.Player1TurnNumber) //사용자 (플레이어1) 턴인 경우
            {
                if (TirednessSystem_AI.Player1Health <= 0)
                    GoToHospital(1);
                else
                {
                    TurnSystem_AI.TurnProcess = 5;
                    HospitalCheckGoingOn = false;
                }
            }
            else if(TurnSystem_AI.CurrentTurnNumber == TurnSystem_AI.Player2TurnNumber) //AI (플레이어2) 턴인 경우
            {
                if (TirednessSystem_AI.Player2Health <= 0)
                    GoToHospital(2);
                else
                {
                    TurnSystem_AI.TurnProcess = 5;
                    HospitalCheckGoingOn = false;
                }
            }
        }
    }

    public void GoToHospital(int Who)
    {
        this.GetComponent<AudioSource>().Play();
        if (Who == 1) //사용자 (플레이어1) 턴인 경우
            HospitalNoticeText.text = "너무 피곤해서 쓰러져버린 당신...\n40만원을 지불하고 병원에 입원합니다";
        else //AI (플레이어2) 턴인 경우
            HospitalNoticeText.text = "상대 플레이어가 과로로 인해\n40만원을 지불하고 병원에 입원합니다";
        HospitalNotice.SetActive(true);
        StartCoroutine(AmbulanceGo(Who));
    }

    IEnumerator AmbulanceGo(int Who)
    {
        GameObject HospitalPlayer;
        Vector3 HospitalPlayerLocation;
        if (Who == 1)
            HospitalPlayer = GameObject.Find("Player1");
        else
            HospitalPlayer = GameObject.Find("Player2");
        HospitalPlayerLocation = HospitalPlayer.transform.position;

        Ambulance.SetActive(true);
        Ambulance.transform.LookAt(HospitalPlayerLocation); //앰뷸런스 출동하기
        Ambulance.transform.rotation = Quaternion.Euler(-90, Ambulance.transform.eulerAngles.y, Ambulance.transform.eulerAngles.z);

        float xdif = (HospitalPlayerLocation.x - Ambulance.transform.position.x) / 50;
        float zdif = (HospitalPlayerLocation.z - Ambulance.transform.position.z) / 50;

        for (int i = 0; i < 31; i++)
        {
            Ambulance.transform.position = new Vector3(Ambulance.transform.position.x + xdif, Ambulance.transform.position.y, Ambulance.transform.position.z + zdif);
            if ((i % 3) == 0)
            {
                if (AmbulanceLight.activeSelf)
                    AmbulanceLight.SetActive(false);
                else
                    AmbulanceLight.SetActive(true);
            }
            yield return new WaitForSeconds(0.07f);
        }
        AmbulanceLight.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        HospitalPlayer.SetActive(false);

        Ambulance.transform.LookAt(HospitalBoard); //앰뷸런스 돌아가기
        Ambulance.transform.rotation = Quaternion.Euler(-90, Ambulance.transform.eulerAngles.y, Ambulance.transform.eulerAngles.z);

        xdif = (HospitalBoard.x - Ambulance.transform.position.x) / 40;
        zdif = (HospitalBoard.z - Ambulance.transform.position.z) / 40;
        AmbulanceLight.SetActive(true);

        for (int i = 0; i < 40; i++)
        {
            Ambulance.transform.position = new Vector3(Ambulance.transform.position.x + xdif, Ambulance.transform.position.y, Ambulance.transform.position.z + zdif);
            if ((i % 3) == 0)
            {
                if (AmbulanceLight.activeSelf)
                    AmbulanceLight.SetActive(false);
                else
                    AmbulanceLight.SetActive(true);
            }
            yield return new WaitForSeconds(0.07f);
        }
        HospitalNoticeText.text = "병원 도착!\n과로하지 맙시다";
        yield return new WaitForSeconds(1.5f);
        AmbulanceLight.SetActive(false);
        Ambulance.SetActive(false);
        HospitalNotice.SetActive(false);
        HospitalPlayer.transform.position = HospitalBoard;
        HospitalPlayer.SetActive(true);
        this.GetComponent<AudioSource>().Stop();
        var EA = GameObject.Find("BoardEffect").GetComponent<EffectApply_AI>();
        StartCoroutine(EA.MoneyChangeApply(Who, -400000, 28));
        StartCoroutine(EA.HealthChangeApply(Who, 200));
        yield return new WaitForSeconds(1.5f);

        if (Who == 1) //사용자(플레이어1)이 병원 간 경우
            Player1InTheHospital = true;
        else if (Who == 2) //AI (플레이어2)가 병원 간 경우
            Player2InTheHospital = true;

        TurnSystem_AI.TurnProcess = 5;
        HospitalCheckGoingOn = false;
    }

}
