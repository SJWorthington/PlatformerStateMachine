public class PlayerWallGrabState : PlayerTouchingWallState
{
    public PlayerWallGrabState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isExitingState) return;
        if (yInput > 0)
        {
            stateMachine.ChangeState(player.WallClimbState);
        }
        else if (yInput < 0 || !player.InputHandler.GrabInput)
        {
            stateMachine.ChangeState(player.WallSlideState);
        }
    }

    public override void Enter()
    {
        base.Enter();
        player.SetForwardVelocity(0);
        player.SetUpwardVelocity(0);
        player.SetGravity(0);
    }

    public override void Exit()
    {
        base.Exit();
        player.SetGravity(playerData.standardGravity);
    }
}