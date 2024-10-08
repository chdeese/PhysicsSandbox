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

    [Space]

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

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
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

    private bool TryGetInput()
    {
        _inputDirection = _input.actions.FindAction("Move").ReadValue<Vector2>();

        if (_inputDirection.magnitude == 0)
        {
            if (_rigidBody.velocity.magnitude < 0.1f)
                _rigidBody.AddForce(-_rigidBody.velocity, ForceMode.VelocityChange);
            return false;
        }

        Mathf.Clamp(_inputDirection.x, -1, 1);
        Mathf.Clamp(_inputDirection.y, -1, 1);

        return true;
    }

    private void RotatePlayer()
    {
        Vector3 lookDirection = _input.actions.FindAction("Look").ReadValue<Vector2>();

        float delta = lookDirection.x * _turnSpeed * Time.fixedDeltaTime;

        transform.Rotate(new Vector3(0, delta, 0));
    }

    private void MovePlayer()
    {
        if (_input.actions.FindAction("Jump").IsPressed() && _grounded.grounded)   
            Jump();

        if (!TryGetInput())  
            return;

        Vector3 delta = Vector3.zero;

        if (_inputDirection.x != 0)
            if (_inputDirection.x > 0)
                delta += transform.right;
            else
                delta -= transform.right;
        if(_inputDirection.y != 0)
            if(_inputDirection.y > 0)
                delta += transform.forward;
            else
                delta -= transform.forward;

        delta.Normalize();

        delta *= _acceleration * Time.fixedDeltaTime;

        _rigidBody.AddForce(delta, ForceMode.Acceleration);
    }
}
