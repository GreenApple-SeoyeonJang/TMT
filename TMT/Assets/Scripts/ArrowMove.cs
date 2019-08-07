using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMove : MonoBehaviour {
    public float Speed = 0.025f;
    public bool IsGoingUp;
    public bool NumeratorGoingOn;

    void Start()
    {
        Speed = 3f;
        IsGoingUp = true;
        NumeratorGoingOn = false;
    }

    void Update()
    {
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
        while (this.transform.position.y <= 0.25f)
        {
            this.transform.position = new Vector3(this.transform.position.x, (this.transform.position.y + (Speed * 0.015f)), this.transform.position.z);
            yield return 0.1f;
        }
        yield return new WaitForSeconds(0.4f);
        IsGoingUp = false;
        NumeratorGoingOn = false;
    }

    public IEnumerator GoDown()
    {
        while (this.transform.position.y >= -0.1f)
        {
            this.transform.position = new Vector3(this.transform.position.x, (this.transform.position.y - (Speed * 0.015f)), this.transform.position.z);
            yield return 0.1f;
        }
        yield return new WaitForSeconds(0.4f);
        IsGoingUp = true;
        NumeratorGoingOn = false;
    }
}
