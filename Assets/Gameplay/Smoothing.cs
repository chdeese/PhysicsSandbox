using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoothing : MonoBehaviour
{
    private float _speed;
    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }

    public Vector3 GetSmoothing(Vector3 target, Vector3 current, float deltaTime)
    {
        Vector3 difference = target - current;
        float newMagnitude = _speed * deltaTime;

        if (difference.magnitude > newMagnitude)
            return difference.normalized * newMagnitude;
        else
            return difference;
    }
}
