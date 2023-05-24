using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pausa : MonoBehaviour
{
    public GameObject canvas;
    private bool isCanvasActive = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isCanvasActive = !isCanvasActive; // Cambiar el estado del Canvas
            canvas.SetActive(isCanvasActive);
        }
    }
}
