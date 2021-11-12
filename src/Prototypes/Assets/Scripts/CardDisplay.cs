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
    [SerializeField] private TMP_Text atk1DmgText;
    [SerializeField] private TMP_Text atk1CostText;

    [Header("Attack 2")]
    [SerializeField] private TMP_Text atk2NameText;
    [SerializeField] private TMP_Text atk2DmgText;
    [SerializeField] private TMP_Text atk2CostText;

    [Header("Attack 3")]
    [SerializeField] private TMP_Text atk3NameText;
    [SerializeField] private TMP_Text atk3DmgText;
    [SerializeField] private TMP_Text atk3CostText;

    [Header("Base Stats")]
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text shieldText;

    void Start()
    {
        nameText.text = card.characterName;

        artworkImage.sprite = card.artwork;

        // Attack 1
        atk1NameText.text = card.attack1Name;
        atk1DmgText.text = card.attack1Dmg.ToString();
        atk1CostText.text = card.attack1Cost.ToString();

        // Attack 2
        atk2NameText.text = card.attack2Name;
        atk2DmgText.text = card.attack2Dmg.ToString();
        atk2CostText.text = card.attack2Cost.ToString();

        // Attack 3
        atk3NameText.text = card.attack3Name;
        atk3DmgText.text = card.attack3Dmg.ToString();
        atk3CostText.text = card.attack3Cost.ToString();

        healthText.text = card.health.ToString();
        shieldText.text = card.shield.ToString();
    }

}
