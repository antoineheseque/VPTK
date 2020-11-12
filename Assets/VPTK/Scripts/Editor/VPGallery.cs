using UnityEngine;
using UnityEditor;

namespace VPTK.Editor.Window
{
    public class VPGalleryWindow : EditorWindow
    {
        [MenuItem("VPTK/Gallery %#G", false, 22)]
        private static void OpenGalleryWindow()
        {
            Debug.Log("opening window.");
        }
    }
}