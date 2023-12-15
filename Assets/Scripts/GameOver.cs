using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public TMP_InputField inputfield;

    [SerializeField] private itemcollectable itemCollectable;

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void AddNewHighscore()
    {
        int score = itemCollectable.collectItem;

        if (inputfield == null)
        {
            Debug.LogError("Inputfield is null. Make sure it is assigned in the Unity Editor.");
            return;
        }

        name = inputfield.GetComponent<TMP_InputField>().text;
        Debug.Log(name);

        HighscoreTable.HighscoreEntry highscoreEntry = new HighscoreTable.HighscoreEntry { score = score, name = name };

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        HighscoreTable.Highscores highscores = JsonUtility.FromJson<HighscoreTable.Highscores>(jsonString);

        if (highscores != null)
        {
            highscores.highscoreEntryList.Add(highscoreEntry);
            string json = JsonUtility.ToJson(highscores);
            PlayerPrefs.SetString("highscoreTable", json);
            PlayerPrefs.Save();
        }
        else
        {
            List<HighscoreTable.HighscoreEntry> highscoreEntryList = new List<HighscoreTable.HighscoreEntry>();
            highscoreEntryList.Add(highscoreEntry);

            HighscoreTable.Highscores highscoresnew = new HighscoreTable.Highscores { highscoreEntryList = highscoreEntryList };
            string json = JsonUtility.ToJson(highscoresnew);
            PlayerPrefs.SetString("highscoreTable", json);
            PlayerPrefs.Save();
            Debug.Log(PlayerPrefs.GetString("highscoreTable"));
        }

        SceneManager.LoadScene("leaderboard");
    }
}
