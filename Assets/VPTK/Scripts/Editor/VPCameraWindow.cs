using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace VPTK.Editor.Window
{
    public class VPCameraWindow : EditorWindow
    {
        // ReSharper disable once InconsistentNaming
        private RenderTexture renderTexture;

        private readonly List<Camera> cameras = new List<Camera>();
        private int selectedCamera = 0;
        
        [MenuItem("VPTK/Camera Preview %#X", false, 21)]
        private static void OpenToolsWindow()
        {
            EditorWindow editorWindow = GetWindow<VPCameraWindow>("VP Camera");
            editorWindow.autoRepaintOnSceneChange = true;
            editorWindow.minSize.Set(800, 800);
            editorWindow.Show();
        }

        private void OnEnable()
        {
            ScanCameras();
        }

        public void Update()
        {
            if (cameras.Count <= 0)
                return;
            
            if (renderTexture == null)
            {
                renderTexture = new RenderTexture((int)position.width,
                    (int)position.height,
                    (int)RenderTextureFormat.ARGB32);
            }
            
            if (cameras[selectedCamera] != null)
            {
                cameras[selectedCamera].targetTexture = renderTexture;
                cameras[selectedCamera].Render();
                cameras[selectedCamera].targetTexture = null;
            }
            else
            {
                cameras[selectedCamera] = Camera.main;
            }
            
            if (renderTexture.width != position.width ||
                renderTexture.height != position.height)
                renderTexture = new RenderTexture((int)position.width,
                    (int)position.height,
                    (int)RenderTextureFormat.ARGB32);
        }

        void OnGUI()
        {
            EditorGUI.BeginChangeCheck();
            // Draw camera
            if(cameras.Count > 0 && renderTexture)
                GUI.DrawTexture(new Rect(0.0f, 0.0f, position.width, position.height), renderTexture);
            
            // Draw Toolbar
            DrawToolbar();
        }

        private void DrawToolbar()
        {
            EditorGUI.BeginChangeCheck();

            int selected = selectedCamera;
            GUILayout.BeginHorizontal(EditorStyles.toolbar);
            {
                // Show all cameras
                if(cameras.Count > 0)
                    selected = EditorGUILayout.Popup(selectedCamera, cameras.Select(cam => cam.name).ToArray(), EditorStyles.toolbarPopup, GUILayout.Width(150));
                else
                    EditorGUILayout.LabelField("No camera selected", EditorStyles.toolbarPopup, GUILayout.Width(150));

                EditorGUILayout.Space();
                
                // Show sync button to sync cameras
                if (GUILayout.Button("Scan cameras", EditorStyles.toolbarButton))
                    ScanCameras();
                
                GUILayout.FlexibleSpace();
            }
            GUILayout.EndHorizontal();

            if (!EditorGUI.EndChangeCheck()) return;

            if (selected != selectedCamera)
            {
                selectedCamera = selected;
                GetWindow<VPToolsWindow>().selectedCamera = cameras[selected];
            }
        }

        private void ScanCameras()
        {
            // List all cameras with their names
            cameras.Clear();
            Camera[] cams = FindObjectsOfType<Camera>();
            foreach (var cam in cams)
            {
                cameras.Add(cam);
            }
            
            // Make sure we can access to the camera
            if (selectedCamera > cameras.Count)
                selectedCamera = 0;
        }
    }
}