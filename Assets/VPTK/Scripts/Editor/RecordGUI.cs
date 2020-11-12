using System.Collections;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace VPTK.Editor.Window
{
    public partial class VPToolsWindow
    {
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
    }
}