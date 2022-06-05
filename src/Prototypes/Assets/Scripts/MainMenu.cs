using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void Start()
    {
        AudioManager.instance.Stop("VictorySound");
        AudioManager.instance.Stop("DefeatSound");
        AudioManager.instance.Play("MenuTheme");
    }

    public void FreePlay()
    {
        AudioManager.instance.Stop("MenuTheme");
        LevelManager.instance.LoadScene("CardSelection");
    }

    public void Campaign()
    {
        AudioManager.instance.Stop("MenuTheme");
        LevelManager.instance.LoadScene("LevelSelection");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
