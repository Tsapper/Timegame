using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoBox : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<TimeObject>())
        {
            TimeObject timeObject = collision.GetComponentInParent<TimeObject>();
            collision.GetComponent<PickupItem>().DropItem();
            if (collision.gameObject == timeObject.presentObject)
            {
                timeObject.Respawn();
            }
            else if (collision.gameObject == timeObject.futureObject)
            {
                timeObject.FutureRespawn();
            }
        }

        if (collision.GetComponent<Player>())
        {
            if (collision.GetComponentInChildren<PickupItem>())
            {
                PickupItem pickupItem = collision.GetComponentInChildren<PickupItem>();
                pickupItem.DropItem();
                pickupItem.transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y, 0f);
            }
        }
    }
}
