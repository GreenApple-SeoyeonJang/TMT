using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageButtonMove : MonoBehaviour {
    float t = 0f;
    float x;
    public GameObject NextPageButton1, NextPageButton2;

    private void Start()
    {
        x = NextPageButton1.transform.position.x;
    }
    void Update () {
        t += Time.deltaTime;
        if (t <= 0.25f)
        {
            NextPageButton1.transform.Translate(Vector2.up * 30f * Time.deltaTime);
            NextPageButton2.transform.Translate(Vector2.up * 30f * Time.deltaTime);
        }
            
        else if (t > 0.7f && t <= 0.95f)
        {
            NextPageButton1.transform.Translate(Vector2.down * 30f * Time.deltaTime);
            NextPageButton2.transform.Translate(Vector2.down * 30f * Time.deltaTime);
        }
        else if (t > 1.1f)
        {
            t = 0f;
            NextPageButton1.transform.position = new Vector3(x, NextPageButton1.transform.position.y, NextPageButton1.transform.position.z);
            NextPageButton2.transform.position = new Vector3(x, NextPageButton2.transform.position.y, NextPageButton2.transform.position.z);
        }
            
            
    }
}
