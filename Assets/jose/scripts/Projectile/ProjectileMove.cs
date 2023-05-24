// CREDITS:
// Jose Lopez

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class ProjectileMove : MonoBehaviour
{
    #region INSPECTOR VARIABLES
    [Tooltip("Please assign the muzzleVFX of the projectile")]
    [SerializeField] GameObject muzzlePrefab;
    [Tooltip("Please assign the hitVFX of the projectile")]
    [SerializeField] GameObject hitPrefab;
    [Tooltip("Please assign the speed of the projectile")]
    [SerializeField] float speed;
    [Tooltip("Please assign the firerate of the projectile")]
    public float fireRate;
    #endregion

    #region EXECUTION
    // Start function initiates the trail of the projectile when is launched
    void Start()
    {
        if(muzzlePrefab != null)
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

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    #endregion

    #region MOVE METHOD
    //this method is use to move the projectile forward
    void Move()
    {
        if (speed != 0)
        {
            transform.position += transform.forward * (speed + Time.deltaTime);
        }
        else
        {
            Debug.Log("No Speed");
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
    // Method to detect whether the projectile hits to be destroy 
    private void OnCollisionEnter(Collision co)
    {
        speed = 0;

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
    #endregion
}
