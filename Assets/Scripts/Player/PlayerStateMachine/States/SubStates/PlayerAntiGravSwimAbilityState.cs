public class PlayerAntiGravSwimAbilityState : PlayerAbilityState
{
    public PlayerAntiGravSwimAbilityState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.InputHandler.UseJumpInput();
        player.SwimUp();
        isAbilityDone = true;
    }
}