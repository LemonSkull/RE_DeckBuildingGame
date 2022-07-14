using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameMaster : MonoBehaviour
{
    public int playerCount, playerTurn;
    public string  playerName;
    private string gameMaster;

    public void GameMasterSave()
    {
        gameMaster = playerName;
        PlayerPrefs.SetString("GameMaster", gameMaster);
        PlayerPrefs.SetInt("PlayerCount", playerCount);
        PlayerPrefs.Save();
        Debug.Log("Game Master data saved!");
        //int loadedNumber = PlayerPrefs.GetInt(myNumber);

    }

}
