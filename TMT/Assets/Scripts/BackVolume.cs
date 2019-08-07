using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackVolume : MonoBehaviour { 
    public Slider backVolume;

    private float backVol = 0.3f;

    // Use this for initialization
    void Start()
    {
        backVol = PlayerPrefs.GetFloat("backvol", 0.3f);
        backVolume.value = backVol;
        GameObject.Find("Main Camera").GetComponent<AudioSource>().volume = backVolume.value;
    }

    // Update is called once per frame
    void Update()
    {
        SoundSlider();
    }

    public void SoundSlider()
    {
        GameObject.Find("Main Camera").GetComponent<AudioSource>().volume = backVolume.value;
        backVol = backVolume.value;
        PlayerPrefs.SetFloat("backvol", backVol);
    }
}
