using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class LobbyRoomControl : MonoBehaviourPunCallbacks
{
    public GameObject CharacterList;
    
    void Start() //DELETE ALL
    {


        string myCharacter = CharacterList.GetComponent<TextFileToList>().GetRandomLineFromList();
        PlayerPrefs.SetString("MyCharacterCard", myCharacter);

        Debug.Log("My character is:" + myCharacter);
    }
    


}
