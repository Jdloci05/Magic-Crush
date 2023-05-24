using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ESCENACREDITOS : MonoBehaviour
{
    // Start is called before the first frame update
    public void escenarcreditos()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Credits");
    }
}
