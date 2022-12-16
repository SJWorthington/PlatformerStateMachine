using UnityEngine;

public class PlayerGravityTunnelIdleState : PlayerGravityTunnelGroundedBaseState
{
    public PlayerGravityTunnelIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (player.InputHandler.NormalisedInputX != 0)
        {
            stateMachine.ChangeState(player.GravityTunnelGroundedMoveState);
        }
    }
}