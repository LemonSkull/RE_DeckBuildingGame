using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CharacterControl : MonoBehaviourPun
{
    public GameObject[] PlayerCharacters;
    public List<Sprite> CharacterSprites;
    public GameObject MainCanvas;
    float normalScale = 1.25f;
    bool isZoomed;
    [SerializeField] private PhotonView view;

    public void OnClickZoomCard()
    {
        if (isZoomed)
        {
            transform.localScale = new Vector2(normalScale, normalScale);
            isZoomed = false;
        }
        else
        {
            transform.localScale = new Vector2(2.2f, 2.2f);
            isZoomed = true;
        }
    }
}
