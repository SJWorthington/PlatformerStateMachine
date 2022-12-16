using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpGravityTunnelController : MonoBehaviour
{
    private PlayerGravityController _playerGravityController;

    private void Awake()
    {
        _playerGravityController = GetComponent<PlayerGravityController>();
    }

    public void Jump()
    {
        _playerGravityController.FlipPlayerGravity();
    }

    public void onExitGravityTunnel()
    {
        _playerGravityController.resetGravity();
    }
}