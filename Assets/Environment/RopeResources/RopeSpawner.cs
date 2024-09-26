using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _ropeSegmentPrefab;

    [SerializeField]
    private int _segmentCount;


    private void Start()
    {
        GameObject current = gameObject;
        GameObject next;
        for(int i = 0; i < _segmentCount; i++)
        {
            //instantiate
            next = Instantiate(_ropeSegmentPrefab, current.transform);

            //connect
            SpringJoint currentJoint = current.GetComponent<SpringJoint>();
            currentJoint.connectedBody = next.GetComponent<Rigidbody>();

            //iterate
            current = next;
        }
    }
}
