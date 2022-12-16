using UnityEngine;

public class PlayerLedgeClimbState : PlayerBaseState
{
    private Vector2 detectedPos;
    private Vector2 cornerPos;
    private Vector2 startPos;
    private Vector2 stopPos;

    private bool isHanging;
    private bool isClimbing;

    private int xInput;
    private int yInput;
    private static readonly int ClimbLedge = Animator.StringToHash("climbLedge"); //TODO - move elsewhere

    public PlayerLedgeClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.setVelocityZero();
        player.transform.position = detectedPos;
        cornerPos = player.DetermineLedgeCornerPos();
        
        startPos.Set(cornerPos.x - player.FacingDirection * playerData.startOffset.x, cornerPos.y - playerData.startOffset.y);
        stopPos.Set(cornerPos.x + player.FacingDirection * playerData.stopOffset.x, cornerPos.y + playerData.stopOffset.y);
    }

    public override void Exit()
    {
        base.Exit();

        isHanging = false;

        if (isClimbing)
        {
            player.transform.position = stopPos;
            isClimbing = false;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else
        {

            player.setVelocityZero();
            player.transform.position = startPos; //TODO - I don't like repeatedly setting position like this, can I just clamp gravity on enter?

            xInput = player.InputHandler.NormalisedInputX;
            yInput = player.InputHandler.NormalisedInputY;

            if (xInput == player.FacingDirection && isHanging && !isClimbing)
            {
                isClimbing = true;
                player.Anim.SetBool(ClimbLedge, true);
            }
            else if (yInput == -1 && isHanging && !isClimbing)
            {
                stateMachine.ChangeState(player.InAirState);
            }
        }
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        isHanging = true;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        
        player.Anim.SetBool("climbLedge", false);
    }

    public void setDetectedPosition(Vector2 pos)
    {
        detectedPos = pos;
    }
}