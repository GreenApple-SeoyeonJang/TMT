using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkHardText : MonoBehaviour {
    public TextMesh PoliceWorkHardText;
    public float Speed;
    public float t;

    void Start () {
        Speed = Random.Range(0.38f, 0.65f);
    }
	
	// Update is called once per frame
	void Update () {
        t += Time.deltaTime;
        if ((t >= 0f) && (t <= Speed))
            PoliceWorkHardText.text = "착";
        else if ((t >= 0f) && (t <= Speed * 2))
            PoliceWorkHardText.text = "착하";
        else if ((t >= 0f) && (t <= Speed * 3))
            PoliceWorkHardText.text = "착하게 ";
        else if ((t >= 0f) && (t <= Speed * 4))
            PoliceWorkHardText.text = "착하게 살";
        else if ((t >= 0f) && (t <= Speed * 5))
            PoliceWorkHardText.text = "착하게 살자";
        else if ((t >= 0f) && (t <= Speed * 7))
            PoliceWorkHardText.text = "착하게 살자!";
        else if ((t >= 0f) && (t <= Speed * 9))
            PoliceWorkHardText.text = "";
        else if ((t >= 0f) && (t <= Speed * 10))
            PoliceWorkHardText.text = "열";
        else if ((t >= 0f) && (t <= Speed * 11))
            PoliceWorkHardText.text = "열심";
        else if ((t >= 0f) && (t <= Speed * 12))
            PoliceWorkHardText.text = "열심히 ";
        else if ((t >= 0f) && (t <= Speed * 13))
            PoliceWorkHardText.text = "열심히 살";
        else if ((t >= 0f) && (t <= Speed * 14))
            PoliceWorkHardText.text = "열심히 살자";
        else if ((t >= 0f) && (t <= Speed * 15))
            PoliceWorkHardText.text = "열심히 살자!";

        else if ((t >= 0f) && (t >= Speed * 18))
        {
            PoliceWorkHardText.text = "";
            t = -1.0f;
        }
    }
}
