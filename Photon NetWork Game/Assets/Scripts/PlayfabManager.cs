using PlayFab;
using Photon.Pun;
using UnityEngine;
using PlayFab.ClientModels;
using UnityEngine.UI;
using Photon.Realtime;
using System.Collections;

public class PlayfabManager : MonoBehaviourPunCallbacks
{
    [SerializeField] string version;

    [SerializeField] GameObject failurePanel;
    [SerializeField] InputField emailInputField;
    [SerializeField] InputField passwordInputField;

    public void Success(LoginResult loginResult) {
        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.GameVersion = version;
        
        StartCoroutine(Connect());

    }


    public override void OnJoinedLobby() {
        PhotonNetwork.LoadLevel("Lobby");
    }

    IEnumerator Connect() {
        PhotonNetwork.ConnectUsingSettings();

        while(PhotonNetwork.IsConnectedAndReady == false) {
            yield return null;
        }

        PhotonNetwork.JoinLobby();
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
