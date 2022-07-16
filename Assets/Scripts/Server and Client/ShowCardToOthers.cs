using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ShowCardToOthers : MonoBehaviour
{
    private Camera cam;
    PhotonView view;
    [SerializeField]//TEST
    private GameObject parentObject;
    public Canvas MainCanvas;
    //private SpriteRenderer rend;
    private Sprite sprt;
    private Vector2 pnCardPos; //Photon Network position
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Awake()
    {
        GameObject canvasObj = GameObject.FindWithTag("MainCanvas");
        MainCanvas = canvasObj.GetComponent<Canvas>();
        gameObject.transform.SetParent(MainCanvas.transform);
        transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
    }
    void Start()
    {
        //rend = GetComponent<SpriteRenderer>();
        //sprt = rend.sprite;
        pnCardPos = gameObject.transform.position;
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        view = GetComponent<PhotonView>();
        /*
        if (view.IsMine) //Only other players see this object
        {
            //gameObject.GetComponent<SpriteRenderer>().enabled = false;

        }
        /*
        if (sprt == null)
        {
            string sprtName = "ammo10";
            GetComponent<SpriteFromAtlas>().spriteName = sprtName;
            //GetComponent<SpriteRenderer>().sprite = parentObject.GetComponent<SpriteRenderer>().sprite;
            Debug.Log("No sprite -> sprite changed");
        }
        */
    }









    void OnMouseDrag()
    {
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

    }
    void OnMouseUp()
    {
        transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
    }


    public void ParentCardPosition(Vector2 pos)
    {
        transform.position = pos;


    }



    public void ChangePosition(Vector2 pos)
    {
        gameObject.transform.position = pos;
    }
}
