using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedBaseState : PlayerStandardGravityBaseState
{
    protected int xInput;
    private bool isGrounded;
    
    public PlayerGroundedBaseState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.JumpState.ResetJumpsRemaining();
    }
    
    public override void LogicUpdate()
    {
        xInput = player.InputHandler.NormalisedInputX;

        if (player.isInGravityTunnel)
        {
            stateMachine.ChangeState(player.GravityTunnelGroundedMoveState);
        }
        else if (player.InputHandler.JumpInput && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.JumpState);
        } else if (!isGrounded && player.rb2d.velocity.y <= 0)
        {
            player.InAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.InAirState);
        } else if (player.CheckIfTouchingWall() && player.InputHandler.GrabInput)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.CheckIfGrounded();
        //tutorial has us do our isTouchingWall here instead of in LogicUpdate, still don't like that
    }
}
