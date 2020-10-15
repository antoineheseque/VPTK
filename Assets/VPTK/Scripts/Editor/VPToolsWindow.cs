using System;
using UnityEditor;
using UnityEngine;
using VPTK.Editor.Utility;

namespace VPTK.Editor.Window
{
    public class VPToolsWindow : EditorWindow
    {
        private void OnGUI()
        {
            GUILayout.Label("Test");
        }

        private void OnEnable()
        {
            //EditorWindow.focusedWindow.titleContent.tooltip = "TEST MEESS";
        }
    }
}