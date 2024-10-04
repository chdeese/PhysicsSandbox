using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]
public class PhysicsObject : MonoBehaviour
{
    [SerializeField]
    private Material _awake;

    [SerializeField]
    private Material _asleep;

    private Rigidbody _rigidBody;
    private MeshRenderer _renderer;
    private bool _wasSleeping = true;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _renderer = GetComponent<MeshRenderer>();
    }

    private void FixedUpdate()
    {
        UpdateMeshRenderer();
    }

    private void UpdateMeshRenderer()
    {
        if(_rigidBody.IsSleeping() && !_wasSleeping && _asleep != null)
        {
            _wasSleeping = true;
            _renderer.material = _asleep;
        }
        if(!_rigidBody.IsSleeping() && _wasSleeping && _awake != null)
        {
            _wasSleeping = false;
            _renderer.material = _awake;
        }
    }
}
