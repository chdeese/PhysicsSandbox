using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Grounded : MonoBehaviour
{
    [HideInInspector]
    public bool grounded;

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
    }
}
