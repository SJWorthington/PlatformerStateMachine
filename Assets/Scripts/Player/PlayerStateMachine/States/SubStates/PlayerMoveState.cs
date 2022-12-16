using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class PlayerMoveState : PlayerGroundedBaseState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.CheckIfShouldFlipXAxis(xInput);
        player.SetForwardVelocity(playerData.movementVelocity * xInput);

        if (xInput == 0) //TODO - base this on velocity, not input
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }
}