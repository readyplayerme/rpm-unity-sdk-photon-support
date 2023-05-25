#if PHOTON_UNITY_NETWORKING && READY_PLAYER_ME
using Photon.Pun;
using UnityEngine;
using ReadyPlayerMe.AvatarLoader;

namespace ReadyPlayerMe.PhotonSupport
{
    /// <summary>
    ///     Used on Ready Player Me 
    /// </summary>
    [RequireComponent(typeof(PhotonView))]
    public class NetworkPlayer : MonoBehaviour
    {
        [SerializeField] private Transform leftEye;
        [SerializeField] private Transform rightEye;
        [SerializeField] private AvatarConfig config;
        [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
        
        private Animator animator;
        private PhotonView photonView;
    
        private const string FULL_BODY_LEFT_EYE_BONE_NAME = "Armature/Hips/Spine/Spine1/Spine2/Neck/Head/LeftEye";
        private const string FULL_BODY_RIGHT_EYE_BONE_NAME = "Armature/Hips/Spine/Spine1/Spine2/Neck/Head/RightEye";
        
        private void Awake()
        {
            animator = GetComponent<Animator>();
            photonView = GetComponent<PhotonView>();
        }

        /// <summary>
        ///     Calls PunRPC with the avatar URL as paramater to load the local and remote avatars.
        /// </summary>
        /// <param name="url">Avatar URL</param>
        public void LoadAvatar(string url)
        {
            photonView.RPC("SetPlayer", RpcTarget.AllBuffered, url);
        }

        [PunRPC]
        private void SetPlayer(string incomingUrl)
        {
            AvatarObjectLoader loader = new AvatarObjectLoader();
            loader.LoadAvatar(incomingUrl);
            loader.AvatarConfig = config;
            loader.OnCompleted += (sender, args) =>
            {
                leftEye.transform.localPosition = args.Avatar.transform.Find(FULL_BODY_LEFT_EYE_BONE_NAME).localPosition;
                rightEye.transform.localPosition = args.Avatar.transform.Find(FULL_BODY_RIGHT_EYE_BONE_NAME).localPosition;
                
                TransferMesh(args.Avatar);
            };
        }

        //TODO: Multiple mesh transfer support.
        private void TransferMesh(GameObject source)
        {
            Animator sourceAnimator = source.GetComponentInChildren<Animator>();
            SkinnedMeshRenderer sourceMesh = source.GetComponentInChildren<SkinnedMeshRenderer>();

            Mesh mesh = sourceMesh.sharedMesh;
            skinnedMeshRenderer.sharedMesh = mesh;

            Material[] materials = sourceMesh.sharedMaterials;
            skinnedMeshRenderer.sharedMaterials = materials;
            
            Avatar avatar = sourceAnimator.avatar;
            animator.avatar = avatar;

            Destroy(source);
        }
    }
}
#endif
