using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartManager : Photon.PunBehaviour
{
    public static GameObject player1 = null;
    public static GameObject player2 = null;
    public int OtherPlayerCharacter = -1;
    public Vector3 StartLocation;
    public bool FoundOtherPlayer = false;
    public Text Player1NameText; //방장 정보가 항상 왼쪽 상단
    public Text Player2NameText; //방장 아닌애 정보가 항상 오른쪽 상단
    public Text RSPPlayer1NameText; //가위바위보 게임에서 필요한 플레이어 이름
    public Text RSPPlayer2NameText;
    public GameObject Player1MeImage;
    public GameObject Player2MeImage;

    private PhotonView pv;

    public void Awake() //게임 시작 전 제일 처음 실행되는 함수
    {
        Screen.SetResolution(1280, 720, false);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        pv = GetComponent<PhotonView>();
        CreatePlayer(); //입장 시, 자신의 플레이어 생성
        PhotonNetwork.isMessageQueueRunning = true;
        FoundOtherPlayer = false;
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) //스트림 송수신 (데이터 동기화 콜백함수)
    { }   

    [PunRPC] //원격 호출용 함수
    void MasterID(string Name) //방장 이름 받기
    {
        Player1NameText.text = Name;
        RSPPlayer1NameText.text = Name;
    }

    [PunRPC] //원격 호출용 함수
    void SubPlayerID(string Name) //방장 아닌애 이름 받기
    {
        Player2NameText.text = Name;
        RSPPlayer2NameText.text = Name;
    }

    void CreatePlayer() //플레이어 인스턴스화
    {
        if (PhotonNetwork.isMasterClient)
            StartLocation = GameObject.Find("Player1Board (0)").transform.position;
        else
            StartLocation = GameObject.Find("Player2Board (0)").transform.position;
        string CharacterType = "Player" + PlayerPrefs.GetInt("MyCharacter", 0).ToString();
        if (PhotonNetwork.isMasterClient == true)
        {
            player1 = PhotonNetwork.Instantiate(CharacterType, StartLocation, Quaternion.Euler(-90, 0, 45), 0); //각자의 위치에다가 생성
            player1.name = "Player1"; //오브젝트 이름 변경
            Player1MeImage.SetActive(true);
            Player2MeImage.SetActive(false);
        }
        else
        {
            player2 = PhotonNetwork.Instantiate(CharacterType, StartLocation, Quaternion.Euler(-90, 0, 45), 0);
            player2.name = "Player2";
            Player1MeImage.SetActive(false);
            Player2MeImage.SetActive(true);
        }
    }

    void Start()
    {
        if (PhotonNetwork.isMasterClient == true)
        {
            string MasterName = PhotonNetwork.player.NickName;
            pv.RPC("MasterID", PhotonTargets.All, MasterName);
        }
        else
        {
            string SubPlayerName = PhotonNetwork.player.NickName;
            pv.RPC("SubPlayerID", PhotonTargets.All, SubPlayerName);
        }
    }

    void Update()
    {
        if(FoundOtherPlayer == false)
        {
            if (PhotonNetwork.isMasterClient)
            {
                if (GameObject.Find("Player0(Clone)") != null)
                {
                    GameObject.Find("Player0(Clone)").name = "Player2";
                    FoundOtherPlayer = true;
                }
                else if (GameObject.Find("Player1(Clone)") != null)
                {
                    GameObject.Find("Player1(Clone)").name = "Player2";
                    FoundOtherPlayer = true;
                }
                else if (GameObject.Find("Player2(Clone)") != null)
                {
                    GameObject.Find("Player2(Clone)").name = "Player2";
                    FoundOtherPlayer = true;
                }
                else if (GameObject.Find("Player3(Clone)") != null)
                {
                    GameObject.Find("Player3(Clone)").name = "Player2";
                    FoundOtherPlayer = true;
                }
                else if (GameObject.Find("Player4(Clone)") != null)
                {
                    GameObject.Find("Player4(Clone)").name = "Player2";
                    FoundOtherPlayer = true;
                }
                else if (GameObject.Find("Player5(Clone)") != null)
                {
                    GameObject.Find("Player5(Clone)").name = "Player2";
                    FoundOtherPlayer = true;
                }
                else if (GameObject.Find("Player6(Clone)") != null)
                {
                    GameObject.Find("Player6(Clone)").name = "Player2";
                    FoundOtherPlayer = true;
                }
                else if (GameObject.Find("Player7(Clone)") != null)
                {
                    GameObject.Find("Player7(Clone)").name = "Player2";
                    FoundOtherPlayer = true;
                }
                else if (GameObject.Find("Player8(Clone)") != null)
                {
                    GameObject.Find("Player8(Clone)").name = "Player2";
                    FoundOtherPlayer = true;
                }
                else if (GameObject.Find("Player9(Clone)") != null)
                {
                    GameObject.Find("Player9(Clone)").name = "Player2";
                    FoundOtherPlayer = true;
                }

            }
            else
            {
                if (GameObject.Find("Player0(Clone)") != null)
                {
                    GameObject.Find("Player0(Clone)").name = "Player1";
                    FoundOtherPlayer = true;
                }
                else if (GameObject.Find("Player1(Clone)") != null)
                {
                    GameObject.Find("Player1(Clone)").name = "Player1";
                    FoundOtherPlayer = true;
                }
                else if (GameObject.Find("Player2(Clone)") != null)
                {
                    GameObject.Find("Player2(Clone)").name = "Player1";
                    FoundOtherPlayer = true;
                }
                else if (GameObject.Find("Player3(Clone)") != null)
                {
                    GameObject.Find("Player3(Clone)").name = "Player1";
                    FoundOtherPlayer = true;
                }
                else if (GameObject.Find("Player4(Clone)") != null)
                {
                    GameObject.Find("Player4(Clone)").name = "Player1";
                    FoundOtherPlayer = true;
                }
                else if (GameObject.Find("Player5(Clone)") != null)
                {
                    GameObject.Find("Player5(Clone)").name = "Player1";
                    FoundOtherPlayer = true;
                }
                else if (GameObject.Find("Player6(Clone)") != null)
                {
                    GameObject.Find("Player6(Clone)").name = "Player1";
                    FoundOtherPlayer = true;
                }
                else if (GameObject.Find("Player7(Clone)") != null)
                {
                    GameObject.Find("Player7(Clone)").name = "Player1";
                    FoundOtherPlayer = true;
                }
                else if (GameObject.Find("Player8(Clone)") != null)
                {
                    GameObject.Find("Player8(Clone)").name = "Player1";
                    FoundOtherPlayer = true;
                }
                else if (GameObject.Find("Player9(Clone)") != null)
                {
                    GameObject.Find("Player9(Clone)").name = "Player1";
                    FoundOtherPlayer = true;
                }
            }
        }
    }
}
