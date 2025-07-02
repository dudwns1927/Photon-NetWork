using PlayFab;
using Photon.Pun;
using UnityEngine;
using PlayFab.ClientModels;
using UnityEngine.UI;
using Photon.Realtime;

public class PlayfabManager : MonoBehaviourPunCallbacks
{
    [SerializeField] string version;

    [SerializeField] GameObject failurePanel;
    [SerializeField] InputField emailInputField;
    [SerializeField] InputField passwordInputField;

    public void Success(LoginResult loginResult) {
        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.GameVersion = version;
        PhotonNetwork.ConnectUsingSettings();

    }

    public override void OnConnectedToMaster() {
        // JoinLobby : Ư�� �κ� �����Ͽ� �����ϴ� �Լ�
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby() {
        PhotonNetwork.LoadLevel("Lobby");
    }

    public void Access() {
        var request = new LoginWithEmailAddressRequest {
            Email = emailInputField.text,
            Password = passwordInputField.text,
        };

        PlayFabClientAPI.LoginWithEmailAddress(
            request,
            Success,
            Failure
        );
    }

    void Failure(PlayFabError playFabError) {
        failurePanel.GetComponent<Failure>().Message(playFabError.GenerateErrorReport());
        failurePanel.SetActive(true);
    }


}
