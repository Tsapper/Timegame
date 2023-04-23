using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    public UnityEvent OnEnter;
    public UnityEvent OnExit;

    private List<Collider2D> colliders = new();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!colliders.Contains(collision))
        {
            colliders.Add(collision);
        }

        if (colliders.Count == 1) OnEnter.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (colliders.Contains(collision))
        {
            colliders.Remove(collision);
        }

        if (colliders.Count == 0) OnExit.Invoke();
    }
}
