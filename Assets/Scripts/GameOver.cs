using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public TMP_InputField inputfield;

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void AddNewHighscore()
    {
        int score = 100;

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

        highscores.highscoreEntryList.Add(highscoreEntry);

        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }
}
