using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarAnimacion : MonoBehaviour
{
    public Animator animCanva;

    public GameObject canva1;
    public GameObject canva2;
    public GameObject canva3;   
    public GameObject canva4;

    public void AnimarHaciaAfuera()
    {
        animCanva.SetBool("sacar", true);
    }

    public void ponerFalseSacar()
    {
        animCanva.SetBool("sacar", false);
    }
        
    public void desactivarCanva()
    {
        gameObject.SetActive(false);
    }

    public void activarCanvasPrincipal()
    {
        canva1.SetActive(true);
    }

    public void activarCanvasCREAR()
    {
        canva2.SetActive(true);
    }
    public void activarCanvasUNIRSE()
    {
        canva3.SetActive(true);
    }

    public void activarCanvasFIND()
    {
        canva4.SetActive(true);
    }
}
