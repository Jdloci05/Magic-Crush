using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    RoomInfo info;

    public GameObject roomlist;
    public GameObject FindRoomsObject;

    public GameObject roomlistReference;
    private void Start()
    {
        roomlistReference = GameObject.Find("/ROOMLISTreference");
        roomlist = roomlistReference.GetComponent<RoomListReference>().roomList;
        FindRoomsObject = GameObject.Find("/CanvasBuscarPartida/FindObjects");
    }
    public void SetUp(RoomInfo _info)
    {
        info = _info;   
        text.text = _info.Name;
    }

    public void OnClick()
    {
        
        CreateAndJoinRooms.Instance.JoinRoom(info);

        if (roomlist != null)
        {
            roomlist.SetActive(true);
        }

        if (FindRoomsObject != null)
        {
            FindRoomsObject.SetActive(false);
        }
    }
}
