using UnityEngine;
using UnityEditor;
using VPTK.Editor.Utility;

namespace VPTK.Editor
{
    [CustomEditor(typeof(VPTK))]
    public class VPTKEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            Debug.Log("Opening VPTK window.");
            base.OnInspectorGUI();
        }
        
        [MenuItem("VPTK/Switch to Virtual Production Layout", false, 1)]
        private static void SwitchLayout()
        {
            LayoutUtility.LoadLayout("Assets/VPTK/Layout/VPLayout.wlt");
            Debug.Log("[VPTK] Virtual Production Layout loaded. Switch back to default layout by clicking on the top-right button 'VP Layout' and select 'Default'.");
        }
        
        [MenuItem("VPTK/(TEMP) Save current layout as VPLayout", false, 2)]
        private static void SaveLayout()
        {
            LayoutUtility.SaveLayout("Assets/VPTK/Layout/VPLayout.wlt");
            Debug.Log("[VPTK] Layout saved.");
        }
    }
}