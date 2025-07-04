using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class DlalogManager : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField inputField;
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] Transform parentTransform;

    private void Update() {
        
        if(Input.GetKeyDown(KeyCode.Return)) {
            inputField.ActivateInputField();

            if(inputField.text.Length <= 0) {
                return;
            }

            string talk = PhotonNetwork.LocalPlayer.NickName + " : " + inputField.text;

            //GameObject talk = Instantiate(Resources.Load<GameObject>("Talk"));
            //talk.transform.SetParent(parentTransform);
            //talk.GetComponent<Text>().text = inputField.text;

            //RTC Target.All : ���� �뿡 �ִ� ��� Ŭ���̾�Ʈ���� Talk() �Լ��� �����϶�� �����Ѵ�.

            photonView.RPC("talk", RpcTarget.All, inputField.text);

            inputField.text = "";

            inputField.ActivateInputField();
        }

    }

    [PunRPC]
    
    void talk(string message) {

        // prefab�� �ϳ� �����Ѵ��� text�� ���� �����մϴ�.
        GameObject talk = Instantiate(Resources.Load<GameObject>("Talk"));
        // prefab ������Ʈ��  text ������Ʈ�� �����ؼ� text �� ���� �����մϴ�.
        talk.GetComponent<Text>().text = message;
        // ��ũ�� �� - content ������Ʈ�� �ڽ����� ����մϴ�.
        talk.transform.SetParent(parentTransform);
        //canvas�� �������� ����ȭ��ŵ�ϴ�.
        Canvas.ForceUpdateCanvases();
        //��ũ�� ��ġ�� �ʱ�ȭ �մϴ�.
        scrollRect.verticalNormalizedPosition = 0.0f;
    }

}
