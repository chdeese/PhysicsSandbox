using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UnityChanAnimationCycle : MonoBehaviour
{
    private Animator _animator;

    private Rigidbody _rigidBody;

    [SerializeField]
    private BoxCollider _walkCollider;

    [SerializeField]
    private float _maxSpeed;

    [SerializeField]
    private float _acceleration;

    [SerializeField]
    private float _direction;

    [SerializeField]
    private float _turnSpeed;

    [SerializeField]
    private float _jumpHeight;

    [SerializeField]
    private float _ragdollDuration;

    [SerializeField]
    private float _ragdollWait;

    private float _currentSpeed = 1;

    private bool _jumping;

    private bool _ragdollEnabled = false;

    private Grounded _grounded;

    private void Awake()
    {
        _grounded = GetComponentInChildren<Grounded>();

        //_animator.SetFloat("GravityControl", 0.5f);
        _rigidBody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
            rb.isKinematic = true;

        GetComponent<Rigidbody>().isKinematic = false;
    }

    private void FixedUpdate()
    {
        if (transform.position.y < -1)
        {
            _rigidBody.Move(new Vector3(transform.position.x, 1, transform.position.z), Quaternion.identity);
            _rigidBody.velocity = Vector3.up;
        }
            

        if (_jumping || !_grounded.grounded)
            return;

        if (_currentSpeed >= _maxSpeed)
        {
            _currentSpeed = _maxSpeed;

            Jump();
        }
        else
            _currentSpeed += _acceleration * Time.fixedDeltaTime;

        _animator.SetFloat("Speed", _currentSpeed);
        _animator.SetFloat("Direction", _direction);

        float delta = _currentSpeed * Time.fixedDeltaTime;

        if (!_ragdollEnabled)
            _rigidBody.AddForce(transform.forward.normalized * delta, ForceMode.VelocityChange);

        if(_direction > 0 || _direction < 0)
        {
            Quaternion rotation = transform.rotation;
            float deltaY = _direction * _turnSpeed * Time.fixedDeltaTime;
            rotation.eulerAngles += new Vector3(0, deltaY, 0);
            transform.rotation = rotation;
        }
    }

    private void Jump()
    {
        if (_jumping || !_grounded)
            return;

        _jumping = true;
        _animator.SetBool("Jump", _jumping);
        _animator.SetFloat("JumpHeight", _jumpHeight);

        _rigidBody.AddForce(new Vector3(0, _jumpHeight, 0), ForceMode.Impulse);

        Invoke("BeginRagdoll", _ragdollWait);
    }

    public void BeginRagdoll()
    {
        StopJumping();

        _animator.enabled = false;
        _ragdollEnabled = true;

        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
            rb.isKinematic = false;

        _walkCollider.enabled = false;
    }

    public void StopJumping()
    {
        _jumping = false;
        _animator.SetBool("Jump", _jumping);
        _animator.SetFloat("JumpHeight", _jumpHeight);
    }

    public void EndRagdoll()
    {
        _animator.enabled = true;
        _ragdollEnabled = false;
        _walkCollider.enabled = true;

        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
            rb.isKinematic = true;

        GetComponent<Rigidbody>().isKinematic = false;

        _currentSpeed = 0;

    }


}
