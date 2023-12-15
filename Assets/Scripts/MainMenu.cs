using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 1f;
    }
    public void PlayGame () {
        
        SceneManager.LoadScene(1);
    }

    public void QuitGame () {
        
        Debug.Log("Quit");
        Application.Quit();
    }

    public void LoadLeaderBoard()
    {
        SceneManager.LoadScene("leaderboard");
    }

    public void LoadHelp()
    {
        SceneManager.LoadScene("help_scene");
    }

    public void LoadOptionsMenu()
    {
        SceneManager.LoadScene("options_scene");
    }
}
