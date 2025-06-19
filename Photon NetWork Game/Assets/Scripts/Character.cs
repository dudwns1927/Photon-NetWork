using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Character : MonoBehaviourPun
{
    [SerializeField] Vector3 direction;

    [SerializeField] Camera VirutalCamera;
    [SerializeField] CharacterController characterController;

    private void Awake() {
        //캐릭터 컨트롤러 컴포넌트 가져오기
        characterController = GetComponent<CharacterController>();
        //가상 카메라 컴포넌트 가져오기
        VirutalCamera = GetComponentInChildren<Camera>();
    
    }

    void Start()
    {
        DisableCamera();
    }


    void Update()
    {
        if(photonView.IsMine) //내 캐릭터가 아니라면(다른 플레이어 캐릭터라면
            {
            Control();
            Move();
        }
        
        
    }

    public void Control() {
        direction.x = Input.GetAxis("Horizontal"); // 좌우 이동
        direction.z = Input.GetAxis("Vertical"); // 앞뒤 이동

        direction.Normalize(); // 방향 벡터 정규화
    }

    public void Move() {
        // 방향 * 속도 * 시간
        characterController.Move(direction * Time.deltaTime);
    }

    public void DisableCamera() {
        //현재 플레이어가 나 자신이라면
        if(photonView.IsMine) {
            //카메라 비활성화
            Camera.main.gameObject.SetActive(false);
        } else {
            VirutalCamera.gameObject.SetActive(false);
        }
    }
}
