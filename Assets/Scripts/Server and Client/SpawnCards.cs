using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnCards : MonoBehaviour
{
    public int cardCount, handCards;
    public GameObject CardPrefab, PNCardPrefab, PlayerDeck, HandCardsParent;
    //public GameObject ItemCards;
    public List<GameObject> Deck;
    public List<GameObject> HandCards;
    public List<GameObject> DiscardPileCards;

    public string currentCard;

    float posX = -5, posY = -2;
    PhotonView view;

    void Awake()
    {
        foreach (Transform child in PlayerDeck.transform)
        {
            cardCount++;
            Deck.Add(child.gameObject);
        }
        HandCardsParent = GameObject.FindWithTag("HandCardsParent");
        
    }
    void Start()
    {
        view = GetComponent<PhotonView>();
        currentCard = "ammo10"; //TESTINGGGGGGG
    }

    public void DrawCard() //Button
    {
        if (Deck.Count >= 1)
        {
            handCards++;
            HandCards.Add(Deck[0]);

            Vector2 pos;
            if(posX>6f&& posY==-2)
            {
                posX = -5;
                posY = -3.5f;
            }
            else if (posX>6&& posY==-4)
            {
                posX = -3.5f;
                posY = -2;
            }

            pos = new Vector2(posX += 1.5f, posY);
            
            GameObject cardSpawn = CardPrefab;
            var render = Deck[0].GetComponent<SpriteRenderer>().sprite;
            GameObject card = PhotonNetwork.Instantiate(cardSpawn.name, pos, Quaternion.identity);
            //card.transform.parent = HandCardsParent.transform; //SHOWS USELESS WARNING IN DEBUG????
            card.transform.SetParent(HandCardsParent.transform, false);

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

    public void PutHandCardsToDiscardPile()
    {
        Debug.Log("DiscardPileActivated");

        int i = 0;
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
        posX = -5; posY = -2;
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
