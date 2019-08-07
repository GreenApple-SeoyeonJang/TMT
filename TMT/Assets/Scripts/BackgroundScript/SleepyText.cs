using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepyText : MonoBehaviour {
    public TextMesh SleepyTextObject;
    public float Speed = 0.7f;
    public float t = 0f;

	// Use this for initialization
	void Start () {
        Speed = 0.7f;
        SleepyTextObject.text = "ZZZ...";
	}
	
	// Update is called once per frame
	void Update () {
        t += Time.deltaTime;
        if ((t >= 0f) && (t <=Speed))
            SleepyTextObject.text = "Z";
        else if ((t >= 0f) && (t <= Speed*2))
            SleepyTextObject.text = "ZZ";
        else if ((t >= 0f) && (t <= Speed * 3))
            SleepyTextObject.text = "ZZZ";
        else if ((t >= 0f) && (t <= Speed * 4))
            SleepyTextObject.text = "ZZZ.";
        else if ((t >= 0f) && (t <= Speed * 5))
            SleepyTextObject.text = "ZZZ..";
        else if ((t >= 0f) && (t <= Speed * 6))
            SleepyTextObject.text = "ZZZ...";
        else if ((t >= 0f) && (t >= Speed * 7))
        {
            SleepyTextObject.text = "";
            t = -1.0f;
        }
            
    }
}
