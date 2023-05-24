// CREDITS:
// Jose Lopez

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class SpawnProjectiles : MonoBehaviourPun
{
    #region INSPECTOR VARIABLES
    [Tooltip("Please assign the Spawn of the projectiles")]
    [SerializeField] GameObject firePoint;
    [Tooltip("Please assign the projectiles")]
    [SerializeField] List<GameObject> Vfx = new List<GameObject>();
    [Tooltip("Please assign the script that is inside the firepoint")]
    [SerializeField] RotateToEnemy rotateToEnemy;
    #endregion

    #region OTHER VARIABLES
    PhotonView pv;
    private float timeToFire = 0;
    #endregion

    #region EXECUTION
    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
            Shoot(); 
    }
    #endregion

    #region SPAWN PROJECTILES RPC METHODS
    // Method to SHOW Spawn on all clients
    public void Shoot()
    {
        pv.RPC("RPCShoot", RpcTarget.All);
    }

    // Method to spawn on click
    [PunRPC]
    public void RPCShoot()
    {
        if (Input.GetMouseButton(0) && Time.time >= timeToFire && pv.IsMine)
        {
            timeToFire = Time.time + 1 / Vfx[0].GetComponent<ProjectileMove>().fireRate;
            SpawnVfx();
            //Debug.Log("you did it");
        }
    }

    // Method to spawn the instancevfx of the projectile
    void SpawnVfx()
    {
        GameObject vfx;

        if(firePoint != null)
        {
            vfx = PhotonNetwork.Instantiate("Prefabs/Ataques/" + "Vfx_Proyectiles", firePoint.transform.position, Quaternion.identity);    
            if(rotateToEnemy != null)
            {
                vfx.transform.localRotation = rotateToEnemy.GetRotation();
            }
        }
        else
        {
            Debug.Log("No fire Point");
        }
        
    }
    #endregion
}
