using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] double Time;
    [SerializeField] double initializeTime;
    [SerializeField] int minute;
    [SerializeField] int second;
    [SerializeField] int millisecond;


    // Start is called before the first frame update
    void Start()
    {
        initializeTime = PhotonNetwork.Time;
    }

    // Update is called once per frame
    void Update()
    {
        Time = PhotonNetwork.Time - initializeTime;
        
        minute = (int)(Time / 60);
        second = (int)(Time % 60);
        millisecond = (int)((Time * 100) % 100);


        Debug.Log($"{minute:D2} : {second:D2} : {millisecond:D2}");
        Debug.Log("Time: " + minute + " : " + second + " : " + millisecond);
        //System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(Time);
        //Debug.Log($"{timeSpan.ToString(@"hh\:mm\:ss")}");
    }

    
}
