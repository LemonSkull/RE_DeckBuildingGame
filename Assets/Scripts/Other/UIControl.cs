using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class UIControl : MonoBehaviour
{
    public GameObject MainTable, PlayerHand;
    private bool activeHandButton;

    public void PlayerHandButton() // ON/OFF -button
    {

        if(activeHandButton==false)
        {
            PlayerHand.SetActive(true);
            activeHandButton = true;
        }
        else
        {
            PlayerHand.SetActive(false);
            activeHandButton = false;
        }
    }

}
