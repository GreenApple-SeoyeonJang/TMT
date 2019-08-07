using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    public bool TextBlink;
    public GameObject TitleImage;
    public AudioClip ButtonClickSound;

    private void Awake()
    {
        Screen.SetResolution(1280,720, false);
    }
    // Use this for initialization
    void Start()
    {
        TextBlink = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(TextBlink == false)
            StartCoroutine(TouchTextBlink());
    }

    IEnumerator TouchTextBlink()
    {
        TextBlink = true;
        TitleImage.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        TitleImage.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        TextBlink = false;
    }

    public void OnClick_Screen()
    {
        this.GetComponent<AudioSource>().clip = ButtonClickSound;
        this.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("Login");
    }

    /*void TouchRecognize()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began: //터치가 입력되었을 때 한 번
                    Debug.Log("TouchPhase Began!");
                    break;

                case TouchPhase.Moved: //Began 상태에서 움직일 때 계속
                    Debug.Log("TouchPhase Moved!");
                    break;

                case TouchPhase.Stationary: //Moved 상태였다가 가만히 있을 때 계속
                    Debug.Log("TouchPhase Stationary!");
                    break;

                case TouchPhase.Ended: // 화면에서 손을 뗏을 때 한 번
                    Debug.Log("TouchPhase Ended!");
                    SceneManager.LoadScene("Login");
                    break;

                case TouchPhase.Canceled: //시스템에 의해 터치가 취소됐을 때 ex)전화
                    Debug.Log("TouchPhase Canceled!");
                    break;
            }
        }
    }*/
}
