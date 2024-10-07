using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{
    private Rigidbody _rigidBody;

    private Vector2 _inputDirection;

    private PlayerInput _input;

    private Grounded _grounded;


    [SerializeField]
    private float _maxSpeed;
    [SerializeField]
    private float _acceleration;

    [Space]

    [SerializeField]
    private float _jumpHeight;
    [SerializeField]
    private float _strafeSpeed;
    [SerializeField]
    private float _turnSpeed;

    private float _deltaTimeOffset = 100;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _input = GetComponent<PlayerInput>();
        _grounded = GetComponent<Grounded>();
    }

    private void Start()
    {
        _maxSpeed *= _deltaTimeOffset;
        _acceleration *= _deltaTimeOffset;
        _turnSpeed *= _deltaTimeOffset;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void LateUpdate()
    {
        //clamp velocity
        _rigidBody.velocity = _rigidBody.velocity.normalized * Mathf.Clamp(_rigidBody.velocity.magnitude, -_maxSpeed, _maxSpeed);
    
        //constrain XZ rotation, rigidbody constraints don't work.
        transform.Rotate(new Vector3(-transform.rotation.eulerAngles.x, 0, -transform.rotation.eulerAngles.z));
    }

    private void Jump()
    {
        if (_inputDirection.magnitude != 0)
        {
            Vector3 direction = transform.up.normalized + _rigidBody.velocity.normalized;

            _rigidBody.AddForce(direction.normalized * _jumpHeight, ForceMode.Impulse);

            return;
        }

        _rigidBody.AddForce(transform.up * _jumpHeight, ForceMode.Impulse);
    }

    private void GetInput()
    {
        _inputDirection = _input.actions.FindAction("Move").ReadValue<Vector2>();
        Mathf.Clamp(_inputDirection.x, -1, 1);
        Mathf.Clamp(_inputDirection.y, -1, 1);
    }

    private void RotatePlayer()
    {
        float delta = _inputDirection.x * _turnSpeed * Time.fixedDeltaTime;

        transform.Rotate(new Vector3(0, delta, 0));
    }

    private void StrafePlayer()
    {
        float newMagnitude = _inputDirection.x * _strafeSpeed * Time.fixedDeltaTime;

        Vector3 newDirection = Vector3.Cross(Vector3.up, transform.forward).normalized;

        _rigidBody.AddForce(newDirection * newMagnitude, ForceMode.VelocityChange);
    }

    private void MovePlayer()
    {
        GetInput();

        if (!_grounded.grounded)
        {
            StrafePlayer();
            //only accelerate/rotate when grounded
            return;
        }
        
        RotatePlayer();

        if (_input.actions.FindAction("Jump").IsPressed())   
            Jump();

        if (_inputDirection.y == 0)
            return;

        Vector3 newVelocity;

        if (_inputDirection.y > 0)
            newVelocity = transform.forward;
        else
            newVelocity = -transform.forward;

        newVelocity.Normalize();

        newVelocity *= _acceleration * Time.fixedDeltaTime;

        _rigidBody.AddForce(newVelocity, ForceMode.Acceleration);
    }
}
