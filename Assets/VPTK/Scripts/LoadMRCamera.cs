using System;
using System.Linq;
using UnityEngine;

namespace VPTK.Tools
{
    public class LoadMRCamera : MonoBehaviour
    {
        public string selectedWebcam;
        private static readonly int MainTexture = Shader.PropertyToID("_BaseColorMap");

        private void Start()
        {
            // Show all the cameras in console
            Debug.Log($"[Camera Selector] Number of camera(s): {WebCamTexture.devices.Length}");
            foreach (var cam in WebCamTexture.devices)
                Debug.Log($"[Camera Selector] {cam.name}");

            // Make sure the camera we selected still exists
            if (WebCamTexture.devices.All(a => a.name != selectedWebcam))
            {
                Debug.LogError($"[Camera Selector] {selectedWebcam} not found.");
            }

            // Try init the camera and assign it to a texture. It may also exist but used by another software.
            if (InitCamera())
                Debug.Log($"[Camera Selector] {selectedWebcam} successfully started.");
            else
                Debug.LogError($"[Camera Selector] {selectedWebcam} can't be started. Is this camera already used?");
        }

        /// <summary>
        /// Init the camera
        /// </summary>
        /// <returns>true if camera is successfully set in a texture, else otherwise</returns>
        private bool InitCamera()
        {
            if (string.IsNullOrEmpty(selectedWebcam))
                return false;

            WebCamTexture webcamTexture = new WebCamTexture(selectedWebcam);
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
}