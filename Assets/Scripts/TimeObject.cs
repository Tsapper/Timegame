using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeObject : MonoBehaviour
{
    public GameObject presentObject;
    public GameObject futureObject;

    Vector3 spawnPos;
    Vector3 lastPresentPosition;

    private void Start()
    {
        lastPresentPosition = presentObject.transform.position;
        spawnPos = presentObject.transform.position;
    }

    private void Update()
    {
        if (Vector3.Distance(lastPresentPosition, presentObject.transform.position) >= 0.15f)
        {
            futureObject.transform.SetParent(transform, true);
            futureObject.transform.position = presentObject.transform.position + new Vector3(0, 30f, 0);
            futureObject.GetComponent<PickupItem>().DropItem();
            lastPresentPosition = presentObject.transform.position;
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

    public void Respawn()
    {
        presentObject.transform.position = spawnPos;
    }

    public void FutureRespawn()
    {
        futureObject.transform.position = spawnPos + new Vector3(0, 30f, 0);
    }    
}
