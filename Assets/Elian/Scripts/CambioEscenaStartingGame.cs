using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class CambioEscenaStartingGame : MonoBehaviour
{
    // Start is called before the first frame update
    public void CargarEscena(String nombreEscena)
    {
        PhotonNetwork.LoadLevel(nombreEscena);
    }
}
