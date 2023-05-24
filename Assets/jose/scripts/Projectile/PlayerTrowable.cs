// CREDITS:
// Jose Lopez

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class PlayerTrowable : MonoBehaviour
{
    
    #region INSPECTOR VARIABLES
    [Tooltip("Please assign the firerate of the launch")]
    [SerializeField] float flightTime = 1f;

    [Tooltip("Please assign the cursor renderer")]
    [SerializeField] GameObject cursor;
    [Tooltip("Please assign the spawn of the prefab")]
    [SerializeField] Transform shootPoint;

    [Tooltip("Please assign the line renderer od the launching")]
    [SerializeField] LineRenderer lineVisual;
    [Tooltip("Please assign the number of line segnments")]
    [SerializeField] int lineSegment = 10;

    [Tooltip("Please assign the layers that the linevisual will collide")]
    [SerializeField] LayerMask layer;
    #endregion

    #region OTHER VARIABLES
    private PhotonView pv;
    private Camera cam;
    private float lastShoot;
    #endregion

    #region EXECUTION
    // Start is called before the first frame update
    void Start()
    {
        cursor.SetActive(false);
        lineVisual.enabled = false;

        lastShoot = Time.time;
        cam = Camera.main;
        lineVisual.positionCount = lineSegment + 1;

        pv = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
            LaunchProjectile();
    }
    #endregion

    #region LAUNCH METHODS
    // Method to SHOW Spawn on all clients
    public void LaunchProjectile()
    {
        pv.RPC("RPCLaunchProjectile", RpcTarget.All);
    }

    // Method to detect posiiton of mouse and detect velocity to launch
    [PunRPC]
    void RPCLaunchProjectile()
    {
        Ray camRay = new Ray();
        if (cam != null) {camRay = cam.ScreenPointToRay(Input.mousePosition); }
        RaycastHit hit;

        if (Physics.Raycast(camRay, out hit, 100f, layer))
        {
            cursor.transform.position = hit.point + Vector3.up * 0.1f;

            Vector3 vo = CalculateVelocty(hit.point, shootPoint.position, flightTime);

            Visualize(vo, cursor.transform.position); //we include the cursor position as the final nodes for the line visual position

            if (Input.GetMouseButtonDown(1) && lastShoot < Time.time && pv.IsMine)
            {
                cursor.SetActive(true);
                lineVisual.enabled = true;
            }
            if (Input.GetMouseButtonUp(1) && pv.IsMine)
            {
                if (lastShoot < Time.time)
                {
                    lastShoot = Time.time + 1.5f;
                    GameObject obj = PhotonNetwork.Instantiate("Prefabs/Ataques/" + "Vfx_ProyectilesAttack2", shootPoint.position, Quaternion.identity);
                    Rigidbody br = obj.GetComponent<Rigidbody>();
                    br.velocity = vo;

                    cursor.SetActive(false);
                    lineVisual.enabled = false;
                }

                
            }
        }
    }

    #region VISUALIZE METHOD
    //added final position argument to draw the last line node to the actual target
    void Visualize(Vector3 vo, Vector3 finalPos)
    {
        for (int i = 0; i < lineSegment; i++)
        {
            Vector3 pos = CalculatePosInTime(vo, (i / (float)lineSegment) * flightTime);
            lineVisual.SetPosition(i, pos);
        }

        lineVisual.SetPosition(lineSegment, finalPos);
    }
    #endregion

    #region VELOCITY METHOD
    // Method to calculate the velocity
    Vector3 CalculateVelocty(Vector3 target, Vector3 origin, float time)
    {
        Vector3 distance = target - origin;
        Vector3 distanceXz = distance;
        distanceXz.y = 0f;

        float sY = distance.y;
        float sXz = distanceXz.magnitude;

        float Vxz = sXz / time;
        float Vy = (sY / time) + (0.5f * Mathf.Abs(Physics.gravity.y) * time);

        Vector3 result = distanceXz.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
    }
    #endregion

    #region POSITION METHOD
    // Method to calculate the position of the cursor on the world space
    Vector3 CalculatePosInTime(Vector3 vo, float time)
    {
        Vector3 Vxz = vo;
        Vxz.y = 0f;

        Vector3 result = shootPoint.position + vo * time;
        float sY = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time)) + (vo.y * time) + shootPoint.position.y;

        result.y = sY;

        return result;
    }
    #endregion

    #endregion
}
