using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timer1 : MonoBehaviour
{
    
    public WaitingRoomCanvaController roomCanvaController;
    public GameObject cortinaCambioEscena;
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(IniciarConteoAtras());
    }
   

    IEnumerator IniciarConteoAtras()
    {
        yield return new WaitForSeconds(1.5f);
        if (roomCanvaController.contadorInicioPartida > 0)
        {
            RestaralContador(roomCanvaController);
        }
        yield return new WaitForSeconds(1f);
        if (roomCanvaController.contadorInicioPartida > 0)
        {
            RestaralContador(roomCanvaController);
        }
        yield return new WaitForSeconds(1f);
        if (roomCanvaController.contadorInicioPartida > 0)
        {
            RestaralContador(roomCanvaController);
        }
        yield return new WaitForSeconds(1f);
        if (roomCanvaController.contadorInicioPartida > 0)
        {
            RestaralContador(roomCanvaController);
        }
        yield return new WaitForSeconds(1f);
        if (roomCanvaController.contadorInicioPartida > 0)
        {
            RestaralContador(roomCanvaController);
        }
        yield return new WaitForSeconds(1f);
        if (roomCanvaController.contadorInicioPartida > 0)
        {
            RestaralContador(roomCanvaController);
        }
        yield return new WaitForSeconds(1f);
        if (roomCanvaController.contadorInicioPartida > 0)
        {
            RestaralContador(roomCanvaController);
        }
        yield return new WaitForSeconds(1f);
        if (roomCanvaController.contadorInicioPartida > 0)
        {
            RestaralContador(roomCanvaController);
        }
        yield return new WaitForSeconds(1f);
        if (roomCanvaController.contadorInicioPartida > 0)
        {
            RestaralContador(roomCanvaController);
        }
        yield return new WaitForSeconds(1f);
        if (roomCanvaController.contadorInicioPartida > 0)
        {
            RestaralContador(roomCanvaController);
        }
        
        if (roomCanvaController.contadorInicioPartida <= 0)
        {
            CargarPartida(cortinaCambioEscena);
        }



    }

    public void CargarPartida(GameObject cortina)
    {
        cortina.SetActive(true);
        //Aqui va el cambio de Escena para iniciar el juego
    }

    public void RestaralContador(WaitingRoomCanvaController roomCanvaController)
    {
        roomCanvaController.contadorInicioPartida--;
    }

}
