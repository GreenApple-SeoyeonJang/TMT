using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoTravelPerson : MonoBehaviour {
    public TextMesh GoTravelText;
    public float Speed;
    public float t;

    void Start () {
        Speed = Random.Range(0.38f, 0.65f);
    }
	

	void Update () {
        t += Time.deltaTime;
        if ((t >= 0f) && (t <= Speed))
            GoTravelText.text = "여";
        else if ((t >= 0f) && (t <= Speed * 2))
            GoTravelText.text = "여행";
        else if ((t >= 0f) && (t <= Speed * 3))
            GoTravelText.text = "여행을 ";
        else if ((t >= 0f) && (t <= Speed * 4))
            GoTravelText.text = "여행을 떠";
        else if ((t >= 0f) && (t <= Speed * 5))
            GoTravelText.text = "여행을 떠나";
        else if ((t >= 0f) && (t <= Speed * 6))
            GoTravelText.text = "여행을 떠나요";
        else if ((t >= 0f) && (t <= Speed * 7))
            GoTravelText.text = "여행을 떠나요~";
        else if ((t >= 0f) && (t <= Speed * 9))
            GoTravelText.text = "여행을 떠나요~♪";
        else if ((t >= 0f) && (t <= Speed * 12))
            GoTravelText.text = "";
        else if ((t >= 0f) && (t <= Speed * 13))
            GoTravelText.text = "청";
        else if ((t >= 0f) && (t <= Speed * 14))
            GoTravelText.text = "청춘";
        else if ((t >= 0f) && (t <= Speed * 15))
            GoTravelText.text = "청춘의 ";
        else if ((t >= 0f) && (t <= Speed * 16))
            GoTravelText.text = "청춘의 낭";
        else if ((t >= 0f) && (t <= Speed * 17))
            GoTravelText.text = "청춘의 낭만";
        else if ((t >= 0f) && (t <= Speed * 18))
            GoTravelText.text = "청춘의 낭만♬";
        else if ((t >= 0f) && (t >= Speed * 20))
        {
            GoTravelText.text = "";
            t = -1.0f;
        }
    }
}
