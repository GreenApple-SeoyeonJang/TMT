using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoardLocationArrow : MonoBehaviour {

    public float Speed = 0.035f;
    public bool IsGoingUp;
    public bool NumeratorGoingOn;

    void Start()
    {
        Speed = 3.2f;
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
            this.transform.position = new Vector3(this.transform.position.x, (this.transform.position.y + (Speed * 0.012f)), this.transform.position.z);
            yield return 0.1f;
        }
        yield return new WaitForSeconds(0.4f);
        IsGoingUp = false;
        NumeratorGoingOn = false;
    }

    public IEnumerator GoDown()
    {
        while (this.transform.position.y >= 0f)
        {
            this.transform.position = new Vector3(this.transform.position.x, (this.transform.position.y - (Speed * 0.01f)), this.transform.position.z);
            yield return 0.1f;
        }
        yield return new WaitForSeconds(0.4f);
        IsGoingUp = true;
        NumeratorGoingOn = false;
    }
}
