using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    [SerializeField]private Card card;

    [SerializeField]private TMP_Text nameText;

    [SerializeField]private Image artworkImage;

    [Header("Attack 1")]
    [SerializeField] private TMP_Text atk1NameText;
    [SerializeField] private TMP_Text atk1CostText;
    [SerializeField] private TMP_Text atk1DmgText;

    [Header("Attack 2")]
    [SerializeField] private TMP_Text atk2NameText;
    [SerializeField] private TMP_Text atk2CostText;
    [SerializeField] private TMP_Text atk2DmgText;

    [Header("Attack 3")]
    [SerializeField] private TMP_Text atk3NameText;
    [SerializeField] private TMP_Text atk3CostText;
    [SerializeField] private TMP_Text atk3DmgText;

    [Header("Base Stats")]
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text shieldText;


    void Start()
    {
        nameText.text = card.characterName;

        artworkImage.sprite = card.artwork;

        // Attack 1
        atk1NameText.text = card.attack1Name;
        atk1CostText.text = card.attack1Cost.ToString();
        atk1DmgText.text = card.attack1Dmg.ToString();
        
        // Attack 2
        atk2NameText.text = card.attack2Name;
        atk2CostText.text = card.attack2Cost.ToString();
        atk2DmgText.text = card.attack2Dmg.ToString();

        // Attack 3
        atk3NameText.text = card.attack3Name;
        atk3CostText.text = card.attack3Cost.ToString();
        atk3DmgText.text = card.attack3Dmg.ToString();

        //Base stats
        healthText.text = card.health.ToString();
        shieldText.text = card.shield.ToString(); 
    }

}
