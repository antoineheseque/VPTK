using System.Collections;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEditor.Recorder;
using UnityEngine;
using UnityEngine.Networking;

namespace VPTK.Editor.Window
{
    public partial class VPToolsWindow
    {
        private RecorderWindow recorderWindow;

        private bool isRecording = false;
        private void RecordGUI()
        {
            //recorderWindow = GetWindow<RecorderWindow>(false);
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.HelpBox("\n\nDon't forget to add at least one Recorder option\n\n", MessageType.Info);
            EditorGUILayout.HelpBox("In your recorder option, please select:\nCapture: Targeted Camera" +
                                    "\nSource: MainCamera\nFeel free to change the other options as you wish.", MessageType.Warning);
            EditorGUILayout.EndHorizontal();
            if(GUILayout.Button("Change recording parameters"))
            {
                GetWindow<RecorderWindow>("Recorder", true);
            }
            EditorGUILayout.Space();
            GUILayout.Label("Record");
            
            if ((isRecording || !GUILayout.Button("Start recording")) &&
                (!isRecording || !GUILayout.Button("Stop recording"))) return;
            isRecording = !isRecording;

            if (isRecording)
            {
                if (recorderWindow == null)
                {
                    recorderWindow = GetWindow<RecorderWindow>("Recorder", false, typeof(VPToolsWindow));
                    Focus();
                }
                
                
                recorderWindow.StartRecording();

                EditorCoroutineUtility.StartCoroutine(CheckIfRecordStarted(), this);
            }
            else
            {
                recorderWindow.StopRecording();
                
                if (recorderWindow != null)
                {
                    recorderWindow.Close();
                    recorderWindow = null;
                }
            }

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

        private IEnumerator CheckIfRecordStarted()
        {
            yield return new EditorWaitForSeconds(3.0f);

            if (!recorderWindow.IsRecording())
            {
                isRecording = false;
                Debug.LogWarning("PROBLEME LORS DU RECORD, EST-CE QUE TU AS MIS UN MOVIERECORDER ?");
            }
        }
    }
}