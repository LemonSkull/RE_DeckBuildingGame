using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class GameUIControl : MonoBehaviourPun
{
    public GameObject PlayerTurnUI; //Shows current player's deck and buttons
    public TMP_Text whoIsPlaying;
    //public GameObject[] PlayerInfoList;
    public GameObject OtherCharacterCard;
    
    [SerializeField] string playerName;
    public int playerID;
    //private SpriteRenderer rendPInfo;

    void Awake()
    {
        playerID=PhotonNetwork.LocalPlayer.ActorNumber;
        whoIsPlaying.text = "";

        //PlayerInfoList = GameObject.FindGameObjectsWithTag("PlayerInfo");


    }

    public void UIPlayerNextTurnStart(string name, int id)
    {
        playerName = name;
        if (playerID == id)
        {
            PlayerTurnUI.SetActive(true);
            OtherCharacterCard.SetActive(false);
            //rendPInfo.enabled = true;
        }
        else
        {
            PlayerTurnUI.SetActive(false);
            OtherCharacterCard.SetActive(true);
            //rendPInfo.enabled = false;
            StartCoroutine(ShowTurnInUI());
        }
    }
    IEnumerator ShowTurnInUI()
    {
        whoIsPlaying.text = playerName + "'s turn!";

        yield return new WaitForSeconds(2f);
        whoIsPlaying.text = "";
    }
}
