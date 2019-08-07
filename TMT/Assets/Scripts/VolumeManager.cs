using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeManager : MonoBehaviour {

    public int musicNum;
    public AudioClip[] intro_music;
    AudioSource soundSource;

    // Use this for initialization
    void Start()
    {
        musicNum = 0;
        soundSource = GetComponent<AudioSource>();

        StartCoroutine("Playlist");
    }

    IEnumerator Playlist()
    {
        soundSource.clip = intro_music[0];
        soundSource.Play();
        while(true)
        {
            yield return new WaitForSeconds(1.0f);

            if(!soundSource.isPlaying)
            {
                if(musicNum == 0)
                {
                    soundSource.clip = intro_music[1];
                    musicNum = 1;
                }
                else if(musicNum == 1)
                {
                    soundSource.clip = intro_music[0];
                    musicNum = 0;
                }

                soundSource.Play();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*GameObject.Find("BoardEffect").GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("effectvol", 1f);
        GameObject.Find("HospitalSystem").GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("effectvol", 1f);
        GameObject.Find("GameOverManager").GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("effectvol", 1f);
        GameObject.Find("TurnSystem").GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("effectvol", 1f);
        GameObject.Find("MovementSystem").GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("effectvol", 1f);*/

        GameObject.Find("Main Camera").GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("backvol", 0.3f);
    }
}
