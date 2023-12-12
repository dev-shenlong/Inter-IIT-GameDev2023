using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.UI;

public class HighscoreTable : MonoBehaviour
{   
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<HighscoreEntry> highscoreEntryList;
    private List<Transform> highscoreEntryTransformList;

    private void Awake()
    {
        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");
        
        entryTemplate.gameObject.SetActive(false);


        highscoreEntryList = new List<HighscoreEntry>()
        {
            new HighscoreEntry { score = 521854, name = "AAA" },
            new HighscoreEntry { score = 358462, name = "ANN" },
            new HighscoreEntry { score = 785123, name = "CAT" },
            new HighscoreEntry { score = 15524, name = "JON" },
            new HighscoreEntry { score = 897621, name = "JOE" },
            new HighscoreEntry { score = 68245, name = "MIK" },
            new HighscoreEntry { score = 872931, name = "DAV" },
            new HighscoreEntry { score = 542024, name = "MAX" },
        };


        highscoreEntryTransformList = new List<Transform>();

        foreach (HighscoreEntry highscoreEntry in highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer ,highscoreEntryTransformList);
        }
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry entry, Transform container, List<Transform> transformList)
    {
        
        float templateHeight = 25f;

        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);

        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;

        switch (rank)
        {
        default:
            rankString = rank + "TH"; break;

        case 1: 
        rankString = "1ST"; break;
            
        case 2: 
        rankString = "2ND"; break;
            
        case 3: 
        rankString = "3RD"; break;
        }


        entryTransform.Find("rankText").GetComponent<Text>().text = rankString;

        
        int score = entry.score;

        entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();

        string name = entry.name;
        entryTransform.Find("nameText").GetComponent<Text>().text = name;


            transformList.Add(entryTransform);
            
        }
        
    private class HighscoreEntry {

        public int score;
        public string name;
    }
}


