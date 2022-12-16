using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int wallJumpDirection;

    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.JumpState.ResetJumpsRemaining();
        player.InputHandler.UseJumpInput();
        if (player.CheckIfTouchingWall())
        {
            wallJumpDirection = -player.FacingDirection;
        }
        else if (player.CheckIfTouchingWallBack())
        {
            wallJumpDirection = player.FacingDirection;
        }

        player.setVelocity(playerData.wallJumpVelocity, playerData.wallJumpAngle, wallJumpDirection);
        player.CheckIfShouldFlipXAxis(wallJumpDirection);
        player.JumpState.DecreaseJumpsRemaining();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.Anim.SetFloat(PlayerInAirState.YVelocity, player.currentVelocity.y);
        player.Anim.SetFloat(PlayerInAirState.XVelocity, Mathf.Abs(player.currentVelocity.x));

        /* TODO - Can  do this time check with a coroutine, though not the checkIfGrounded bit. Could do a grounded event though, observe that where necessary
             would probs add some complexity to be observing events all over the show, maybe worth it to not have to do this polling though, improve performance  */
        if (Time.time >= startTime + playerData.wallJumpTime || player.CheckIfGrounded())
        {
            isAbilityDone = true;
        }
    }
}