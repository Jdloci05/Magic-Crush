using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUNTOSXP : MonoBehaviour
{
    public GameObject controladorXP;
    public int Puntajecontrolador1;

    public int Puntajecontrolador2;


    void Update()
    {

    }
    private void FixedUpdate()
    {
        controladorXP = GameObject.Find("controladorPUNTAJE");
        Puntajecontrolador1 = controladorXP.GetComponent<PuntajeControlador>().puntaje1;
        Puntajecontrolador2 = controladorXP.GetComponent<PuntajeControlador>().puntaje2;
    }
    // Start is called before the first frame update




}
