// CREDITS:
// Jose Lopez

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class PublicLaunch : MonoBehaviour
{
    #region INSPECTOR VARIABLES
    [Tooltip("Please assign the sapwnpoint of the projectiles")]
    [SerializeField] Transform launchPoint;

    [Tooltip("bool to stopspawning projectiles")]
    [SerializeField] bool stopSpawning = false;

    [Tooltip("Please assign the velocity of projectile")]
    [SerializeField] float launchVelocity = 20f;
    [Tooltip("Please assign the time that takes the first projectile to spawn")]
    [SerializeField] float spawnTime;
    [Tooltip("Please assign the time that takes the nexts projectiles to spawn")]
    [SerializeField] float spawnDelay;
    #endregion

    #region EXECUTION
    // Start is called before the first frame update
    private void Start()
    {

        InvokeRepeating("Spawner", spawnTime, spawnDelay);
    }
    #endregion

    #region LAUNCH METHOD
    // Method to spawn projectiles and rotate to certain range to launch
    public void Spawner()
    {
        if(gameObject.name == "NPC1")
        {
            launchPoint.rotation = Quaternion.Euler(Random.Range(0.0f, 0.0f), Random.Range(-44.0f, 44.0f), Random.Range(10.5f, 55f));
        }
        if (gameObject.name == "NPC2")
        {
            launchPoint.rotation = Quaternion.Euler(Random.Range(0.0f, 0.0f), Random.Range(-20.0f, 80.0f), Random.Range(-10.5f, -70f));
        }
        if (gameObject.name == "NPC3")
        {
            launchPoint.rotation = Quaternion.Euler(Random.Range(-10.5f, -80.0f), Random.Range(-50.0f, 50.0f), Random.Range(0.0f, 0.0f));
        }
        if (gameObject.name == "NPC4")
        {
            launchPoint.rotation = Quaternion.Euler(Random.Range(10.5f, 70.0f), Random.Range(-10.0f, 115f), Random.Range(0.0f, 0.0f));
        }
        var _projectile = PhotonNetwork.Instantiate("Prefabs/Ataques/" + "Vfx_ProyectilesPublic", launchPoint.position, launchPoint.rotation);
        _projectile.GetComponent<Rigidbody>().velocity = launchPoint.up * launchVelocity;
        if (stopSpawning)
        {
            CancelInvoke("SpawnObject");
        }
    }
    #endregion
}
