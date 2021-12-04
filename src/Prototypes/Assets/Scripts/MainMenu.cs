using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Start()
    {
        AudioManager.instance.Stop("VictorySound");
        AudioManager.instance.Stop("DefeatSound");
        AudioManager.instance.Play("MenuTheme");
    }

    public void PlayGame()
    {
        AudioManager.instance.Stop("MenuTheme");
        SceneManager.LoadScene("MainGame");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
