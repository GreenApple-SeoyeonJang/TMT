using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ReadyButtonScript : Photon.PunBehaviour {

    public static int HowManyReady = 0; //몇명이 레디 했는가
    public GameObject ButtonCover;
    public Texture[] ButtonImage = new Texture[2]; //0레디버튼 이미지, 1스타트버튼 이미지
    private PhotonView pv;
    public AudioClip ButtonClickSound;

    public static bool ButtonClicked;

    private void Awake()
    {
        ButtonCover = GameObject.Find("ButtonCoverButton");
    }
    void Start () {
        HowManyReady = 0;
        ButtonCover.SetActive(false);
        pv = GetComponent<PhotonView>();

        ButtonClicked = false;
    }

    [PunRPC] //원격 호출용 함수
    void GameStart() //턴 정보 동기화 위함
    {
        PhotonNetwork.isMessageQueueRunning = false;
        SceneManager.LoadScene("Game(1vs1)");
    }

    [PunRPC] //원격 호출용 함수
    void PlayerReady(int ReadyNumber) //턴 정보 동기화 위함
    {
        HowManyReady = ReadyNumber;
        ButtonClicked = true;
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) //스트림 송수신 (데이터 동기화 콜백함수)
    {
        if (stream.isWriting) //데이터 변경을 올림
        {


        }
        else //데이터 변경된 부분이 있으면 받아옴 (다른 플레이어가 만든 변경 내용을 받아옴)
        {

        }

    }

    void Update () {
        if (PhotonNetwork.isMasterClient)
        {
            this.GetComponent<RawImage>().texture = ButtonImage[1];
            if (HowManyReady < 1)
                ButtonCover.SetActive(true);
            if(HowManyReady == 1)
                ButtonCover.SetActive(false);
        }
        else
        {
            this.GetComponent<RawImage>().texture = ButtonImage[0];
        }
    }

    public void ClickReadyButton()
    {
        this.GetComponent<AudioSource>().clip = ButtonClickSound;
        this.GetComponent<AudioSource>().Play();
        if (PhotonNetwork.isMasterClient)
            pv.RPC("GameStart", PhotonTargets.All, null);
        else
        {
            ButtonCover.SetActive(true);
            pv.RPC("PlayerReady", PhotonTargets.All, (HowManyReady+1));
        }
    }

    public void ButtonCoverClick()
    {
        this.GetComponent<AudioSource>().clip = ButtonClickSound;
        this.GetComponent<AudioSource>().Play();
        if (!PhotonNetwork.isMasterClient)
        {
            ButtonCover.SetActive(false);
            pv.RPC("PlayerReady", PhotonTargets.All, (HowManyReady - 1));
        }
    }
}
