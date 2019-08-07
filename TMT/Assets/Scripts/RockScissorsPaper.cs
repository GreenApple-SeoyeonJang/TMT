using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

public class RockScissorsPaper : Photon.MonoBehaviour {
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
    public Image ResultImage;
    private PhotonView pv;
    public Sprite[] RSPChoiceImage = new Sprite[4]; //0제외 1:가위, 2:주먹, 3:보
    public Sprite[] RSPResultPanelImages = new Sprite[3]; //0이겼을 때, 2:비겼을 떄, 2:졌을 때
    public GameObject WinResultImageObject;
    public GameObject DrawResultImageObject;
    public GameObject LoseResutlImageObject;
    public AudioClip RSPPanelPopUpSound;
    public AudioClip ButtonSound;
    public AudioClip ResultSound;
    
    public int Player1Choice;
    public int Player2Choice;
    public int WhoAmI;
    public bool RSPResultShowGoingOn;

    void Start() {
        RSPGoingOn = false;
        RSPResultShowGoingOn = false;
        pv = GetComponent<PhotonView>();
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
        Player2Choice = 0;
        if (PhotonNetwork.isMasterClient)
            WhoAmI = 1;
        else
            WhoAmI = 2;
    }

    void Update()
    {
       if((Player1Choice != 0) && (Player2Choice != 0) && (RSPResultShowGoingOn == false))
        {
            RSPResultShowGoingOn = true;
            StartCoroutine(RSPResultShow());
        }
    }

    public void RockScissorsPaperStart()
    {
        pv.RPC("ShowRockScissorsPaper", PhotonTargets.All, null);
    }

    [PunRPC]
    public void ShowRockScissorsPaper()
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
        if (RockScissorsPaperTimer.activeSelf == true)
        {
            int RandomNumber;
            RockScissorsPaperTimer.SetActive(false);
            RandomNumber = Random.Range(1, 4);
            if (RandomNumber == 1) //가위
            {
                RockButton.interactable = false;
                PaperButton.interactable = false;
                ScissorsButtonSelectImage.SetActive(true);
                pv.RPC("RSPChoice", PhotonTargets.All, WhoAmI, 1);
            }
            else if (RandomNumber == 2) //주먹
            {
                ScissorsButton.interactable = false;
                PaperButton.interactable = false;
                RockButtonSelectImage.SetActive(true);
                pv.RPC("RSPChoice", PhotonTargets.All, WhoAmI, 2);
            }
            else if (RandomNumber == 3) //보
            {
                ScissorsButton.interactable = false;
                RockButton.interactable = false;
                PaperButtonSelectImage.SetActive(true);
                pv.RPC("RSPChoice", PhotonTargets.All, WhoAmI, 3);
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
        pv.RPC("RSPChoice", PhotonTargets.All, WhoAmI, 2);
    }

    public void ScissorsClick()
    {
        this.GetComponent<AudioSource>().clip = ButtonSound;
        this.GetComponent<AudioSource>().Play();
        RockScissorsPaperTimer.SetActive(false);
        RockButton.interactable = false;
        PaperButton.interactable = false;
        ScissorsButtonSelectImage.SetActive(true);
        pv.RPC("RSPChoice", PhotonTargets.All, WhoAmI, 1);
    }

    public void PaperClick()
    {
        this.GetComponent<AudioSource>().clip = ButtonSound;
        this.GetComponent<AudioSource>().Play();
        RockScissorsPaperTimer.SetActive(false);
        ScissorsButton.interactable = false;
        RockButton.interactable = false;
        PaperButtonSelectImage.SetActive(true);
        pv.RPC("RSPChoice", PhotonTargets.All, WhoAmI, 3);
    }

    [PunRPC]
    public void RSPChoice(int Who, int Choice)
    {
        if (Who == 1)
            Player1Choice = Choice;
        else
            Player2Choice = Choice;
    }

    IEnumerator RSPResultShow()
    {
        this.GetComponent<AudioSource>().clip = ResultSound;
        this.GetComponent<AudioSource>().Play();
        var EA = GameObject.Find("BoardEffect").GetComponent<EffectApply>();
        yield return new WaitForSeconds(1.0f);
        RockScissorsPaperGameStartPanel.SetActive(false);
        Player1ChoiceImage.sprite = RSPChoiceImage[Player1Choice];
        Player2ChoiceImage.sprite = RSPChoiceImage[Player2Choice];
        if(WhoAmI == 1) //플레이어1 기준
        {
            int ResultValue = Player1Choice - Player2Choice;
            if((ResultValue == 1) || (ResultValue == -2)) //이긴 경우
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
                EffectApply.MoneyEffectApplyingGoingOn = true;
                StartCoroutine(EA.MoneyChangeApply(2, -100000, MovementSystem.MyCurrentBoard));
            }
            else if(ResultValue == 0) //비긴 경우
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
                EffectApply.MoneyEffectApplyingGoingOn = true;
                StartCoroutine(EA.MoneyChangeApply(1, -50000, MovementSystem.MyCurrentBoard));
                StartCoroutine(EA.MoneyChangeApply(2, -50000, MovementSystem.MyCurrentBoard));
            }
            else //진 경우
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
                EffectApply.MoneyEffectApplyingGoingOn = true;
                StartCoroutine(EA.MoneyChangeApply(1, -100000, MovementSystem.MyCurrentBoard));
            }
        }
        else //플레이어2 기준
        {
            int ResultValue = Player2Choice - Player1Choice;
            if ((ResultValue == 1) || (ResultValue == -2)) //이긴 경우
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
                EffectApply.MoneyEffectApplyingGoingOn = true;
                StartCoroutine(EA.MoneyChangeApply(1, -100000, MovementSystem.MyCurrentBoard));
            }
            else if (ResultValue == 0) //비긴 경우
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
                EffectApply.MoneyEffectApplyingGoingOn = true;
                StartCoroutine(EA.MoneyChangeApply(1, -50000, MovementSystem.MyCurrentBoard));
                StartCoroutine(EA.MoneyChangeApply(2, -50000, MovementSystem.MyCurrentBoard));
            }
            else //진 경우
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
                EffectApply.MoneyEffectApplyingGoingOn = true;
                StartCoroutine(EA.MoneyChangeApply(2, -100000, MovementSystem.MyCurrentBoard));
            }
        }
        while (EffectApply.MoneyEffectApplyingGoingOn == true)
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
        RSPResultShowGoingOn = false;
        Player1Choice = 0;
        Player2Choice = 0;
    }
}
