using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;


public class PlayerInfo : MonoBehaviourPunCallbacks //SINGLETON
{
    public int playerNumber;
    public string id;
    public int myCharacterCard;
    private SpriteRenderer rend;

    [SerializeField] private bool isHost;
    //public List<Sprite> allCharacters;
    public GameObject PlayableCharactersObject;

    private void Awake()
    {
        isHost = false;

        string currentScene = SceneManager.GetActiveScene().name;
        if(currentScene=="Game"||currentScene == "LobbyRoom")
            DontDestroyOnLoad(this.gameObject);

        rend = GetComponent<SpriteRenderer>();
        rend.enabled = false;
        //CheckCharacter();
    }
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            isHost = true;
            transform.gameObject.tag = "PlayerInfoMaster";
            GetRandomPlayableCharacterFromList();
        }

    }
    public void WhenIsCreated(bool ishost, int playernumb)
    {
            playerNumber = playernumb;
            isHost = ishost;


    }


    void CheckCharacter() //NOT IN USE ATM
    {
        if(PlayerPrefs.HasKey("MyCharacterCard"))
        {
            myCharacterCard = PlayerPrefs.GetInt("MyCharacterCard");
        }
        else
        {
            myCharacterCard = 0;
            PlayerPrefs.SetInt("MyCharacterCard", myCharacterCard);
        }
    }

    private void GetRandomPlayableCharacterFromList()
    {

        int count = PlayableCharactersObject.GetComponent<ListOfCharacters>().CharacterCards.Count;
        int cardNumber = Random.Range(0,count);

        Sprite sprite = PlayableCharactersObject.GetComponent<ListOfCharacters>().CharacterCards[cardNumber];
        rend.sprite = sprite;
    }
}
