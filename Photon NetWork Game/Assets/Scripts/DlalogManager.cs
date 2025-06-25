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
            GameObject talk = Instantiate(Resources.Load<GameObject>("Talk"));

            talk.transform.SetParent(parentTransform);

            talk.GetComponent<Text>().text = inputField.text;

            inputField.text = "";

            inputField.ActivateInputField();
        }

    }


}
