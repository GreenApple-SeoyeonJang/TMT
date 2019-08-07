using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayTimeManagement_AI : MonoBehaviour {
    public TextMesh LeftTimeText;
    public bool CountDownStart;
    public static bool TimeOver;

	void Start () {
        TimeOver = false;
        CountDownStart = false;
	}

    public void Update()
    {
        if((TurnChoice_AI.TurnChoiceGoingOn == false) && (CountDownStart == false))
        {
            CountDownStart = true;
            StartCoroutine(Countdown());
        }
    }
    IEnumerator Countdown()
    {
        for (int Minute = 19; Minute >= 0; Minute--)
        {
            for (int Second = 59; Second >= 0; Second--)
            {
                if (Second <= 9)
                    LeftTimeText.text = "남은시간:" + Minute.ToString() + "분0" + Second.ToString() + "초";
                else
                    LeftTimeText.text = "남은시간:" + Minute.ToString() + "분" + Second.ToString() + "초";
                yield return new WaitForSeconds(1.0f);
            }
        }
        TimeOver = true;
        //게임 오버
    }
}
