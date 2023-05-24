using UnityEngine;
using UnityEngine.SceneManagement;

public class salir : MonoBehaviour
{
    public string sceneName; // Nombre de la escena a la que deseas cambiar

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName); // Cambia a la escena especificada
    }
}
