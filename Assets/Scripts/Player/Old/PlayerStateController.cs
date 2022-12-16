using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    private BasicPlayerControls basicPlayerControls;
    private MovementState _movementState;
    private PlayerController _playerController;
    private PlayerLayerController _playerLayerController;
    private PlayerJumpGravityTunnelController _playerJumpGravityTunnelController;
    private PlayerAntiGravityController _playerAntiGravityController;
    private PlayerRotationController _playerRotationController;
    private Attractable _attractable;
    [SerializeField] private float jumpPower = 20f;
    private Transform _transform;

    public static bool _isBash { get; private set; } //TODO - set this static so I can disable movementSmoothing in PlayerController since it was making bash useless, terrible coding though, fix
    private float bashForce = 40f;
    private float bashRange = 1f;
    private ProjectileController _bashedProjectile;

    private Rigidbody2D _rigidBody;

    private void Awake()
    {
        basicPlayerControls = new BasicPlayerControls();
        _transform = GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _movementState = MovementState.STANDARD;
        _rigidBody = GetComponent<Rigidbody2D>();
        _playerLayerController = GetComponent<PlayerLayerController>();
        _playerController = GetComponent<PlayerController>();
        _attractable = GetComponent<Attractable>();
        _playerJumpGravityTunnelController = GetComponent<PlayerJumpGravityTunnelController>();
        _playerAntiGravityController = GetComponent<PlayerAntiGravityController>();
        _playerRotationController = GetComponent<PlayerRotationController>();

        //basicPlayerControls.RunJump.Jump.performed += _ => Jump();
       // basicPlayerControls.RunJump.Bash.performed += context => Bash(context.ReadValue<float>());
        //basicPlayerControls.RunJump.SwapLayer.performed += _ => _playerLayerController.swapLayer();
    }

    private void OnEnable()
    {
        basicPlayerControls.Enable();
    }

    private void OnDisable()
    {
        basicPlayerControls.Disable();
    }

    private void Update()
    {
        var movementInputXValue = basicPlayerControls.Gameplay.Move.ReadValue<float>();
        switch (_movementState)
        {
            case MovementState.STANDARD:
            case MovementState.JUMP_GRAVITY_TUNNEL:
                _playerController.SetXMoveDir(movementInputXValue);
                break;
            case MovementState.ATTRACTABLE:
                _attractable.SetXMoveDir(movementInputXValue);
                break;
            default:
                _playerAntiGravityController.SetXMoveDir(movementInputXValue);
                break;
        }
    }

    private void Jump()
    {
        switch (_movementState)
        {
            case MovementState.STANDARD:
                _playerController.Jump(jumpPower);
                break;
            case MovementState.ATTRACTABLE:
                _attractable.Jump(jumpPower);
                break;
            case MovementState.JUMP_GRAVITY_TUNNEL:
                _playerJumpGravityTunnelController.Jump();
                break;
            default:
                _playerAntiGravityController.Jump();
                break;
        }
    }

    public void setIsInJumpGravityTunnel(bool isInTunnel)
    {
        if (isInTunnel)
        {
            _movementState = MovementState.JUMP_GRAVITY_TUNNEL;
        }
        else
        {
            _movementState = MovementState.STANDARD;
            _playerJumpGravityTunnelController.onExitGravityTunnel();
            _playerController.enabled = true;
        }
    }

    //TODO - this isn't a good way to do state, but will do for now (it's only going to get worse Stephen)
    public void setIsAttracted(bool isAttracted)
    {
        if (isAttracted)
        {
            _rigidBody.gravityScale = 0f; // Todo - Don't do this here
            _playerController.enabled = false; //TODO Need to reset movement or could create come jank, create a ResetAndDisable function or similar
            _attractable.enabled = true;
            _movementState = MovementState.ATTRACTABLE;
        }
        else
        {
            _playerController.enabled = true;
            _attractable.enabled = false;
            _rigidBody.gravityScale = 1f;
            _transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
            _movementState = MovementState.STANDARD;
        }
    }

    public void SetIsInAntiGravity(bool isAntiGrav)
    {
        if (isAntiGrav)
        {
            _rigidBody.gravityScale = 0f; // Todo - Don't do this here
            _playerController.enabled = false;
            _movementState = MovementState.ANTI_GRAV;
        }
        else
        {
            _rigidBody.gravityScale = 1f; // Todo - Don't do this here#
            _playerController.enabled = true;
            _movementState = MovementState.STANDARD;
            _playerRotationController.resetPlayerRotation();
        }
    }

    //TODO - this should be in a separate script
    void Bash(float readValue)
    {
        if (readValue > 0)
        {
            var hits = Physics2D.CircleCastAll(transform.position, bashRange, Vector3.forward);
            foreach (var hit in hits)
            {
                if (hit.collider.gameObject.tag.Equals("Bashable"))
                {
                    _isBash = true;
                    _bashedProjectile = hit.collider.gameObject.GetComponent<ProjectileController>();
                    Time.timeScale = 0f;
                    return;
                }
            }
        }
        else
        {
            if (_isBash)
            {
                Invoke(nameof(setBashFalse), 0.5f);
                var aimDir = basicPlayerControls.Gameplay.BashAim.ReadValue<Vector2>().normalized;
                var bashDir = aimDir * (Vector2.left + Vector2.down);
                aimDir = new Vector2(aimDir.x * 0.8f, aimDir.y);
                Time.timeScale = 1;
                _rigidBody.velocity = Vector2.zero;
                _rigidBody.AddForce(aimDir * bashForce, ForceMode2D.Impulse);
                _bashedProjectile.bashLaunch(bashDir);
                _bashedProjectile = null;
            }
        }
    }

    private void setBashFalse()
    {
        _isBash = false;
    }

    enum MovementState
    {
        STANDARD,
        ATTRACTABLE,
        JUMP_GRAVITY_TUNNEL,
        ANTI_GRAV
    }
}