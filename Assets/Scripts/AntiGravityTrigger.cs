using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiGravityTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered antigrav zone");
        other.gameObject.GetComponent<Player>()?.SetIsInAntiGravZone(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        other.gameObject.GetComponent<Player>()?.SetIsInAntiGravZone(false);
    }
}
