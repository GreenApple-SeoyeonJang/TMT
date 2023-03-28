using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

public class NickNameManager : Photon.PunBehaviour
{
    public GameObject Characters;
    public InputField userId;
    public GameObject ChangeNickNamePanel;
    public AudioClip OkButtonClickSound;

    // Use this for initialization
    void Start () {
        userId.text = GetUserId();
        ChangeNickNamePanel.SetActive(true); // 처음 setActive시 안보이는 오류 있어서 미리 한번 보이게 함
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
        //Characters.SetActive(false);
        ChangeNickNamePanel.SetActive(true);
    }

    public void OnClickOkayButton()
    {
        this.GetComponent<AudioSource>().clip = OkButtonClickSound;
        this.GetComponent<AudioSource>().Play();
        PhotonNetwork.player.NickName = userId.text;
        PlayerPrefs.SetString("USER_ID", userId.text);
        Characters.SetActive(true);
        ChangeNickNamePanel.SetActive(false);
    }

    public void OnClickCloseButton()
    {
        this.GetComponent<AudioSource>().clip = OkButtonClickSound;
        this.GetComponent<AudioSource>().Play();
        Characters.SetActive(true);
        ChangeNickNamePanel.SetActive(false);
    }


}
