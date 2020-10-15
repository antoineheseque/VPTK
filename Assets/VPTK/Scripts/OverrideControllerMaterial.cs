using UnityEngine;
using Valve.VR;

/// <summary>
/// Override the material and texture of the HTC Vive controllers, with your own material after SteamVR has loaded and
/// applied the original material. This is useful to help preserve interactions in the model itself.
///
/// NOTE: This is only compatible with the default HTC vive controllers (see UpdateControllerMaterial() below).
///
/// Modified by Antoine HESEQUE and Patrick Nelson / chunk_split (pat@catchyour.com) from original "OverrideControllerTexture" class
/// by Mr_FJ (from https://steamcommunity.com/app/358720/discussions/0/357287304420388604/) to allow override of full
/// material instead of just textures and also utilize the latest SteamVR_Events model.
///
/// See also: https://forum.unity.com/threads/changing-the-htc-vive-controller-model-with-a-custom-hand-model.395107/
/// </summary>
namespace VPTK.Tools
{
    public class OverrideControllerMaterial : MonoBehaviour
    {
        #region Public variables

        [Header("Variables")] public Material defaultMaterial;
        public bool fixTiling = true; // Check this to correct the texture orientation.

        #endregion

        void OnEnable()
        {
            //Subscribe to the event that is called by SteamVR_RenderModel, when the controller and it's associated material has been loaded completely.
            SteamVR_Events.RenderModelLoaded.Listen(OnControllerLoaded);
        }

        void OnDisable()
        {
            //Unsubscribe to the event if this object is disabled.
            SteamVR_Events.RenderModelLoaded.Remove(OnControllerLoaded);
        }

        /// <summary>
        /// Override the material of each of the parts, with your custom material.
        /// </summary>
        /// <param name="newMaterial">Override material</param>
        /// <param name="modelTransform">Transform of the gameobject, which has the SteamVR_RenderModel component.</param>
        /// <param name="modelTransform">Correct the texture orientation (if it looks offset incorrectly)</param>
        public void UpdateControllerMaterial(Material newMaterial, Transform modelTransform, bool fixTiling = true)
        {
            if (fixTiling)
            {
                // Internally aligns the textures so that they wrap properly around our models the same as the original
                // "onepointfive" texture. Prevents you from having to manually flip/rotate the texture yourself in PhotoShop. 
                newMaterial.mainTextureScale = new Vector2(1, -1);
            }

            for (int i = 0; i < modelTransform.childCount; i++)
            {
                Transform tr = modelTransform.GetChild(i);
                if (tr.GetComponent<MeshRenderer>())
                    tr.GetComponent<MeshRenderer>().material = newMaterial;
            }
        }

        /// <summary>
        /// Call this method, when the "RenderModelLoaded" event is triggered.
        /// </summary>
        /// <param name="controllerRenderModel"></param>
        /// <param name="success"></param>
        void OnControllerLoaded(SteamVR_RenderModel controllerRenderModel, bool success)
        {
            UpdateControllerMaterial(defaultMaterial, controllerRenderModel.gameObject.transform, fixTiling);
        }
    }
}