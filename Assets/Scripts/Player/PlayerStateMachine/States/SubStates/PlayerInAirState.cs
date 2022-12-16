using UnityEngine;

public class PlayerInAirState : PlayerStandardGravityBaseState
{
    private bool isGrounded;
    private bool isTouchingWall;
    private bool oldIsTouchingWall;
    private bool isTouchingWallBack;
    private bool oldIsTouchingWallBack;
    private int xInput;
    private bool jumpInputStop;
    private bool isCoyoteTime;
    private bool wallJumpCoyoteTime;
    private bool isJumping;
    private bool isTouchingLedge;

    public static readonly int YVelocity = Animator.StringToHash("yVelocity"); //TODO - move this to a dedicated AnimKeys file or similar
    public static readonly int XVelocity = Animator.StringToHash("xVelocity");

    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetGravity(playerData.standardGravity);
        player.ResetRotation();
    }

    public override void Exit()
    {
        base.Exit();
        oldIsTouchingWall = false;
        oldIsTouchingWallBack = false;
        isTouchingWall = false;
        oldIsTouchingWallBack = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormalisedInputX;
        jumpInputStop = player.InputHandler.jumpInputStop;

        checkJumpMultiplier();
        if (player.isInGravityTunnel)
        {
            stateMachine.ChangeState(player.GravityTunnelInAirState);
        }
        else if (player.isInGravPlanetRange)
        {
            stateMachine.ChangeState(player.GravPlanetInAirState);
        }
        else if (player.isInAntiGravZone)
        {
            stateMachine.ChangeState(player.AntiGravityZoneIdleState);
        }
        else if (isGrounded && player.currentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.LandState);
        }
        else if (isTouchingWall && !isTouchingLedge)
        {
            stateMachine.ChangeState(player.LedgeClimbState);
        }
        else if (player.InputHandler.JumpInput && (isTouchingWall || isTouchingWallBack || wallJumpCoyoteTime))
        {
            StopWallJumpCoyoteTime();
            stateMachine.ChangeState(player.WallJumpState);
        }
        else if (player.InputHandler.JumpInput && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else if (isTouchingWall && player.InputHandler.GrabInput)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
        else if (isTouchingWall && xInput == player.FacingDirection && player.currentVelocity.y <= 0)
        {
            stateMachine.ChangeState(player.WallSlideState);
        }
        else
        {
            player.CheckIfShouldFlipXAxis(xInput);
            player.SetForwardVelocity(playerData.movementVelocity * xInput);
            player.Anim.SetFloat(YVelocity, player.currentVelocity.y);
            player.Anim.SetFloat(XVelocity, Mathf.Abs(player.currentVelocity.x)); // TODO - probably shouldn't handle the Abs here, maybe make an animator wrapper I call? 
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        oldIsTouchingWall = isTouchingWall; //TODO has to be a better way to do this?
        oldIsTouchingWallBack = isTouchingWallBack;

        isGrounded = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall(); //TODO - can I not just call this function when I wnt to check it instead? Don't see benefit of this
        isTouchingWallBack = player.CheckIfTouchingWallBack();
        isTouchingLedge = player.CheckIfTouchingLedge(); // TODO Looks like I'm supposed to be touching a ledge is this is false, and check if touching wall is true, really misleading function name

        if (isTouchingWall && !isTouchingLedge)
        {
            player.LedgeClimbState.setDetectedPosition(player.transform.position); // TODO - I don't like states setting things in other states like this, messy
        }

        if (!wallJumpCoyoteTime && !isTouchingWall && !isTouchingWallBack && (oldIsTouchingWall || oldIsTouchingWallBack))
        {
            StartWallJumpCoyoteTime();
        }
    }

    private void checkJumpMultiplier()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                player.SetUpwardVelocity(player.currentVelocity.y * playerData.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (player.currentVelocity.y <= 0)
            {
                isJumping = false;
            }
        }
    }

    private void StopCoyoteTime()
    {
        if (!isCoyoteTime) return;
        isCoyoteTime = false;
        player.JumpState.DecreaseJumpsRemaining();
    }

    public void StartCoyoteTime()
    {
        isCoyoteTime = true;
        player.TimedFunctionInvoke(StopCoyoteTime, playerData.coyoteTime);
    }

    public void StartWallJumpCoyoteTime()
    {
        wallJumpCoyoteTime = true;
        player.TimedFunctionInvoke(StopWallJumpCoyoteTime, playerData.coyoteTime); //TODO - Use TimedFunctionInvoke for other delayed checks
    }

    private void StopWallJumpCoyoteTime()
    {
        wallJumpCoyoteTime = false;
    }

    public void SetIsJumping()
    {
        //if jump happened in coyote time, setting false will prevent unnecessary decrement in CheckCoyoteTime call
        isCoyoteTime = false;
        isJumping = true;
    }
}