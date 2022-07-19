using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class LobbyRoomOpen : MonoBehaviourPunCallbacks
{
    public GameObject PlayerInfoPrefab;
    public GameObject CharacterList;
    [SerializeField] private GameObject[] playerInfoList;
    public int joinedPlayerCount;
    public GameObject startGame_btn;
    private bool isMaster;
    Vector2 playerInfoPos = new Vector2(-6.9f, -1.6f);
    PhotonView view;

    void Start()
    {
        view = GetComponent<PhotonView>();
        isMaster = PhotonNetwork.IsMasterClient;

        /*
        if (isMaster)
        {
            int hostID = 1;//Host is always ActorNumber 1!
            joinedPlayerCount++;
            
            GameObject player = Instantiate(PlayerInfoPrefab, playerInfoPos, Quaternion.identity);
            joinedPlayerCount = 1;
            //playerInfoList.Add(player);
            player.GetComponent<PlayerInfo>().WhenInstanceIsCreated(hostID);
        }
        */
            if(view.IsMine)
                startGame_btn.SetActive(true);
            else
                startGame_btn.SetActive(false);
    }

    public void UpdateAllPlayerInfos() //When player enters room (CreateAndJoinRooms.cs)
    {
        if (view.IsMine)
        {
            joinedPlayerCount = 0;
            foreach (Player p in PhotonNetwork.PlayerList)
            {
                int count = joinedPlayerCount;
                int _playerID = PhotonNetwork.PlayerList[count].ActorNumber;

                GameObject player = PhotonNetwork.Instantiate(PlayerInfoPrefab.name, playerInfoPos, Quaternion.identity);
                player.GetComponent<PlayerInfo>().WhenInstanceIsCreated(_playerID);

                //PhotonView photonView = photonNetwork.Instantiate(PlayerInfoPrefab.name, transform.position, transform.rotation, 0).GetComponent();
                //photonView.transform.gameObject.GetComponent< PlayerInfo>().PublicMethod(Values);


                joinedPlayerCount++;
            }
        }
        //PhotonNetwork.InstantiateRoomObject(PlayerInfoPrefab.name, new Vector2(0f, 0f), Quaternion.identity);
    }
    [PunRPC]
    public void GetCharacterCards()
    {
        playerInfoList = GameObject.FindGameObjectsWithTag("PlayerInfo");

        foreach (GameObject o in playerInfoList)
        {
            string myCharacter = CharacterList.GetComponent<TextFileToList>().GetRandomLineFromList();
            o.GetComponent<PlayerInfo>().myCharacterCard = myCharacter;
            Debug.Log("My character is:" + myCharacter);
            joinedPlayerCount++;
        }

    }


    public void OnClickGoToGameScene() //LobbyRoom Button
    {
        UpdateAllPlayerInfos();

        if (view.IsMine)
        {
            view.RPC("GetCharacterCards", RpcTarget.AllBuffered);
            view.RPC("SetPlayerCardsAndGoToGameScene", RpcTarget.AllBuffered);

        }
    }
    [PunRPC]
    public void SetPlayerCardsAndGoToGameScene()
    {
        SceneManager.LoadScene("Game");
    }
}
