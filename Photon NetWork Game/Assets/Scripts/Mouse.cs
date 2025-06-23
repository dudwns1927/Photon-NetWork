using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviourPun
{
    void Start()
    {      
        SetMouse(false);
    }

    public void SetMouse(Boolean state) {
        if (photonView.IsMine) {
            Cursor.visible = state;
            Cursor.lockState = (CursorLockMode)Convert.ToInt32(!state);
        
        }
    }

    private void OnDestroy() {
        SetMouse(true);
    }
}
