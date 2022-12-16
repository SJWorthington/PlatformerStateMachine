using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private MovingPlatformChecker _movingPlatformChecker;
    private BasicPlayerControls basicPlayerControls;
    private float moveRate = 5f; 
    private Rigidbody2D _rigidBody;
    private float _xMoveDirection;
    private Vector3 refVelocity = Vector3.zero;
    private float smoothingChangeThreshold = 5;
    private readonly float _moveSmoothing = .05f;
    private readonly float _postBashSmoothing = 2f;

    private void Awake()
    {
        basicPlayerControls = new BasicPlayerControls();
        _movingPlatformChecker = GetComponent<MovingPlatformChecker>();
    }

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnDisable()
    {
        _xMoveDirection = 0;
    }

    void Update()
    {
        if (!(_movingPlatformChecker.movingPlatformVector is null))
        {
            transform.parent = _movingPlatformChecker.movingPlatformVector;
        }
        else
        {
            transform.parent = null;
        }
        if (Mathf.Abs(_xMoveDirection) > float.Epsilon)
        {
            var velocity =  _rigidBody.velocity;
            var targetVelocity = new Vector2(_xMoveDirection * moveRate , velocity.y);
            var dampicus = Mathf.Abs(velocity.x) > smoothingChangeThreshold ? _postBashSmoothing : _moveSmoothing;
            _rigidBody.velocity = Vector3.SmoothDamp(velocity, targetVelocity, ref refVelocity, dampicus);
            Debug.Log($"Regular velocity is {_rigidBody.velocity}");
            //TODO - figure out what refVelocity does, could probably use it instead of the velocity var here
        }
    }

    public void Jump(float power)
    {
        var velocity = _rigidBody.velocity;
        velocity.y = 0;
        _rigidBody.velocity = velocity;
        _rigidBody.AddForce(Vector2.up * power, ForceMode2D.Impulse);
    }

    public void SetXMoveDir(float xMovementInput)
    {
        _xMoveDirection = xMovementInput;
    }
}