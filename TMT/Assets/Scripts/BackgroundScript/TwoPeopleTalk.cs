using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwoPeopleTalk : MonoBehaviour {
    public TextMesh Person1;
    public TextMesh Person2;
    public int RandomPrice;
    public float t;
    public float Speed;

    void Start () {
        RandomPrice = Random.Range(1, 15);
        Speed = 2f;
        Person1.text = "";
        Person2.text = "";
	}
	

	void Update () {
        t += Time.deltaTime;
        if ((t >= 0f) && (t <= Speed))
            Person1.text = "얼마에요?";
        else if ((t >= 0f) && (t <= Speed * 2))
            Person2.text = RandomPrice.ToString() + "만원이요";
        else if ((t >= 0f) && (t <= Speed * 2.5))
            Person1.text = "...";
        else if ((t >= 0f) && (t <= Speed * 3))
            Person2.text = "...";
        else if ((t >= 0f) && (t <= Speed * 4))
            Person1.text = "";
        else if ((t >= 0f) && (t <= Speed * 4.2))
            Person2.text = "";
        else if ((t >= 0f) && (t >= Speed * 6))
        {
            RandomPrice = Random.Range(1, 15);
            t = -1.0f;
        }
    }
}
