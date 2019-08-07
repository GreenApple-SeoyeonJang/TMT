using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReadyAgainButton : MonoBehaviour {

    private void Awake()
    {
        Screen.SetResolution(1280, 720, true);
    }

    void Start () {
		
	}


    public void ReadyButtonClick()
    {
        Debug.Log("Back To Ready Room");

        StartCoroutine(this.LoadGameField());
    }

    IEnumerator LoadGameField()
    {
        PhotonNetwork.isMessageQueueRunning = false;
        AsyncOperation ao = SceneManager.LoadSceneAsync("Ready");
        yield return ao;
    }

    void Update () {
		
	}
}
