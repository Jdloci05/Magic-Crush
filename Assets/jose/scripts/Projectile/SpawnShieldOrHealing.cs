// CREDITS:
// Jose Lopez

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class SpawnShieldOrHealing : MonoBehaviour
{
    #region INSPECTOR VARIABLES
    [Tooltip("Please assign the spawnPoint Of shield")]
    [SerializeField] GameObject spawnPointShield;
    [Tooltip("Please assign the spawnPoint Of The healing")]
    [SerializeField] GameObject spawnPointHealing;
    
    [Tooltip("Please assign the time to activate shield and healing when the round starts")]
    [SerializeField] float TimerToActive;
    [Tooltip("Please assign the time to destroy shield when use")]
    [SerializeField] float TimerToDestroyShield;
    [Tooltip("Please assign the time to activate shield after use")]
    [SerializeField] float TimerToActiveShield;
    [Tooltip("Please assign time to destroy healing when use")]
    [SerializeField] float TimerToDestroyHealing;
    [Tooltip("Please assign the time to activate healing after use")]
    [SerializeField] float TimerToActiveHealing;
    #endregion

    #region OTHER VARIABLES
    private GameObject activeShield;
    private GameObject activeHealing;
    private GameObject ground;
    private bool Spawned = true;
    PhotonView pv;
    #endregion

    #region EXECUTION
    // Start is called before the first frame update
    public void Start()
    {
        pv = GetComponent<PhotonView>();
        ground = GameObject.FindWithTag("Ground");
        Invoke("StartTimer", TimerToActive);
    }

    public void Update()
    {
            Spawn();   
    }
    #endregion

    #region SPAWN SHIELD AND HEALING RPC METHODS
    // Method to SHOW Spawn on all clients
    public void Spawn()
    {
        pv.RPC("RPCSpawn", RpcTarget.All);
    }

    // Method to spawn healing or shield
    [PunRPC]
    public void RPCSpawn()
    {
        if (Spawned == false && Input.GetKey(KeyCode.Alpha1) && pv.IsMine)
        {
            activeShield = PhotonNetwork.Instantiate("Prefabs/Ataques/" + "Shield", spawnPointShield.transform.position, Quaternion.identity);
            activeShield.transform.parent = spawnPointShield.transform;
        //    activeShield.layer = LayerMask.NameToLayer("NonCollide");
            Spawned = true;
            Invoke("DestroyTimer", TimerToDestroyShield);
                
        }
        //else if (Spawned == false && Input.GetKey(KeyCode.Alpha2) && pv.IsMine)
        //{
        //    activeHealing = PhotonNetwork.Instantiate("Prefabs/Ataques/" + "Healing", spawnPointHealing.transform.position, Quaternion.identity);
        //    Spawned = true;
        //    Invoke("DestroyTimer", TimerToDestroyHealing);
        //}
    }
    #endregion

    #region DESTROY ON ALL CLIENTS METHODS
    // Method to know if shield or healing are active and then destroy on all clients
    void DestroyTimer()
    {
        if(activeShield != null)
        {
            PhotonNetwork.Destroy(activeShield);
            Invoke("StartTimer", TimerToActiveShield);
        }
        //if(activeHealing != null)
        //{
        //    PhotonNetwork.Destroy(activeHealing);
        //    Invoke("StartTimer", TimerToActiveHealing);
        //}
        
    }

    // Method to know if shield or healing are active
    void StartTimer()
    {
        Spawned = false;
    }
    #endregion
}
