using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class VPTKTripod : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform xAxisRot, yAxisRot, zAxisRot;

    private Vector3 defaultEulerY;
    private Vector3 defaultEulerZ;

    private void Awake()
    {
        defaultEulerY = yAxisRot.localEulerAngles + new Vector3(0, 83, 0);
        defaultEulerZ = zAxisRot.localEulerAngles;
    }

    private void Update()
    {
        yAxisRot.localRotation = Quaternion.Euler(defaultEulerY + new Vector3(0, cameraTransform.eulerAngles.y, 0));
        zAxisRot.localRotation = Quaternion.Euler(defaultEulerZ + new Vector3(0, 0, -cameraTransform.eulerAngles.x));
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(cameraTransform.position, cameraTransform.forward);
        //Gizmos.DrawRay(xAxisRot.position, xAxisRot.forward);
        Gizmos.DrawRay(yAxisRot.position, yAxisRot.forward);
        Gizmos.DrawRay(zAxisRot.position, zAxisRot.forward);
    }
}
