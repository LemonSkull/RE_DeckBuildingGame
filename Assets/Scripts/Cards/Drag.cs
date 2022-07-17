using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using UnityEngine.UI;

public class Drag : MonoBehaviour
{
    [SerializeField]
    private GameObject MainCanvas;
    private Canvas canvas;
    private PhotonView view;
    [SerializeField]
    private Vector2 cardPosition;
    [SerializeField]
    private Vector2 lastPos;
    private bool isVisible;
    //[SerializeField] //DEBUG
    private float minX = -8.5f, maxX = 8.5f, minY = -4.5f, maxY = 4.5f, pnLimitY = -2f;

    void Awake()
    {
        if(canvas == null)
        {
            //canvas = GetComponent<ShowCardToOthers>().MainCanvas;
            //GameObject canvasObj = GameObject.FindWithTag("MainCanvas");
            MainCanvas = gameObject.transform.parent.gameObject;
            canvas = MainCanvas.GetComponent<Canvas>();
            Debug.Log("MainCanvas =" + MainCanvas + "inDrag.cs");
        }
    }
    void Start()
    {
        view = GetComponent<PhotonView>();
        lastPos = transform.position;

        //view = PhotonView.Find(1); //MainCanvas -> error when pointerExit;

        isVisible = true;

        /* //NOT IN USE RN
        if (view.IsMine)
        {
            isVisible = true;
        }
            
        else
        {
            isVisible = false;
            GetComponent<Image>().color = new Color32(0, 0, 0, 100);
        }
        */
        cardPosition = transform.position;
    }


    [PunRPC]
    public void DragHandler(BaseEventData data)
    {
        //if (view.IsMine) //TESTING WHEN OFF
        {
            PointerEventData pointerData = (PointerEventData)data;

            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                (RectTransform)canvas.transform,
                pointerData.position,
                canvas.worldCamera,
                out position);

            transform.position = canvas.transform.TransformPoint(position);
            transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        }
    }
    public void PointerEnter()
    {
        transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        transform.SetAsLastSibling();
    }
    public void PointerExit()
    {
        transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
    }
    public void DragEnd()
    {
        cardPosition = transform.position;

        if (cardPosition.x <= minX || cardPosition.x >= maxX
     || cardPosition.y <= minY || cardPosition.y >= maxY)
        {
            transform.position = lastPos;

        }
        else if (!isVisible) //Instantiate PNPrefab (other players see it)
        {
            if (cardPosition.y >= pnLimitY)
            {
                
                //view.RPC("changeImage", PhotonTargets);
                //view.RPC("RPC_ChangeImage", RpcTarget.AllBuffered);
            }
        }

        lastPos = transform.position;
        view.RPC("RPC_PointerExit", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void RPC_PointerExit()
    {
        transform.position = lastPos;
    }


    [PunRPC]
    public void RPC_ChangeImage()
    {
        //GetComponent<Image>().color = new Color32(255, 255, 255, 255);

        GetComponent<SpriteFromAtlas>().SetCardSpriteVisible();
    }
}

