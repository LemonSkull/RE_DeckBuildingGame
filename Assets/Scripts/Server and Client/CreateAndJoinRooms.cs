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
    [SerializeField] private bool isHost;
    private string currentScene;

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
            string nickName = PlayerPrefs.GetString("NickName");
            PhotonNetwork.NickName = nickName;

        }
        isHost = PhotonNetwork.IsMasterClient;

        if (currentScene == "Game")
            PhotonNetwork.CurrentRoom.IsOpen=false;


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

        UpdateJoinedPlayerList();
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("LobbyRoom");

    }

    public override void OnPlayerEnteredRoom(Player newPlayer) //When others join your room!
    {
        Debug.Log(newPlayer +" OnPlayerEnteredRoom");


            UpdateJoinedPlayerList();

        //string player = newPlayer.ToStringFull();
        //base.photonView.TransferOwnership(newPlayer);

    }

    private void UpdateJoinedPlayerList()
    {
        if(view!=null)
            view.RPC("RPCUpdateJoinedPlayers", RpcTarget.AllBuffered);

    }
    [PunRPC]
    private void RPCUpdateJoinedPlayers()
    {
        if (PlayerListTMP != null)
            PlayerListTMP.GetComponent<PlayerListTMP>().UpdatePlayerList();

        if (LobbyRoomControlPrefab != null)
            LobbyRoomControlPrefab.GetComponent<LobbyRoomOpen>().AddNewPlayerInfoPrefab();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log(otherPlayer + " Left Room");

        UpdateJoinedPlayerList();
    }

}
