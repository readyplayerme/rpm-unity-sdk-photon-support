using ReadyPlayerMe.Core.Editor;
using UnityEditor;
using UnityEngine;

namespace ReadyPlayerMe.PhotonSupport.Editor
{
    public static class TransferPrefab
    {
        private const string PREFAB_PATH = "Assets/Ready Player Me/Resources/RPM_Photon_Character.prefab";

        [MenuItem("Ready Player Me/Transfer Photon Prefab", false, priority = 34)]
        public static void Transfer()
        {
            string[] guids = AssetDatabase.FindAssets("t:prefab RPM_Photon_Character");

            if (guids.Length == 0)
            {
                Debug.Log("RPM_Photon_Character prefab not found. Please reimport Photon Support package.");
            }
            else
            {
                if (AssetDatabase.LoadAssetAtPath(PREFAB_PATH, typeof(GameObject)))
                {
                    if (!EditorUtility.DisplayDialog("Warning", "RPM_Photon_Character prefab already exists. Do you want to overwrite it?", "Yes", "No"))
                    {
                        return;
                    }
                }
                PrefabTransferHelper.TransferPrefabByGuid(guids[0], PREFAB_PATH);
                Debug.Log($"Photon prefab transferred to {PREFAB_PATH}");
            }
        }
    }
}
