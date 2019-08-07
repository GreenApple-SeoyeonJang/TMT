using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleJump2 : MonoBehaviour {
    public float RandomJumpSpeed;
    public bool IsGoingUp;
    public bool NumeratorGoingOn;

    void Start () {
        RandomJumpSpeed = Random.Range(3f, 6.5f);
        IsGoingUp = true;
        NumeratorGoingOn = false;
    }
	
	void Update () {
        if ((IsGoingUp == true) && (NumeratorGoingOn == false))
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

    public IEnumerator GoUp()
    {
        while (this.transform.position.y <= -30f)
        {
            this.transform.position = new Vector3(this.transform.position.x, (this.transform.position.y + (RandomJumpSpeed * 0.2f)), this.transform.position.z);
            yield return 0.1f;
        }
        yield return new WaitForSeconds(0.15f);
        IsGoingUp = false;
        NumeratorGoingOn = false;
    }

    public IEnumerator GoDown()
    {
        while (this.transform.position.y >= -48.3f)
        {
            this.transform.position = new Vector3(this.transform.position.x, (this.transform.position.y - (RandomJumpSpeed * 0.2f)), this.transform.position.z);
            yield return 0.1f;
        }
        yield return new WaitForSeconds(0.07f);
        IsGoingUp = true;
        NumeratorGoingOn = false;
    }
}
