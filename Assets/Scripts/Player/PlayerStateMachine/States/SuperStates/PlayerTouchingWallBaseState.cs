public class PlayerTouchingWallState : PlayerStandardGravityBaseState
{
    protected bool isGrounded;
    protected bool isTouchingWall;
    protected int xInput;
    protected int yInput; //TODO - Don't see why I need this when I can just get it from the inputHandler directly, probably refactor
    protected bool isTouchingLedge;
    
    public PlayerTouchingWallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall(); //TODO - I don't like copying state in like this, feels clumsy
        isTouchingLedge = player.CheckIfTouchingLedge();

        if (isTouchingWall && !isTouchingLedge)
        {
            player.LedgeClimbState.setDetectedPosition(player.transform.position);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.InputHandler.NormalisedInputX;
        yInput = player.InputHandler.NormalisedInputY;

        if (isGrounded && !player.InputHandler.GrabInput)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else if (!isGrounded && isTouchingWall && player.InputHandler.JumpInput)
        {
            stateMachine.ChangeState(player.WallJumpState);
        }
        else if (!isTouchingWall || xInput != player.FacingDirection && !player.InputHandler.GrabInput)
        {
            stateMachine.ChangeState(player.InAirState);
        } else if (isTouchingWall && !isTouchingLedge)
        {
            stateMachine.ChangeState(player.LedgeClimbState);
        }
    }
}