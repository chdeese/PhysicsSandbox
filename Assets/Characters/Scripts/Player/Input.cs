using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{
    private Rigidbody _rigidBody;

    private Vector2 _inputDirection;

    private PlayerInput _input;

    [SerializeField]
    private float _acceleration;

    [SerializeField]
    private float _maxSpeed;

    [SerializeField]
    private float _turnSpeed;

    private float _deltaTimeOffset = 100;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _input = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        _maxSpeed *= _deltaTimeOffset;
        _acceleration *= _deltaTimeOffset;
    }

    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        RefreshInput();
    }

    private void GetInput()
    {
        _inputDirection += _input.actions.FindAction("Move").ReadValue<Vector2>();
        Mathf.Clamp(_inputDirection.x, -1, 1);
        Mathf.Clamp(_inputDirection.y, -1, 1);
    }

    private void MovePlayer()
    {
        Vector3 newVelocity;
        Quaternion newRotation;

        if(_inputDirection.y != 0) 
        { 
            if (_inputDirection.y > 0)
                newVelocity = transform.forward;
            else
                newVelocity = -transform.forward;

            newVelocity.Normalize();

            newVelocity *= _acceleration * Time.fixedDeltaTime;

            Vector3 maxVelocity = newVelocity.normalized * _maxSpeed * Time.fixedDeltaTime;

            if (_rigidBody.velocity.magnitude > maxVelocity.magnitude)
                _rigidBody.AddForce(maxVelocity, ForceMode.VelocityChange);
            else
                _rigidBody.AddForce(newVelocity, ForceMode.Acceleration);


        }


        newRotation = transform.rotation;

        newRotation.eulerAngles += new Vector3(0, _inputDirection.x * _turnSpeed * Time.fixedDeltaTime, 0);

        _rigidBody.MoveRotation(newRotation);
    }

    private void RefreshInput()
    {
        _inputDirection = new Vector2(0, 0);
    }


}
