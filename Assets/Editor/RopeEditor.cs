using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RopeSpawner))]
public class RopeEditor : Editor
{
    [SerializeField]
    private GameObject _ropePrefab;

    [SerializeField]
    private GameObject _segmentPrefab;

    [SerializeField]
    private RopeSpawner _ropeSpawner;
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Generate"))
        {
            if (!_ropePrefab)
                return;


            _ropeSpawner = FindFirstObjectByType<RopeSpawner>();

            Transform cameraTransform = SceneView.lastActiveSceneView.camera.transform;

            //spawn rope
            GameObject ropeOrigin = (GameObject)PrefabUtility.InstantiatePrefab(_ropePrefab);
            GameObject current = ropeOrigin;
            current.transform.position = cameraTransform.position + (cameraTransform.forward * _ropeSpawner.distance);

            //add segments

            GameObject next;
            for (int i = 0; i < _ropeSpawner.count; i++)
            {
                //instantiate
                next = (GameObject)PrefabUtility.InstantiatePrefab(_segmentPrefab);
                next.transform.position = current.transform.position;

                //connect
                SpringJoint currentJoint = current.GetComponent<SpringJoint>();
                currentJoint.connectedBody = next.GetComponent<Rigidbody>();

                //set space between
                currentJoint.connectedBody.gameObject.transform.position += new Vector3(0, -_ropeSpawner.space, 0);

                next.transform.SetParent(ropeOrigin.transform);

                //iterate
                current = next;
            }
        }
        base.OnInspectorGUI();
    }
}
