using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Photon.Pun;

public class dragndrop : MonoBehaviour
{
    //private Camera cam;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRend;
    //[SerializeField]
    public int cardNumber;
    private Vector2 spawnPos, cardPos;
    public GameObject MoveBlocker, SpawnCards;
    [SerializeField]//TESTING
    private GameObject PNChildCard;
    //PhotonView view;
    Vector2 mousePosition;
    private int minX=-7, maxX=7, minY=-3, maxY=4, pnLimitY=-1;
    private bool CardVisibleToOthers;

    void Awake()
    {
        CardVisibleToOthers = false;
        //cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        spriteRend = GetComponent<SpriteRenderer>();
        cardPos = gameObject.transform.position;
        spawnPos = cardPos;

        gameObject.name = "playerCard_" + cardNumber.ToString();
        
    }
    private void Start()
    {
        //view = GetComponent<PhotonView>();

    }
    public int GetCardNumber()
    {
        return cardNumber;
    }

    void OnMouseDrag()
    {
        //if(isMyTurn)
        //if (view.IsMine)
        {
            rb.bodyType = RigidbodyType2D.Static;
            transform.position = GetMousePos();
            transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            spriteRend.sortingOrder = 6;
        }
    }

    Vector3 GetMousePos()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; //koska 2d peli
        mousePosition = new Vector2(mousePos.x, mousePos.y);
        return mousePos;
    }
    void OnMouseUp()
    {
        //if (isMyTurn)
            //if (view.IsMine)
            {
            
            rb.bodyType = RigidbodyType2D.Dynamic;


            if (mousePosition.x <= minX || mousePosition.x >= maxX
                || mousePosition.y <= minY || mousePosition.y >= maxY){

                transform.position = cardPos;

            }
            else if (mousePosition.y <= pnLimitY)
            {
                transform.position = cardPos;

            }
            else if (mousePosition.y >= pnLimitY) //Instantiate PNPrefab (other players see it)
            {
                if (!CardVisibleToOthers)
                {
                    Vector2 pos = new Vector2(transform.position.x, transform.position.y);
                    PNChildCard = SpawnCards.GetComponent<SpawnCards>().SpawnPNCardPrefab(gameObject, pos);
                    CardVisibleToOthers = true;
                }
                else
                {
                    Vector2 pos = new Vector2(transform.position.x, transform.position.y);
                    PNChildCard.GetComponent<ShowCardToOthers>().ParentCardPosition(pos);
                }

                
            }
                spriteRend.sortingOrder = 5;
                transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            }
    }




    /*
    void OnTriggerEnter2D(Collider2D other)
    {
        //if (isMyTurn)
        
        {
            if (other.gameObject.tag == "notHoldingCard")
                if (other.gameObject.layer == 7) //Card Holders
                {
                    cardPos = other.transform.position;
                    isOnHolder = true;
                    other.gameObject.tag = "holdingCard";
                    
                }
                else if (other.gameObject.tag == "HoldingCard")
                {
                    cardPos = spawnPos;
                    //transform.position = spawnPos;
                }
        }
    }
    */
    /*
    void OnTriggerExit2D(Collider2D other)
    {
        if (isMyTurn)
        if (view.IsMine)
        {
            if (other.gameObject.tag == "holdingCard")
                if (other.gameObject.layer == 7) //Card Holders
                {
                    Debug.Log("Exit");
                    cardPos = spawnPos;
                        isOnHolder = false;
                        other.gameObject.tag = "notHoldingCard";
                }
        }
    }
    */

}
