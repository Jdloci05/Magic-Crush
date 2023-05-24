// CREDITS:
// Jose Lopez

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RotateToEnemy : MonoBehaviour
{
    #region OTHER VARIABLES
    private GameObject[] target;
    private GameObject otherPlayer;

    private Vector3 pos;
    private Vector3 direction;
    private Quaternion rotation;
    #endregion

    #region EXECUTION
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //method that get the otherplayer to shoot
    private void Update()
    {
        target = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject g in target)
        {
            if(!g.GetComponent<PhotonView>().IsMine)
            {
                otherPlayer = g;
            }
        }
        if(GameObject.FindGameObjectsWithTag("Player").Length == 2)
        {
            TargetRotation(otherPlayer);
        }
        
        
    }
    #endregion

    #region ROTATE METHODS
    //method that get the position or destination of the other player
    void RotateToEnemyDirection(GameObject obj, Vector3 destination)
    {
        direction = destination - obj.transform.position;
        rotation = Quaternion.LookRotation(direction);
        obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, rotation, 1);
    }

    //method that get the rotation of the other player
    public Quaternion GetRotation()
    {
        return rotation;
    }

    //method that get the rotation of the otherplayer
    void TargetRotation(GameObject target)
    {
        Vector3 targetRotation = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
            RotateToEnemyDirection(gameObject, targetRotation);

    }
    #endregion
}
