using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Head : MonoBehaviourPunCallbacks
{
    [SerializeField] float speed;
    [SerializeField] float mouseX;

    void Update()
    {
        if (photonView.IsMine == false) return;

        RotateX();
    }

    public void RotateX() {
        // mouseX 에 마우스로 입력한 값을 저장합니다.
        mouseX += Input.GetAxisRaw("Mouse Y") * speed * Time.deltaTime;
        // mouseX 의 값을 -65 ~ 65 사이로 제한합니다.
        
        Mathf.Clamp(transform.localEulerAngles.x, -65, 65);
        transform.localEulerAngles = new Vector3(-mouseX, 0, 0);
    }
}
