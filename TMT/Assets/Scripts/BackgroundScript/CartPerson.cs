using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartPerson : MonoBehaviour {
    public Vector3 FirstLocation;
    public float t;


    void Start () {
        t = 0f;
        FirstLocation = this.transform.position;
    }
	
	void Update () {
        t += Time.deltaTime;
        if ((t >= 0f) && (t <= 4f))
            this.transform.Translate(Vector3.up * 2f * Time.deltaTime);
        if (t >= 7f)
        {
            t = -3f;
            this.transform.position = FirstLocation;
        }
    }
}
