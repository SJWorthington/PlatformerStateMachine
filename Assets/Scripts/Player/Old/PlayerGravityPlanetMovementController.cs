using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class Attractable : MonoBehaviour
{
    private HashSet<Transform> _inRangeTransforms = new HashSet<Transform>();
    private PlayerStateController _playerStateController;
    private PlayerRotationController _playerRotationController;
    private Transform _transform;
    private Rigidbody2D _rigidBody;
    private float _xMoveDirection;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _playerStateController = GetComponent<PlayerStateController>();
        _playerRotationController = GetComponent<PlayerRotationController>();
    }

    public void TrySetAttractorTransform(Transform attractorTransform)
    {
        _inRangeTransforms.Add(attractorTransform);
        _playerStateController.setIsAttracted(true);
       
    }

    public void RemoveAttractorTransform(Transform exitedAttractorTransform)
    {
        {
            _inRangeTransforms.Remove( exitedAttractorTransform);
            if (_inRangeTransforms.Count == 0)
            {
                _playerStateController.setIsAttracted(false);
            }
        }
    }

    private void OnDisable()
    {
        _xMoveDirection = 0;
    }

    private void RotateToNearestPlanet()
    {
        //TODO - move this to PlayerRotationController
        var nearestPlanet = _inRangeTransforms.OrderBy(planetTransform => (planetTransform.position - gameObject.transform.position).sqrMagnitude).First();
        var distanceVector = (Vector2) nearestPlanet.position - (Vector2) _transform.position;
        var angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        _transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }
    
    private void Update()
    {
        
        if (_inRangeTransforms.Count > 0)
        {
            RotateToNearestPlanet();
        }
        
        if (Mathf.Abs(_xMoveDirection) > float.Epsilon)
        {
            var rotation = _transform.rotation;
            var moveVector = new Vector2(_xMoveDirection, 0);
            Vector2 move = rotation * moveVector;
            var transform1 = transform;
            Vector2 currentPos = transform1.position;
            currentPos += move * (5 * Time.deltaTime);
            transform1.position = currentPos;
        }
    }

    public void Jump(float jumpPower)
    {
        var velocity = _rigidBody.velocity;
        velocity.y = 0;
        velocity.x = 0;
        _rigidBody.velocity = velocity;
        var rotation = _transform.rotation;
        var rotationVector = rotation * Vector2.up;
        _rigidBody.AddForce(rotationVector * jumpPower, ForceMode2D.Impulse);
    }

    public void SetXMoveDir(float movementInputXValue)
    {
        _xMoveDirection = movementInputXValue;
    }
}