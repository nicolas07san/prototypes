using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text levelName;
    [SerializeField] private TMP_Text levelDescription;
    [SerializeField] private Image levelImage;
    [SerializeField] private Button playButton;
    [SerializeField] private GameObject lockIcon;

    public void DisplayLevel(Level level)
    {
        levelName.text = level.levelName;
        levelDescription.text = level.levelDescription;
        levelImage.sprite = level.levelImage;

        bool levelUnlocked = PlayerPrefs.GetInt("lastUnlockedLevel", 0) >= level.levelIndex;

        lockIcon.SetActive(!levelUnlocked);
        playButton.interactable = levelUnlocked;

        if(levelUnlocked)
        {
            levelImage.color = Color.white;
        }
        else
        {
            levelImage.color = Color.grey;
        } 
    }

}
