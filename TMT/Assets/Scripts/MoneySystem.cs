using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneySystem : Photon.MonoBehaviour {
    public static int MyPossessMoney;
    public static int Player1PossessMoney;
    public static int Player2PossessMoney;
    public Text Player1PossessMoneyText;
    public Text Player2PossessMoneyText;
    public Text Player1RankText;
    public Text Player2RankText;
    public int WhoAmI;

	void Start () {
        Player1PossessMoney = 1000000;
        Player2PossessMoney = 1000000;
        if (PhotonNetwork.isMasterClient)
            WhoAmI = 1;
        else
            WhoAmI = 2;
	}

	void Update () {
        Player1PossessMoneyText.text = MoneyTextConversion(Player1PossessMoney);
        Player2PossessMoneyText.text = MoneyTextConversion(Player2PossessMoney);

        if(Player1PossessMoney > Player2PossessMoney)
        {
            Player1RankText.text = "1등";
            Player2RankText.text = "2등";
        }
        else if(Player1PossessMoney < Player2PossessMoney)
        {
            Player1RankText.text = "2등";
            Player2RankText.text = "1등";
        }
	}

    public string MoneyTextConversion(int Money)
    {
        return(string.Format("{0:#,###}", Money));
    }
}
