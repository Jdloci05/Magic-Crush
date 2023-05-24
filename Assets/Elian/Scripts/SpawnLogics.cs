using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SpawnLogics : MonoBehaviour
{
    public GameObject p1;
    public GameObject p2;

    public GameObject spawnpp1;
    public GameObject spawnpp2;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 positicionspp1 = spawnpp1.GetComponent<Vector2>();
        Vector2 positicionspp2 = spawnpp2.GetComponent<Vector2>();

        PhotonNetwork.Instantiate(p1.name, positicionspp2, Quaternion.identity);

        PhotonNetwork.Instantiate(p2.name, positicionspp2, Quaternion.identity);
    }

  
}
