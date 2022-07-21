using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class TextFileToList : MonoBehaviour
{
    [SerializeField] private string textFileName;
    [SerializeField] private List<string> TextList;
    public List<string> RandomList;
    private int listCount;

    void Start()
    {
        string readFromFilePath = Application.streamingAssetsPath + "/Recall_Chat/" + textFileName + ".txt";
        List<string> fileLines = File.ReadAllLines(readFromFilePath).ToList();

        foreach (string line in fileLines)
        {
            TextList.Add(line);
        }
        listCount = TextList.Count;

    }
    public void SetRandomizedList(int playerCount)
    {
        for (int i = 0; i < playerCount; i++)
        {
            int value = Random.Range(0, listCount);
            string name = TextList[value];
            RandomList.Add(name);
        }
    }
    public string GetStringFromTextByNumber(int numb)
    {
        return TextList[numb];
    }

    public string GetRandomizedCharacterName()
    {
        int count = TextList.Count;
        int value = Random.Range(0, count);
        return TextList[value];

    }

}
