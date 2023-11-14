#if PHOTON_UNITY_NETWORKING && READY_PLAYER_ME
using Photon.Pun;
using UnityEngine;
using ReadyPlayerMe.Core;

namespace ReadyPlayerMe.PhotonSupport
{
    /// <summary>
    ///     Used on Ready Player Me 
    /// </summary>
    [RequireComponent(typeof(PhotonView))]
    public class NetworkPlayer : MonoBehaviour
    {
        private const string SET_PLAYER_METHOD = "SetPlayer";
        [SerializeField] private AvatarConfig config;

        private Animator animator;
        private PhotonView photonView;

        private Transform leftEye;
        private Transform rightEye;
        private SkinnedMeshRenderer[] skinnedMeshRenderers;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            photonView = GetComponent<PhotonView>();

            leftEye = AvatarBoneHelper.GetLeftEyeBone(transform, true);
            rightEye = AvatarBoneHelper.GetRightEyeBone(transform, true);
            skinnedMeshRenderers = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
        }

        /// <summary>
        ///     Calls PunRPC with the avatar URL as paramater to load the local and remote avatars.
        /// </summary>
        /// <param name="url">Avatar URL</param>
        public void LoadAvatar(string url)
        {
            photonView.RPC(SET_PLAYER_METHOD, RpcTarget.AllBuffered, url);
        }

        [PunRPC]
        private void SetPlayer(string incomingUrl)
        {
            AvatarObjectLoader loader = new AvatarObjectLoader();
            loader.LoadAvatar(incomingUrl);
            loader.AvatarConfig = config;
            loader.OnCompleted += (sender, args) =>
            {
                leftEye.transform.localPosition = AvatarBoneHelper.GetLeftEyeBone(args.Avatar.transform, true).localPosition;
                rightEye.transform.localPosition = AvatarBoneHelper.GetRightEyeBone(args.Avatar.transform, true).localPosition;

                AvatarMeshHelper.TransferMesh(args.Avatar, skinnedMeshRenderers, animator);
                Destroy(args.Avatar);
            };
        }

    }
}
#endif
