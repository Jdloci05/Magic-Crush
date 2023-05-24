using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PuntajeControlador : MonoBehaviour
{
    public Text puntajeP1; 
    public Text puntajeP2;

    public GameObject jugador1;
    public GameObject jugador2;

    public PhotonView idJugador1;
    public PhotonView idJugador2;

    public int puntaje1;
    public int puntaje2;
     void Start()
    {
        jugador1 = GameObject.Find("Player1");
        jugador2 = GameObject.Find("Player2");

        idJugador1 = jugador1.GetComponent<PhotonView>();
        idJugador2 = jugador2.GetComponent<PhotonView>();
    }

     void Update()
    {
        if (idJugador1.ViewID == 1)
        {

            puntajeP1.text = puntaje1.ToString();
        }
        if (idJugador2.ViewID == 2)
        {
            puntajeP2.text = puntaje2.ToString();
        }
    }

   
}
