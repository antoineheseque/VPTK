using System;
using UnityEditor;
using UnityEngine;

public class LoadMRCamera : MonoBehaviour
{
    [SerializeField] private string cameraName;
    
    private WebCamDevice selectedWebcam;
    private static readonly int MainTexture = Shader.PropertyToID("_BaseColorMap");

    private void Start()
    {
        Debug.Log($"[Camera Selector] Number of camera(s): {WebCamTexture.devices.Length}");
        foreach (var cam in WebCamTexture.devices)
        {
            Debug.Log($"[Camera Selector] {cam.name}");
            
            if (cam.name == cameraName)
                selectedWebcam = cam;
        }

        if (InitCamera())
            Debug.Log($"[Camera Selector] {selectedWebcam.name} successfully started.");
        else
            Debug.LogError($"[Camera Selector] {selectedWebcam.name} can't be started. Is this camera already used?");
    }

    private bool InitCamera()
    {
        if (string.IsNullOrEmpty(selectedWebcam.name))
            return false;
        
        WebCamTexture webcamTexture = new WebCamTexture(selectedWebcam.name);
        GetComponent<Renderer>().material.SetTexture(MainTexture, webcamTexture);

        try
        {
            webcamTexture.Play();
        }
        catch
        {
            return false;
        }

        return true;
    }
}
