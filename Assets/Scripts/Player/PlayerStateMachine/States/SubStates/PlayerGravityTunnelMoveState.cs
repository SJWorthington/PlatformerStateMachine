using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class PlayerGravityTunnelGroundedMoveState : PlayerGravityTunnelGroundedBaseState
{
    public PlayerGravityTunnelGroundedMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.CheckIfShouldFlipXAxis(player.InputHandler.NormalisedInputX);
        player.SetForwardVelocity(playerData.movementVelocity * player.InputHandler.NormalisedInputX);

        if (!player.isInGravityTunnel)
        {
            stateMachine.ChangeState(player.MoveState);
        }
        if (player.InputHandler.NormalisedInputX == 0)
        {
            stateMachine.ChangeState(player.GravityTunnelIdleState);
        }
    }
}