using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinRotation : MonoBehaviour {
    public Sprite[] CoinImages = new Sprite[6];

	void Start () {
        StartCoroutine(CoinRotate());
	}
	
    IEnumerator CoinRotate()
    {
        for(int i = 0; i < 6; i++)
        {
            this.GetComponent<Image>().sprite = CoinImages[i];
            yield return new WaitForSeconds(0.15f);
            if (i == 5)
                i = -1;
        }
    }
}
