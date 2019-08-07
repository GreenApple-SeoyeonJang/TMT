using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginVolumeManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        //GameObject.Find("LoginButton").GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("effectvol", 1f);

        GameObject.Find("Main Camera").GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("backvol", 0.3f);
    }
}
