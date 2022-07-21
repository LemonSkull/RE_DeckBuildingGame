using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class SpriteFromAtlas : MonoBehaviour
{
    [SerializeField] SpriteAtlas atlas;
    public string spriteName, characterName;
    private Image image;
    private GameObject SpawnCardsPrefab;
    [SerializeField] private bool isVisible, isCharacterCard;
    public int characterCardNumber;

    void Awake()
    {
        image = GetComponent<Image>();
        SetHandCardSpriteVisibility(isVisible);
        SpawnCardsPrefab = GameObject.FindWithTag("Respawn");
    }

    void Start()
    {
        if (isCharacterCard)
        {
            //spriteName = SpawnCardsPrefab.GetComponent<SpawnCards>().GetCharacterCardByID(characterCardNumber);
            spriteName = characterName;
            SetHandCardSpriteVisibility(true);
        }
        else
        {
            spriteName = SpawnCardsPrefab.GetComponent<SpawnCards>().currentCard; //Not in use -> SpawnCards sends data now
            image.sprite = atlas.GetSprite(spriteName);
        }
    }
    public void SetCardSprite(string name) //Not in use rn
    {
        image.sprite = atlas.GetSprite(name);
    }

    public void SetHandCardSpriteVisibility(bool show)
    {
        if (show)
        {
            image.color = new Color(255, 255, 255, 255);
            image.sprite = atlas.GetSprite(spriteName);
        }
        else
            image.color = new Color(0, 0, 0, 255);
    }
}
