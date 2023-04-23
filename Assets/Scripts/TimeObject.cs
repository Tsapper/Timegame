using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeObject : MonoBehaviour
{
    public GameObject presentObject;
    public GameObject futureObject;

    Vector3 lastPresentPosition;

    private void Start()
    {
        lastPresentPosition = presentObject.transform.position;
    }

    private void Update()
    {
        if (Vector3.Distance(lastPresentPosition, presentObject.transform.position) >= 0.05f)
        {
            futureObject.transform.SetParent(transform, true);
            futureObject.transform.position = presentObject.transform.position + new Vector3(0, 30f, 0);
        }
    }

    public void ToFuture()
    {
        lastPresentPosition = presentObject.transform.position;
    }

    public void ToPresent()
    {
        lastPresentPosition = presentObject.transform.position;
    }
}
