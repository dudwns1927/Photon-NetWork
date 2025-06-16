using PlayFab;
using Photon.Pun;
using UnityEngine;
using PlayFab.ClientModels;
using UnityEngine.UI;
using Photon.Realtime;

public class PlayfabManager : MonoBehaviourPunCallbacks
{
    [SerializeField] string version;
    [SerializeField] InputField emailInputField;
    [SerializeField] InputField passwordInputField;

    public void Success(LoginResult loginResult) {
        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.GameVersion = version;
        PhotonNetwork.ConnectUsingSettings();

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
        Debug.LogError(playFabError.GenerateErrorReport());
    }


}
