using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    /*
    public GameObject PlayerInfoObj;//TEST

    public void OnClickChooseCharacterCard(int whichCharacter)
    {
        if(PlayerInfo.PI !=null)
        {
            PlayerInfo.PI.myCharacterCard = whichCharacter;
            PlayerPrefs.SetInt("MyCharacterCard", whichCharacter);
        }
    }

    public void ChooseRandomCharacterCard(int maxCharacters)
    {
        int val = Random.Range(0, maxCharacters);
        PlayerPrefs.SetInt("MyCharacterCard", val);
    }
    */
    public void OnClickGoToGameScene()
    {
        //int allCharCount = PlayerInfoObj.GetComponent<PlayerInfo>().allCharacters.Count;

        //ChooseRandomCharacterCard(allCharCount);//TEST
        SceneManager.LoadScene("Game");
    }
}
