using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class WaitingRoomCanvaController : MonoBehaviour
{
    [SerializeField] TMP_Text roomName;
    [SerializeField] TMP_Text waitingorstartingtext;

    public GameObject contadorConteo;
    [SerializeField] TMP_Text counterToStartGame;

    public int playercont;

    public int contadorInicioPartida = 10;

    
    private void Start()
    {
        roomName.text = PhotonNetwork.CurrentRoom.Name;
        
    }
    private void Update()
    {
       
        playercont = PhotonNetwork.CurrentRoom.PlayerCount;
        counterToStartGame.text = contadorInicioPartida.ToString();

        if (playercont >= 2)
        {
            waitingorstartingtext.text = "Preparing arena...";
            contadorConteo.SetActive(true);
        }
        else
        {
            waitingorstartingtext.text = "Waiting for a player...";
            
            
        }
    }

    

}
