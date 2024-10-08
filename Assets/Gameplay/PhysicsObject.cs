using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]
public class PhysicsObject : MonoBehaviour
{
    [SerializeField]
    private Material _awakeMaterial;

    [SerializeField]
    private Material _asleepMaterial;

    private Rigidbody _rigidBody;
    private MeshRenderer _renderer;
    private bool _wasSleeping = true;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        if(!_rigidBody)
            _rigidBody = GetComponentInChildren<Rigidbody>();
        _renderer = GetComponent<MeshRenderer>();
    }

    private void FixedUpdate()
    {
        UpdateMeshRenderer();
    }

    private void UpdateMeshRenderer()
    {
        if(_rigidBody.IsSleeping() && !_wasSleeping && _asleepMaterial != null)
        {
            _wasSleeping = true;
            _renderer.material = _asleepMaterial;
        }
        if(!_rigidBody.IsSleeping() && _wasSleeping && _awakeMaterial != null)
        {
            _wasSleeping = false;
            _renderer.material = _awakeMaterial;
        }
    }
}
