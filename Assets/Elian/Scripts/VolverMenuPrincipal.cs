using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VolverMenuPrincipal : MonoBehaviour
{
   public void cambioEscena()
    {
        SceneManager.LoadScene("Lobby");
    }
}
