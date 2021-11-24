using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Start()
    {
        FindObjectOfType<AudioManager>().Play("MenuTheme");
    }

    public void PlayGame()
    {
        FindObjectOfType<AudioManager>().Stop("MenuTheme");
        SceneManager.LoadScene("MainGame");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
