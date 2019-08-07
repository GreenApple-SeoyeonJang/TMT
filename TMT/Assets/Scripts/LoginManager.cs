using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon;

public class LoginManager : Photon.PunBehaviour {

    public InputField userId;
    public AudioClip OkButtonClickSound;

    void Awake()
    {
        Screen.SetResolution(1280, 720, false);

        if (PlayerPrefs.HasKey("USER_ID"))
        {
            userId.text = PlayerPrefs.GetString("USER_ID");

            PhotonNetwork.player.NickName = userId.text;
            PlayerPrefs.SetString("USER_ID", userId.text);

            SceneManager.LoadScene("MainMenu");
        }
    }

    // Use this for initialization
    void Start () {
        userId.text = GetUserId();
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

    public void OnClickLoginButton()
    {
        this.GetComponent<AudioSource>().clip = OkButtonClickSound;
        this.GetComponent<AudioSource>().Play();
        PhotonNetwork.player.NickName = userId.text;
        PlayerPrefs.SetString("USER_ID", userId.text);

        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
