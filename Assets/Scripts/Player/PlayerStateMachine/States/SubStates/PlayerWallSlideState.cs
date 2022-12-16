public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isExitingState) return;
        
        player.SetUpwardVelocity(-playerData.wallSlideVelocity);

        if (player.InputHandler.GrabInput && player.InputHandler.NormalisedInputY == 0)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
    }
}