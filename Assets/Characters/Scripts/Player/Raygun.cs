using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Raygun : MonoBehaviour
{
    [SerializeField]
    private GameObject _projectilePrefab;

    [SerializeField]
    private Transform _camera;

    [SerializeField]
    private float _cooldown;
    private float _elapsedTime;

    private PlayerInput _input;

    private void Awake()
    {
        _input = GetComponentInParent<PlayerInput>();
    }

    public void FixedUpdate()
    {
        if (_input.actions.FindAction("Fire").IsPressed())
            Shoot();
        _elapsedTime += Time.fixedDeltaTime;
    }

    public void Shoot()
    {
        if (_elapsedTime < _cooldown)
            return;

        Ray newRay = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);

        GameObject newProjectile = Instantiate(_projectilePrefab);

        newProjectile.transform.position = _camera.transform.position;
        newProjectile.transform.rotation = _camera.transform.rotation;

        newProjectile.SetActive(true);

        Projectile comp = newProjectile.GetComponentInChildren<Projectile>();

        if (!comp)
            comp = newProjectile.GetComponentInChildren<RotatingProjectile>();

        comp.SetTrajectory(comp.Speed * newRay.direction);

        _elapsedTime = 0;
    }
}
