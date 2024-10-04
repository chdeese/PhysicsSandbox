using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    public float Speed { get => _speed; set => _speed = value; }

    public Vector3 Trajectory { get; set; }

    public Rigidbody Body { get; set; }

    public bool Active { get; set; } = false;

    public virtual void Awake ()
    {
         Body = GetComponent<Rigidbody>();
    }

    public void SetTrajectory(Vector3 trajectory)
    {
        Trajectory = trajectory;

        Active = true;
    }

    public virtual void FixedUpdate()
    {
        Body.AddForce(Trajectory, ForceMode.VelocityChange);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        Active = false;

        Destroy(gameObject);
    }
}
