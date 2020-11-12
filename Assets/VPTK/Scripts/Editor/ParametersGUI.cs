using UnityEditor;
using UnityEngine;

namespace VPTK.Editor.Window
{
    public partial class VPToolsWindow
    {
        private void ParametersGUI()
        {
            EditorGUI.BeginChangeCheck();
            
            GUILayout.Label("Parameters");
            
            if(EditorGUI.EndChangeCheck())
            {
                // There is something that changed from before
            }
        }
    }
}