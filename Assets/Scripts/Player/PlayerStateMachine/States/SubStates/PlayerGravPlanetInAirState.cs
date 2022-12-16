using UnityEngine;

public class PlayerGravPlanetInAirState : PlayerBaseState
{
    public PlayerGravPlanetInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetGravity(0);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        //TODO - grav planet landed state, go to it from here
        if (!player.isInGravPlanetRange)
        {
            stateMachine.ChangeState(player.InAirState);
        }
        else if (player.InputHandler.JumpInput && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else if (player.CheckIfGrounded())
        {
            if (player.InputHandler.NormalisedInputX == 0)
            {
                stateMachine.ChangeState(player.GravPlanetIdleState);
            }
            else
            {
                stateMachine.ChangeState(player.GravPlanetMoveState);
            }
        }
        else
        {
            player.CheckIfShouldFlipXAxis(player.InputHandler.NormalisedInputX);
            player.SetForwardVelocity(playerData.movementVelocity * player.InputHandler.NormalisedInputX);
            player.Anim.SetFloat(PlayerInAirState.YVelocity, player.currentVelocity.y);
            player.Anim.SetFloat(PlayerInAirState.XVelocity, Mathf.Abs(player.currentVelocity.x));
            //TODO - get this to refelct x/y relative to player's facing position, cmmented lines below show how not to do this
            // player.Anim.SetFloat(PlayerInAirState.YVelocity, player.currentVelocity.y * player.transform.rotation.y);
            // player.Anim.SetFloat(PlayerInAirState.XVelocity, Mathf.Abs(player.currentVelocity.x  * player.transform.rotation.x));
            player.RotateToNearestGravPlanet();
            //TODO - if I clean the PlayerInAirState, clean this too
        }
    }
}