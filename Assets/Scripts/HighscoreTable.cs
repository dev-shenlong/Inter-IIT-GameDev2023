using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HighscoreTable : MonoBehaviour
{   
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;

    private void Awake()
    {
        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");
        
        entryTemplate.gameObject.SetActive(false);

        /*
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
        }; */


        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null)
        {
            return;
        }
        if (highscores.highscoreEntryList == null)
        {
            return;
        }

        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                {
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }

        highscoreEntryTransformList = new List<Transform>();

        for(int i = 0; i < Math.Min(highscores.highscoreEntryList.Count, 10); i++)
        {
            CreateHighscoreEntryTransform(highscores.highscoreEntryList[i], entryContainer ,highscoreEntryTransformList);
        }

        /*
        Highscores highscores = new Highscores { highscoreEntryList = highscoreEntryList };
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetString("highscoreTable"));*/
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry entry, Transform container, List<Transform> transformList)
    {
        
        float templateHeight = 30f;

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

        entryTransform.Find("background").gameObject.SetActive(rank % 2 == 1);

        transformList.Add(entryTransform);
            
        }
    
    private void AddHighScoreEntry(int score, string name)
    {

        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        highscores.highscoreEntryList.Add(highscoreEntry);

        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    public class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    [System.Serializable]
    public class HighscoreEntry {

        public int score;
        public string name;
    }
}


