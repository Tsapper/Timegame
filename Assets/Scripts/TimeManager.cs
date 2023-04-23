using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static bool present = true;

    private TimeObject[] timeObjects;

    private void Start()
    {
        timeObjects = FindObjectsOfType<TimeObject>();
    }

    public void ToFuture()
    {
        Camera.main.transform.position += new Vector3(0, 30f, 0);
        foreach (TimeObject timeObject in timeObjects)
        {
            present = false;
            timeObject.ToFuture();
            StartCoroutine(AudioManager.GetAudioManager().PlaySound(0, 0f));
        }
    }

    public void ToPresent()
    {
        Camera.main.transform.position += new Vector3(0, -30f, 0);
        foreach (TimeObject timeObject in timeObjects)
        {
            present = true;
            timeObject.ToPresent();
            StartCoroutine(AudioManager.GetAudioManager().PlaySound(1, 0f));
        }
    }
}
