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

    public void Pause()
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

        if(LevelManager.isCampaignLevel)
        {

            if(CombatManager.playerWin)
            {
                if(PlayerPrefs.GetInt("lastUnlockedLevel", 0) <= LevelManager.instance.level.levelIndex)
                {
                    PlayerPrefs.SetInt("lastUnlockedLevel", LevelManager.instance.level.levelIndex + 1);
                    PlayerPrefs.Save();
                }
                
            }

            LevelManager.instance.level = null;

            LevelManager.isCampaignLevel = false;

            LevelManager.instance.LoadScene("LevelSelection");
            
        }
        else
        {
            LevelManager.instance.LoadScene("CardSelection");
        }
            
        Time.timeScale = 1f;
    }
}
