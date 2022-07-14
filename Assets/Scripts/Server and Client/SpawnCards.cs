using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnCards : MonoBehaviour
{
    public int cardCount, handCards;
    public GameObject CardPrefab, PlayerDeck;
    public List<GameObject> Deck;
    public List<GameObject> HandCards;
    public List<GameObject> DiscardPileCards;
    float posX = -4, posY = -2;
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
                posY = -4;
            }
            else if (posX>6&& posY==-4)
            {
                posX = -4;
                posY = -2;
            }

                pos = new Vector2(posX += 1.5f, posY);

            GameObject cardSpawn = CardPrefab;
            var render = Deck[0].GetComponent<SpriteRenderer>().sprite;

            cardSpawn.GetComponent<SpriteRenderer>().sprite = render;

            PhotonNetwork.Instantiate(cardSpawn.name, pos, Quaternion.identity);

            cardCount--;
            Deck.Remove(Deck[0]);
        }
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
