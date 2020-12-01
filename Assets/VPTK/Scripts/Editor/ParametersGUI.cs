using System.Linq;
using UnityEditor;
using UnityEngine;
using VPTK.Tools;

namespace VPTK.Editor.Window
{
    public partial class VPToolsWindow
    {
        private bool greenScreen = false;
        private LoadMRCamera loadMRCamera = null;
        private UnityEditor.Editor mrCameraEditor;
        
        private int selected;
        private Light[] lights = null;
        
        private void ParametersGUI()
        {
            GreenScreenGUI();
            EditorGUILayout.Space();
            LightsGUI();
        }

        private void GreenScreenGUI()
        {
            GUILayout.Label("Global", EditorStyles.largeLabel);
            EditorGUI.BeginChangeCheck();
            
            greenScreen = EditorGUILayout.BeginToggleGroup("Enable Green Screen", greenScreen);
            
            VerifyGreenScreen();
            if(mrCameraEditor)
                mrCameraEditor.OnInspectorGUI();
            
            EditorGUILayout.EndToggleGroup();
            
            if(EditorGUI.EndChangeCheck())
            {
                // There is something that changed from before
                Debug.Log((" * - * "));
            }
        }
        
        private void VerifyGreenScreen()
        {
            // If Green screen is null, init one by creating it or finding it.
            if (greenScreen && loadMRCamera == null)
            {
                if (FindObjectOfType<LoadMRCamera>())
                    loadMRCamera = FindObjectOfType<LoadMRCamera>();
                else
                {
                    GameObject greenScreenPrefab = Resources.Load<GameObject>("Prefabs/GreenScreen");
                    loadMRCamera = Instantiate(greenScreenPrefab)
                        .GetComponent<LoadMRCamera>();
                    loadMRCamera.gameObject.name = "VP_GreenScreen";

                    //loadMRCamera = new GameObject("VPTK_GreenScreen", typeof(LoadMRCamera)).GetComponent<LoadMRCamera>();
                }

                mrCameraEditor = UnityEditor.Editor.CreateEditor(loadMRCamera);
            }
            else if(!greenScreen && (loadMRCamera != null || mrCameraEditor != null))
            {
                DestroyImmediate(loadMRCamera.gameObject);
                loadMRCamera = null;
                mrCameraEditor = null;
            }
        }

        private void LightsGUI()
        {
            GUILayout.Label("Lights", EditorStyles.largeLabel);
            EditorGUI.BeginChangeCheck();
            
            VerifyLights();
            if (lights.Length > 0)
            {
                selected = EditorGUILayout.Popup(selected, lights.Select(lumiere => lumiere.name).ToArray());
                
                GUILayout.Label("¤ Type : "+lights[selected].type);
                GUILayout.Label("¤ Luminosity : "+lights[selected].intensity);
                EditorGUILayout.ColorField("¤ Color: ", lights[selected].color);
                GUILayout.Label("¤ Color Temp : "+lights[selected].colorTemperature);
            }

            if(EditorGUI.EndChangeCheck())
            {
                // There is something that changed from before
                Debug.Log((" * - * "));
            }
        }
            
        private void VerifyLights()
        {
            Light[] lights = FindObjectsOfType<Light>();
            if (this.lights.Length == lights.Length) return;
            
            this.lights = lights;
            selected = Mathf.Min(selected, lights.Length);
        }
    }
}