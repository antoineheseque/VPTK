using UnityEditor;
using UnityEngine;
using VPTK.Tools;

namespace VPTK.Editor.Window
{
    public partial class VPToolsWindow
    {
        private bool greenScreen = false;
        private LoadMRCamera loadMRCamera = null;
        private UnityEditor.Editor mrCameraEditor;

        private void CameraGUI()
        {
            EditorGUI.BeginChangeCheck();
            
            GUILayout.Label("Camera name: " + (selectedCamera != null ? selectedCamera.name : "NO CAMERA SELECTED"), EditorStyles.largeLabel);
            GUILayout.Label("Functionnalities", EditorStyles.largeLabel);
            
            // GREEN SCREEN TOGGLE GROUP
            greenScreen = EditorGUILayout.BeginToggleGroup("Enable Green Screen", greenScreen);
            
            VerifyGreenScreen();
            if(mrCameraEditor)
                mrCameraEditor.OnInspectorGUI();
            
            EditorGUILayout.EndToggleGroup();
            // END GREEN SCREEN TOGGLE GROUP
            
            if(EditorGUI.EndChangeCheck())
            {
                // There is something that changed from before
                Debug.Log(("Soooomething changed here"));
            }
        }
        
        private void VerifyGreenScreen()
        {
            // If Green screen is null, init one by creating it or finding it.
            if (greenScreen && loadMRCamera == null)
            {
                if (FindObjectOfType<LoadMRCamera>())
                    loadMRCamera = FindObjectOfType<LoadMRCamera>();
                else
                    loadMRCamera = new GameObject("VPTK_GreenScreen", typeof(LoadMRCamera)).GetComponent<LoadMRCamera>();
                mrCameraEditor = UnityEditor.Editor.CreateEditor(loadMRCamera);
            }
            else if(!greenScreen && (loadMRCamera != null || mrCameraEditor != null))
            {
                DestroyImmediate(loadMRCamera.gameObject);
                loadMRCamera = null;
                mrCameraEditor = null;
            }
        }
    }
}