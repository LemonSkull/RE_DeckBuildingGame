using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class dragndrop : MonoBehaviour
{
    private Camera cam;
    private Rigidbody2D rb;
    [SerializeField]
    private Vector2 spawnPos, cardPos;
    public GameObject MoveBlocker;
    PhotonView view;
    public bool isMyTurn;
    private bool isOnHolder;

    void Awake()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        cardPos = gameObject.transform.position;
        spawnPos = cardPos;
        isMyTurn = true; //TESTING 1PLAYER
        isOnHolder = false;
    }
    private void Start()
    {
        view = GetComponent<PhotonView>();


    }
    public void CheckViewBlocker()
    {
        if (view.IsMine)
        {
            MoveBlocker.SetActive(false);
        }
        else
            MoveBlocker.SetActive(true);


    }


    public void MyTurn(bool myTurn)
    {
        if(myTurn)
        {
            isMyTurn = true;
        }
        else
        {
            isMyTurn = false;
        }
    }

    void OnMouseDrag()
    {
        if(isMyTurn)
        if (view.IsMine)
        {
            //rb.bodyType = RigidbodyType2D.Static;
            transform.position = GetMousePos();
            transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        }
    }

    Vector3 GetMousePos()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; //koska 2d peli
        return mousePos;
    }
    void OnMouseUp()
    {
        if (isMyTurn)
            //if (view.IsMine)
            {
                if (transform.position.y <= -1.3)
                    cardPos = spawnPos;


                transform.position = cardPos;
                transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (isMyTurn)
        //if (view.IsMine)
        {
            if (other.gameObject.tag == "notHoldingCard")
                if (other.gameObject.layer == 7) //Card Holders
                {
                    cardPos = other.transform.position;
                    isOnHolder = true;
                    other.gameObject.tag = "holdingCard";
                    gameObject.GetComponent<SpriteRenderer>().enabled = true;
                }
                else if (other.gameObject.tag == "HoldingCard")
                {
                    cardPos = spawnPos;
                    //transform.position = spawnPos;
                }
        }
    }

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
