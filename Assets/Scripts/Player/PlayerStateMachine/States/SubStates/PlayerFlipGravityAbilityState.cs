using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerFlipGravityAbilityState : PlayerAbilityState
{
    public PlayerFlipGravityAbilityState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.InputHandler.UseJumpInput();
        player.InvertGravity();
        isAbilityDone = true;
    }
}