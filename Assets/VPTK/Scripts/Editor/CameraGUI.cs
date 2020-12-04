using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using VPTK.Tools;

namespace VPTK.Editor.Window
{
    public partial class VPToolsWindow
    {
        private Vector2 lensShift;
        private Vector2 sensorSize;
        private bool hdr;
        private bool antialiasing;
        private float iso;
        private float foview;
        private float fov_axis;
        private float focal;
        private float shutterSpeed;
        private float aperture;
        private int AntialiasingMode = 0;
        public bool usePhysicalProperties;

        private void CameraGUI()
        {
            if (!selectedCamera) return;
            
            GUILayout.Label(selectedCamera.name);
            
            EditorGUI.BeginChangeCheck();
            
            GeneralSettings();
            EditorGUILayout.Space();
            PhysicalSettings();
            EditorGUILayout.Space();
            LensSettings();
            
            if(EditorGUI.EndChangeCheck())
            {
                // There is something that changed from before
                selectedCamera.lensShift = lensShift;
                GUILayout.BeginHorizontal();
                selectedCamera.sensorSize = sensorSize;
                GUILayout.EndHorizontal();
                selectedCamera.fieldOfView = foview;
                selectedCamera.allowHDR = hdr;
                selectedCamera.focalLength = focal;
                selectedCamera.GetComponent<HDAdditionalCameraData>().physicalParameters.shutterSpeed = shutterSpeed;
                selectedCamera.GetComponent<HDAdditionalCameraData>().physicalParameters.aperture = aperture;

            }
        }
        
        private void GeneralSettings() //general : fov, fov axis, hdr, antialiasing
        {
            GUILayout.Label("General settings", EditorStyles.boldLabel);
            EditorGUILayout.Space();


            hdr = EditorGUILayout.Toggle("Enable HDR", selectedCamera.allowHDR);
            foview = EditorGUILayout.Slider("Field Of View", selectedCamera.fieldOfView,0,179);
            //antialiasing = EditorGUILayout.Popup("Antialiasing", AntialiasingMode, selectedCamera.GetComponent<HDAdditionalCameraData>().antialiasing);
            //fov_axis = EditorGUILayout.SelectableLabel("Field Of View Axis", new GUILayoutOption[]);


        }

        private void PhysicalSettings()
        {
            GUILayout.Label("Physical settings", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            {
                //sensorType = EditorGUILayout.DropdownButton("sensor type", selectedCamera.GetComponent<HDAdditionalCameraData>().)
                
                sensorSize = EditorGUILayout.Vector2Field("Sensor Size", selectedCamera.sensorSize);
                iso = EditorGUILayout.Slider("ISO", selectedCamera.GetComponent<HDAdditionalCameraData>().physicalParameters.iso, 1,1000);
                shutterSpeed = EditorGUILayout.Slider("Shutter Speed", selectedCamera.GetComponent<HDAdditionalCameraData>().physicalParameters.shutterSpeed, 1,1000);

            }
            EditorGUILayout.Space();

        }
        private void LensSettings()
        {
            GUILayout.Label("Lens settings", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            {
                lensShift = EditorGUILayout.Vector2Field("Lens shift", selectedCamera.lensShift);
                focal = EditorGUILayout.Slider("Focal Length", selectedCamera.focalLength, 0, 100000);
                aperture = EditorGUILayout.Slider("Aperture", selectedCamera.GetComponent<HDAdditionalCameraData>().physicalParameters.aperture, 0, 179);
            }
            EditorGUILayout.Space();

        }
    }
}