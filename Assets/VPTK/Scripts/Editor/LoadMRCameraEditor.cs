using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Show a dropdown with all the possible cameras in the LoadMRCamera script hierarchy.
/// </summary>
[CustomEditor(typeof(LoadMRCamera))]
public class LoadMRCameraEditor : Editor
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
        selected = options.FindIndex(a => a == selectedWebcam.stringValue);
    }

    public override void OnInspectorGUI()
    {
        selected = EditorGUILayout.Popup("WEBCAM", selected, options.ToArray());
        selectedWebcam.stringValue = options[selected];
        
        serializedObject.ApplyModifiedProperties();
    }
}
