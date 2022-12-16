using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformChecker : MonoBehaviour
{
    public Transform movingPlatformVector;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("MovingPlatform"))
        {
            movingPlatformVector = other.gameObject.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        Debug.Log("Collision exit");
        if (other.gameObject.CompareTag("MovingPlatform"))
        {
            movingPlatformVector = null;
        }
    }

    // private void OnCollisionExit2D(Collision other)
    // {
    //     Debug.Log("Collision exit");
    //     if (other.gameObject.CompareTag("MovingPlatform"))
    //     {
    //         Debug.Log("Moving platform exit");
    //         isOnMovingPlatform = false;
    //     }
    // }
}
