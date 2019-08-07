using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneLifePerson : MonoBehaviour {
    public TextMesh OneLife;
    public float Speed;
    public float t;

    void Start () {
        Speed = Random.Range(0.38f, 0.65f);
    }
	
	void Update () {
        t += Time.deltaTime;
        if ((t >= 0f) && (t <= Speed))
            OneLife.text = "인";
        else if ((t >= 0f) && (t <= Speed * 2))
            OneLife.text = "인생";
        else if ((t >= 0f) && (t <= Speed * 3))
            OneLife.text = "인생한";
        else if ((t >= 0f) && (t <= Speed * 4))
            OneLife.text = "인생한방";
        else if ((t >= 0f) && (t <= Speed * 5))
            OneLife.text = "인생한방?";
        else if ((t >= 0f) && (t <= Speed * 7))
            OneLife.text = "";
        else if ((t >= 0f) && (t <= Speed * 8))
            OneLife.text = "배";
        else if ((t >= 0f) && (t <= Speed * 9))
            OneLife.text = "배고";
        else if ((t >= 0f) && (t <= Speed * 10))
            OneLife.text = "배고프";
        else if ((t >= 0f) && (t <= Speed * 11))
            OneLife.text = "배고프다";
        else if ((t >= 0f) && (t <= Speed * 12))
            OneLife.text = "배고프다.";
        else if ((t >= 0f) && (t <= Speed * 13))
            OneLife.text = "배고프다..";
        else if ((t >= 0f) && (t <= Speed * 14))
            OneLife.text = "배고프다...";
        else if ((t >= 0f) && (t >= Speed * 16))
        {
            OneLife.text = "";
            t = -1.0f;
        }
    }
}
