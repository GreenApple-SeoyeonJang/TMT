using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TirednessSystem_AI : MonoBehaviour
{
    public static int Player1Health;
    public static int Player2Health;
    public Slider Player1HealthSlider;
    public Slider Player2HealthSlider;

    void Start()
    {
        Player1Health = 300;
        Player2Health = 300;
        Player1HealthSlider.value = 300;
        Player2HealthSlider.value = 300;
    }

    void Update()
    {
        if (Player1Health < 0)
            Player1Health = 0;
        if (Player2Health < 0)
            Player2Health = 0;
        if (Player1Health >= 600)
            Player1Health = 600;
        if (Player2Health >= 600)
            Player2Health = 600;

        Player1HealthSlider.value = Player1Health;
        Player2HealthSlider.value = Player2Health;
    }
}
