// CREDITS:
// Jose Lopez

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class ShockTrap : MonoBehaviour
{
    #region INSPECTOR VARIABLES
    [Tooltip("Please assign the projectile gameobject")]
    [SerializeField] GameObject Projectile;
    [Tooltip("Please assign the electricity particle system")]
    [SerializeField] GameObject electricity;

    [Tooltip("Please assign the timer that the electricity is visualizable")]
    [SerializeField] float timerVFX = 3;
    [Tooltip("Please assign the timer of life of the shocktrap")]
    [SerializeField] float LifeVFX = 2;
    #endregion

    #region OTHER VARIABLES
    private GameObject[] ground;
    private MovementCC scriptdisabled;
    public float IsCurrentlyWorking = 0;
    #endregion

    #region EXECUTION
    public void Start()
    {
        ground = GameObject.FindGameObjectsWithTag("Ground");
    }
    #endregion

    #region TRIGGER DETECTION
    // Method to detect whether the player or gorund collide with the projectile
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsCurrentlyWorking += 1;
            if(IsCurrentlyWorking == 1)
            {
                scriptdisabled = collision.GetComponent<MovementCC>();
                electricity.SetActive(true);
                scriptdisabled.enabled = false;
                Invoke("Destroy", timerVFX);
            }
            else
            {

            }
        }
        if (collision.CompareTag("Ground"))
        {
            Invoke("Destroy", LifeVFX);
        }
    }
    #endregion

    #region Destroy on all clients
    // Method to destroy gameobject on all clients
    public void Destroy()
    {
        scriptdisabled.enabled = true;
        PhotonNetwork.Destroy(Projectile);
    }
    #endregion
}
