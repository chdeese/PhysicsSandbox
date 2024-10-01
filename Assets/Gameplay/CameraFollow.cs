using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Smoothing))]
public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private GameObject _cameraPosition;

    [SerializeField]
    private float _smoothingSpeed;

    private Smoothing _smoothing;

    private void Awake()
    {
        _smoothing = GetComponent<Smoothing>();
        _smoothing.Speed = _smoothingSpeed;
    }

    private void Start()
    {
        gameObject.transform.position = _cameraPosition.transform.position;
        gameObject.transform.rotation = _cameraPosition.transform.rotation;
    }

    private void FixedUpdate()
    {
        Transform currentTransform = gameObject.transform;
        Transform targetTransform = _cameraPosition.transform;


        gameObject.transform.position = _cameraPosition.transform.position;
        gameObject.transform.rotation = targetTransform.rotation;
    }
}
