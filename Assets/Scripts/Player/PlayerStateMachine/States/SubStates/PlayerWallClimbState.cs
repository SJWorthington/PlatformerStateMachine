public class PlayerWallClimbState : PlayerTouchingWallState
{
    public PlayerWallClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isExitingState) return;
        player.SetUpwardVelocity(playerData.wallClimbVelocity);

        if (yInput != 1) //TODO - look at making these normalised inputs a constant somewhere, so I don't have to hardcode it to 1 
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
    }
}