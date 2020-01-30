using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody _playerRidigbody;
    public Transform _goalPosition;
    public Animation _runningAnimation;

    public float _speedForceMove = 3f;
    private bool _usedKey;




    void Update()
    {
        float _distance = Vector3.Distance(transform.position, _goalPosition.position);

        if (GameManager._raceIsActive)
        {
            // The user has to switch key back and forth to get forward Speed

            if (!_usedKey && Input.GetKeyDown(KeyCode.RightArrow) && !Input.GetKeyDown(KeyCode.LeftArrow)) 
            {
                _usedKey = true;
                _playerRidigbody.AddForce(Vector3.forward * _speedForceMove * Time.fixedDeltaTime, ForceMode.Impulse);
                
            }

            if (_usedKey && Input.GetKeyDown(KeyCode.LeftArrow) && !Input.GetKeyDown(KeyCode.RightArrow))
            {
                _usedKey = false;
                _playerRidigbody.AddForce(Vector3.forward * _speedForceMove * Time.fixedDeltaTime, ForceMode.Impulse);
            }
        }
        
        // Animation speed Gets faster when the players rigidbodys velocity magnitude gets faster
        _runningAnimation["Take 001"].speed = _playerRidigbody.velocity.magnitude / 10f; 
    }
}

