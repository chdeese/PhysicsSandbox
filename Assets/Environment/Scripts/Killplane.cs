using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killplane : MonoBehaviour
{
    private void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile") || !other.CompareTag("Player"))
            return;

        Vector3 returnPoint;
        Grounded ground = other.GetComponent<Grounded>();
        if (ground)
            returnPoint = ground.LastPosition;
        else
        {
            ground = other.GetComponentInParent<Grounded>();
            if (ground)
                returnPoint = ground.LastPosition;
            else returnPoint = new Vector3(0, 10, 0);
        }
        other.transform.SetPositionAndRotation(returnPoint, Quaternion.identity);
    }
}
