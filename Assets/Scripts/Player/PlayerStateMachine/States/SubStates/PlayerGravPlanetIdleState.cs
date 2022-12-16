public class PlayerGravPlanetIdleState : PlayerGravPlanetGroundedBaseState
{
    public PlayerGravPlanetIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (player.InputHandler.NormalisedInputX != 0)
        {
            stateMachine.ChangeState(player.GravPlanetMoveState);
        }
    }
}