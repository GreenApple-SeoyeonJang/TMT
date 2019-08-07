using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrow : MonoBehaviour {
    public bool FindArrow = false;
    public GameObject Turn1PlayerArrow;
    public GameObject Turn2PlayerArrow;
    //방장 플레이어가 빨간 화살표, 방장 아닌애가 파랑

    void Start () {
        FindArrow = false;
    }

	void Update () {
        if ((FindArrow == false) && (TurnChoice.TurnChoiceGoingOn == false) )
        {
            if(TurnChoice .MasterChoice == 1)
            {
                Turn1PlayerArrow = GameObject.Find("Player1/Arrows/Arrow1");
                Turn2PlayerArrow = GameObject.Find("Player2/Arrows/Arrow2");
            }
            else
            {
                Turn2PlayerArrow = GameObject.Find("Player1/Arrows/Arrow1");
                Turn1PlayerArrow = GameObject.Find("Player2/Arrows/Arrow2");
            }
            GameObject.Find("Player1/Arrows/Arrow2").SetActive(false);
            GameObject.Find("Player2/Arrows/Arrow1").SetActive(false);
            FindArrow = true;
        }

        if (TurnSystem.CurrentTurnNumber == 1)
        {
            if(Turn1PlayerArrow != null)
                Turn1PlayerArrow.SetActive(true);
            if(Turn2PlayerArrow != null)
                Turn2PlayerArrow.SetActive(false);
        }
        else 
        {
            if (Turn1PlayerArrow != null)
                Turn1PlayerArrow.SetActive(false);
            if (Turn2PlayerArrow != null)
                Turn2PlayerArrow.SetActive(true);
        }
	}
}
