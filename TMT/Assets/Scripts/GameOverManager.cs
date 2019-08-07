using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon;

public class GameOverManager : Photon.PunBehaviour
{
    public bool Disconnected;
    public string DisconnectedNick;

    public GameObject GameOverPanel;
    public GameObject GameOverImage;
    public GameObject ConnectionImage;
    public GameObject WinPanel;
    public GameObject LosePanel;
    public GameObject RockScissorsPaper;
    public GameObject RoulettePanel;
    public GameObject ChoiceCards;

    public Text WinnerText;
    public Text LoserText;
    public Text WinnerMoneyText;
    public Text LoserMoneyText;
    public AudioClip OkButtonClick;
    public AudioClip WinSound;
    public AudioClip LoseSound;
    public GameObject CannotQuitMessage;

    public Sprite[] GameOverPanel_Image = new Sprite[2]; // 0 - 빨승파패, 1 - 빨패파승

    private PhotonView pv;

    // Use this for initialization
    void Start()
    {
        pv = GetComponent<PhotonView>();
        ConnectionImage.SetActive(false);
        WinPanel.SetActive(false);
        LosePanel.SetActive(false);
        GameOverPanel.SetActive(false);
        Disconnected = false;
        CannotQuitMessage.SetActive(false);
    }

    [PunRPC]
    void ShowGameOverPanel()
    {
        WinOrLose();
        RockScissorsPaper.SetActive(false);
        RoulettePanel.SetActive(false);
        ChoiceCards.SetActive(false);
        GameOverPanel.SetActive(true);
    }

    [PunRPC]
    void WinnerOrLoserNick(string Winner, string Loser)
    {
        WinnerText.text = Winner;
        LoserText.text = Loser;
    }

    [PunRPC]
    void WinnerOrLoserMoney(int ToChange)
    {
        GameOverImage.GetComponent<Image>().sprite = GameOverPanel_Image[ToChange];
    }

    [PunRPC]
    void IsItDiconnected(bool State)
    {
        Disconnected = State;
    }

    [PunRPC]
    void StopTime()
    {
        Time.timeScale = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                StartCoroutine(CannotQuitMessageShow());
            }
        }

        if (MoneySystem.Player1PossessMoney <= 0 || MoneySystem.Player2PossessMoney <= 0 || PlayTimeManagement.TimeOver == true || Disconnected == true)
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Coin"))
            {
                Destroy(obj);
            }
            RockScissorsPaper.SetActive(false);
            RoulettePanel.SetActive(false);
            ChoiceCards.SetActive(false);
            if (MoneySystem.Player1PossessMoney <= 0)
                MoneySystem.Player1PossessMoney = 0;
            else if (MoneySystem.Player2PossessMoney <= 0)
                MoneySystem.Player2PossessMoney = 0;

            StartCoroutine(Wait());
        }
    }

    IEnumerator CannotQuitMessageShow()
    {
        CannotQuitMessage.SetActive(true);
        for(int i = 10; i > 0; i--)
        {
            CannotQuitMessage.GetComponent<Image>().color = new Color(1, 1, 1, i/10f);
            yield return new WaitForSeconds(0.07f);
        }
        CannotQuitMessage.SetActive(false);
    }
        
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.0f);

        if (GameOverPanel.activeSelf == false)
            pv.RPC("ShowGameOverPanel", PhotonTargets.All, null);
        pv.RPC("StopTime", PhotonTargets.All, null);

    }

    void WinOrLose()
    {
        if (Disconnected == true)
        {
            pv.RPC("WinnerOrLoserNick", PhotonTargets.All, PhotonNetwork.player.NickName, DisconnectedNick);
            pv.RPC("IsItDiconnected", PhotonTargets.All, false);
            ConnectionImage.SetActive(true);

            if (RouletteManager.WhoAmI == 2)
            {
                WinnerMoneyText.text = MoneyTextConversion(MoneySystem.Player2PossessMoney);
                LoserMoneyText.text = "";
                pv.RPC("WinnerOrLoserMoney", PhotonTargets.All, 1);

                this.GetComponent<AudioSource>().clip = WinSound;
                this.GetComponent<AudioSource>().Play();
                
                WinPanel.SetActive(true);
            }
            else
            {
                WinnerMoneyText.text = MoneyTextConversion(MoneySystem.Player1PossessMoney);
                LoserMoneyText.text = "";
                pv.RPC("WinnerOrLoserMoney", PhotonTargets.All, 0);

                this.GetComponent<AudioSource>().clip = WinSound;
                this.GetComponent<AudioSource>().Play();

                WinPanel.SetActive(true);
            }
        }

        else if (MoneySystem.Player1PossessMoney < MoneySystem.Player2PossessMoney)
        {
            WinnerMoneyText.text = MoneyTextConversion(MoneySystem.Player2PossessMoney);

            if (MoneySystem.Player1PossessMoney < 0)
                LoserMoneyText.text = "0";
            else
                LoserMoneyText.text = MoneyTextConversion(MoneySystem.Player1PossessMoney);

            pv.RPC("WinnerOrLoserMoney", PhotonTargets.All, 1);

            if (!PhotonNetwork.isMasterClient)
            {
                this.GetComponent<AudioSource>().clip = WinSound;
                this.GetComponent<AudioSource>().Play();
                pv.RPC("WinnerOrLoserNick", PhotonTargets.All, PhotonNetwork.player.NickName, PhotonNetwork.masterClient.NickName);
                WinPanel.SetActive(true);
            }
            else
            {
                this.GetComponent<AudioSource>().clip = LoseSound;
                this.GetComponent<AudioSource>().Play();
                LosePanel.SetActive(true);
            }
                
        }

        else if (MoneySystem.Player1PossessMoney > MoneySystem.Player2PossessMoney)
        {
            WinnerMoneyText.text = MoneyTextConversion(MoneySystem.Player1PossessMoney);

            if (MoneySystem.Player2PossessMoney < 0)
                LoserMoneyText.text = "0";
            else
                LoserMoneyText.text = MoneyTextConversion(MoneySystem.Player2PossessMoney);
            
            pv.RPC("WinnerOrLoserMoney", PhotonTargets.All, 0);

            if (!PhotonNetwork.isMasterClient)
            {
                this.GetComponent<AudioSource>().clip = LoseSound;
                this.GetComponent<AudioSource>().Play();
                pv.RPC("WinnerOrLoserNick", PhotonTargets.All, PhotonNetwork.masterClient.NickName, PhotonNetwork.player.NickName);
                LosePanel.SetActive(true);
            }
            else
            {
                this.GetComponent<AudioSource>().clip = WinSound;
                this.GetComponent<AudioSource>().Play();
                WinPanel.SetActive(true);
            }
                
        }
    }

    private string MoneyTextConversion(int Money)
    {
        return (string.Format("{0:#,###}", Money));
    }


    public void OnClick_OkayButton()
    {
        this.GetComponent<AudioSource>().clip = OkButtonClick;
        this.GetComponent<AudioSource>().Play();
        ConnectionImage.SetActive(false);
        GameOverPanel.SetActive(false);
        RockScissorsPaper.SetActive(true);
        RoulettePanel.SetActive(true);
        ChoiceCards.SetActive(true);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Ready");
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        pv.RPC("IsItDiconnected", PhotonTargets.All, true);
        DisconnectedNick = otherPlayer.NickName; // 접속 끊긴 사람
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) //스트림 송수신 (데이터 동기화 콜백함수)
    {
    }
}