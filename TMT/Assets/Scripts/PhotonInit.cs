using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon;


public class PhotonInit : Photon.PunBehaviour {

    public string version = "v1.0";
    public string[] RoomName = {"즐거운 게임 한 판~", "나랑 한 판 붙어볼래?", "용자만 들어오시오", "나는 부자가 될 거야!"};

    public InputField roomName;
    public GameObject scrollContents;
    public GameObject roomItem;
    public GameObject PanelCreateRoom;
    public AudioClip ButtonClickSound;

    void Awake ()
    {
        Screen.SetResolution(1280, 720, false);
        PhotonNetwork.ConnectUsingSettings(version);

        roomName.text = RoomName[Random.Range(0, 4)];
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

    public override void OnJoinedLobby()
    {
        //Debug.Log("Entered Lobby !");
    }

    void OnPhotonRandomJoinFailed ()
    {
        Debug.Log("No rooms !");
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 2;

        PhotonNetwork.CreateRoom(RoomName[Random.Range(0, 4)], roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom ()
    {
        //Debug.Log("Enter Room");

        StartCoroutine(this.LoadGameField());
    }

    IEnumerator LoadGameField()
    {
        PhotonNetwork.isMessageQueueRunning = false;
        AsyncOperation ao = SceneManager.LoadSceneAsync("Ready");
        yield return ao;
    }

    public void OnClick_BackButton()
    {
        this.GetComponent<AudioSource>().clip = ButtonClickSound;
        this.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("MainMenu");
    }

    public void OnClick_JoinRandomRoom ()
    {
        this.GetComponent<AudioSource>().clip = ButtonClickSound;
        this.GetComponent<AudioSource>().Play();
        PhotonNetwork.JoinRandomRoom();
    }

    public void OnClick_Go()
    {
        this.GetComponent<AudioSource>().clip = ButtonClickSound;
        this.GetComponent<AudioSource>().Play();
        string _roomName = roomName.text;

        if (string.IsNullOrEmpty(roomName.text))
        {
            _roomName = RoomName[Random.Range(0, 4)];
        }

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 2;

        PhotonNetwork.CreateRoom(_roomName, roomOptions, TypedLobby.Default);
    }

    public void OnClick_CreateRoom ()
    {
        this.GetComponent<AudioSource>().clip = ButtonClickSound;
        this.GetComponent<AudioSource>().Play();
        PanelCreateRoom.SetActive(true);
    }

    public void OnClick_CloseButton()
    {
        this.GetComponent<AudioSource>().clip = ButtonClickSound;
        this.GetComponent<AudioSource>().Play();
        PanelCreateRoom.SetActive(false);
    }

    public override void OnPhotonCreateRoomFailed (object[] codeAndMsg)
    {
        Debug.Log("Create Room Failed = " + codeAndMsg[1]);
    }

    public override void OnReceivedRoomListUpdate ()
    {
       
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("ROOM_ITEM"))
        {
            Destroy(obj);
        }

        foreach (RoomInfo _room in PhotonNetwork.GetRoomList())
        {
            GameObject room = (GameObject) Instantiate(roomItem);
            room.transform.SetParent(scrollContents.transform, false);

            RoomData roomData = room.GetComponent<RoomData>();
            roomData.roomName = _room.Name;
            roomData.connectPlayer = _room.PlayerCount;
            roomData.maxPlayers = _room.MaxPlayers;

            roomData.DispRoomData();
            roomData.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { OnClick_RoomItem(roomData.roomName); });
        }
    }

    void OnClick_RoomItem(string roomName)
    {
        this.GetComponent<AudioSource>().clip = ButtonClickSound;
        this.GetComponent<AudioSource>().Play();
        PhotonNetwork.JoinRoom(roomName);
    }

    void OnGUI ()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

}
