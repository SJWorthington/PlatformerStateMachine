using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotationController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void flipPlayerRotation()
    {
        _rigidbody2D.rotation += 180;
    }

    public void resetPlayerRotation()
    {
        _rigidbody2D.rotation = 0;
    }

    public void rotatePlayer(float rotation)
    {
        _rigidbody2D.rotation += rotation;
    }

    //TODO - move code from Attractable to this
    public void rotatePlayerAroundPoint()
    {
        
    }
}
