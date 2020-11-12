using System;
using System.Collections;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using VPTK.Tools;

namespace VPTK.Editor.Window
{
    public class VPToolsWindow : EditorWindow
    {
        public Camera selectedCamera = null;
        [MenuItem("VPTK/Virtual Production Tools %#V", false, 20)]
        private static void OpenToolsWindow()
        {
            GetWindow<VPToolsWindow>("Virtual Production Tools");
        }
        
        private int selectedToolbar = 0;
        private readonly string[] toolbarOptions = {"Camera", "Parameters", "Record"};

        private void OnGUI()
        {
            selectedToolbar = GUILayout.Toolbar(selectedToolbar, toolbarOptions);
            switch (selectedToolbar) {
                case 0:
                    CameraGUI();
                    break;
                case 1:
                    ParametersGUI();
                    break;
                case 2:
                    RecordGUI();
                    break;
            }
        }

        #region Camera
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
        #endregion
        
        #region PARAMETERS
        private void ParametersGUI()
        {
            EditorGUI.BeginChangeCheck();
            
            GUILayout.Label("Parameters");
            
            if(EditorGUI.EndChangeCheck())
            {
                // There is something that changed from before
            }
        }
        #endregion

        #region RECORD
        private bool isRecording = false;
        private void RecordGUI()
        {
            EditorGUI.BeginChangeCheck();
            
            GUILayout.Label("Record");
            
            if ((isRecording || !GUILayout.Button("Start recording")) &&
                (!isRecording || !GUILayout.Button("Stop recording"))) return;
            isRecording = !isRecording;
                
            EditorCoroutineUtility.StartCoroutine(SendRECCommand(), this);
            
            if(EditorGUI.EndChangeCheck())
            {
                // There is something that changed from before
            }
        }
        
        private static IEnumerator SendRECCommand()
        {
            using (UnityWebRequest www = UnityWebRequest.Get("192.168.0.80/api/cam/drivelens?zoom=tele3"))
            {
                yield return www.SendWebRequest();
 
                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.LogError(www.error);
                }
                else
                {
                    Debug.Log(www.downloadHandler.text);
                }
            }
        }
        #endregion
    }
}