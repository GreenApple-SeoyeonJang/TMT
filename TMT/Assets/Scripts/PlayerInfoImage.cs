using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoImage : MonoBehaviour {

    public Sprite[] PlayerImage = new Sprite[3];

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {

        if (ReadyButtonScript.ButtonClicked == true || ReadyData.PlayerChanged == true) {

            ReadyButtonScript.ButtonClicked = false;
            ReadyData.PlayerChanged = false;

            if (PhotonNetwork.isMasterClient == true)
            {
                foreach (GameObject obj in GameObject.FindGameObjectsWithTag("PLAYER_INFO"))
                {
                    obj.GetComponent<Image>().sprite = PlayerImage[2];
                }

                if (ReadyButtonScript.HowManyReady == 1)
                    this.GetComponent<Image>().sprite = PlayerImage[1];
                else if (ReadyButtonScript.HowManyReady < 1)
                    this.GetComponent<Image>().sprite = PlayerImage[0];
            }

            else
            {
                if (ReadyButtonScript.HowManyReady == 1)
                {
                    foreach (GameObject obj in GameObject.FindGameObjectsWithTag("PLAYER_INFO"))
                    {
                        obj.GetComponent<Image>().sprite = PlayerImage[1];
                    }
                    this.GetComponent<Image>().sprite = PlayerImage[2];
                }
                else if (ReadyButtonScript.HowManyReady < 1)
                {
                    foreach (GameObject obj in GameObject.FindGameObjectsWithTag("PLAYER_INFO"))
                    {
                        obj.GetComponent<Image>().sprite = PlayerImage[0];
                    }
                    this.GetComponent<Image>().sprite = PlayerImage[2];
                }
            }
        }
    }
}
