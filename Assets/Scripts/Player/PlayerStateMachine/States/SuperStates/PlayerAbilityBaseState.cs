using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerBaseState
{
    protected bool isAbilityDone;
    private bool isGrounded;
    
    public PlayerAbilityState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        isAbilityDone = false;
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAbilityDone)
        {
            //TODO - Need a better way to check what ability has just been used, this only works if the only grav tunnel ability is flip gravity 
            if (player.isInGravityTunnel)
            {
                stateMachine.ChangeState(player.GravityTunnelInAirState);
            }
            else if (player.isInAntiGravZone)
            {
                stateMachine.ChangeState(player.AntiGravityZoneIdleState);
            }
            else if (isGrounded && player.currentVelocity.y < 0.01f)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else
            {
                stateMachine.ChangeState(player.InAirState);
            }
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();
        
        isGrounded = player.CheckIfGrounded();
    }
}
