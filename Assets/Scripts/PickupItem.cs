using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [SerializeField] float distance = 1.2f;
    [SerializeField] bool presentObject;
    private bool pickedUp;
    private Transform originalParent;

    private void Start()
    {
        originalParent = transform.parent;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }

        if (pickedUp && presentObject && !TimeManager.present)
        {
            pickedUp = false;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            transform.SetParent(originalParent, true);
            transform.position += new Vector3(0, -30f, 0);
            GetComponentInParent<TimeObject>().ToPresent();
        }

        if (pickedUp && Player.GetPlayer().GetComponent<Rigidbody2D>().velocity.x > 0)
        {
            transform.localPosition = new Vector3(1.1f, 0.25f, 0f);
        }
        if (pickedUp && Player.GetPlayer().GetComponent<Rigidbody2D>().velocity.x < 0)
        {
            transform.localPosition = new Vector3(-1.1f, 0.25f, 0f);
        }
    }

    private void PickUp()
    {
        if (((Vector2)Player.GetPlayer().transform.position - (Vector2)transform.position).magnitude <= distance)
        {
            if (!pickedUp)
            {
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                transform.SetParent(Player.GetPlayer().transform, false);
                transform.localPosition = new Vector3(-1.1f, 0.25f, 0f);
                originalParent.GetComponent<TimeObject>().ToPresent();
            }
            if (pickedUp)
            {
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                transform.SetParent(originalParent, true);
            }
            pickedUp = !pickedUp;
        }
    }

    public void DropItem()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        transform.SetParent(originalParent, true);
        pickedUp = false;
    }
}
