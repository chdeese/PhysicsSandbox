using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    public float Speed { get => _speed; set => _speed = value; }

    private Vector3 _trajectory;

    public Rigidbody Body { get; set; }

    public bool Active { get; set; } = false;

    private void Awake()
    {
        Body = GetComponent<Rigidbody>();
    }

    public void SetTrajectory(Vector3 trajectory)
    {
        _trajectory = trajectory;

        Active = true;
    }

    private void FixedUpdate()
    {
        Body.AddForce(_trajectory, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        Active = false;

        Destroy(gameObject);
    }
}
