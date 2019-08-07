using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager_AI : MonoBehaviour
{
    public GameObject GameOverPanel;
    public GameObject GameOverImage;
    public GameObject WinPanel;
    public GameObject LosePanel;
    public GameObject RockScissorsPaper;
    public GameObject RoulettePanel;
    public GameObject ChoiceCards;
    public GameObject PanelQuitGame;

    public Text Player1Name;
    public Text Player2Name;
    public Text WinnerText;
    public Text LoserText;
    public Text WinnerMoneyText;
    public Text LoserMoneyText;
    public AudioClip OkButtonClick;
    public AudioClip WinSound;
    public AudioClip LoseSound;

    public Sprite[] GameOverPanel_Image = new Sprite[2]; // 0 - 빨승파패, 1 - 빨패파승


    // Use this for initialization
    void Start()
    {
        WinPanel.SetActive(false);
        LosePanel.SetActive(false);
        GameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PanelQuitGame.SetActive(true);
                Time.timeScale = 0.0f;
            }
        }

        if (MoneySystem_AI.Player1PossessMoney <= 0 || MoneySystem_AI.Player2PossessMoney <= 0 || PlayTimeManagement_AI.TimeOver == true)
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Coin"))
            {
                Destroy(obj);
            }
            RockScissorsPaper.SetActive(false);
            RoulettePanel.SetActive(false);
            ChoiceCards.SetActive(false);
            if (MoneySystem_AI.Player1PossessMoney <= 0)
                MoneySystem_AI.Player1PossessMoney = 0;
            else if (MoneySystem_AI.Player2PossessMoney <= 0)
                MoneySystem_AI.Player2PossessMoney = 0;

            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.0f);
        ShowGameOverPanel();
        Time.timeScale = 0.0f;
    }

    public void OnClick_YesButton()
    {
        this.GetComponent<AudioSource>().clip = OkButtonClick;
        this.GetComponent<AudioSource>().Play();
        GameOverPanel.SetActive(false);
        RockScissorsPaper.SetActive(true);
        RoulettePanel.SetActive(true);
        ChoiceCards.SetActive(true);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }

    public void OnClick_NoButton()
    {
        this.GetComponent<AudioSource>().clip = OkButtonClick;
        this.GetComponent<AudioSource>().Play();
        PanelQuitGame.SetActive(false);
        Time.timeScale = 1.0f;
    }

    void ShowGameOverPanel()
    {
        WinOrLose();
        RockScissorsPaper.SetActive(false);
        RoulettePanel.SetActive(false);
        ChoiceCards.SetActive(false);
        GameOverPanel.SetActive(true);
    }

    void WinOrLose()
    {
        if (MoneySystem_AI.Player1PossessMoney < MoneySystem_AI.Player2PossessMoney) //AI 승
        {
            WinnerText.text = Player2Name.text;
            LoserText.text = Player1Name.text;

            WinnerMoneyText.text = MoneyTextConversion(MoneySystem_AI.Player2PossessMoney);

            if (MoneySystem_AI.Player1PossessMoney < 0)
                LoserMoneyText.text = "0";
            else
                LoserMoneyText.text = MoneyTextConversion(MoneySystem_AI.Player1PossessMoney);

            GameOverImage.GetComponent<Image>().sprite = GameOverPanel_Image[1];
            this.GetComponent<AudioSource>().clip = LoseSound;
            this.GetComponent<AudioSource>().Play();
            LosePanel.SetActive(true);
        }

        else if (MoneySystem_AI.Player1PossessMoney > MoneySystem_AI.Player2PossessMoney) //사용자 승
        {
            WinnerText.text = Player1Name.text;
            LoserText.text = Player2Name.text;

            WinnerMoneyText.text = MoneyTextConversion(MoneySystem_AI.Player1PossessMoney);

            if (MoneySystem_AI.Player2PossessMoney < 0)
                LoserMoneyText.text = "0";
            else
                LoserMoneyText.text = MoneyTextConversion(MoneySystem_AI.Player2PossessMoney);

            GameOverImage.GetComponent<Image>().sprite = GameOverPanel_Image[0];

            this.GetComponent<AudioSource>().clip = WinSound;
            this.GetComponent<AudioSource>().Play();
            WinPanel.SetActive(true);
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
        GameOverPanel.SetActive(false);
        RockScissorsPaper.SetActive(true);
        RoulettePanel.SetActive(true);
        ChoiceCards.SetActive(true);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }

}