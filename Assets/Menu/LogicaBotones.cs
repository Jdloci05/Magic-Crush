using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaBotones : MonoBehaviour
{
    public GameObject ac;


    public void activar()
    {
        ac.SetActive(false);
    }
    public void desactivar()
    {
        ac.SetActive(true);
    }
}