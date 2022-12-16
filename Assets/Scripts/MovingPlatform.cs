using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class MovingPlatform : MonoBehaviour
{
    private float _inverseMoveTime;
    private Vector2 _initialPos;
    [SerializeField]private List<Vector2> relativePositions;
    private List<Vector2> _worldPositions;
    private Vector2 _currentDestination;
    [SerializeField] private float moveVelocity = 2;
    private int _currentTargetIndex = 1;
    [SerializeField] private bool doLoop;
    private bool _headingForwards = true;

    private void Awake()
    {
        _initialPos = transform.position;
        generateWorldPositions();
        _currentDestination = _worldPositions[_currentTargetIndex];
    }

    private void FixedUpdate()
    {
        var transform1 = transform;
        var position = transform1.position;
        var xVelocity = _currentDestination.x - position.x;
        var yVelocity = _currentDestination.y - position.y;
        
        //TODO - remove this greatestDimen stuff from fixed update
        var greatestDimen = Mathf.Abs(xVelocity) > Mathf.Abs(yVelocity) ? Mathf.Abs(xVelocity) : Mathf.Abs(yVelocity);

        position = new Vector2(position.x + xVelocity / greatestDimen * moveVelocity * Time.deltaTime,
            position.y + yVelocity / greatestDimen * moveVelocity * Time.deltaTime);
        transform1.position = position;
    }

    private void Update()
    {
        var sqrRemainingDistance = ((Vector2)transform.position - _currentDestination).sqrMagnitude;
        if (sqrRemainingDistance < 0.0005) 
        {
            NextDestination();
        }
    }

    private void NextDestination()
    {
        if (doLoop)
        {
            _currentTargetIndex++;
            if (_currentTargetIndex >= _worldPositions.Count)
            {
                _currentTargetIndex = 0;
            }
        } 
        else if (_headingForwards)
        {
            _currentTargetIndex++;
            if (_currentTargetIndex >= _worldPositions.Count)
            {
                _currentTargetIndex -= 2;
                _headingForwards = false;
            }
        }
        else
        {
            _currentTargetIndex--;
            if (_currentTargetIndex < 0)
            {
                _currentTargetIndex = 1;
                _headingForwards = true;
            }
        }

        _currentDestination = _worldPositions[_currentTargetIndex];
    }

    private void generateWorldPositions()
    {
        _worldPositions = new List<Vector2> {_initialPos};
        foreach (var relativePos in relativePositions)
        {
            _worldPositions.Add(relativePos + _initialPos);
        }
    }

    //Commented out to prevent NPEs before _worldPositions was populated
    // private void OnDrawGizmos()
    // {
    //     foreach (Vector2 turnPoints in _worldPositions)
    //     {
    //         Gizmos.DrawWireSphere(turnPoints, 0.1f );
    //     }
    // }
}