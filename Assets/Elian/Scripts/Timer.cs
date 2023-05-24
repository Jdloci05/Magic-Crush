using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public Text timeText;
    public PuntajeControlador puntajeControlador;

    public GameObject pantallaAMARILLO;
    public GameObject pantallaNEGRO;
    public GameObject pantallaEMPATE;

    public GameObject canvaPrincipal;
    private void Start()
    {
        puntajeControlador = FindObjectOfType<PuntajeControlador>();
        // Starts the timer automatically
        timerIsRunning = true;
    }
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                
                timeRemaining = 0;
                timerIsRunning = false;
                TerminarPartida();
            }
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void TerminarPartida()
    {

        canvaPrincipal.SetActive(false);
        Time.timeScale = 0f;
        if (puntajeControlador.puntaje1 > puntajeControlador.puntaje2)
        {
            pantallaAMARILLO.SetActive(true);
        }
        if (puntajeControlador.puntaje1 < puntajeControlador.puntaje2)
        {
            pantallaNEGRO.SetActive(true);
        }

        if (puntajeControlador.puntaje1 == puntajeControlador.puntaje2)
        {
            pantallaEMPATE.SetActive(true);
        }
    }
}
