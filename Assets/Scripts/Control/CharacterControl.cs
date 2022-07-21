using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CharacterControl : MonoBehaviourPun
{
    public GameObject MainCanvas;
    public GameObject[] PlayerInfoList;
    public List<Sprite> CharacterSprites;
    public List<string> SpriteNames;
    private int playerCount;
    float normalScale = 1.25f;
    private bool isZoomed;
    private PhotonView view;
    [SerializeField] private bool isLocalPlayer;
    [SerializeField] private int playerID;

    void Awake()
    {
        view = GetComponent<PhotonView>();
        playerID = PhotonNetwork.LocalPlayer.ActorNumber;
        //PlayerInfoList = GameObject.FindGameObjectsWithTag("PlayerInfo");
        playerCount = 0;
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("PlayerInfo"))
        {
            string characterName = p.GetComponent<PlayerInfo>().myCharacterCard;
            SpriteNames.Add(characterName);
            playerCount++;
        }

        if (isLocalPlayer)
            SetLocalCharacterSprite();
        else
            SetOtherCharacterSprite();

    }

    private void SetLocalCharacterSprite()
    {
        int addZero = 1;
        string name = SpriteNames[playerID - addZero];
        ChangeSpriteByName(name);
    }

    public void SetOtherCharacterSprite()
    {
        int test = view.OwnerActorNr-1;
        string name = SpriteNames[test]; //TEST
        ChangeSpriteByName(name);
    }

    public void AddAllSpriteNamesToList(string name)
    {
        SpriteNames.Add(name);

    }

    public void ChangeSpriteByName(string name)
    {
        GetComponent<SpriteFromAtlas>().characterName = name;
        Debug.Log("Changing sprite in sfa");
    }



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
