using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using VPTK.Tools;

namespace VPTK.Editor
{
    /// <summary>
    /// Show a dropdown with all the possible cameras in the LoadMRCamera script hierarchy.
    /// </summary>
    [CustomEditor(typeof(LoadMRCamera))]
    public class LoadMRCameraEditor : UnityEditor.Editor
    {
        private int selectedWebcamIndex = 0;
        private readonly List<string> options = new List<string>();

        private SerializedProperty selectedWebcam;

        private void OnEnable()
        {
            options.Clear();
            foreach (var cam in WebCamTexture.devices)
            {
                options.Add(cam.name);
            }
            
            selectedWebcam = serializedObject.FindProperty("selectedWebcam");
            selectedWebcam.stringValue = options[0];
            serializedObject.ApplyModifiedProperties();
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            
            int selected = EditorGUILayout.Popup("WEBCAM", selectedWebcamIndex, options.ToArray());

            if (!EditorGUI.EndChangeCheck()) return;

            // If selected webcam changed
            if (selected != selectedWebcamIndex)
            {
                selectedWebcamIndex = selected;
                selectedWebcam.stringValue = options[selectedWebcamIndex];
                serializedObject.ApplyModifiedProperties();
            }
            
            Debug.Log(("Soooomething changed here in LoadMRCameraEditor"));
        }
    }
}