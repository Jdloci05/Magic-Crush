using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class RandomSpawner : MonoBehaviour
{
    #region INSPECTOR VARIABLES
    [Tooltip("Please assign the distance on  y to spawn the prefab")]
    [SerializeField] float distanceGround;
    [Tooltip("bool to stopspawning holes")]
    [SerializeField] bool stopSpawning = false;
    [Tooltip("Please assign the time that takes the first hole to spawn")]
    [SerializeField] float spawnTime;
    [Tooltip("Please assign the time that takes the nexts holes to spawn")]
    [SerializeField] float spawnDelay;
    #endregion

    #region EXECUTION
    // Start is called before the first frame update
    private void Start()
    {
        InvokeRepeating("Spawner", spawnTime, spawnDelay);
    }
    #endregion

    #region SPAWN METHOD
    // Method to spawn HOLES on certain range randomly
    public void Spawner()
    {
        Vector3 randomSpawnPosition = new Vector3(Random.Range(-11, 11), distanceGround, Random.Range(-11, 11));
        PhotonNetwork.Instantiate("Prefabs/Ataques/Entorno/" + "Hole", randomSpawnPosition, Quaternion.Euler(-90, 0, 0));
        if(stopSpawning)
        {
            CancelInvoke("SpawnObject");
        }
    }
    #endregion
}
