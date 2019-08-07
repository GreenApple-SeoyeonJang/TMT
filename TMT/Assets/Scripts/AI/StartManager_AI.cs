using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartManager_AI : MonoBehaviour
{
    public static GameObject player1 = null;
    public static GameObject player2 = null;
    public Vector3 StartLocation;
    public Text Player1NameText; //방장 정보가 항상 왼쪽 상단
    public Text Player2NameText; //방장 아닌애 정보가 항상 오른쪽 상단
    public GameObject Player1MeImage;
    public GameObject[] Characters = new GameObject[10];


    public void Awake() //게임 시작 전 제일 처음 실행되는 함수
    {
        Screen.SetResolution(1280, 720, false);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        CreatePlayer(); //입장 시, 자신의 플레이어 생성
    }


    void CreatePlayer() //플레이어 인스턴스화
    {
        StartLocation = GameObject.Find("Player1Board (0)").transform.position;
        player1 = (GameObject)Instantiate(Characters[PlayerPrefs.GetInt("MyCharacter", 0)], StartLocation, Quaternion.Euler(-90, 0, 45));
        player1.name = "Player1";
        StartLocation = GameObject.Find("Player2Board (0)").transform.position;
        player2 = (GameObject)Instantiate(Characters[Random.Range(0, 10)], StartLocation, Quaternion.Euler(-90, 0, 45));
        player2.name = "Player2";
        Player1MeImage.SetActive(true);
    }

    void Start()
    {
        Player1NameText.text = PlayerPrefs.GetString("USER_ID", "아무개");
        int RandomName = Random.Range(1, 8);
        if(RandomName == 1)
            Player2NameText.text = "자린고비";
        else if(RandomName == 2)
            Player2NameText.text = "인생한방";
        else if(RandomName == 3)
            Player2NameText.text = "벼락부자";
        else if(RandomName == 4)
            Player2NameText.text = "이판사판";
        else if (RandomName == 5)
            Player2NameText.text = "리치맨";
        else if (RandomName == 6)
            Player2NameText.text = "건물주";
        else
            Player2NameText.text = "부자관계";
    }

}