using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private float jumpsRemaining;
    
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        jumpsRemaining = playerData.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();
        player.InputHandler.UseJumpInput();
        player.SetUpwardVelocity(playerData.jumpVelocity);
        isAbilityDone = true;
        DecreaseJumpsRemaining();
        player.InAirState.SetIsJumping();
    }

    public bool CanJump()
    {
        return jumpsRemaining > 0;
    }

    public void ResetJumpsRemaining() => jumpsRemaining = playerData.amountOfJumps;

    public void DecreaseJumpsRemaining()
    {
        jumpsRemaining--;
    }
}