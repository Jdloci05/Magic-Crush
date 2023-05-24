using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Hashstable = ExitGames.Client.Photon.Hashtable;
using Photon.Realtime;

public class StatsPersonaje : MonoBehaviourPunCallbacks, IDamagable
{

    public float salud = 100.0f;
    const float maxSalud = 100.0f;
    float saludLocal;
    public float velocidad = 5f;
    public float fuerzaSalto = 5f;
    public float transformSpawn;
    public int roundsWin;
    public PhotonView PV;
    public int numPLayer;
    PlayerManager playerManager;

    //Slider healthBar;
    //Slider enemyBar;


    // Start is called before the first frame update
    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    void Start()
    {
        salud = maxSalud;
        saludLocal = maxSalud;
        playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();
        SetHealth(saludLocal);
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetHealth(float saludSet)
    {
        float _salud = saludSet;
        PV.RPC("RPC_setHealth", RpcTarget.All, _salud);
    }


    [PunRPC]
    public void RPC_setHealth(float setSalud)
    {
        salud = setSalud;
        if(playerManager!=null) playerManager.saludPlayer = setSalud;
    }


    public void TakeDamage(float damage)
    {
        PV.RPC("RPC_TakeDamage", RpcTarget.All, damage);

    }

    [PunRPC]
    public void RPC_TakeDamage(float damage)
    {
        if (!PV.IsMine) return;
        //Debug.Log("Daño recibido = " + damage);
        saludLocal -= damage;
        SetHealth(saludLocal);
        //healthBar.value = salud;
        if (salud <= 0)
        {
            Die();
        }

    }


    void Die()
    {
        playerManager.Die();
    }
}

       
