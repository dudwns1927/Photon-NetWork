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
        //ĳ���� ��Ʈ�ѷ� ������Ʈ ��������
        characterController = GetComponent<CharacterController>();
        //���� ī�޶� ������Ʈ ��������
        VirutalCamera = GetComponentInChildren<Camera>();
    
    }

    void Start()
    {
        DisableCamera();
    }


    void Update()
    {
        if(photonView.IsMine) //�� ĳ���Ͱ� �ƴ϶��(�ٸ� �÷��̾� ĳ���Ͷ��
            {
            Control();
            Move();
        }
        
        
    }

    public void Control() {
        direction.x = Input.GetAxis("Horizontal"); // �¿� �̵�
        direction.z = Input.GetAxis("Vertical"); // �յ� �̵�

        direction.Normalize(); // ���� ���� ����ȭ
    }

    public void Move() {
        // ���� * �ӵ� * �ð�
        characterController.Move(direction * Time.deltaTime);
    }

    public void DisableCamera() {
        //���� �÷��̾ �� �ڽ��̶��
        if(photonView.IsMine) {
            //ī�޶� ��Ȱ��ȭ
            Camera.main.gameObject.SetActive(false);
        } else {
            VirutalCamera.gameObject.SetActive(false);
        }
    }
}
