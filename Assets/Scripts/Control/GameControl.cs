using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI; //DEBUGGING

public class GameControl : MonoBehaviourPunCallbacks
{
    PhotonView view;
    public GameObject SpawnCardsPrefab, UIGameControl;
    private GameObject MainSpawnCards;
    [SerializeField] private GameObject[] PlayerInfoList;
    public int currentPlayerID;
    public Text currentPlayer; //DEBUGGING
    private string currentPlayerName;
    bool isMaster;
    [SerializeField] private int whoIsPlaying, playerCount; //IN USE

    // Start is called before the first frame update
    void Awake()
    {
        view = GetComponent<PhotonView>();
        isMaster = PhotonNetwork.IsMasterClient;

        MainSpawnCards = PhotonNetwork.Instantiate(SpawnCardsPrefab.name, new Vector3(0f,0f,0f), Quaternion.identity);

        PlayerInfoList = GameObject.FindGameObjectsWithTag("PlayerInfo");
    }
    void Start()
    {
        currentPlayerID = view.ViewID;
        //CharacterControl.GetComponent<CharacterControl>().SetCharacterCardByID();

        if (isMaster)
        {
            currentPlayerName = PhotonNetwork.NickName;
            ShowCurrentPlayer(currentPlayerName);
        }
        string hostName = PhotonNetwork.PlayerList[0].NickName;
        UIGameControl.GetComponent<GameUIControl>().UIPlayerNextTurnStart(hostName, currentPlayerID);

        //if (view.IsMine)
            CreateCharacterCards();
        //CreateCharacterCards();
    }

    public void CreateCharacterCards()
    {
        int count = 0;
        foreach(GameObject o in PlayerInfoList)
        {

            //string characterName = o.GetComponent<PlayerInfo>().myCharacterCard;
            MainSpawnCards.GetComponent<SpawnCards>().CreateCharacterCard(count);
            count++;
        }


    }
    /*
    public void SendCharacterCardNames() //T
    {
        GameObject[] spawnCards = GameObject.FindGameObjectsWithTag("Respawn");
        foreach(GameObject spawn in spawnCards)
        {
            foreach (GameObject info in PlayerInfoList)
            {
                string characterName = info.GetComponent<PlayerInfo>().myCharacterCard;
                spawn.GetComponent<SpawnCards>().CreateCharacterCard(0);

            }

        }


    }
    */





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

        if (currentPlayerID >= playerCount)
            currentPlayerID = 1;
        else
            currentPlayerID++;

        Player _player = PhotonNetwork.PlayerList[currentPlayerID - 1];
        

        view.TransferOwnership(_player);
        Debug.Log("View transfered to: " + _player + "    Player count: " + playerCount);

        currentPlayerName = _player.NickName;
        ShowCurrentPlayer(currentPlayerName);

        UIGameControl.GetComponent<GameUIControl>().UIPlayerNextTurnStart(currentPlayerName, currentPlayerID);
    }

    public void ShowCurrentPlayer(string name)//DEBUGGING
    {
        currentPlayer.text = "Now Playing: " +name;
    }

}
