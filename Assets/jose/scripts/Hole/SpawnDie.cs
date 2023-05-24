using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDie : MonoBehaviour
{
    #region INSPECTOR VARIABLES
    [Tooltip("Please assign the time of life of the hole")]
    [SerializeField] float LifeTime = 5f;
    #endregion

    #region OTHER VARIABLES
    private GameObject TargetSpawn;
    private GameObject Hole;
    #endregion

    #region EXECUTION
    private void Awake()
    {
        Hole = gameObject;
        TargetSpawn = GameObject.FindWithTag("DieSpawner");
        StartCoroutine(Die());
    }
    #endregion

    #region TRIGGER DETECTION
    // Method to detect whether the player enters the trigger zone
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag=="Player")
        {
            collision.transform.position = TargetSpawn.transform.position;
            Destroy(Hole);
        }
    }
    #endregion

    #region DESTROY METHOD
    // Method to destroy game object
    IEnumerator Die()
    {
        yield return new WaitForSeconds(LifeTime);
        Destroy(gameObject);
    }
    #endregion
}
