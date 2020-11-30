using UnityEditor;
using UnityEngine;
using VPTK.Tools;

namespace VPTK.Editor.Window
{
    public partial class VPToolsWindow
    {
        private void CameraGUI()
        {
            //
            GUILayout.Label(selectedCamera.name);
        }
    }
}