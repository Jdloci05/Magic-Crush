using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class MatchManager : MonoBehaviourPunCallbacks, IPunObservable
{

    public Slider player1HealthBar;
    public Slider player2HealthBar;
    public Text WinnerText;
    public Text player1Name;
    public Text player2Name;
    [SerializeField] Canvas normalCanva;
    [SerializeField] Canvas gameOverCanva;
    float player1Health;
    float player2Health;
    int player1Deads;
    int player2Deads;
    public bool dead1, dead2;


    public Toggle[] checkBoxes;

    public bool gameStarted;
    public bool roundStarted;
    public bool gameFinished = false;
    public int roundNum;

    PhotonView PV;

    [SerializeField] TimerController timerController;
    public bool overTime;

    private GameObject[] players_manager;
    private GameObject[] players_controller;

    PlayerManager playerManager_1, playerManager_2;

    private void Start()
    {
        normalCanva.enabled = true;
        gameOverCanva.enabled = false;
        PV = GetComponent<PhotonView>();
        roundNum = 0;
        roundStarted = false;   
        gameStarted = false;
        
    }

    private void Update()
    {
        if (gameStarted)
        {
            // Busca los objetos de jugador por su tag
            overTime = timerController.gameOverTime;
            players_manager = GameObject.FindGameObjectsWithTag("Manager");
            players_controller = GameObject.FindGameObjectsWithTag("Player");
            //OBTENER LA SALUD DE CADA JUGADOR
            if (players_manager.Length == 2 && players_controller.Length == 2)
            {
                roundStarted = true;
                playerManager_1 = players_manager[0].GetComponent<PlayerManager>();
                playerManager_2 = players_manager[1].GetComponent<PlayerManager>();

                playerManager_1.nombreJugador = player1Name.text;
                playerManager_2.nombreJugador = player2Name.text;

                player1Health = playerManager_1.saludPlayer;
                player2Health = playerManager_2.saludPlayer;

                player1Deads = playerManager_1.deads;
                player2Deads = playerManager_2.deads;

                dead1 = playerManager_1.dead;
                dead2 = playerManager_2.dead;

                UpdateHealthBar(player1Health, player2Health);

                if (player2Deads > 0 || player1Deads > 0)
                {
                    PV.RPC("RPC_RoundCheck", RpcTarget.All, player1Deads, player2Deads);
                }

                if (roundStarted)
                {

                    if (player1Deads >= 2 || player2Deads >= 2)
                    {
                        PV.RPC("EndOfTheGame", RpcTarget.All);
                    }
                    else
                    {
                        timerController.RestartTime();
                        if (dead1 || dead2 || overTime)
                        {
                            if (overTime)
                            {
                                GameOverTime();
                            }
                            else if(dead1 || dead2)
                            {
                                FinishRound();
                            }
                        }
                    }
                }
            }
            else
            {
                roundStarted = false;
            }
            

            
            //UpdateCheckBoxes(player1Health, player2Health);
            //Debug.Log("salud1: " + player1Health + " salud2: " + player2Health);
        }
    }

    void GameOverTime()
    {
        
        PV.RPC("RPC_GameOverTime", RpcTarget.All);
    }
    [PunRPC]
    void RPC_GameOverTime()
    {
        roundStarted = false;
        timerController.isTimerRunning = false;
        if (!roundStarted)
        {
            if (player1Health < player2Health)
            {
                playerManager_1.overTime = true;
            }
            if (player1Health == player2Health)
            {
                int numeroAleatorio = Random.Range(1, 3);
                switch (numeroAleatorio)
                {
                    case 1: playerManager_1.overTime = true; break;
                    case 2: playerManager_2.overTime = true; break;
                }
            }
            if (player1Health > player2Health)
            {
                playerManager_2.overTime = true;
            }
            float time = 5;
            time -= Time.deltaTime;
            if (time <= 0.0)
            {
                roundStarted = true;
            }
        }
        
    }

    void FinishRound()
    {
        roundStarted = false;
        timerController.isTimerRunning = false;
        Debug.Log("round finished");
        PV.RPC("RPC_FinishRound", RpcTarget.All);
    }
    [PunRPC]
    void RPC_FinishRound()
    {
        roundStarted = false;
        timerController.isTimerRunning = false;
        
        float time = 5;
        time -= Time.deltaTime;
        if (time <= 0.0)
        {
            roundStarted = true;
        }


    }
    [PunRPC]
    void EndOfTheGame()
    {
        gameOverCanva.enabled = true;
        normalCanva.enabled = false;
        if (player1Deads>=2)
        {
            WinnerText.text = player1Name.text;
        }
        else if(player2Deads>=2)
        {
            WinnerText.text = player1Name.text;
        }
        
    }
    private void UpdateHealthBar(float currentHealth_1, float currentHealth_2)
    {
        player1HealthBar.value = currentHealth_1;
        player2HealthBar.value = currentHealth_2;
        PV.RPC("RPC_UdatadeHeathBar", RpcTarget.All, player1Health, player2Health); 

    }

    [PunRPC]
    void RPC_UdatadeHeathBar(float health_1, float health_2)
    {
        //Debug.Log("salud Update");
        player1HealthBar.value = health_1;
        player2HealthBar.value = health_2;
    }



    //VER OPCION DE QUITAR
    [PunRPC]
    public void RPC_RoundCheck(int deads1, int deads2)
    {
        roundNum = deads1 + deads2;
        switch (deads2, deads1)
        {
            case (1, 0):
                checkBoxes[2].isOn = true;
                break;
            case (2, 0):
                checkBoxes[2].isOn = true;
                checkBoxes[3].isOn = true;
                break;
            case (1, 1):
                checkBoxes[2].isOn = true;
                checkBoxes[0].isOn = true;
                break;
            case (2, 1):
                checkBoxes[3].isOn = true;
                checkBoxes[2].isOn = true;
                checkBoxes[0].isOn = true;
                break;
            case (1, 2):
                checkBoxes[2].isOn = true;
                checkBoxes[1].isOn = true;
                checkBoxes[0].isOn = true;
                break;
            case (0, 1):
                checkBoxes[0].isOn = true;
                break;
            case (0, 2):
                checkBoxes[0].isOn = true;
                checkBoxes[1].isOn = true;
                break;
            case (0, 0):
                for(int i = 0; i <= checkBoxes.Length; i++)
                {
                    checkBoxes[i].isOn = false;
                }
                break;

        }
    }

    public void LeftGame()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
    }
    public void CheckPlayerCount()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            gameStarted = true;
            
        }
        else
        {
            
            gameStarted = false;
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        CheckPlayerCount();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        CheckPlayerCount();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(player1Deads);
            stream.SendNext(player2Deads);
            stream.SendNext(gameStarted);
            stream.SendNext(roundStarted);
            stream.SendNext(gameFinished);
            stream.SendNext(roundNum);
            stream.SendNext(overTime);
        }
        else
        {
            player1Deads = (int)stream.ReceiveNext();
            player2Deads = (int)stream.ReceiveNext();
            gameStarted = (bool)stream.ReceiveNext();
            roundStarted = (bool)stream.ReceiveNext();
            gameFinished = (bool)stream.ReceiveNext();
            roundNum = (int)stream.ReceiveNext();
            overTime = (bool)stream.ReceiveNext();

        }
    }
}


