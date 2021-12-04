using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    [SerializeField] private GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void LoadMenu()
    {
        FindObjectOfType<AudioManager>().Stop("CombatTheme");
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    public void RestartScene()
    {
        FindObjectOfType<AudioManager>().Stop("CombatTheme");
        SceneManager.LoadScene("MainGame");
        Time.timeScale = 1f;
    }
}
