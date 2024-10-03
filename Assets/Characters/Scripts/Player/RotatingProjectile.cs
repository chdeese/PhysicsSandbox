using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingProjectile : Projectile
{
    [SerializeField]
    private float _rotateSpeed;

    private float _currentRotatingSpeed;

    private void Awake()
    {
        _currentRotatingSpeed = _rotateSpeed;
    }

    private void FixedUpdate()
    {
        if (Active == false)
            return;

        Vector3 deltaEuler = new Vector3(Body.rotation.x, Body.rotation.y + (_currentRotatingSpeed * Time.fixedDeltaTime), Body.rotation.z);

        Body.MoveRotation(Quaternion.Euler(deltaEuler));

        _currentRotatingSpeed -= Time.fixedDeltaTime;
    }


}
