using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpPerson : MonoBehaviour {
    public TextMesh PowerUpText;
    public float t;
    public float t2;
    public float Speed = 2f;
    public Vector3 FirstLocation;
    public Vector3 FrontPersonLocation;

    void Start () {
        Speed = 2f;
        t = 0f;
        t2 = 0f;
        FirstLocation = this.transform.position;
        FrontPersonLocation = GameObject.Find("OrangeRedPerson").transform.position;
	}
	

	void Update () {
        t += Time.deltaTime;
        t2 += Time.deltaTime;

        if ((t2 >= 4f) && (t <= 8f))
        {
            StartCoroutine(PowerUpTextF());
            t2 = 0f;
        }

        if (t <= 4f)
            this.transform.position = Vector3.MoveTowards(this.transform.position, FrontPersonLocation, 0.009f);
        else if ((t >= 6f) && (t <= 7.3f))
        {
            this.transform.Rotate(Vector3.down, 2.45f, Space.World);
            PowerUpTextF();
        }
        else if ((t >= 9f) &&(t <= 13f))
        {
            this.transform.rotation = Quaternion.Euler(0, -180, 0);
            this.transform.position = Vector3.MoveTowards(this.transform.position, FirstLocation, 0.009f);
        }
        else if ((t >= 14f) && (t <= 15.3f))
        {
            this.transform.Rotate(Vector3.up, 2.45f, Space.World);
        }
        else if ((t >= 16f) &&  (t <= 17f))
        {
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
            this.transform.Rotate(new Vector3(0, 0, 0), Space.World);
            t = 0f;
        }
    }

    IEnumerator PowerUpTextF()
    {
        PowerUpText.text = "힘";
        yield return new WaitForSeconds(0.4f);
        PowerUpText.text = "힘내";
        yield return new WaitForSeconds(0.4f);
        PowerUpText.text = "힘내라";
        yield return new WaitForSeconds(0.4f);
        PowerUpText.text = "힘내라 힘";
        yield return new WaitForSeconds(0.4f);
        PowerUpText.text = "힘내라 힘!";
        yield return new WaitForSeconds(0.4f);
        PowerUpText.text = "힘내라 힘!!";
        yield return new WaitForSeconds(1f);
        PowerUpText.text = "";
    }
}
