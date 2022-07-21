using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CharacterCard : MonoBehaviourPunCallbacks
{
    public List<string> CurrentCharactersList;


    public void AddCharacterToList(string name)
    {
        //if (PhotonNetwork.IsMasterClient)
        {
            CurrentCharactersList.Add(name);
            Debug.Log(name + " added!");
        }
    }

    void OnDestroy()
    {
        //ClearCharactersList();
    }
}
