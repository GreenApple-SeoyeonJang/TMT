using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon;

public class ReadyData : Photon.PunBehaviour
{
    [HideInInspector]
    public int connectPlayer = 0;
    [HideInInspector]
    public int maxPlayers = 0;

    public Text textPlayerID;

    public GameObject panel_PI;
    public GameObject playerInfo;

    public GameObject ReadyButton;

    public Text txtLogMsg;
    private PhotonView pv;

    public static int playerWhoIsIt = 0;
    public static bool PlayerChanged;
    public AudioClip ButtonClickSound;

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) //스트림 송수신 (데이터 동기화 콜백함수)
    {
        if (stream.isWriting) //데이터 변경을 올림
        {

        }
        else //데이터 변경된 부분이 있으면 받아옴 (다른 플레이어가 만든 변경 내용을 받아옴)
        {

        }

    }

    void CreatePlayerInfo()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("PLAYER_INFO"))
        {
            Destroy(obj);
        }

        foreach (PhotonPlayer player in PhotonNetwork.playerList)
        {
            textPlayerID.text = player.NickName;

            //CreatePlayerInfo();
            //button.SetActive(false);
            GameObject info = (GameObject)Instantiate(playerInfo);
            info.transform.SetParent(panel_PI.transform, false);
        }
    }

    void Awake()
    {
        Screen.SetResolution(1280, 720, false);
        pv = GetComponent<PhotonView>();
        PhotonNetwork.isMessageQueueRunning = true;
        CreatePlayerInfo();
    }

    void Start()
    {
        string msg = "\n<color=#00ff00>※[" + PhotonNetwork.player.NickName + "]님이 입장하셨습니다 ※</color>";
        pv.RPC("LogMsg", PhotonTargets.AllBuffered, msg);
        PlayerChanged = false;
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        CreatePlayerInfo();
        if (PhotonNetwork.playerList.Length == 1)
            playerWhoIsIt = PhotonNetwork.player.ID;
        
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        CreatePlayerInfo();

        string msg = "\n<color=#ff0000>※[" + otherPlayer.NickName + "]님이 퇴장하셨습니다 ※</color>";
        pv.RPC("LogMsg", PhotonTargets.AllBuffered, msg);

        if(PhotonNetwork.isMasterClient)
        {
            if(otherPlayer.ID == playerWhoIsIt)
            {
                TagPlayer(PhotonNetwork.player.ID);
            }
        }
        //base.OnPhotonPlayerDisconnected(otherPlayer);
    }


    public void TagPlayer(int playerID)
    {
        pv.RPC("TaggedPlayer", PhotonTargets.All, playerID);
    }

    [PunRPC]
    void TaggedPlayer(int playerID)
    {
        playerWhoIsIt = playerID;
    }

    void OnMasterClientSwitched()
    {
        Debug.Log("OnMasterClientSwitched");
        
    }

    [PunRPC]
    void LogMsg(string msg)
    {
        txtLogMsg.text = txtLogMsg.text + msg;
        PlayerChanged = true;
    }

    public void OnClickExitRoom()
    {
        this.GetComponent<AudioSource>().clip = ButtonClickSound;
        this.GetComponent<AudioSource>().Play();
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Lobby");
    }

    // Update is called once per frame
    void Update () {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PhotonNetwork.LeaveRoom();
            }
        }
    }
}
