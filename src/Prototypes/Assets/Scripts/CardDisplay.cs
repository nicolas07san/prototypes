using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card card;

    public Text nameText;

    public Image artworkImage;

    public Text atk1NameText;
    public Text atk1DmgText;
    public Text atk1CostText;

    public Text healthText;
    public Text shieldText;

    void Start()
    {
        nameText.text = card.characterName;

        artworkImage.sprite = card.artwork;

        atk1NameText.text = card.attack1Name;
        atk1DmgText.text = card.attack1Dmg.ToString();
        atk1CostText.text = card.attack1Cost.ToString();

        healthText.text = card.health.ToString();
        shieldText.text = card.shield.ToString();
    }

}
