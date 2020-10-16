using System;
using System.Collections;
using System.Net;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using VPTK.Editor.Utility;

namespace VPTK.Editor.Window
{
    public class VPToolsWindow : EditorWindow
    {
        [MenuItem("VPTK/Virtual Production Tools %#V", false, 20)]
        private static void OpenToolsWindow()
        {
            GetWindow<VPToolsWindow>("Virtual Production Tools");
        }
        
        #region #Variables

        private int selectedToolbar = 0;
        private string[] toolbarOptions = {"Camera", "Parameters", "Record"};
        
        #endregion
        
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

        private void CameraGUI()
        {
            GUILayout.Label("Camera");
        }

        private void ParametersGUI()
        {
            GUILayout.Label("Parameters");
        }


        private bool isRecording = false;
        private void RecordGUI()
        {
            GUILayout.Label("Record");
            
            if ((!isRecording && GUILayout.Button("Start recording")) || (isRecording && GUILayout.Button("Stop recording")))
            {
                isRecording = !isRecording;
                
                EditorCoroutineUtility.StartCoroutine(SendRECCommand(), this);
            }
        }

        private void OnEnable()
        {
            //EditorWindow.focusedWindow.titleContent.tooltip = "TEST MEESS";
        }

        private IEnumerator SendRECCommand()
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
    }
}