using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravityController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private PlayerRotationController _playerRotationController;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerRotationController = GetComponent<PlayerRotationController>();
    }

    public void FlipPlayerGravity()
    {
        if (_rigidbody2D.gravityScale < 0)
        {
            _rigidbody2D.gravityScale = Mathf.Abs(_rigidbody2D.gravityScale);
            _playerRotationController.flipPlayerRotation();
        }
        else
        {
            var gravityScale = _rigidbody2D.gravityScale;
            gravityScale -= (gravityScale * 2);
            _rigidbody2D.gravityScale = gravityScale;
            _playerRotationController.flipPlayerRotation();
        }
    }

    public void resetGravity()
    {
        _rigidbody2D.gravityScale = 1;
        _playerRotationController.resetPlayerRotation();
    }
}