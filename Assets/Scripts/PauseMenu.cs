using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused = false;

    public GameObject pauseMenuUI;

    void Update() {
        if  (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused){
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void Resume()
    {
        
        IsPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;

    }

    void Pause()
    {

        IsPaused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void LoadMenu()
    {   
        Time.timeScale = 1f;
        IsPaused = false;
        SceneManager.LoadScene(0);  
    }
}
