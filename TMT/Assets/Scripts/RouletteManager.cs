using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteManager : Photon.PunBehaviour
{
    public static int WhoAmI;
    public float angle;
    public float rotateSpeed = 0;
    public bool button_click = false;
    public bool TurnMentOn = false;
    public bool RouletteSystemGoingOn = false;
    public static int RouletteColor; // 돌려서 나온 룰렛 색깔(1,2,3,4,5,6,7)
    public GameObject RouletteMent;
    public GameObject RoulettePanel; // 룰렛 전체 패널
    public GameObject Roulette; // 실제로 돌아가는 룰렛 판
    public Button RouletteButton;
    public Text RouletteMentText;
    private PhotonView pv;
    public Text Player1Name;
    public Text Player2Name;

    void Start()
    {
        RouletteSystemGoingOn = false;
        pv = GetComponent<PhotonView>();
        RoulettePanel.SetActive(false);
        RouletteMent.SetActive(false);

        if (PhotonNetwork.isMasterClient)
            WhoAmI = 1;
        else
            WhoAmI = 2;
    }

    void Update()
    {
        if((TurnSystem.TurnProcess == 1) && (RouletteSystemGoingOn == false)) //지금 턴인 플레이어만 걸림(turnprocess 1일때)
        {
            RouletteSystemGoingOn = true;
            pv.RPC("ShowTurnMent", PhotonTargets.All, WhoAmI);
        }
        if (button_click == true)
            pv.RPC("SpinRoulette", PhotonTargets.All, WhoAmI);
    }

    public void RouletteButtonClick()
    {
        float RouletteSpeed;
        button_click = true;
        RouletteSpeed = Random.Range(17f, 19f);
        pv.RPC("RouletteSpeed", PhotonTargets.All, RouletteSpeed);
        RouletteButton.interactable = false;
    }

    [PunRPC]
    public void RouletteSpeed (float Speed)
    {
        this.rotateSpeed = Speed;
    }

    [PunRPC]
    public void SpinRoulette(int Who)
    {
        transform.Rotate(0, 0, this.rotateSpeed);

        if ( (Roulette.transform.eulerAngles.z >= 52f) && (Roulette.transform.eulerAngles.z  <=  58f) )
            this.GetComponent<AudioSource>().Play();
        else if((Roulette.transform.eulerAngles.z >= 106f) && (Roulette.transform.eulerAngles.z <= 112f) )
            this.GetComponent<AudioSource>().Play();
        else if ((Roulette.transform.eulerAngles.z >= 160f) && (Roulette.transform.eulerAngles.z <= 166f))
            this.GetComponent<AudioSource>().Play();
        else if ((Roulette.transform.eulerAngles.z >= 214f) && (Roulette.transform.eulerAngles.z <= 220f))
            this.GetComponent<AudioSource>().Play();
        else if ((Roulette.transform.eulerAngles.z >= 261f) && (Roulette.transform.eulerAngles.z <= 267f))
            this.GetComponent<AudioSource>().Play();
        else if ((Roulette.transform.eulerAngles.z >= 308f) && (Roulette.transform.eulerAngles.z <= 314f))
            this.GetComponent<AudioSource>().Play();
        else if ((Roulette.transform.eulerAngles.z >= 354f) && (Roulette.transform.eulerAngles.z <= 359.9f))
            this.GetComponent<AudioSource>().Play();

        if (this.rotateSpeed > 7.5)
        {
            this.rotateSpeed *= 0.993f;

        }

        else if (this.rotateSpeed > 5.7)
        {
            this.rotateSpeed *= 0.977f;
        }

        else if (this.rotateSpeed > 1.7)
        {
            this.rotateSpeed *= 0.975f;
        }

        else if (this.rotateSpeed <= 1.7)
        {
            this.rotateSpeed = 0;
            angle = Roulette.transform.eulerAngles.z;
            if (WhoAmI == Who)
                WhatColor();
        }
        
    }

    void WhatColor()
    {
        button_click = false;

        if (angle < 54f) //빨강
        {
            RouletteColor = 1;
        }
        else if (angle < 108f) //주황
        {
            RouletteColor = 2;
        }
        else if (angle < 162f) //노랑
        {
            RouletteColor = 3;
        }
        else if (angle < 216f) //초록
        {
            RouletteColor = 4;
        }
        else if (angle < 262.8f) //파랑
        {
            RouletteColor = 5;
        }
        else if (angle < 309.6f) //분홍
        {
            RouletteColor = 6;
        }
        else//보라
        {
            RouletteColor = 7;
        }
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        pv.RPC("HideTurnMent", PhotonTargets.All, null); 
        TurnSystem.TurnProcess = 2;
        RouletteSystemGoingOn = false;
    } 

    [PunRPC]
    public void ShowTurnMent(int Who)
    {
        if(Who == 1)
            RouletteMentText.text = Player1Name.text + "의 턴입니다";
        else
            RouletteMentText.text = Player2Name.text + "의 턴입니다";

        if (WhoAmI == Who)
        {
            RouletteButton.interactable = true;
        }
        else //나머지 플레이어는 버튼 선택 비활성화
        {
            RouletteButton.interactable = false;
        }
        RouletteMent.SetActive(true);
    }

    [PunRPC]
    public void HideTurnMent()
    {
        transform.Rotate(0, 0, (360f - angle));
        RouletteButton.interactable = false;
        RouletteMent.SetActive(false);
        TurnMentOn = false;
        RoulettePanel.SetActive(false);
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) //스트림 송수신 (데이터 동기화 콜백함수)
    { }
}