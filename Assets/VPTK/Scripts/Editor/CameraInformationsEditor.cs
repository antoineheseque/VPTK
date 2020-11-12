using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CameraInformations))]
public class CameraInformationsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    }
}
