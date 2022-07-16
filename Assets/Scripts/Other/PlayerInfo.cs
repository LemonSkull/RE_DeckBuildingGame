using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInfo : MonoBehaviour //SINGLETON
{
    public static PlayerInfo PI;

    public string id;
    public int myCharacterCard;

    public List<Sprite> allCharacters;
    //public Sprite[] allCharacters;

    private void OnEnable()
    {
        if(PlayerInfo.PI == null)
        {
            PlayerInfo.PI = this;
        }
        else
        {
            if(PlayerInfo.PI !=this)
            {
                Destroy(PlayerInfo.PI.gameObject);
                PlayerInfo.PI = this;
            }
        }
        string currentScene = SceneManager.GetActiveScene().name;
        if(currentScene=="Game"||currentScene == "LobbyRoom")
            DontDestroyOnLoad(this.gameObject);

        CheckCharacter();
    }


    void CheckCharacter()
    {
        if(PlayerPrefs.HasKey("MyCharacterCard"))
        {
            myCharacterCard = PlayerPrefs.GetInt("MyCharacterCard");
        }
        else
        {
            myCharacterCard = 0;
            PlayerPrefs.SetInt("MyCharacterCard", myCharacterCard);
        }
    }


}
