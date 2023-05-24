using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using System.Collections.Generic;
using Photon.Realtime;
using System.Linq;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public static CreateAndJoinRooms Instance;
   
    // Start is called before the first frame update
    public TMP_InputField createInput;
    public TMP_InputField joinInput;

    public GameObject canvaLobby;

    public GameObject canvaExit;


    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListItemPrefab;

    [SerializeField] Transform playerListContent;
    [SerializeField] GameObject playerListItemPrefab;
    [SerializeField] GameObject startGameButton;
    public GameObject playerList;
    public GameObject createCanvas;
    public GameObject joinObjects;
    public TMP_Text roomNameText;
    private void Awake()
    {
        Instance = this;
    }
    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(createInput.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(createInput.text);
        playerList.SetActive(true);
        createCanvas.SetActive(false);
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        playerList.SetActive(false);
        joinObjects.SetActive(true);

    }
    public void JoinRoom()
    {
   
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        //PhotonNetwork.LoadLevel("WaitingRoom");

        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Count(); i++)
        {
            Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public void ExitPrimerPASO()
    {
       canvaLobby.SetActive(false);
       canvaExit.SetActive(true);
    }

    public void ExitCONFIRMAR()
    {
        Application.Quit();
    }

    public void ExitCancelar()
    {
        canvaLobby.SetActive(true);
        canvaExit.SetActive(false);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }
        for(int i = 0; i < roomList.Count; i++)
        {
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);

       
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
       Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(2);
    }
}
