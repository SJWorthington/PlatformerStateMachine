using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 rawMovementInput { get; private set; }
    public int NormalisedInputX { get; private set; }
    public int NormalisedInputY { get; private set; }
    public bool GrabInput { get; private set; }
    public bool JumpInput { get; private set; }
    
    
    public bool jumpInputStop { get; private set; }

    [SerializeField] private float inputHoldTime = 0.2f;
    private float jumpInputStartTime;

    [SerializeField] private float yInputDeadzoneThreshold = 0.4f;

    private void Update()
    {
        CheckJumpInputHoldTime(); 
        //TODO Could definitely do this better by using a coroutine to set _jumpInput false after input hold time, much better than constantly checking in update
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        rawMovementInput = context.ReadValue<Vector2>();

        NormalisedInputX = (int)(rawMovementInput * Vector2.right).normalized.x;
        if (Mathf.Abs(rawMovementInput.y) > yInputDeadzoneThreshold)
        {
            NormalisedInputY = (int) (rawMovementInput * Vector2.up).normalized.y;
        }
        else
        {
            NormalisedInputY = 0;
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            jumpInputStop = false;
            jumpInputStartTime = Time.time;
        }

        if (context.canceled)
        {
            jumpInputStop = true;
        }
    }

    //TODO - having to manually use this is just asking for bugs
    public void UseJumpInput()
    {
        JumpInput = false;
    }

    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }

    public void OnGrabInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GrabInput = true;
        }

        if (context.canceled)
        {
            GrabInput = false;
        }
    }
}