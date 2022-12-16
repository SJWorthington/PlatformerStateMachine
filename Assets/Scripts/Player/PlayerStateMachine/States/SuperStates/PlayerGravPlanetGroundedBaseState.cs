using UnityEngine;

public class PlayerGravPlanetGroundedBaseState : PlayerBaseState
{
    protected PlayerGravPlanetGroundedBaseState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        player.SetGravity(0);
        player.JumpState.ResetJumpsRemaining();
    }

    public override void LogicUpdate()
    {
        player.RotateToNearestGravPlanet();

        if (player.InputHandler.JumpInput && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.JumpState);
            player.InputHandler.UseJumpInput();
        }
    }
}