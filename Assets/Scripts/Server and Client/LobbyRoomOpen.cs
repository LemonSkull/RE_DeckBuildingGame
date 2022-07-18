using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class LobbyRoomOpen : MonoBehaviourPunCallbacks
{
    public GameObject PlayerInfoPrefab;

    [SerializeField] List<GameObject> playerInfoList;
    public int joinedPlayerCount;
    public GameObject startGame_btn;
    PhotonView view;

    void Start()
    {
        view = GetComponent<PhotonView>();

        if (playerInfoList.Count == 0)
        {
            int hostID = 1;//Host is always ActorNumber 1!
            joinedPlayerCount++;

            GameObject player = Instantiate(PlayerInfoPrefab, new Vector2(0f, 0f), Quaternion.identity);
            joinedPlayerCount = 1;
            playerInfoList.Add(player);
            player.GetComponent<PlayerInfo>().WhenInstanceIsCreated(hostID);
        }
            if(view.IsMine)
                startGame_btn.SetActive(true);
            else
                startGame_btn.SetActive(false);
    }

    public void AddNewPlayerInfoPrefab()
    {
        joinedPlayerCount = 0;
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            joinedPlayerCount++;
        }
        int count = joinedPlayerCount - 1;
        int _playerID = PhotonNetwork.PlayerList[count].ActorNumber;

        GameObject player = Instantiate(PlayerInfoPrefab, new Vector2(0f, 0f), Quaternion.identity);
        playerInfoList.Add(player);
        player.GetComponent<PlayerInfo>().WhenInstanceIsCreated(_playerID);


        //PhotonNetwork.InstantiateRoomObject(PlayerInfoPrefab.name, new Vector2(0f, 0f), Quaternion.identity);
    }

    public void OnClickGoToGameScene() //LobbyRoom Button
    {
        //int allCharCount = PlayerInfoObj.GetComponent<PlayerInfo>().allCharacters.Count;

        //ChooseRandomCharacterCard(allCharCount);//TEST
        if (view.IsMine)
            view.RPC("GoToGameScene", RpcTarget.AllBuffered);
    }
    [PunRPC]
    public void GoToGameScene()
    {
        SceneManager.LoadScene("Game");
    }
}
