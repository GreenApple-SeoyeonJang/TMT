using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YOLOPerson : MonoBehaviour {
    public TextMesh Yolo;
    public float Speed;
    public float t;

    void Start () {
        Speed = Random.Range(0.38f, 0.65f);
    }
	

	void Update () {
        t += Time.deltaTime;
        if ((t >= 0f) && (t <= Speed))
            Yolo.text = "Y";
        else if ((t >= 0f) && (t <= Speed * 2))
            Yolo.text = "YO";
        else if ((t >= 0f) && (t <= Speed * 3))
            Yolo.text = "YOL";
        else if ((t >= 0f) && (t <= Speed * 4))
            Yolo.text = "YOLO";
        else if ((t >= 0f) && (t <= Speed * 5))
            Yolo.text = "YOLO!";
        else if ((t >= 0f) && (t <= Speed * 7))
            Yolo.text = "YOLO!!";
        else if ((t >= 0f) && (t >= Speed * 8))
        {
            Yolo.text = "";
            t = -1.0f;
        }
    }
}
