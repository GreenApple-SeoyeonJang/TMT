using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogJump : MonoBehaviour {
    public float JumpSpeed;
    public bool IsGoingUp;
    public bool NumeratorGoingOn;
    public bool DoubleJumpCheck;

    void Start () {
        JumpSpeed = 0.4f;
        IsGoingUp = true;
        NumeratorGoingOn = false;
        DoubleJumpCheck = false;
    }
	
	// Update is called once per frame
	void Update () {
        if ((IsGoingUp == true) && (NumeratorGoingOn == false))
        {
            NumeratorGoingOn = true;
            StartCoroutine(GoUp());
        }
        else if ((IsGoingUp == false) && (NumeratorGoingOn == false) && (DoubleJumpCheck == false))
        {
            DoubleJumpCheck = true;
            NumeratorGoingOn = true;
            StartCoroutine(GoDownFirst());
        }
        else if ((IsGoingUp == false) && (NumeratorGoingOn == false) && (DoubleJumpCheck == true))
        {
            DoubleJumpCheck = false;
            NumeratorGoingOn = true;
            StartCoroutine(GoDownSecond());
        }
    }

    IEnumerator GoUp()
    {
        while (this.transform.position.y <= 0.3f)
        {
            this.transform.position = new Vector3(this.transform.position.x, (this.transform.position.y + (JumpSpeed * 0.1f)), this.transform.position.z);
            yield return 0.1f;
        }
        yield return new WaitForSeconds(0.08f);
        IsGoingUp = false;
        NumeratorGoingOn = false;
    }

    IEnumerator GoDownFirst()
    {
        while (this.transform.position.y >= -0.2f)
        {
            this.transform.position = new Vector3(this.transform.position.x, (this.transform.position.y - (JumpSpeed * 0.08f)), this.transform.position.z);
            yield return 0.1f;
        }
        IsGoingUp = true;
        NumeratorGoingOn = false;
    }
    IEnumerator GoDownSecond()
    {
        while (this.transform.position.y >= -0.2f)
        {
            this.transform.position = new Vector3(this.transform.position.x, (this.transform.position.y - (JumpSpeed * 0.08f)), this.transform.position.z);
            yield return 0.1f;
        }
        yield return new WaitForSeconds(1.5f);
        IsGoingUp = true;
        NumeratorGoingOn = false;
    }
}
