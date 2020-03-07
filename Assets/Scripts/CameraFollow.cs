using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;

    void Update()
    {
        Vector3 relativePos = _target.position - transform.position;
        transform.position = _target.position;
        Quaternion camPosition = transform.rotation;
        camPosition.eulerAngles = Vector3.Lerp(camPosition.eulerAngles, new Vector3(camPosition.eulerAngles.x, _target.eulerAngles.y, camPosition.eulerAngles.z), Time.deltaTime * 1);
        transform.rotation = camPosition;
    }
}
