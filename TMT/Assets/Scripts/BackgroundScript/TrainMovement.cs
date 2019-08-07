using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMovement : MonoBehaviour {
    public float t;
    public Vector3 StartLocation;

	// Use this for initialization
	void Start () {
        StartLocation = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        t += Time.deltaTime;
        if(t <= 8f)
            this.transform.Translate(Vector3.forward * 13f * Time.deltaTime);
        if (t >= 20f){
            t = 0f;
            this.transform.position = StartLocation;
        }
	}
}
