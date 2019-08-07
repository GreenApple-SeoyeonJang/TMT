using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : Photon.PunBehaviour {
    public Text UserNameText;
    public GameObject[] People = new GameObject[9];
    public GameObject MyCharacter;
    public GameObject[] CharacterChoices = new GameObject[10];
    public GameObject[] CharacterChoiceBox = new GameObject[10];
    public GameObject OptionPanel;
    public GameObject PanelQuitGame;
    public AudioClip OkButtonSound;
    public AudioClip CharacterChoiceButtonSound;
    public GameObject Tutorial1, Tutorial2, Tutorial3;

    private void Awake()
    {
        Screen.SetResolution(1280, 720, false);
    }

    void Start () {
        for (int i = 1; i < 10; i++)
        {
            CharacterChoices[i].SetActive(false);
            CharacterChoiceBox[i].SetActive(false);
        }
        for (int i = 0; i < 9; i++)
            People[i].SetActive(true);
        
        MyCharacter = CharacterChoices[0];
        OptionPanel.SetActive(false);
        PanelQuitGame.SetActive(false);
        Tutorial1.SetActive(false);
        Tutorial2.SetActive(false);
        Tutorial3.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        UserNameText.text = PhotonNetwork.playerName;
        MyCharacter.transform.Rotate(Vector3.up, 0.5f, Space.World);
        CharacterChoiceBox[PlayerPrefs.GetInt("MyCharacter", 0)].SetActive(true);
        MyCharacter = CharacterChoices[PlayerPrefs.GetInt("MyCharacter", 0)];

        if(Application.platform == RuntimePlatform.Android)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                PanelQuitGame.SetActive(true);
                for (int i = 0; i < 9; i++)
                    People[i].SetActive(false);
                MyCharacter.SetActive(false);
            }
        }

        for (int i = 0; i < 10; i++)
        {
            if ((PlayerPrefs.GetInt("MyCharacter", 0) == i))
            {
                if(OptionPanel.activeSelf == false)
                    if(PanelQuitGame.activeSelf == false)
                        if(Tutorial1.activeSelf == false)
                            if(Tutorial2.activeSelf == false)
                                if(Tutorial3.activeSelf == false)
                                    CharacterChoices[i].SetActive(true);
                CharacterChoiceBox[i].SetActive(true);
                MyCharacter = CharacterChoices[i];
            }
            else
            {
                CharacterChoices[i].SetActive(false);
                CharacterChoiceBox[i].SetActive(false);
            } 
        }
	}

    public void OnClick_QutiYesButton()
    {
        this.GetComponent<AudioSource>().clip = OkButtonSound;
        this.GetComponent<AudioSource>().Play();
        Application.Quit();
    }

    public void OnClick_QuitNoButton()
    {
        this.GetComponent<AudioSource>().clip = OkButtonSound;
        this.GetComponent<AudioSource>().Play();
        PanelQuitGame.SetActive(false);
        for (int i = 0; i < 9; i++)
        {
            People[i].SetActive(true);
        }
        MyCharacter.SetActive(true);
    }

    public void OnClick_MultiGameStart()
    {
        this.GetComponent<AudioSource>().clip = OkButtonSound;
        this.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("Lobby");
    }

    public void OnClick_AIGameStart()
    {
        this.GetComponent<AudioSource>().clip = OkButtonSound;
        this.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("Game(AI)");
    }

    public void OnClick_Tutorial()
    {
        this.GetComponent<AudioSource>().clip = OkButtonSound;
        this.GetComponent<AudioSource>().Play();
        Tutorial1.SetActive(true);
        for (int i = 0; i < 9; i++)
            People[i].SetActive(false);
        MyCharacter.SetActive(false);
    }

    public void OnClick_TutorialBackButton()
    {
        this.GetComponent<AudioSource>().clip = OkButtonSound;
        this.GetComponent<AudioSource>().Play();
        Tutorial1.SetActive(false);
        Tutorial2.SetActive(false);
        Tutorial3.SetActive(false);
        for (int i = 0; i < 9; i++)
            People[i].SetActive(true);
        MyCharacter.SetActive(true);
    }

    public void OnClick_Tutorial_NextPage1Button()
    {
        this.GetComponent<AudioSource>().clip = CharacterChoiceButtonSound;
        this.GetComponent<AudioSource>().Play();
        Tutorial1.SetActive(false);
        Tutorial2.SetActive(true);
    }
    public void OnClick_Tutorial_NextPage2Button()
    {
        this.GetComponent<AudioSource>().clip = CharacterChoiceButtonSound;
        this.GetComponent<AudioSource>().Play();
        Tutorial2.SetActive(false);
        Tutorial3.SetActive(true);
    }
    public void OnClick_Tutorial_PreviousPage1Button()
    {
        this.GetComponent<AudioSource>().clip = CharacterChoiceButtonSound;
        this.GetComponent<AudioSource>().Play();
        Tutorial2.SetActive(false);
        Tutorial1.SetActive(true);
    }
    public void OnClick_Tutorial_PreviousPage2Button()
    {
        this.GetComponent<AudioSource>().clip = CharacterChoiceButtonSound;
        this.GetComponent<AudioSource>().Play();
        Tutorial3.SetActive(false);
        Tutorial2.SetActive(true);
    }

    public void OnClick_Option()
    {
        this.GetComponent<AudioSource>().clip = OkButtonSound;
        this.GetComponent<AudioSource>().Play();
        for (int i = 0; i < 9; i++)
            People[i].SetActive(false);
        MyCharacter.SetActive(false);
        OptionPanel.SetActive(true);
    }
    public void OptionBackButton()
    {
        this.GetComponent<AudioSource>().clip = OkButtonSound;
        this.GetComponent<AudioSource>().Play();
        for (int i = 0; i < 9; i++)
        {
            People[i].SetActive(true);
        }
            
        MyCharacter.SetActive(true);
        OptionPanel.SetActive(false);
    }

    public void Character0ButtonClick()
    {
        this.GetComponent<AudioSource>().clip = CharacterChoiceButtonSound;
        this.GetComponent<AudioSource>().Play();
        PlayerPrefs.SetInt("MyCharacter", 0);
    }
    public void Character1ButtonClick()
    {
        this.GetComponent<AudioSource>().clip = CharacterChoiceButtonSound;
        this.GetComponent<AudioSource>().Play();
        PlayerPrefs.SetInt("MyCharacter", 1);
    }
    public void Character2ButtonClick()
    {
        this.GetComponent<AudioSource>().clip = CharacterChoiceButtonSound;
        this.GetComponent<AudioSource>().Play();
        PlayerPrefs.SetInt("MyCharacter", 2);
    }
    public void Character3ButtonClick()
    {
        this.GetComponent<AudioSource>().clip = CharacterChoiceButtonSound;
        this.GetComponent<AudioSource>().Play();
        PlayerPrefs.SetInt("MyCharacter", 3);
    }
    public void Character4ButtonClick()
    {
        this.GetComponent<AudioSource>().clip = CharacterChoiceButtonSound;
        this.GetComponent<AudioSource>().Play();
        PlayerPrefs.SetInt("MyCharacter", 4);
    }
    public void Character5ButtonClick()
    {
        this.GetComponent<AudioSource>().clip = CharacterChoiceButtonSound;
        this.GetComponent<AudioSource>().Play();
        PlayerPrefs.SetInt("MyCharacter", 5);
    }
    public void Character6ButtonClick()
    {
        this.GetComponent<AudioSource>().clip = CharacterChoiceButtonSound;
        this.GetComponent<AudioSource>().Play();
        PlayerPrefs.SetInt("MyCharacter", 6);
    }
    public void Character7ButtonClick()
    {
        this.GetComponent<AudioSource>().clip = CharacterChoiceButtonSound;
        this.GetComponent<AudioSource>().Play();
        PlayerPrefs.SetInt("MyCharacter", 7);
    }
    public void Character8ButtonClick()
    {
        this.GetComponent<AudioSource>().clip = CharacterChoiceButtonSound;
        this.GetComponent<AudioSource>().Play();
        PlayerPrefs.SetInt("MyCharacter", 8);
    }
    public void Character9ButtonClick()
    {
        this.GetComponent<AudioSource>().clip = CharacterChoiceButtonSound;
        this.GetComponent<AudioSource>().Play();
        PlayerPrefs.SetInt("MyCharacter", 9);
    }
}
