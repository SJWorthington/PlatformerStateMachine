public class PlayerAntiGravZoneRotateState : PlayerBaseState
{
    public PlayerAntiGravZoneRotateState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.CheckIfShouldFlipXAxis(player.InputHandler.NormalisedInputX);
        player.RotatePlayer(player.InputHandler.NormalisedInputX);

        if (!player.isInAntiGravZone)
        {
            stateMachine.ChangeState(player.InAirState);
        }
        else if (player.InputHandler.JumpInput)
        {
            stateMachine.ChangeState(player.AntiGravSwimAbilityState);
            player.InputHandler.UseJumpInput();
        }
        else if (player.InputHandler.NormalisedInputX == 0)
        {
            stateMachine.ChangeState(player.AntiGravityZoneIdleState);
        }
    }
}