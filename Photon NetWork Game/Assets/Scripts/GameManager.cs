using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] double Time;
    [SerializeField] double initializeTime;
    [SerializeField] int minute;
    [SerializeField] int second;
    [SerializeField] int millisecond;

    [SerializeField] Text timeText;

    [SerializeField] GameObject pausePanel;

    // Start is called before the first frame update
    void Start()
    {
        SetMouse(false);

        initializeTime = PhotonNetwork.Time;
    }

    // Update is called once per frame
    void Update()
    {
        Time = PhotonNetwork.Time - initializeTime;
        
        minute = (int)(Time / 60);
        second = (int)(Time % 60);
        millisecond = (int)((Time * 100) % 100);


        timeText.text = $"{minute:D2}:{second:D2}:{millisecond:D2}";
        //System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(Time);
        //Debug.Log($"{timeSpan.ToString(@"hh\:mm\:ss")}");
        if (photonView.IsMine) {
            if (Input.GetKeyDown(KeyCode.Escape)) {

                SetMouse(true);

                pausePanel.SetActive(true);

            }
        }
    }

    public void SetMouse(Boolean state) {
        if (photonView.IsMine) {
            Cursor.visible = state;
            Cursor.lockState = (CursorLockMode)Convert.ToInt32(!state);

        }
    }

    public void Continue() {
        if (photonView.IsMine) {
            {
                SetMouse(false);

                pausePanel.SetActive(false);
            }
        }
    }

    public void Exit() {
        PhotonNetwork.LeaveRoom();

    }

    public override void OnLeftRoom() {
        PhotonNetwork.LoadLevel("Lobby");
    }
    private void OnDestroy() {
        SetMouse(true);
    }
}
