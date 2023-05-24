using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HitBox")
        {
            Debug.Log("Hit_Trigger");
            other.GetComponentInParent<IDamagable>()?.TakeDamage(damage/2);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "HitBox")
        {
            Debug.Log("Hit_collision");
            collision.gameObject.GetComponentInParent<IDamagable>()?.TakeDamage(damage / 2);
        }

    }
}
