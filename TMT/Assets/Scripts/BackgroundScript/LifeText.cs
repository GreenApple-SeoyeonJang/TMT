using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeText : MonoBehaviour {
    public TextMesh Life;
    public float Speed;
    public float t;

    void Start () {
        Speed = Random.Range(0.38f, 0.65f);
    }
	

	void Update () {
        t += Time.deltaTime;
        if ((t >= 0f) && (t <= Speed))
            Life.text = "인";
        else if ((t >= 0f) && (t <= Speed * 2))
            Life.text = "인생";
        else if ((t >= 0f) && (t <= Speed * 3))
            Life.text = "인생이";
        else if ((t >= 0f) && (t <= Speed * 4))
            Life.text = "인생이란";
        else if ((t >= 0f) && (t <= Speed * 5))
            Life.text = "인생이란.";
        else if ((t >= 0f) && (t <= Speed * 7))
            Life.text = "인생이란..";
        else if ((t >= 0f) && (t <= Speed * 8))
            Life.text = "인생이란...";
        else if ((t >= 0f) && (t >= Speed * 9))
        {
            Life.text = "";
            t = -1.0f;
        }
    }
}
