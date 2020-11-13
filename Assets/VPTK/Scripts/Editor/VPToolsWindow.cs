using UnityEditor;
using UnityEngine;

namespace VPTK.Editor.Window
{
    public partial class VPToolsWindow : EditorWindow
    {
        public Camera selectedCamera = null;
        
        [MenuItem("VPTK/Virtual Production Tools %#V", false, 20)]
        private static void OpenToolsWindow()
        {
            GetWindow<VPToolsWindow>("Virtual Production Tools");
        }
        
        private int selectedToolbar = 0;
        private readonly string[] toolbarOptions = {"Camera", "Parameters", "Record"};

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

        private void OnHierarchyChange()
        {
            Repaint();
        }
    }
}