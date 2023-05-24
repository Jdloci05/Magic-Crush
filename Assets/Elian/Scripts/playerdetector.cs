using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class playerdetector : MonoBehaviour
{

    public GameObject letreroRemaining;
    
    // Update is called once per frame
    void Update()
    {
       
        if (PhotonNetwork.CurrentRoom.PlayerCount== 2)
        {
            letreroRemaining.SetActive(false);
            PhotonNetwork.LoadLevel("ArenaGame");
        }
    }
}
