using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityTunnelTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.GetComponent<Player>()?.SetIsInGravityTunnel(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        other.gameObject.GetComponent<Player>()?.SetIsInGravityTunnel(false);
    }
}
