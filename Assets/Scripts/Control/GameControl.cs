using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI; //DEBUGGING

public class GameControl : MonoBehaviourPunCallbacks
{
    PhotonView view;
    public GameObject SpawnCardsPrefab;
    private GameObject MainSpawnCards;
    public int currentPlayerID;
    public Text currentPlayer; //DEBUGGING

    [SerializeField] private int whoIsPlaying, playerCount; //IN USE

    // Start is called before the first frame update
    void Awake()
    {
        view = GetComponent<PhotonView>();
        currentPlayerID = view.ViewID;
        MainSpawnCards = PhotonNetwork.Instantiate(SpawnCardsPrefab.name, new Vector3(0f,0f,0f), Quaternion.identity);

        if(PhotonNetwork.IsMasterClient)
            ShowCurrentPlayer(PhotonNetwork.NickName);
    }

    public void OnClickDrawCard()
    {
        if(view.IsMine)
            MainSpawnCards.GetComponent<SpawnCards>().DrawCard();

    }
    public void OnClickCardsToDiscardPile()
    {
        if (view.IsMine)
            MainSpawnCards.GetComponent<SpawnCards>().PutHandCardsToDiscardPile();

    }


    public void OnClickTransferOwnershipToNextPlayer() //NOT WORKING
    {
        //currentPlayerID = PhotonNetwork.PlayerList[count].ActorNumber;
        //currentPlayerID = view.ActorNumber;


        if (view.IsMine)
        {
            view.RPC("PunTransferOwnershipToNextPlayer", RpcTarget.AllBuffered);
        }
            

    }
    [PunRPC]
    public void PunTransferOwnershipToNextPlayer()
    {
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

        //currentPlayerID = view.ViewID;

        if (currentPlayerID == playerCount)
            currentPlayerID = 1;
        else
            currentPlayerID++;

        Player _player = PhotonNetwork.PlayerList[currentPlayerID - 1];
        

        view.TransferOwnership(_player);
        Debug.Log("View transfered to: " + _player + "    Player count: " + playerCount);
        Debug.Log("CurrentPlayerID: " + currentPlayerID);

        string name = _player.NickName;
        ShowCurrentPlayer(name);
    }

    public void ShowCurrentPlayer(string name)//DEBUGGING
    {
        currentPlayer.text = "Now Playing: " +name;
    }
}
