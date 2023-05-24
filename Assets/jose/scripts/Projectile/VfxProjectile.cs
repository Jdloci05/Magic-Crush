// CREDITS:
// Jose Lopez

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class VfxProjectile : MonoBehaviour
{
    #region INSPECTOR VARIABLES
    [Tooltip("Please assign the muzzleVFX of the projectile")]
    [SerializeField] GameObject muzzlePrefab;
    [Tooltip("Please assign the hitVFX of the projectile")]
    [SerializeField] GameObject hitPrefab;
    [Tooltip("Please assign the ShockTrap of the projectile")]
    [SerializeField] GameObject ShockTrap;
    #endregion

    private Damage damageScript;
    public bool IsVFXPublic;
    public bool IsShocktrap;

    #region EXECUTION
    // Start function initiates the trail of the projectile when is launched
    void Start()
    {
        damageScript = GetComponent<Damage>();

        if (muzzlePrefab != null)
        {
            var muzzleVFX = Instantiate(muzzlePrefab, transform.position, Quaternion.identity);
            muzzleVFX.transform.forward = gameObject.transform.forward;
            var psMuzzle = muzzleVFX.GetComponent<ParticleSystem>();
            if (psMuzzle != null)
                Destroy(muzzleVFX, psMuzzle.main.duration);
            else
            {
                var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzzleVFX, psChild.main.duration);
            }
        }
    }
    #endregion

    #region TRIGGER DETECTION
    // Method to detect whether the projectile hits a shield
    public void OnTriggerEnter(Collider Collision)
    {
        if (Collision.CompareTag("Shield"))
        {
            Destroy(gameObject);
        }
    }
    #endregion

    #region COLLISION DETECTION
    // Method to detect whether the projectile hits to be destroy or bounce or ceratins elements
    private void OnCollisionEnter(Collision co)
    {
        if (IsVFXPublic == false && co.gameObject.tag == "Ground")
        {
            damageScript.damage = 0;
            ShockTrap.SetActive(true);
            IsShocktrap = true;
        }
        else if (IsVFXPublic == true && co.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
        else if (IsShocktrap == false && co.gameObject.tag == "Player" || IsShocktrap == false && co.gameObject.tag == "PlayerGround")
        {
            ContactPoint contact = co.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;

            if (hitPrefab != null)
            {
                var hitVFX = Instantiate(hitPrefab, pos, rot);
                var psHit = hitVFX.GetComponent<ParticleSystem>();
                if (psHit != null)
                    Destroy(hitVFX, psHit.main.duration);
                else
                {
                    var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(hitVFX, psChild.main.duration);
                }
            }
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    #endregion
}
