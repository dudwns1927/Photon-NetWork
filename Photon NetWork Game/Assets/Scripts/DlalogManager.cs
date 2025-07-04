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

            //RTC Target.All : 현재 룸에 있는 모든 클라이언트에게 Talk() 함수를 실행하라고 전달한다.

            photonView.RPC("talk", RpcTarget.All, inputField.text);

            inputField.text = "";

            inputField.ActivateInputField();
        }

    }

    [PunRPC]
    
    void talk(string message) {

        // prefab을 하나 생성한다음 text에 값을 설정합니다.
        GameObject talk = Instantiate(Resources.Load<GameObject>("Talk"));
        // prefab 오브젝트의  text 컴포넌트로 접근해서 text 의 값을 설정합니다.
        talk.GetComponent<Text>().text = message;
        // 스크롤 뷰 - content 오브젝트에 자식으로 등록합니다.
        talk.transform.SetParent(parentTransform);
        //canvas를 수동으로 동기화시킵니다.
        Canvas.ForceUpdateCanvases();
        //스크롤 위치를 초기화 합니다.
        scrollRect.verticalNormalizedPosition = 0.0f;
    }

}
