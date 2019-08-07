using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChattingSystem : Photon.PunBehaviour {


    public GameObject Open;
    public GameObject Close;
    public GameObject Chatting;
    public GameObject Player1ChattingPanel;
    public GameObject Player2ChattingPanel;
    public GameObject Player1Emoji;
    public GameObject Player2Emoji;
    public Sprite[] EmojiImageArray = new Sprite[5];

    private PhotonView pv;

    // Use this for initialization
    void Start () {
        pv = GetComponent<PhotonView>();
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

    [PunRPC] //원격 호출용 함수
    void ShowPlayer1ChattingPanel(int index)
    {
        Player1ChattingPanel.SetActive(true);
        Player1Emoji.GetComponent<Image>().sprite = EmojiImageArray[index];
    }

    [PunRPC] //원격 호출용 함수
    void ShowPlayer2ChattingPanel(int index)
    {
        Player2ChattingPanel.SetActive(true);
        Player2Emoji.GetComponent<Image>().sprite = EmojiImageArray[index];
    }

    [PunRPC] //원격 호출용 함수
    void HidePlayer1ChattingPanel()
    {
        Player1ChattingPanel.SetActive(false);
    }

    [PunRPC] //원격 호출용 함수
    void HidePlayer2ChattingPanel()
    {
        Player2ChattingPanel.SetActive(false);
    }

    public void CloseClick()
    {
        Chatting.SetActive(false);
        Close.SetActive(false);
        Open.SetActive(true);
    }

    public void OpenClick()
    {
        Chatting.SetActive(true);
        Close.SetActive(true);
        Open.SetActive(false);
    }

    void showPanel(int index)
    {
        if (PhotonNetwork.isMasterClient == true)
        {
            pv.RPC("ShowPlayer1ChattingPanel", PhotonTargets.All, index);
            StartCoroutine(Wait());
        }  
        else
        {
            pv.RPC("ShowPlayer2ChattingPanel", PhotonTargets.All, index);
            StartCoroutine(Wait());
        }
    }

    public void Emoji1_Click()
    {
        showPanel(0);
    }

    public void Emoji2_Click()
    {
        showPanel(1);
    }

    public void Emoji3_Click()
    {
        showPanel(2);
    }

    public void Emoji4_Click()
    {
        showPanel(3);
    }

    public void Emoji5_Click()
    {
        showPanel(4);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(4.0f);

        if(PhotonNetwork.isMasterClient == true)
            pv.RPC("HidePlayer1ChattingPanel", PhotonTargets.All, null);
        else
            pv.RPC("HidePlayer2ChattingPanel", PhotonTargets.All, null);
    }

}
