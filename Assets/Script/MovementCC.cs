using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MovementCC : MonoBehaviour
{
    //Publicas
    private float moveSpeed; // velocidad de movimiento
    private float jumpForce; // fuerza del salto
    private float gravity = -9.81f; // gravedad
    public Transform groundCheck; // objeto que se utiliza para comprobar si el personaje est� en el suelo
    public float groundDistance = 0.4f; // distancia a la que se considera que el personaje est� en el suelo
    public LayerMask groundMask; // capa que representa el suelo
    public float rotateSpeed = 50.0f;//velocidad de rotacion
    StatsPersonaje stats;
    //Privadas
    private Transform enemyTransform;
    private GameObject[] players;
    private PhotonView view_1;
    private Quaternion angulosOriginales; // angulos originales del personaje
    private float maxAngle = 50.0f; // angulo maximo de inclinaci�n
    private CharacterController controller; // referencia al componente CharacterController del objeto
    private Collider personajeCollider;
    private Vector3 velocity; // velocidad del movimiento

    private bool isGrounded; // indica si el personaje est� en el suelo

    private bool isMovible;


    void Start()
    {
        stats = GetComponent<StatsPersonaje>();
        controller = GetComponent<CharacterController>(); // obtener la referencia al componente CharacterController
        view_1 = GetComponent<PhotonView>();
        personajeCollider = GetComponent<Collider>();
        moveSpeed = stats.velocidad;
        jumpForce = stats.fuerzaSalto;
        isMovible = true;
        angulosOriginales = transform.rotation; //Obtener angulos originales del personaje
        controller = GetComponent<CharacterController>(); // obtener la referencia al componente CharacterController
        
        Physics.IgnoreCollision(controller, personajeCollider);//ignora las colisiones del character controller

    }

    void Update()
    {

        players = GameObject.FindGameObjectsWithTag("Player");

        if (view_1.IsMine)
        {
            if (isMovible)
            {
                Movimiento();
                //SetEnemyPosition();
            }
            else
            {

            }
        }
        else
        {
            controller.enabled = false;
        }

    }
    public void Movimiento()
    {
        // obtener la entrada horizontal y vertical del teclado
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


        //---------------------------BALANCEO--------------------------------------------------------------

        // calcular la dirección del movimiento

        Vector3 v = SetEnemyPosition();
        Quaternion rotation = Quaternion.LookRotation(v);

        Vector3 moveDirection = transform.right * horizontalInput + transform.forward * verticalInput;

        Quaternion currentRotation = transform.rotation;
        

        if (horizontalInput != 0 || verticalInput != 0)
        {
            // mover el personaje en la dirección calculada
            controller.Move(moveDirection * moveSpeed * Time.deltaTime);
            Vector3 rotateDirection = new Vector3(-verticalInput * maxAngle, 0, horizontalInput * maxAngle);

            Quaternion q = Quaternion.Euler(rotateDirection.x, 0, rotateDirection.z);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.deltaTime * rotateSpeed);

          
        }
        else
        {
            Quaternion quaternion = Quaternion.LookRotation(v);
            Quaternion newRotation = Quaternion.RotateTowards(currentRotation, quaternion, Time.deltaTime * (rotateSpeed - 15.0f));
            transform.rotation = newRotation;
        }

        

        //---------------------------SALTO-----------------------------------------------------------------

        // comprobar si el personaje está en el suelo
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        //Debug.Log(isGrounded); 

        // si está en el suelo, resetear la velocidad de movimiento vertical
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

       

        

        // si se pulsa el botón de salto y el personaje está en el suelo, aplicar la fuerza del salto
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            //Debug.Log("Salto");
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        // aplicar la gravedad
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public Vector3 SetEnemyPosition()
    {
        Vector3 v = Vector3.zero;
        if (players.Length == 2)
        {
            foreach (GameObject g in players)
            {
                if (!g.GetComponent<PhotonView>().IsMine)
                {
                    enemyTransform = g.transform;
                    v = enemyTransform.position - transform.position;
                }
            }
            //transform.LookAt(enemyTransform);
        }
        v.y = 0;
        return v;
    }
}
