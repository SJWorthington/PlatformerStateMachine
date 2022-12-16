using UnityEngine;

public class PlayerGravityTunnelLandState : PlayerGravityTunnelGroundedBaseState
{
    public PlayerGravityTunnelLandState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isExitingState)
        {
            if (player.InputHandler.NormalisedInputX != 0)
            {
                stateMachine.ChangeState(player.GravityTunnelGroundedMoveState);
            }
            else if (isAnimationFinished)
            {
                stateMachine.ChangeState(player.GravityTunnelIdleState);
            }
        }
    }
}