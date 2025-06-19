using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] Vector3 direction;

    private void Start() {
        PhotonNetwork.Instantiate("Character", transform.position, Quaternion.identity);
    }
}
