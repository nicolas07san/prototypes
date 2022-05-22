using UnityEngine;

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
        LevelManager.instance.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    public void Continue()
    {
        FindObjectOfType<AudioManager>().Stop("CombatTheme");

        if(CombatManager.isCampaignLevel)
        {
            LevelManager.instance.LoadScene("LevelSelection");

            if(CombatManager.playerWin)
            {
                PlayerPrefs.SetInt("lastUnlockedLevel", LevelManager.instance.level.levelIndex + 1);
                PlayerPrefs.Save();
            }

            LevelManager.instance.level = null;
            
        }
        else
        {
            LevelManager.instance.LoadScene("CardSelection");
        }
            
        Time.timeScale = 1f;
    }
}
