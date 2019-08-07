using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartVolumeManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        GameObject.Find("Main Camera").GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("backvol", 0.3f);
    }
}
