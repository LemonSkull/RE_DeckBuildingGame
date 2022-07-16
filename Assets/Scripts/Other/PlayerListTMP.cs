using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerListTMP : MonoBehaviour
{
    //private GameObject MainCanvas;
    private TMP_Text playerListTMP;
    public List<string> JoinedPlayerList;
    private PhotonView view;

    void Awake()
    {
        //MainCanvas = GameObject.FindWithTag("MainCanvas");
        playerListTMP = GetComponent<TextMeshProUGUI>();
        view = GetComponent<PhotonView>();
    }
    void Start()
    {
        JoinedPlayerList = RoomInfoList.RI.JoinedPlayerList;
    }


    public void UpdatePlayerList()
    {
        JoinedPlayerList = RoomInfoList.RI.JoinedPlayerList;
        view.RPC("UpdatePlayerListTMP", RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void UpdatePlayerListTMP()
    {
        int length = JoinedPlayerList.Count;
        string list = "";
        for (int i = 0; i < length; i++)
        {
            list = list + "\n" + JoinedPlayerList[i];
        }
        playerListTMP.SetText(list);
    }


}
