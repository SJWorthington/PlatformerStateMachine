using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerAntiGravityController : MonoBehaviour
    {
        private PlayerRotationController _playerRotationController;
        private Rigidbody2D _rigidbody2D;
        private float _xRotation;
        private Transform _transform;
        [SerializeField] private float _rotationSpeed = 20;
        [SerializeField] private float _boostSpeed = 5;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _playerRotationController = GetComponent<PlayerRotationController>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (Mathf.Abs(_xRotation) > float.Epsilon)
            {
                var rotaticles = _xRotation * _rotationSpeed * -1;
                //Debug.Log($"rotaticles = {rotaticles}");
                _playerRotationController.rotatePlayer(rotaticles);
            }
        }

        public void SetXMoveDir(float movementInputXValue)
        {
            _xRotation = movementInputXValue;
        }

        public void Jump()
        {
            _rigidbody2D.AddForce(transform.rotation * Vector2.up * _boostSpeed, ForceMode2D.Impulse);
        }
    }
}