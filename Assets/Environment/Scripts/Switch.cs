using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefab;

    [SerializeField]
    private GameObject _spawnPoint;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Projectile"))
            return;

        _prefab = Instantiate(_prefab);

        _prefab.transform.position = _spawnPoint.transform.position;
        _prefab.transform.rotation = _spawnPoint.transform.rotation;
        _prefab.SetActive(true);

        enabled = false;
    }
}
