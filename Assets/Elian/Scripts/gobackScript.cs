using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gobackScript : MonoBehaviour
{

    public GameObject canvasPrincipal;
    public GameObject canvasPrincipal2;

    public void activarAnim()
    {
        Animator anim = gameObject.GetComponent<Animator>();

        anim.SetBool("sacar", true);
    }

    public void desactivarSacar()
    {
        Animator anim = gameObject.GetComponent<Animator>();

        anim.SetBool("sacar", false);
    }

    public void desactivarObjeto()
    {
        gameObject.SetActive(false);
    }

    public void activarCanvasPrincipal()
    {
        canvasPrincipal.SetActive(true);
        canvasPrincipal2.SetActive(true);
    }
}
