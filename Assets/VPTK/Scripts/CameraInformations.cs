using System;
using UnityEngine;

public class CameraInformations : MonoBehaviour
{
    private Camera cam;

    public float fov = 0;
    
    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        fov = cam.fieldOfView;
    }
}
