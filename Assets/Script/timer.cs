using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class timer : MonoBehaviour
{
    public TMP_Text timerText;
    private float startTime;
    private bool isRunning = false;

    void Start()
    {
        // Iniciar el contador al comenzar el juego
        StartTimer();
    }

    void Update()
    {
        if (isRunning)
        {
            // Calcular el tiempo transcurrido desde el inicio
            float elapsedTime = Time.time - startTime;

            // Formatear el tiempo en minutos y segundos
            string minutes = Mathf.Floor(elapsedTime / 60).ToString("00");
            string seconds = Mathf.Floor(elapsedTime % 60).ToString("00");

            // Actualizar el texto del contador
            timerText.text = minutes + ":" + seconds;
        }
    }

    public void StartTimer()
    {
        // Iniciar el contador de tiempo
        startTime = Time.time;
        isRunning = true;
    }

    public void StopTimer()
    {
        // Detener el contador de tiempo
        isRunning = false;
    }

    public void ResetTimer()
    {
        // Reiniciar el contador de tiempo
        startTime = 0f;
        timerText.text = "00:00";
    }
}