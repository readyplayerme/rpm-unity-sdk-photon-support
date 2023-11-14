using ReadyPlayerMe.Core.Editor;
using UnityEditor;
using UnityEngine;

namespace ReadyPlayerMe.PhotonSupport.Editor
{
    public static class TransferPrefab
    {
        private const string NEW_PREFAB_PATH = "Assets/Ready Player Me/Resources/RPM_Photon_Character.prefab";
        private const string PREFAB_ASSET_NAME = "t:prefab RPM_Photon_Character";

        [MenuItem("Ready Player Me/Transfer Photon Prefab", false, priority = 34)]
        public static void Transfer()
        {
            var guids = AssetDatabase.FindAssets(PREFAB_ASSET_NAME);

            if (guids.Length == 0)
            {
                Debug.Log("RPM_Photon_Character prefab not found. Please reimport Photon Support package.");
            }
            else
            {
                if (AssetDatabase.LoadAssetAtPath(NEW_PREFAB_PATH, typeof(GameObject)))
                {
                    if (!EditorUtility.DisplayDialog("Warning", "RPM_Photon_Character prefab already exists. Do you want to overwrite it?", "Yes", "No"))
                    {
                        return;
                    }
                }
                PrefabHelper.TransferPrefabByGuid(guids[0], NEW_PREFAB_PATH);
                Debug.Log($"Photon prefab transferred to {NEW_PREFAB_PATH}");
            }
        }
    }
}
