using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Collider2D collider2D;

    public void Open(float t)
    {
        if (spriteRenderer.enabled == false) return;
        StartCoroutine(TimedOpen(t));
    }

    private IEnumerator TimedOpen(float t)
    {
        yield return new WaitForSeconds(t);
        collider2D.enabled = false;
        spriteRenderer.enabled = false;
    }

    public void Close(float t)
    {
        if (spriteRenderer.enabled == true) return;
        StartCoroutine(TimedClose(t));
    }

    private IEnumerator TimedClose(float t)
    {
        yield return new WaitForSeconds(t);
        collider2D.enabled = true;
        spriteRenderer.enabled = true;
    }
}
