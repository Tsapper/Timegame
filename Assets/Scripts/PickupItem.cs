using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [SerializeField] float distance = 1.2f;
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
    }

    private void PickUp()
    {
        if (((Vector2)Player.GetPlayer().transform.position - (Vector2)transform.position).magnitude <= distance)
        {
            if (!pickedUp)
            {
                pickedUp = true;
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                transform.SetParent(Player.GetPlayer().transform, false);
                transform.localPosition = new Vector3(-1.1f, 0.25f, 0f);
            }
            else
            {
                pickedUp = false;
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                transform.SetParent(originalParent, true);
            }
            
        }
    }
}
