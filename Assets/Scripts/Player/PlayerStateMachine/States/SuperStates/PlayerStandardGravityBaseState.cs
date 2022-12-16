public class PlayerStandardGravityBaseState : PlayerBaseState
{
    protected PlayerStandardGravityBaseState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //TODO - this won't do, won't flip player right up
        player.ResetToStandardGravityFromInverted();
    }
}