using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnCards : MonoBehaviour
{
    public int cardCount, handCards;
    public GameObject CardPrefab, PNCardPrefab, PlayerDeck;
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

        
    }
    void Start()
    {
        view = GetComponent<PhotonView>();
        currentCard = "ammo10";
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

            //cardSpawn.GetComponent<SpriteRenderer>().sprite = render;
            //Instantiate(cardSpawn, pos, Quaternion.identity);
            PhotonNetwork.InstantiateRoomObject(cardSpawn.name, pos, Quaternion.identity);

            /*
            GameObject cardSpawn = ItemCards;

            //cardSpawn.GetComponent<SpriteFromAtlas>().SetCardSprite("FirstAidKit");

            //cardSpawn.transform.SetParent(GameObject.Find("MainCanvas").transform);
            PhotonNetwork.InstantiateRoomObject(cardSpawn.name, pos, Quaternion.identity);
            */
            cardCount--;
            Deck.Remove(Deck[0]);
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
