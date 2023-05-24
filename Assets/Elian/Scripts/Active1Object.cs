using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active1Object : MonoBehaviour
{
    public GameObject activeObject;

    public void active1object()
    {
        activeObject.SetActive(true);   
    }

    public void desactive1object()
    {
        activeObject.SetActive(false);
    }
}
