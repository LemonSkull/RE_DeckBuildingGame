using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class TextFileToList : MonoBehaviour
{
    [SerializeField] private string textFileName;
    public List<string> TextList;

    void Start()
    {
        string readFromFilePath = Application.streamingAssetsPath + "/Recall_Chat/" + textFileName + ".txt";
        List<string> fileLines = File.ReadAllLines(readFromFilePath).ToList();

        foreach (string line in fileLines)
        {
            TextList.Add(line);
        }
    }

    public int GetTextListCount()
    {
        return TextList.Count;

    }
    public string GetStringFromTextByNumber(int numb)
    {
        return TextList[numb];
    }



    public string GetRandomLineFromList() //NOT IN USE RN
    {
        int listCount = TextList.Count;
        int value = Random.Range(0, listCount);
        return TextList[value];
    }
}
