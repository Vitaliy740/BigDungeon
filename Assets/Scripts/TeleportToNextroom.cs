using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToNextroom : MonoBehaviour
{
    public Transform nextroom;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
            collision.gameObject.transform.position = nextroom.position;
    }
}
