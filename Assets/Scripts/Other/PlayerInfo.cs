using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
//using Photon.Realtime;


public class PlayerInfo : MonoBehaviour
{
    public GameObject CharacterCardPrefab;
    [SerializeField] private int playerID; //PhotonNetwork ActorNumber
    //public int myCharacterCard;
    public string myCharacterCard;
    private SpriteRenderer rend;

    [SerializeField] private bool isHost;
    //public List<Sprite> allCharacters;
    public GameObject PlayableCharactersObject;
    PhotonView view;

    private void Awake()
    {
        view = GetComponent<PhotonView>();
        isHost = PhotonNetwork.IsMasterClient;
        string currentScene = SceneManager.GetActiveScene().name;
        //if(currentScene=="Game"||currentScene == "LobbyRoom")
            DontDestroyOnLoad(this.gameObject);


        rend = GetComponent<SpriteRenderer>();
        rend.enabled = false;
        //CheckCharacter();
    }

    public void WhenInstanceIsCreated(int _playerID) //from LobbyRoomOpen.cs
    {
        Debug.Log("PlayerInfo created for playerID: " +_playerID);
        playerID = _playerID;

    }


}
