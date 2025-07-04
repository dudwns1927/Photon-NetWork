using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks {
    [SerializeField] Transform parentTransform;

    [SerializeField] Dictionary<string, GameObject> dictionary = new Dictionary<string, GameObject>();

    public override void OnConnectedToMaster() {
        if (PhotonNetwork.InLobby == false) {
            PhotonNetwork.JoinLobby();
        }
    }

    public void OnCreateRoom() {
        RoomOptions roomOptions = new RoomOptions();

        roomOptions.MaxPlayers = 4;

        roomOptions.IsOpen = true;

        roomOptions.IsVisible = true;

        PhotonNetwork.CreateRoom("Battle", roomOptions);
    }

    public override void OnJoinedRoom() {
        PhotonNetwork.LoadLevel("Game");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList) {
        GameObject prefab = null;

        foreach(RoomInfo room in roomList) {
            // 룸이 삭제된 경우
            if (room.RemovedFromList == true) {
                dictionary.TryGetValue(room.Name, out prefab);

                Destroy(prefab);

                dictionary.Remove(room.Name);
            }
            else {// 룸의 정보가 변경되는 경우

                //룸이 처음 생성되는 경우
                if (dictionary.ContainsKey(room.Name)==false) {

                    GameObject clone = Instantiate(Resources.Load<GameObject>("Room"), parentTransform);

                    clone.GetComponent<Information>().Details(room.Name, room.PlayerCount, room.MaxPlayers);

                    dictionary.Add(room.Name, clone);

                } else { //룸이 갱신되었을 때
                    dictionary.TryGetValue(room.Name, out prefab);

                    prefab.GetComponent<Information>().Details(room.Name, room.PlayerCount, room.MaxPlayers);
                }
            }
        }
    }
    /*
    public override void OnCreatedRoom() {
        Debug.Log("Room Created");
    }

    public override void OnCreateRoomFailed(short returnCode, string message) {
        Debug.Log("Room Create Failed: " + message);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList) {
        foreach (var room in roomList) {
            if (room.RemovedFromList) {
                if (dictionary.ContainsKey(room.Name)) {
                    Destroy(dictionary[room.Name]);
                    dictionary.Remove(room.Name);
                }
            } else {
                if (!dictionary.ContainsKey(room.Name)) {
                    GameObject roomItem = new GameObject(room.Name);
                    roomItem.transform.SetParent(parenttransform);
                    dictionary.Add(room.Name, roomItem);
                }
            }
        }
    }*/
}