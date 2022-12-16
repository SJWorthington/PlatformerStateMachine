using UnityEngine;

public class PlayerGravityTunnelInAirState : PlayerBaseState
{
    public PlayerGravityTunnelInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if (player.InputHandler.JumpInput)
        {
            stateMachine.ChangeState(player.FlipGravityAbilityState);
        }
        else if (player.CheckIfGrounded() && player.currentVelocity.y < 0.01f || player.CheckIfOnCeiling() && player.currentVelocity.y > -0.01f)
        {
            stateMachine.ChangeState(player.GravityTunnelLandState);
        } else if (!player.isInGravityTunnel)
        {
            stateMachine.ChangeState(player.InAirState);
        }
        else
        {
            player.CheckIfShouldFlipXAxis(player.InputHandler.NormalisedInputX);
            player.SetForwardVelocity(playerData.movementVelocity * player.InputHandler.NormalisedInputX);
            var animVelocityY = 0f;
            if (player.isInInvertedGravity)
            {
                animVelocityY = -player.currentVelocity.y;
            }
            else
            {
                animVelocityY = player.currentVelocity.y;
            }
            player.Anim.SetFloat(PlayerInAirState.YVelocity, animVelocityY);
            player.Anim.SetFloat(PlayerInAirState.XVelocity, Mathf.Abs(player.currentVelocity.x));
        }
    }
}


//TODO - really should add these constants to the anim class
