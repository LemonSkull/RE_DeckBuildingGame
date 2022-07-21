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
    [SerializeField] private int myID;
    [SerializeField] private int joinedPlayerCount;
    [SerializeField] private string myCharacterCard;
    public GameObject startGame_btn;
    private bool isMaster, goToNextLevel;
    Vector2 playerInfoPos = new Vector2(-6.9f, -1.6f);
    PhotonView view;


    void Start()
    {
        goToNextLevel = false;
        view = GetComponent<PhotonView>();
        myID = view.OwnerActorNr;
        isMaster = PhotonNetwork.IsMasterClient;

            if(view.IsMine)
                startGame_btn.SetActive(true);
            else
                startGame_btn.SetActive(false);
        
    }
    private void Update()
    {
        if(goToNextLevel)
        {
            myCharacterCard = CharacterList.GetComponent<TextFileToList>().GetRandomizedCharacterName();
            PlayerPrefs.SetString("myCharacterCard", myCharacterCard);


            GameObject myInfo = PhotonNetwork.Instantiate(PlayerInfoPrefab.name, playerInfoPos, Quaternion.identity);
            //myInfo.GetComponent<PlayerInfo>().myCharacterCard = myCharacterCard;

            goToNextLevel = false;
            if (view.IsMine)
            {
                view.RPC("GoToGameScene", RpcTarget.AllBuffered);

            }
        }
    }



    public void OnClickGoToGameScene() //LobbyRoom Button
    {
        if(view.IsMine)
        view.RPC("RPC_OnClickGoToGameScene", RpcTarget.AllBuffered);

    }
    [PunRPC]
    public void RPC_OnClickGoToGameScene()
    {
        goToNextLevel = true;


    }

    public void UpdateAllPlayerInfos() //When player enters room (CreateAndJoinRooms.cs)
    {

        {
            //joinedPlayerCount = 0;
            //foreach (Player p in PhotonNetwork.PlayerList)
            {
                //int count = PhotonNetwork.CurrentRoom.PlayerCount;
                //int _playerID = PhotonNetwork.PlayerList[count].ActorNumber;
                //PlayerInfoHold = PhotonNetwork.Instantiate(PlayerInfoPrefab.name, playerInfoPos, Quaternion.identity);
                //PlayerInfoHold.GetComponent<PlayerInfo>().myCharacterCard = myCharacterCard;
                //player.GetComponent<PlayerInfo>().myCharacterCard = myCharacterCard;
                //joinedPlayerCount++;
            }

            //playerInfoList = GameObject.FindGameObjectsWithTag("PlayerInfo");

            //playerInfoList[myID - 1].GetComponent<PlayerInfo>().GetCharacterSpriteName(myCharacterCard);
        }
    }

    [PunRPC]
    public void GoToGameScene()
    {
        SceneManager.LoadScene("Game");
    }
}
