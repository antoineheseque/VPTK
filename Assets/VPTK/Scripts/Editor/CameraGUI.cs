using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using VPTK.Tools;

namespace VPTK.Editor.Window
{
    public partial class VPToolsWindow
    {
        private void CameraGUI()
        {
            if (!selectedCamera) return;
            
            GUILayout.Label(selectedCamera.name);
            
            GeneralSettings();
            EditorGUILayout.Space();
            PhysicalSettings();
            EditorGUILayout.Space();
            LensSettings();
        }
        
        private void GeneralSettings() //general : fov, fov axis, hdr, antialiasing
        {
            GUILayout.Label("General settings", EditorStyles.toolbarDropDown);
            // faire fonctionner le dropdown
            EditorGUI.BeginChangeCheck();

            // HDR
                bool hdr = true;
                hdr = EditorGUILayout.Toggle("enable hdr", selectedCamera.allowHDR);
                //if(GUILayout.Button("hdr"))
                    //this.//condition hdr à remplir
            
                EditorGUILayout.Separator();
                    
                //Antialiasing
                bool antialiasing = true;
                antialiasing = EditorGUILayout.Toggle("enable antialiasing", true);
                //if(GUILayout.Button("hdr"))
                    //this.//condition hdr à remplir
                
                EditorGUILayout.Separator();
                
                float foview;
                foview = EditorGUILayout.Slider("Field Of View", selectedCamera.fieldOfView,0,179);
                // condition fov
                
                EditorGUILayout.Separator();
                
                float fov_axis;
                //fov_axis = EditorGUILayout.SelectableLabel("Field Of View Axis", new GUILayoutOption["vertical"]);
                // condition fov axis

            EditorGUI.EndChangeCheck();
            
        }

        private void PhysicalSettings()
        {
            GUILayout.Label("Physical settings", EditorStyles.toolbarDropDown);
            {
                // Physical : iso, sensor size, shutter speed
                
            }
        }
        private void LensSettings()
        {
            GUILayout.Label("Lens settings", EditorStyles.toolbarDropDown);
            {
                // lens :  focal, aperture
            }
        }
    }
}