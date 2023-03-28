using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

public class NickNameManager : Photon.PunBehaviour
{
    public GameObject MyCharacter;
    public InputField userId;
    public GameObject ChangeNickNamePanel;
    public AudioClip OkButtonClickSound;

    // Use this for initialization
    void Start () {
        userId.text = GetUserId();
        ChangeNickNamePanel.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    string GetUserId()
    {
        string userId = PlayerPrefs.GetString("USER_ID");

        if (string.IsNullOrEmpty(userId))
        {
            userId = "USER_" + Random.Range(0, 999).ToString("000");
        }
        return userId;
    }

    public void OnClickChangeNickNameButton()
    {
        this.GetComponent<AudioSource>().clip = OkButtonClickSound;
        this.GetComponent<AudioSource>().Play();
        MyCharacter.SetActive(false);
        ChangeNickNamePanel.SetActive(true);
    }

    public void OnClickOkayButton()
    {
        this.GetComponent<AudioSource>().clip = OkButtonClickSound;
        this.GetComponent<AudioSource>().Play();
        PhotonNetwork.player.NickName = userId.text;
        PlayerPrefs.SetString("USER_ID", userId.text);
        MyCharacter.SetActive(true);
        ChangeNickNamePanel.SetActive(false);
    }

    public void OnClickCloseButton()
    {
        this.GetComponent<AudioSource>().clip = OkButtonClickSound;
        this.GetComponent<AudioSource>().Play();
        MyCharacter.SetActive(true);
        ChangeNickNamePanel.SetActive(false);
    }


}
