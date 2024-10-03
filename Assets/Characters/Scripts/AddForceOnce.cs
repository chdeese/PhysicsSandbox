using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceOnce : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 1000), ForceMode.Impulse);
    }
}
