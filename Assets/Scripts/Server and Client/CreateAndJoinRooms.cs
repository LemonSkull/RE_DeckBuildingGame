using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;


public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField createInput;
    public InputField joinInput;

    public GameObject RoomInfoListPrefab;
    public GameObject PlayerInfoPrefab;
    public GameObject LobbyRoomControlPrefab;
    public GameObject PlayerListTMP;

    private string currentScene, nickName;

    private PhotonView view;

    void Awake()
    {
        view = GetComponent<PhotonView>();
        currentScene = SceneManager.GetActiveScene().name;
        RoomInfoListPrefab = GameObject.FindWithTag("RoomInfoList"); //FIND SINGLETON
    }
    void Start()
    {
        if(view.IsMine)
        {
            nickName = PlayerPrefs.GetString("NickName");
            PhotonNetwork.NickName = nickName;

        }


    }
    public void OnClickCreateRoom()
    {
        PhotonNetwork.CreateRoom(createInput.text);
    }

    public void OnClickJoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }
    public override void OnCreatedRoom() //When Host makes room
    {
        Debug.Log("OnCreatedRoom");

        if (PlayerListTMP != null)
            PlayerListTMP.GetComponent<PlayerListTMP>().UpdatePlayerList();
        else
            Debug.Log("PlayerListTMP = null");


    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("LobbyRoom");

    }

    public override void OnPlayerEnteredRoom(Player newPlayer) //When others join your room!
    {
        Debug.Log(newPlayer +" OnPlayerEnteredRoom");

        if (LobbyRoomControlPrefab!=null)
                LobbyRoomControlPrefab.GetComponent<LobbyOpen>().AddNewPlayerInfoPrefab(false);
            
        //string player = newPlayer.ToStringFull();
        //base.photonView.TransferOwnership(newPlayer);
        
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log(otherPlayer + " Left Room");

        if (PlayerListTMP != null)
            PlayerListTMP.GetComponent<PlayerListTMP>().UpdatePlayerList();
        else
            Debug.Log("PlayerListTMP = null");
    }

}
