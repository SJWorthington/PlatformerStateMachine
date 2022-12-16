using UnityEngine;

public class PlayerAntiGravityZoneIdleState : PlayerBaseState
{
    public PlayerAntiGravityZoneIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("We are in anti gravity");
        player.SetGravity(0);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!player.isInAntiGravZone)
        {
            stateMachine.ChangeState(player.InAirState);
        }
        else if (player.InputHandler.JumpInput)
        {
            stateMachine.ChangeState(player.AntiGravSwimAbilityState);
            player.InputHandler.UseJumpInput();
        }
        else if (player.InputHandler.NormalisedInputX != 0)
        {
            stateMachine.ChangeState(player.AntiGravZoneRotateState);
        }
    }
}