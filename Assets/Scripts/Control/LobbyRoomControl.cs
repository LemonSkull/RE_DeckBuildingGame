using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class LobbyRoomControl : MonoBehaviourPunCallbacks
{


    void Start()
    { }
    
    /*
    public GameObject PlayerInfoObj;//TEST

    public void OnClickChooseCharacterCard(int whichCharacter)
    {
        if(PlayerInfo.PI !=null)
        {
            PlayerInfo.PI.myCharacterCard = whichCharacter;
            PlayerPrefs.SetInt("MyCharacterCard", whichCharacter);
        }
    }

    public void ChooseRandomCharacterCard(int maxCharacters)
    {
        int val = Random.Range(0, maxCharacters);
        PlayerPrefs.SetInt("MyCharacterCard", val);
    }
    */


}
