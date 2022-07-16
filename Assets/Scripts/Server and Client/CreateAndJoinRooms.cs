using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField createInput;
    public InputField joinInput;

    public GameObject RoomInfoListPrefab;

    void Awake()
    {
        RoomInfoListPrefab = GameObject.FindWithTag("RoomInfoList"); //FIND SINGLETON
    }

    public void OnClickCreateRoom()
    {
        PhotonNetwork.CreateRoom(createInput.text);
    }

    public void OnClickJoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom");

    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("LobbyRoom");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) //When others join your room!!!!!!!!!!!!!!!!!!
    {
        Debug.Log(newPlayer +" OnPlayerEnteredRoom");

        //string player = newPlayer.ToStringFull();

    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log(otherPlayer + " OnPlayerLeftRoom");
    }
}
