using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSpawner : MonoBehaviour
{
    [SerializeField]
    private int _segmentCount;

    public int count { get => _segmentCount; }

    [SerializeField]
    private float _distanceFromCamera;

    public float distance { get => _distanceFromCamera; }

    [SerializeField]
    private float _spaceBetween = 1;

    public float space { get => _spaceBetween; }

}
