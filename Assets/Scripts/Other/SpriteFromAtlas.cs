using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class SpriteFromAtlas : MonoBehaviour
{
    [SerializeField] SpriteAtlas atlas;
    public string spriteName;
    private GameObject SpawnCardsPrefab;


    void Start()
    {
        SpawnCardsPrefab = GameObject.FindWithTag("Respawn");
        spriteName = SpawnCardsPrefab.GetComponent<SpawnCards>().currentCard;
        GetComponent<Image>().sprite = atlas.GetSprite(spriteName);
    }
    public void SetCardSprite(string name)
    {
        GetComponent<Image>().sprite = atlas.GetSprite(name);
    }
    public void SetCardSpriteVisible()
    {
        GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        GetComponent<Image>().sprite = atlas.GetSprite(spriteName);
    }
}
