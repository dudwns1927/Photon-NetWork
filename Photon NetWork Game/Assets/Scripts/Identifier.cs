using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using Photon.Pun;

public class Identifier : MonoBehaviourPunCallbacks
{
    [SerializeField] Text nameText;
    private void Awake() {
        Load();
    }

    void Load() {
        PlayFabClientAPI.GetAccountInfo(
            new GetAccountInfoRequest(),
            Success,
            Failuer
        );
    }

    void Success(GetAccountInfoResult getAccountInfoResult) {
        PhotonNetwork.LocalPlayer.NickName = getAccountInfoResult.AccountInfo.TitleInfo.DisplayName;

        if (photonView.IsMine){
            nameText.text = PhotonNetwork.LocalPlayer.NickName;
        } else {
            nameText.text = photonView.Owner.NickName;
        }

    }

    void Failuer(PlayFabError playFabError) {
        Debug.Log(playFabError.GenerateErrorReport());
    }
}
