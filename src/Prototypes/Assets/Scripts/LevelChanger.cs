using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] private Level[] levels;

    [SerializeField] private LevelDisplay levelDisplay;

    private int currentIndex;

    private void Awake() 
    {
        ChangeLevel(0); 
        AudioManager.instance.Play("CardSelectionTheme");   
    }

    public void ChangeLevel(int changeValue)
    {
        currentIndex += changeValue;

        if(currentIndex < 0)
            currentIndex = levels.Length - 1;

        else if(currentIndex > levels.Length - 1)
            currentIndex = 0;

        if(levelDisplay != null)
            levelDisplay.DisplayLevel(levels[currentIndex]);
    }

    public void PlayLevel()
    {
        GameObject playerHand = levels[currentIndex].playerHand.gameObject;
        GameObject enemyHand = levels[currentIndex].enemyHand.gameObject;

        Object.Instantiate(playerHand, LevelManager.instance.transform);
        Object.Instantiate(enemyHand, LevelManager.instance.transform);

        LevelManager.instance.level = levels[currentIndex];
        
        AudioManager.instance.Stop("CardSelectionTheme");

        LevelManager.instance.LoadScene("Dialogue");
    }

    public void BackButton()
    {
        AudioManager.instance.Stop("CardSelectionTheme");
        LevelManager.instance.LoadScene("MainMenu");
    }
}
