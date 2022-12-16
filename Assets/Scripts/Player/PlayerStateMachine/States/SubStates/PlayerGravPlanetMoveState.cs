public class PlayerGravPlanetMoveState : PlayerGravPlanetGroundedBaseState
{
    public PlayerGravPlanetMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (player.InputHandler.NormalisedInputX == 0) //TODO - base this on velocity, not input
        {
            stateMachine.ChangeState(player.GravPlanetIdleState);
        }
        else
        {
            player.CheckIfShouldFlipXAxis(player.InputHandler.NormalisedInputX);
            player.SetForwardVelocity(playerData.movementVelocity * player.InputHandler.NormalisedInputX);
        }
    }
}