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
        private int selected = 0;
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
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            
            int sel = selected;
            sel = EditorGUILayout.Popup("WEBCAM", selected, options.ToArray());

            if (!EditorGUI.EndChangeCheck()) return;

            // If selected webcam changed
            if (sel != selected)
            {
                selected = sel;
                selectedWebcam.stringValue = options[selected];
                serializedObject.ApplyModifiedProperties();
            }
            
            Debug.Log(("Soooomething changed here in LoadMRCameraEditor"));
        }
    }
}