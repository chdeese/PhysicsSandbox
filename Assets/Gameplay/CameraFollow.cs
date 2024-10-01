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

        Quaternion newRotation = currentTransform.rotation;

        float eulerY;
        float eulerYDistance = targetTransform.eulerAngles.y - currentTransform.eulerAngles.y;
        float delta = (eulerYDistance/eulerYDistance * _smoothingSpeed * Time.fixedDeltaTime);
        if (delta > eulerYDistance)
            delta = eulerYDistance;

        eulerY = newRotation.eulerAngles.y + delta;

        if(eulerY > 360)
            eulerY -= 360;
        if (eulerY < 0)
            eulerY += 360;

        newRotation.eulerAngles = new Vector3 (newRotation.eulerAngles.x, eulerY, newRotation.eulerAngles.z);

        gameObject.transform.position = _cameraPosition.transform.position;
        gameObject.transform.rotation = newRotation;
    }
}
