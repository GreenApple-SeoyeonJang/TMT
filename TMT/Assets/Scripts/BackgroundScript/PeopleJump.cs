using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleJump : MonoBehaviour {
    public float RandomJumpSpeed;
    public float RandomOntheFloorTime;
    public bool IsGoingUp;
    public bool NumeratorGoingOn;

	void Start () {
        RandomJumpSpeed = Random.Range(1.5f, 4f);
        RandomOntheFloorTime = Random.Range(2.0f, 8.0f);
        IsGoingUp = true;
        NumeratorGoingOn = false;
	}
	

	void Update () {

		if((IsGoingUp == true) && (NumeratorGoingOn == false))
        {
            NumeratorGoingOn = true;
            StartCoroutine(GoUp());
        }
        else if ((IsGoingUp == false) && (NumeratorGoingOn == false))
        {
            NumeratorGoingOn = true;
            StartCoroutine(GoDown());
        }
	}

    IEnumerator GoUp()
    {
        while(this.transform.position.y <= 0.9f)
        {
            this.transform.position = new Vector3(this.transform.position.x, (this.transform.position.y + (RandomJumpSpeed * 0.02f)), this.transform.position.z);
            yield return 0.1f;
        }
        yield return new WaitForSeconds(0.08f);
        IsGoingUp = false;
        NumeratorGoingOn = false;
    }

    IEnumerator GoDown()
    {
        while (this.transform.position.y >= 0.05f)
        {
            this.transform.position = new Vector3(this.transform.position.x, (this.transform.position.y - (RandomJumpSpeed * 0.02f)), this.transform.position.z);
            yield return 0.1f;
        }
        yield return new WaitForSeconds(RandomOntheFloorTime);
        IsGoingUp = true;
        NumeratorGoingOn = false;
    }
}
