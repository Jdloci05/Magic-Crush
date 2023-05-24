using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieZone : MonoBehaviour
{
    #region INSPECTOR VARIABLES
    [Tooltip("Please assign the meshcollider of the die zone")]
    [SerializeField] Collider DieCollider;
    [Tooltip("Please assign the gameobject with colliders of the die zone")]
    [SerializeField] GameObject boxCollider;
    #endregion

    #region TRIGGER DETECTION
    // Method to detect whether the player enters the trigger zone
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player" || collision.CompareTag("PlayerGround"))
        {
            Debug.Log("collision");
            StartCoroutine(Desactivated());
        }
    }

    // Method to detect whether the player has exited the trigger zone
    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Player" || collision.CompareTag("PlayerGround"))
        {
            boxCollider.SetActive(true);
            DieCollider.enabled = true;
        }
    }
    #endregion

    #region DESACTIVATED METHOD
    // Method to desactivate game objects
    IEnumerator Desactivated()
    {
        yield return new WaitForSeconds(3);
        boxCollider.SetActive(false);
        DieCollider.enabled = false;
    }
    #endregion
}
