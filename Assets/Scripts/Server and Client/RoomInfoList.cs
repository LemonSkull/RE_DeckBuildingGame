using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
//using TMPro;

public class RoomInfoList : MonoBehaviour
{
    public static RoomInfoList RI;
    
    public List<string> JoinedPlayerList;

   // private GameObject playerListTMPObject;

    private void OnEnable()
    {
        if (RoomInfoList.RI == null)
        {
            RoomInfoList.RI = this;
        }
        else
        {
            if (RoomInfoList.RI != this)
            {
                Destroy(RoomInfoList.RI.gameObject);
                RoomInfoList.RI = this;
            }
        }
        //playerListTMPObject = GameObject.FindWithTag("PlayerListTMP");
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Game" || currentScene == "LobbyRoom")
        {
            DontDestroyOnLoad(this.gameObject);
        }

    }
    void Start()
    {
        if (JoinedPlayerList.Count == 0)
        {
            JoinedPlayerList.Add(PlayerPrefs.GetString("NickName"));
            //playerListTMPObject.GetComponent<PlayerListTMP>().AddJoinedPlayerToList(JoinedPlayerList[0]); //ADD HOST
        }
    }

    public void AddToJoinedPlayerList(string playerName)
    {
        JoinedPlayerList.Add(playerName);

    }

}
