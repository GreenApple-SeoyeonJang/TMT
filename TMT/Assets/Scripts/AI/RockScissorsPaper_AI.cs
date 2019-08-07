using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RockScissorsPaper_AI : MonoBehaviour
{
    public static bool RSPGoingOn;
    public GameObject RockScissorsPaperTimer;
    public Text RockScissorsPaperTimerText;
    public GameObject RockScissorsPaperGameStartPanel;
    public GameObject RockScissorsPaperGameResultPanel;
    public Image RockScissorsPaperGameResultPanelImage;
    public Button RockButton;
    public Button ScissorsButton;
    public Button PaperButton;
    public GameObject RockButtonSelectImage;
    public GameObject ScissorsButtonSelectImage;
    public GameObject PaperButtonSelectImage;
    public Image Player1ChoiceImage;
    public Image Player2ChoiceImage;
    public Sprite[] RSPChoiceImage = new Sprite[4]; //0제외 1:가위, 2:주먹, 3:보
    public Sprite[] RSPResultPanelImages = new Sprite[3]; //0이겼을 때, 2:비겼을 떄, 2:졌을 때
    public GameObject WinResultImageObject;
    public GameObject DrawResultImageObject;
    public GameObject LoseResutlImageObject;
    public AudioClip RSPPanelPopUpSound;
    public AudioClip ButtonSound;
    public AudioClip ResultSound;
    public Text RSPPlayer1Name;
    public Text RSPPlayer2Name;
    public Text Player1Name;
    public Text Player2Name;

    public int Player1Choice;

    void Start()
    {
        RSPGoingOn = false;
        RockScissorsPaperTimer.SetActive(false);
        RockScissorsPaperGameStartPanel.SetActive(false);
        RockScissorsPaperGameResultPanel.SetActive(false);
        RockButtonSelectImage.SetActive(false);
        ScissorsButtonSelectImage.SetActive(false);
        PaperButtonSelectImage.SetActive(false);
        WinResultImageObject.SetActive(false);
        LoseResutlImageObject.SetActive(false);
        DrawResultImageObject.SetActive(false);
        Player1Choice = 0;
        RSPPlayer1Name.text = Player1Name.text;
        RSPPlayer2Name.text = Player2Name.text;
    }

    public void RockScissorsPaperStart()
    {
        this.GetComponent<AudioSource>().clip = RSPPanelPopUpSound;
        this.GetComponent<AudioSource>().Play();
        RockScissorsPaperGameStartPanel.SetActive(true);
        RockButton.interactable = true;
        ScissorsButton.interactable = true;
        PaperButton.interactable = true;
        StartCoroutine(RSPTimerStart());
    }

    public IEnumerator RSPTimerStart()
    {
        RockScissorsPaperTimer.SetActive(true);
        RockScissorsPaperTimerText.text = "15";
        for (int i = 15; i >= 0; i--)
        {
            RockScissorsPaperTimerText.text = i.ToString();
            yield return new WaitForSeconds(1.0f);
        }
        if (RockScissorsPaperTimer.activeSelf == true) //시간 내에 못골랐으면
        {
            int RandomNumber;
            RockScissorsPaperTimer.SetActive(false);
            RandomNumber = Random.Range(1, 4);
            if (RandomNumber == 1) //가위
            {
                RockButton.interactable = false;
                PaperButton.interactable = false;
                ScissorsButtonSelectImage.SetActive(true);
                RSPChoice(1);
            }
            else if (RandomNumber == 2) //주먹
            {
                ScissorsButton.interactable = false;
                PaperButton.interactable = false;
                RockButtonSelectImage.SetActive(true);
                RSPChoice(2);
            }
            else if (RandomNumber == 3) //보
            {
                ScissorsButton.interactable = false;
                RockButton.interactable = false;
                PaperButtonSelectImage.SetActive(true);
                RSPChoice(3);
            }
        }
    }

    public void RockButtonClick()
    {
        this.GetComponent<AudioSource>().clip = ButtonSound;
        this.GetComponent<AudioSource>().Play();
        RockScissorsPaperTimer.SetActive(false);
        ScissorsButton.interactable = false;
        PaperButton.interactable = false;
        RockButtonSelectImage.SetActive(true);
        RSPChoice(2);
    }

    public void ScissorsClick()
    {
        this.GetComponent<AudioSource>().clip = ButtonSound;
        this.GetComponent<AudioSource>().Play();
        RockScissorsPaperTimer.SetActive(false);
        RockButton.interactable = false;
        PaperButton.interactable = false;
        ScissorsButtonSelectImage.SetActive(true);
        RSPChoice(1);
    }

    public void PaperClick()
    {
        this.GetComponent<AudioSource>().clip = ButtonSound;
        this.GetComponent<AudioSource>().Play();
        RockScissorsPaperTimer.SetActive(false);
        ScissorsButton.interactable = false;
        RockButton.interactable = false;
        PaperButtonSelectImage.SetActive(true);
        RSPChoice(3);
    }

    public void RSPChoice(int Choice)
    {
        Player1Choice = Choice;
        StartCoroutine(RSPResultShow());
    }

    IEnumerator RSPResultShow()
    {
        int Player2Choice = Random.Range(1, 4);
        yield return new WaitForSeconds(1.0f);
        this.GetComponent<AudioSource>().clip = ResultSound;
        this.GetComponent<AudioSource>().Play();
        var EA = GameObject.Find("BoardEffect").GetComponent<EffectApply_AI>();
        yield return new WaitForSeconds(0.5f);
        RockScissorsPaperGameStartPanel.SetActive(false);
        Player1ChoiceImage.sprite = RSPChoiceImage[Player1Choice];
        Player2ChoiceImage.sprite = RSPChoiceImage[Player2Choice];

        int ResultValue = Player1Choice - Player2Choice;
        if ((ResultValue == 1) || (ResultValue == -2)) //사용자가 이긴 경우
        {
            RockScissorsPaperGameResultPanelImage.GetComponent<Image>().sprite = RSPResultPanelImages[0];
            RockScissorsPaperGameResultPanel.SetActive(true);
            WinResultImageObject.SetActive(true);
            yield return new WaitForSeconds(0.7f);
            WinResultImageObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            WinResultImageObject.SetActive(true);
            yield return new WaitForSeconds(0.7f);
            WinResultImageObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            WinResultImageObject.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            RockScissorsPaperGameResultPanel.SetActive(false);
            EffectApply_AI.MoneyEffectApplyingGoingOn = true;
            StartCoroutine(EA.MoneyChangeApply(2, -100000, MovementSystem_AI.Player2CurrentBoard));
        }
        else if (ResultValue == 0) //사용자와 AI가 비긴 경우
        {
            RockScissorsPaperGameResultPanelImage.GetComponent<Image>().sprite = RSPResultPanelImages[1];
            RockScissorsPaperGameResultPanel.SetActive(true);
            DrawResultImageObject.SetActive(true);
            yield return new WaitForSeconds(0.7f);
            DrawResultImageObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            DrawResultImageObject.SetActive(true);
            yield return new WaitForSeconds(0.7f);
            DrawResultImageObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            DrawResultImageObject.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            RockScissorsPaperGameResultPanel.SetActive(false);
            EffectApply_AI.MoneyEffectApplyingGoingOn = true;
            StartCoroutine(EA.MoneyChangeApply(1, -50000, MovementSystem_AI.Player1CurrentBoard));
            StartCoroutine(EA.MoneyChangeApply(2, -50000, MovementSystem_AI.Player2CurrentBoard));
        }
        else //사용자가 진 경우
        {
            RockScissorsPaperGameResultPanelImage.GetComponent<Image>().sprite = RSPResultPanelImages[2];
            RockScissorsPaperGameResultPanel.SetActive(true);
            LoseResutlImageObject.SetActive(true);
            yield return new WaitForSeconds(0.7f);
            LoseResutlImageObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            LoseResutlImageObject.SetActive(true);
            yield return new WaitForSeconds(0.7f);
            LoseResutlImageObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            LoseResutlImageObject.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            RockScissorsPaperGameResultPanel.SetActive(false);
            EffectApply_AI.MoneyEffectApplyingGoingOn = true;
            StartCoroutine(EA.MoneyChangeApply(1, -100000, MovementSystem_AI.Player1CurrentBoard));
        }

        while (EffectApply_AI.MoneyEffectApplyingGoingOn == true)
            yield return null;

        RSPGoingOn = false;
        RockButton.interactable = true;
        ScissorsButton.interactable = true;
        PaperButton.interactable = true;
        RockButtonSelectImage.SetActive(false);
        PaperButtonSelectImage.SetActive(false);
        ScissorsButtonSelectImage.SetActive(false);
        WinResultImageObject.SetActive(false);
        LoseResutlImageObject.SetActive(false);
        DrawResultImageObject.SetActive(false);
        Player1Choice = 0;
    }
}
