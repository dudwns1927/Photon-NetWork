using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class MasterManager : MonoBehaviourPunCallbacks
{
    [SerializeField] WaitForSeconds waitForSeconds = new WaitForSeconds(5.0f);
    [SerializeField] Vector3 direction;
 
    private void Start() {
        if (PhotonNetwork.IsMasterClient) {
            StartCoroutine(Create());
        }
    }

    public IEnumerator Create() {
        while (true) {

            if (PhotonNetwork.CurrentRoom != null) {
                PhotonNetwork.InstantiateRoomObject("Unit", direction, Quaternion.identity);
            }
            yield return waitForSeconds;

        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient) {
        Debug.Log(newMasterClient);
        PhotonNetwork.SetMasterClient(PhotonNetwork.PlayerList[0]);
    }
}
