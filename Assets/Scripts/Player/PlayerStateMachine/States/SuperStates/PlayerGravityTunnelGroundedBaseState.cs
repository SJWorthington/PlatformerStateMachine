using UnityEngine;

public class PlayerGravityTunnelGroundedBaseState : PlayerBaseState
{
    protected PlayerGravityTunnelGroundedBaseState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        if (player.InputHandler.JumpInput)
        {
            stateMachine.ChangeState(player.FlipGravityAbilityState);
            player.InputHandler.UseJumpInput();
        }
    }
}