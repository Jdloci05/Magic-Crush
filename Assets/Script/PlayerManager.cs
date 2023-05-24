using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Rendering;

public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
{

    PhotonView PV;
    public string nombreJugador;
    public PhotonView playerPrefab;
    public float saludPlayer = 0.0f;
    public int deads = 0;
    public bool overTime = false;
    public bool finishedGame = false;
    public bool dead;

    

    private GameObject controller;
    // Start is called before the first frame update

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    void Start()
    {
        if (PV.IsMine)
        {
            CreateController();
        }
    }

    
    void CreateController()
    {
        dead = false;
        controller = PhotonNetwork.Instantiate("Prefabs/" + playerPrefab.name, new Vector3(0, 15, 0), Quaternion.identity, 0, new object[] {PV.ViewID});
    }
    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            OverTime();
        }
    }

    void OverTime()
    {
        if (overTime)
        {
            saludPlayer -= 5;
            if (saludPlayer < 0)
            {
                overTime = false;
                Die();
            } 
        }
    }

    public void Die()
    {
        dead = true;
        PV.RPC("RPC_SetDeaths", RpcTarget.All);
        PhotonNetwork.Destroy(controller);
        StartCoroutine(RespawnAfterDelay(5f));

    }

    private IEnumerator RespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        CreateController();
    }



    [PunRPC]
    void RPC_SetDeaths()
    {
        bool Flag = false;
        if (!Flag)
        {
            deads += 1;

            Flag = true;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(overTime);
            stream.SendNext(finishedGame);
            stream.SendNext(dead);
        }
        else
        {
            overTime = (bool)stream.ReceiveNext();
            finishedGame = (bool)stream.ReceiveNext();
            dead = (bool)stream.ReceiveNext();
        }
    }
}
