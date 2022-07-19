using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnCards : MonoBehaviourPunCallbacks
{
    public int cardCount, handCards;
    public GameObject PNCardPrefab, PlayerDeck, CharacterCardPrefab, CharacterCardListPrefab;
    private GameObject HandCardsParent;
    //public GameObject ItemCards;
    public List<GameObject> Deck;
    public List<GameObject> HandCards;
    public List<GameObject> DiscardPileCards;

    public GameObject[] PlayerInfoList;
    public List<string> CharacterCards;

    public string currentCard, characterCard;

    float startPosX = -3f, startPosY = -1.8f;
    float posX, posY;
    PhotonView view;

    void Awake()
    {
        view = GetComponent<PhotonView>();
        foreach (Transform child in PlayerDeck.transform)
        {
            cardCount++;
            Deck.Add(child.gameObject);
        }

        HandCardsParent = GameObject.FindWithTag("HandCardsParent");
        posX = startPosX; posY = startPosY;
        //if (view.IsMine)
        //CreateCharacterCard();

        PlayerInfoList = GameObject.FindGameObjectsWithTag("PlayerInfo");
        GetHandCardSpriteData();
        //GetPlayerInfoAndCharacterCards();
    }

    public void DrawCard() //Button
    {
        if (Deck.Count >= 1)
        {
            handCards++;
            HandCards.Add(Deck[0]);

            Vector2 pos;
            if(posX>6f&& posY== startPosY) //Change row
            {
                posX = -3.2f;
                posY = -3.5f;
            }
            else if (posX>6&& posY==-4) //Change row
            {
                posX = startPosX;
                posY = startPosY;
            }
            else if (posX>6)
            {
                posX = startPosX;
                posY = startPosY;
            }

            pos = new Vector2(posX += 1.5f, posY);
            
            GameObject cardSpawn = PNCardPrefab;
            var render = Deck[0].GetComponent<SpriteRenderer>().sprite;
            GameObject card = PhotonNetwork.Instantiate(cardSpawn.name, pos, Quaternion.identity);
            card.transform.SetParent(HandCardsParent.transform, false);
            //card.GetComponent<SpriteFromAtlas>().spriteName = currentCard; //Sets currentCard for spawned card!
            //card.GetComponent<SpriteFromAtlas>().isVisible = true;
            card.GetComponent<SpriteFromAtlas>().SetHandCardSpriteVisibility(true);

            cardCount--;
            Deck.Remove(Deck[0]);
        }
        if(Deck.Count==0)
        {
            foreach (GameObject c in DiscardPileCards)
            {
                Deck.Add(c);

            }
            DiscardPileCards.Clear();
            Debug.Log("Shuffling Deck !");
        }
    }
    private void GetHandCardSpriteData()
    {
        currentCard = "ammo30"; //TESTINGGGGGGG
    }

    public void CreateCharacterCard(int count)
    {
        //characterCard = PlayerPrefs.GetString("MyCharacterCard");
        GameObject pInfo = PlayerInfoList[count];
        string cardName = pInfo.GetComponent<PlayerInfo>().myCharacterCard;
        CharacterCards.Add(cardName);
        characterCard = cardName;

        Vector2 characterCardPos = new Vector3(-301.9f, -80.7f, 0f);
        GameObject parent = GameObject.FindWithTag("MainCanvas");

        GameObject card = PhotonNetwork.Instantiate(CharacterCardPrefab.name, characterCardPos, Quaternion.identity);
        card.transform.SetParent(parent.transform, false);

    }


    public void PutHandCardsToDiscardPile()
    {
        Debug.Log("DiscardPileActivated");

        foreach(GameObject c in HandCards)
        {
            DiscardPileCards.Add(c);
            
        }
        HandCards.Clear();

        foreach (Transform child in HandCardsParent.transform)
        {
            PhotonNetwork.Destroy(child.gameObject);
        }
        handCards = 0;
        posX = startPosX; posY = startPosY;
        //view.RPC("DeleteCards", RpcTarget.AllBuffered);
    }
    [PunRPC]
    public void DeleteCards()
    {
        if(view.IsMine)
        foreach (GameObject c in DiscardPileCards)
        {
            PhotonNetwork.Destroy(c);

        }
    }


    public GameObject SpawnPNCardPrefab(GameObject other, Vector2 pos)
    {
        //SpriteRenderer rend = other.GetComponent<SpriteRenderer>();
        GameObject pnObj = PhotonNetwork.Instantiate(PNCardPrefab.name, pos, Quaternion.identity);
        //pnObj.GetComponent<SpriteRenderer>().sprite = rend.sprite;

        //pnObj.GetComponent<ShowCardToOthers>().SetAsParent(other);
        
        return pnObj;

    }


    public void DeletePhotonCard(GameObject obj)
    {

        PhotonNetwork.Destroy(obj);

    }



    public void TossToDiscardPile()
    {
        for (int i = 0; i < handCards; i++)
        {
            DiscardPileCards.Add(HandCards[i]);
            
        }
        HandCards.Clear();
        handCards = 0;

    }


}
