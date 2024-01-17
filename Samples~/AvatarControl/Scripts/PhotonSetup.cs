#if PHOTON_UNITY_NETWORKING && READY_PLAYER_ME
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

namespace ReadyPlayerMe.PhotonSupport
{
    public class PhotonSetup : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject UI;
        [SerializeField] private Button button;
        [SerializeField] private InputField inputField;

        private void Awake()
        {
            button.onClick.AddListener(OnButtonClicked);
            PhotonNetwork.AutomaticallySyncScene = true;
        }
        
        private void OnButtonClicked()
        {
            PhotonNetwork.GameVersion = "0.1.0";
            PhotonNetwork.ConnectUsingSettings();
        }
        
        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected to master");
            
            if (!string.IsNullOrEmpty(inputField.text))
            {
                PhotonNetwork.NickName = inputField.text;
                RoomOptions roomOptions = new RoomOptions();
                roomOptions.MaxPlayers = 10;
                PhotonNetwork.JoinOrCreateRoom("Ready Player Me", roomOptions, TypedLobby.Default);
            }
            else
            {
                Debug.Log("Please enter avatar URL");
            }
        }
        
        public override void OnJoinedRoom()
        {
            Debug.Log("Joined room");
            
            UI.SetActive(false);
            GameObject character = PhotonNetwork.Instantiate("RPM_Photon_Test_Character", Vector3.zero, Quaternion.identity);
            character.GetComponent<NetworkPlayer>().LoadAvatar(inputField.text);
        }
    }
}
#endif
