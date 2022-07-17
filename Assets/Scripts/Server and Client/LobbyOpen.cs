using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LobbyOpen : MonoBehaviourPunCallbacks
{
    public GameObject PlayerInfoPrefab;

    [SerializeField] List<GameObject> otherPlayerInfos;
    public int joinedPlayerCount;
    PhotonView view;

    void Start()
    {
        view = GetComponent<PhotonView>();

        if (otherPlayerInfos.Count == 0)
        {
            joinedPlayerCount++;

            otherPlayerInfos.Add(Instantiate(PlayerInfoPrefab, new Vector2(0f, 0f), Quaternion.identity));
            joinedPlayerCount = 1;
        }

    }

    public void AddNewPlayerInfoPrefab(bool isHost)
    {
        if (isHost)
        {
            joinedPlayerCount++;
            GameObject player = Instantiate(PlayerInfoPrefab, new Vector2(0f, 0f), Quaternion.identity);
            otherPlayerInfos.Add(player);
            player.GetComponent<PlayerInfo>().WhenIsCreated(isHost, joinedPlayerCount);
        }
        else
        {


        }

        //PhotonNetwork.InstantiateRoomObject(PlayerInfoPrefab.name, new Vector2(0f, 0f), Quaternion.identity);
    }


}
