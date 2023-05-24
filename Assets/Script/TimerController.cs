using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Photon.Realtime;
using TMPro;

public class TimerController : MonoBehaviourPunCallbacks, IPunObservable
{
    public float gameTime = 180f; // Duración total del juego en segundos
    public float currentGameTime; // Tiempo actual del juego
    public bool isTimerRunning; // Indica si el temporizador está en marcha
    public bool gameOverTime = false;

    public TMP_Text timerText; // Referencia al componente de texto que mostrará el temporizador

    private void Start()
    {
        currentGameTime = gameTime;
        isTimerRunning = false;
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            gameOverTime = false;
            currentGameTime -= Time.deltaTime; 
            
            if (currentGameTime <= 0f)
            {
                gameOverTime = true;
                isTimerRunning = false;
            }

            // Actualizar el texto del temporizador
            int minutes = Mathf.FloorToInt(currentGameTime / 60f);
            int seconds = Mathf.FloorToInt(currentGameTime - minutes * 60f);
            timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        }
    }

    public void StartTime()
    {
        isTimerRunning = true;
        gameOverTime = false;
    }

    public void RestartTime()
    {
        if (!isTimerRunning)
        {
            currentGameTime = gameTime;
            isTimerRunning = true;
            gameOverTime = false;
        }
    }

    // Método para sincronizar el temporizador a través de Photon
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(currentGameTime);
            stream.SendNext(isTimerRunning);
            stream.SendNext(gameOverTime);
        }
        else
        {
            currentGameTime = (float)stream.ReceiveNext();
            isTimerRunning = (bool)stream.ReceiveNext();
            gameOverTime = (bool)stream.ReceiveNext();
        }
    }
}
