using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.UI;
//using Photon.Realtime;


public class PlayerInfo : MonoBehaviourPun, IPunInstantiateMagicCallback
{
    public GameObject CharacterCardPrefab;
    [SerializeField] private int playerID; //PhotonNetwork ActorNumber
    //public int myCharacterCard;
    public string myCharacterCard;
    public GameObject[] PlayerInfoList;
    private SpriteRenderer rend;


    [SerializeField] private bool isMaster;
    //public List<Sprite> allCharacters;
    PhotonView view;

    private void Awake()
    {
        view = GetComponent<PhotonView>();
        playerID = view.OwnerActorNr;
        isMaster = PhotonNetwork.IsMasterClient;
        //if(currentScene=="Game"||currentScene == "LobbyRoom")
        DontDestroyOnLoad(this.gameObject);
        rend = GetComponent<SpriteRenderer>();
        rend.enabled = false;
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        //if (view.IsMine)
        {
            myCharacterCard = PlayerPrefs.GetString("myCharacterCard");
            if(view.IsMine)
            view.RPC("SendMyCharacterCardToOthers", RpcTarget.OthersBuffered, myCharacterCard);
        }
    }
    [PunRPC]
    void SendMyCharacterCardToOthers(string cardName)
    {
        myCharacterCard = cardName;
    }

    private void Start()
    {
        if(myCharacterCard==null)
        {
            Debug.Log("Null myCharacterCard - Player: " +playerID);




        }

    }



}
