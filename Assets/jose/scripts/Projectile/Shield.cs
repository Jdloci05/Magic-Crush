// CREDITS:
// Jose Lopez

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class Shield : MonoBehaviour
{
    #region TRIGGER DETECTION
    // Method to detect whether the projectile collide with the shield
    public void OnTriggerEnter(Collider Collision)
    {
        if (Collision.CompareTag("Projectile"))
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
    #endregion
}
