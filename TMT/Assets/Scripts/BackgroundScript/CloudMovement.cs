using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour {
    public float t;
    public Vector3 current;

    // Use this for initialization
    void Start () {
        current = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        t += Time.deltaTime;
        if (t <= 30.5f)
            this.transform.Translate(Vector3.left * 18f * Time.deltaTime);
        else
        {
            t = 0f;
            this.transform.position = current;
        }
    }
}
