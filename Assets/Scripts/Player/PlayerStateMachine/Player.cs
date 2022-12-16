using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Player : MonoBehaviour
{
    private PlayerStateMachine StateMachine { get; set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }
    public PlayerFlipGravityAbilityState FlipGravityAbilityState { get; private set; }
    public PlayerGravityTunnelIdleState GravityTunnelIdleState { get; private set; }
    public PlayerGravityTunnelGroundedMoveState GravityTunnelGroundedMoveState { get; private set; }
    public PlayerGravityTunnelInAirState GravityTunnelInAirState { get; private set; }
    public PlayerGravityTunnelLandState GravityTunnelLandState { get; private set; }
    public PlayerAntiGravityZoneIdleState AntiGravityZoneIdleState { get; private set; }
    public PlayerAntiGravZoneRotateState AntiGravZoneRotateState { get; private set; }
    public PlayerAntiGravSwimAbilityState AntiGravSwimAbilityState { get; private set; }
    public PlayerGravPlanetIdleState GravPlanetIdleState { get; private set; }
    public PlayerGravPlanetMoveState GravPlanetMoveState { get; private set; }
    public PlayerGravPlanetInAirState GravPlanetInAirState { get; private set; }
    public PlayerGravPlanetJumpState GravPlanetJumpState { get; private set; }


    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }

    [SerializeField] private PlayerData playerData;

    public Rigidbody2D rb2d { get; private set; }
    public Transform Transform { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; }
    private Vector2 workspaceVector;
    public int FacingDirection { get; private set; }

    //TODO - getting a bit messy, mixing patterns a little here
    public bool isInGravityTunnel { get; private set; }
    public bool isInInvertedGravity { get; private set; }
    public bool isInAntiGravZone { get; private set; }

    public bool isInGravPlanetRange { get; private set; }

    private HashSet<Transform> _inRangeGravPlanetTransforms = new HashSet<Transform>();

    #region check transforms

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;

    #endregion

    //TODO - these commented fields are part of a previous attempt at smoothdamp, fix or delete
    // private Vector2 _currentVelocity;
    // public Vector2 CurrentVelocity { get => _currentVelocity; private set => _currentVelocity = value; }
    public Vector2 currentVelocity { get; private set; }

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        WallGrabState = new PlayerWallGrabState(this, StateMachine, playerData, "wallGrab");
        WallClimbState = new PlayerWallClimbState(this, StateMachine, playerData, "wallClimb");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "inAir");
        LedgeClimbState = new PlayerLedgeClimbState(this, StateMachine, playerData, "ledgeClimbState");
        //TODO - this assumes flip gravity will always go straight to inAir, which I guess is a safe assumption. Could add a new animation?
        FlipGravityAbilityState = new PlayerFlipGravityAbilityState(this, StateMachine, playerData, "inAir");
        GravityTunnelIdleState = new PlayerGravityTunnelIdleState(this, StateMachine, playerData, "idle");
        GravityTunnelGroundedMoveState = new PlayerGravityTunnelGroundedMoveState(this, StateMachine, playerData, "move");
        GravityTunnelInAirState = new PlayerGravityTunnelInAirState(this, StateMachine, playerData, "inAir");
        GravityTunnelLandState = new PlayerGravityTunnelLandState(this, StateMachine, playerData, "land");
        AntiGravityZoneIdleState = new PlayerAntiGravityZoneIdleState(this, StateMachine, playerData, "inAir"); //TODO - anti grav animations
        AntiGravZoneRotateState = new PlayerAntiGravZoneRotateState(this, StateMachine, playerData, "inAir"); //TODO - anti grav animations
        AntiGravSwimAbilityState = new PlayerAntiGravSwimAbilityState(this, StateMachine, playerData, "inAir"); //TODO - anti grav animations
        GravPlanetIdleState = new PlayerGravPlanetIdleState(this, StateMachine, playerData, "idle");
        GravPlanetMoveState = new PlayerGravPlanetMoveState(this, StateMachine, playerData, "move");
        GravPlanetInAirState = new PlayerGravPlanetInAirState(this, StateMachine, playerData, "inAir");
        GravPlanetJumpState = new PlayerGravPlanetJumpState(this, StateMachine, playerData, "inAir");

        rb2d = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();

        StateMachine.Initialize(IdleState);

        FacingDirection = 1; // todo - Don't love this at all, problem if we ever start a level/area facing left
    }

    private void Update()
    {
        currentVelocity = rb2d.velocity;
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public void SetForwardVelocity(float xVelocity)
    {
        //TODO Less instantiation of Vector2, make more workspaces?
        workspaceVector.Set(xVelocity, 0);
        var transform1 = transform;
        workspaceVector = transform1.rotation * new Vector2(xVelocity, 0);
        Vector2 currentPos = transform1.position;
        currentPos += workspaceVector * Time.deltaTime;
        transform1.position = currentPos;

        //TODO - this was previous move code before making it rotation based
        // workspaceVector.Set(xVelocity, currentVelocity.y); //TODO - smoothdamp maybe?
        // rb2d.velocity = workspaceVector;
        // currentVelocity = workspaceVector; // TODO - this line will have to change after smoothdamp comes in

        //TODO - this is a previous failed attempt at smooth damp, but player didn't move, revisit or delete
        // var currentVelocity = RigidBody.velocity;
        // movementVector.Set(xVelocity, CurrentVelocity.y); 
        // Debug.Log($"xVelocity is {xVelocity}");
        // var cSharpIsShit = new Vector2();
        // //RigidBody.velocity = Vector2.SmoothDamp(RigidBody.velocity, movementVector, ref cSharpIsShit, 0.5f);
        // RigidBody.velocity = Vector2.SmoothDamp(currentVelocity, movementVector, ref cSharpIsShit, 0.5f);
    }

    public void SetUpwardVelocity(float upwardVelocity) // TODO Will have to replace this with rotation based jump, 
    {
        rb2d.velocity = Vector2.zero;
        var rotationVector = transform.rotation * Vector2.up;
        rb2d.AddForce(rotationVector * upwardVelocity, ForceMode2D.Impulse);

        //TODO - previous, non-rotation based jump, rpobs delete
        // workspaceVector.Set(currentVelocity.x, yVelocity);
        // rb2d.velocity = workspaceVector;
        // currentVelocity = workspaceVector;
    }

    public void setVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workspaceVector.Set(angle.x * velocity * direction, angle.y * velocity);
        rb2d.velocity = workspaceVector;
        currentVelocity = workspaceVector;
    }

    public void setVelocityZero()
    {
        rb2d.velocity = Vector2.zero;
        currentVelocity = Vector2.zero;
    }

    public void CheckIfShouldFlipXAxis(int xInput)
    {
        if (SpriteRenderer.flipX && xInput > 0 || !SpriteRenderer.flipX && xInput < 0)
        {
            Flip();
        }
    }

    public bool CheckIfGrounded()
    {
        if (isInInvertedGravity) return false;
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }

    public bool CheckIfOnCeiling()
    {
        if (!isInInvertedGravity) return false;
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }

    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
    }

    public bool CheckIfTouchingWallBack()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * -FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
    }

    public bool CheckIfTouchingLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
    }

    private void Flip()
    {
        FacingDirection *= -1;
        SpriteRenderer.flipX = !SpriteRenderer.flipX;
    }

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    public Vector2 DetermineLedgeCornerPos()
    {
        RaycastHit2D xHit = Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
        float xDistance = xHit.distance;

        workspaceVector.Set(xDistance * FacingDirection, 0f);
        RaycastHit2D yHit = Physics2D.Raycast(ledgeCheck.position + (Vector3) (workspaceVector), Vector2.down, ledgeCheck.position.y - wallCheck.position.y, playerData.whatIsGround);
        float yDistance = yHit.distance;

        workspaceVector.Set(wallCheck.position.x + (xDistance * FacingDirection), ledgeCheck.position.y - yDistance);
        return workspaceVector;
    }

    public void SetIsInGravityTunnel(bool value)
    {
        isInGravityTunnel = value;
    }

    public void SetIsInAntiGravZone(bool value)
    {
        isInAntiGravZone = value;
    }

    public void AddToInRangeGravityPlanetTransforms(Transform planetTransform)
    {
        _inRangeGravPlanetTransforms.Add(planetTransform);
        isInGravPlanetRange = true;
    }

    public void RemoveFromInRangeGravityPlanetTransforms(Transform planetTransform)
    {
        Debug.Log("Removicles");
        _inRangeGravPlanetTransforms.Remove(planetTransform);
        if (_inRangeGravPlanetTransforms.Count == 0)
        {
            isInGravPlanetRange = false;
        }
    }

    public void SetGravity(float gravityLevel)
    {
        rb2d.gravityScale = gravityLevel;
    }

    public void ResetToStandardGravityFromInverted() //TODO - Replace with a function that just resets all to standard gravity
    {
        if (isInInvertedGravity)
        {
            InvertGravity();
        }
    }

    public void SwimUp()
    {
        rb2d.AddForce(transform.rotation * Vector2.up * playerData.swimStrength, ForceMode2D.Impulse);
    }

    public void RotatePlayer(float xInput)
    {
        var rotaticles = xInput * playerData.rotationSpeed * -1 * Time.deltaTime;
        rb2d.rotation += rotaticles;
    }

    public void ResetRotation()
    {
        rb2d.rotation = 0;
    }

    public void InvertGravity()
    {
        Debug.Log("Inverting that gravity");
        transform.localScale = new Vector2(transform.localScale.x, -transform.localScale.y);
        rb2d.gravityScale = -rb2d.gravityScale;
        isInInvertedGravity = !isInInvertedGravity;
    }

    public void RotateToNearestGravPlanet()
    {
        //TODO - move this to PlayerRotationController
        var nearestPlanet = _inRangeGravPlanetTransforms.OrderBy(planetTransform => (planetTransform.position - gameObject.transform.position).sqrMagnitude).First();
        var distanceVector = (Vector2) nearestPlanet.position - (Vector2) transform.position;
        var angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }

    public void TimedFunctionInvoke(Action function, float invokeDelay)
    {
        StartCoroutine(delayedCall(function, invokeDelay));
    }

    private IEnumerator delayedCall(Action aDelegate, float aDelay)
    {
        yield return new WaitForSeconds(aDelay);
        aDelegate();
    }
}