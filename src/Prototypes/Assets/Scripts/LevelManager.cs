using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] private GameObject transitionCanvas;
    [SerializeField] private Animator transition;
    [SerializeField] private int transitionTimeMiliseconds = 1000;
    
    void Awake()
    {
        if(instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public async void LoadScene(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        transitionCanvas.SetActive(true);
        transition.SetTrigger("Start");

        await Task.Delay(transitionTimeMiliseconds);

        transitionCanvas.SetActive(false);

        scene.allowSceneActivation = true;
    }
}
