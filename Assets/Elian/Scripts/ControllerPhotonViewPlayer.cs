using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
//using System;

public class ControllerPhotonViewPlayer : MonoBehaviour
{
    public PhotonView playerPrefab;

    public Transform sppp11;
    public Transform sppp12;
    public Transform sppp21;
    public Transform sppp22;

    public int playercont;

    // Start is called before the first frame update
    IEnumerator waiting()
    {
        yield return new WaitForSeconds(1.5f);
        playercont = PhotonNetwork.CurrentRoom.PlayerCount;
        if (playercont == 1)
        {
            PhotonNetwork.Instantiate("Prefabs/" + playerPrefab.name, new Vector3(Random.Range(sppp11.position.x, sppp12.position.x), Random.Range(sppp11.position.y, sppp12.position.y), Random.Range(sppp11.position.z, sppp12.position.z)), Quaternion.identity);
        }

        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.Instantiate("Prefabs/" + playerPrefab.name, new Vector3(Random.Range(sppp21.position.x, sppp22.position.x), Random.Range(sppp21.position.y, sppp22.position.y), Random.Range(sppp21.position.z, sppp22.position.z)), Quaternion.identity);
        }
    }
    void Start()
    {
        StartCoroutine(waiting());

    }

    private void Update()
    {
        playercont = PhotonNetwork.CurrentRoom.PlayerCount;
    }

   

}
