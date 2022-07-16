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

    // Start is called before the first frame update
    void Awake()
    {
        view = GetComponent<PhotonView>();

        if(view.IsMine)
        {
            MainSpawnCards = PhotonNetwork.Instantiate(SpawnCardsPrefab.name, new Vector3(0f,0f,0f), Quaternion.identity);
        }

    }

    public void OnClickDrawCard()
    {
        MainSpawnCards.GetComponent<SpawnCards>().DrawCard();

        Debug.Log("DrawCard from GameRoomControl");
    }


}
