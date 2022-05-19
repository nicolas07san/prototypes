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
    public void DisplayLevel(Level level)
    {
        levelName.text = level.levelName;
        levelDescription.text = level.levelDescription;
        levelImage.sprite = level.levelImage; 
    }

}
