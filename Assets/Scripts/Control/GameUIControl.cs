using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class GameUIControl : MonoBehaviourPun
{
    public GameObject PlayerTurnUI,PlayerInfoPrefab; //Scipt = GameUIChild.cs
    public TMP_Text whoIsPlaying;
    [SerializeField] string playerName;
    public int playerID;
    //private SpriteRenderer rendPInfo;

    void Awake()
    {
        playerID=PhotonNetwork.LocalPlayer.ActorNumber;
        whoIsPlaying.text = "";
        //PlayerInfoPrefab = GameObject.FindWithTag("PlayerInfo");
        //rendPInfo = PlayerInfoPrefab.GetComponent<SpriteRenderer>();
    }



    public void UIPlayerNextTurnStart(string name, int id)
    {
        playerName = name;
        if (playerID == id)
        {
            PlayerTurnUI.SetActive(true);
            //rendPInfo.enabled = true;
        }
        else
        {
            PlayerTurnUI.SetActive(false);
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
