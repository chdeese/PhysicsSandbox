using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Raygun : MonoBehaviour
{
    [SerializeField]
    private GameObject _projectilePrefab;

    [SerializeField]
    private float _cooldown;
    private float _elapsedTime;

    public void FixedUpdate()
    {
        _elapsedTime += Time.fixedDeltaTime;
    }

    public void Shoot()
    {
        if (_elapsedTime < _cooldown)
            return;

        Ray newRay = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);

        GameObject newProjectile = Instantiate(_projectilePrefab);

        newProjectile.transform.position = transform.position;
        newProjectile.transform.rotation = transform.rotation;

        newProjectile.SetActive(true);

        Projectile comp = newProjectile.GetComponent<Projectile>();

        comp.SetTrajectory(comp.Speed * newRay.direction);

        _elapsedTime -= _cooldown;
    }
}
