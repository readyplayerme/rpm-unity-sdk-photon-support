using UnityEditor;
using UnityEngine;

namespace ReadyPlayerMe.PhotonSupport.Editor
{
    public static class TransferPrefab
    {
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
                if (AssetDatabase.LoadAssetAtPath("Assets/Ready Player Me/Resources/RPM_Character.prefab", typeof(GameObject)))
                {
                    if (!EditorUtility.DisplayDialog("Warning", "RPM_Character prefab already exists. Do you want to overwrite it?", "Yes", "No"))
                    {
                        return;
                    }
                }

                string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                AssetDatabase.CopyAsset(path, "Assets/Ready Player Me/Resources/RPM_Character.prefab");
                Selection.activeObject = AssetDatabase.LoadAssetAtPath("Assets/Ready Player Me/Resources/RPM_Character.prefab", typeof(GameObject));
                Debug.Log("Photon prefab transferred to Assets/Ready Player Me/Resources/RPM_Character.prefab");
            }
        }
    }
}
