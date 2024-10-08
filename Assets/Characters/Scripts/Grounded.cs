using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Grounded : MonoBehaviour
{
    [HideInInspector]
    public bool grounded;

    public Vector3 LastPosition { get; private set; } = new Vector3(0, 10, 0);

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Terrain"))
            return;
        grounded = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Terrain"))
            return;
        grounded = false;

        LastPosition = transform.position;
    }
}
