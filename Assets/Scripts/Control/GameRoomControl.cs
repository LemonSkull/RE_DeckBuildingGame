using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameRoomControl : MonoBehaviourPunCallbacks
{
    PhotonView view;
    public GameObject SpawnCardsPrefab;
    private GameObject MainSpawnCards;

    [SerializeField] private int whoIsPlaying, playerCount; //IN USE

    // Start is called before the first frame update
    void Awake()
    {
        view = GetComponent<PhotonView>();


        MainSpawnCards = PhotonNetwork.Instantiate(SpawnCardsPrefab.name, new Vector3(0f,0f,0f), Quaternion.identity);


    }

    public void OnClickDrawCard()
    {
        MainSpawnCards.GetComponent<SpawnCards>().DrawCard();

        Debug.Log("DrawCard from GameRoomControl");
    }

    public void OnClickTransferOwnershipToNextPlayer() //NOT WORKING
    {
        if (view.IsMine)
        {
            int playercount = 0;

            foreach (Player p in PhotonNetwork.PlayerList)
            {
                playercount++;

            }
            //playercount--;

            if (whoIsPlaying > playercount)
            {
                whoIsPlaying = 0;
                Debug.Log("New Round!");
            }
            else
                whoIsPlaying++;

            Debug.Log("playercount=" + playercount);
            Debug.Log("whoIsPlaying=" + whoIsPlaying);
            view.TransferOwnership(1);

        }
    }
}
