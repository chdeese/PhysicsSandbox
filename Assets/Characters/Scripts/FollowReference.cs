using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowReference : MonoBehaviour
{
    [SerializeField]
    private GameObject _reference;

    private void Update()
    {
        Vector3 reference = _reference.transform.position;

        gameObject.transform.position = new Vector3(reference.x, gameObject.transform.position.y, reference.z);

    }
}
