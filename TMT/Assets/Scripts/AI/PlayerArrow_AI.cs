using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrow_AI : MonoBehaviour
{
    public GameObject Turn1PlayerArrow;
    public GameObject Turn2PlayerArrow;
    //사용자 플레이어가 빨간 화살표, AI가 파랑

    void Start()
    {
        Turn1PlayerArrow = GameObject.Find("Player1/Arrows/Arrow1");
        Turn2PlayerArrow = GameObject.Find("Player2/Arrows/Arrow2");
    }

    void Update()
    {
        if (TurnSystem_AI.CurrentTurnNumber == TurnSystem_AI.Player1TurnNumber)
        {
            if (Turn1PlayerArrow != null)
                Turn1PlayerArrow.SetActive(true);
            if (Turn2PlayerArrow != null)
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
