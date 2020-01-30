using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentRunning : MonoBehaviour
{
    private Rigidbody _rigidBody;
    public static float _speedForce = 200f;
    public static float _maxVelocity = 15;
    public static float _randomOffsetValue = 3f;
    private float _maxVelocityRandomValue;
    private Animation _runningAnimation;



    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _maxVelocityRandomValue = Random.Range(_maxVelocity - _randomOffsetValue, _maxVelocity + _randomOffsetValue); // Gets a random Max Velocity Value 

        if (GetComponent<Animation>())
        {
            _runningAnimation = GetComponent<Animation>();
        }

        SetRunnerSpeed(_maxVelocityRandomValue);
    }




    // Sets The runners Speed With The Random Value
    public void SetRunnerSpeed(float velocity)
    {
        _maxVelocityRandomValue = velocity;
    }





    void Update()
    {
        if (GameManager._raceIsActive)
        {
            if (_rigidBody.velocity.magnitude < _maxVelocityRandomValue)
            {
                _rigidBody.AddRelativeForce(0f, 0f, _speedForce * Time.deltaTime);
            }
        }

        if (_runningAnimation != null)
        {
            _runningAnimation["Take 001"].speed = _rigidBody.velocity.magnitude / 10f;
        }
    }
}
