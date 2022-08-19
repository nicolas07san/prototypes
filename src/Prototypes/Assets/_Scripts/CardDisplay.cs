using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    [SerializeField]private Card _card;

    [SerializeField]private TMP_Text _nameText;

    [SerializeField]private Image _artworkImage;

    [Header("Light Attack")]
    [SerializeField] private TMP_Text _lightAttackCostText;
    [SerializeField] private TMP_Text _lightAttackDmgText;
    [SerializeField] private Button _lightAttackButton;

    [Header("Heavy Attack")]
    [SerializeField] private TMP_Text _heavyAttackCostText;
    [SerializeField] private TMP_Text _heavyAttackDmgText;
    [SerializeField] private Button _heavyAttackButton;

    [Header("Support Action")]
    [SerializeField] private TMP_Text _supportActionCostText;
    [SerializeField] private TMP_Text _supportActionAmountText;
    [SerializeField] private Button _supportActionButton;

    [Header("Special Attack")]
    [SerializeField] private TMP_Text _specialAttackDmgText;
    [SerializeField] private Button _specialAttackButton;


    [Header("Base Stats")]
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private TMP_Text _shieldText;

    [Header("Icons")]
    [SerializeField] private Image _lightAttackIcon;
    [SerializeField] private Image _heavyAttackIcon;
    [SerializeField] private Image _shieldIcon;
    [SerializeField] private Image _healthIcon;


    void Start()
    {
        _nameText.text = _card.characterName;

        _artworkImage.sprite = _card.artwork;

        ////Buttons
        //atk1Button.interactable = false;
        //atk2Button.interactable = false;
        //atk3Button.interactable = false;

        // Light Attack
        _lightAttackCostText.text = _card.lightAttackCost.ToString();
        _lightAttackDmgText.text = _card.lightAttackDmg.ToString();

        // Heavy Atttack
        _heavyAttackCostText.text = _card.heavyAttackCost.ToString();
        _heavyAttackDmgText.text = _card.heavyAttackDmg.ToString();

        // Support Action
        _supportActionCostText.text = _card.supportActionCost.ToString();
        _supportActionAmountText.text = _card.supportActionAmount.ToString();

        // Special Attack
        _specialAttackDmgText.text = _card.specialAttackDmg.ToString();

        //Base stats
        _healthText.text = _card.health.ToString();
        _shieldText.text = _card.shield.ToString(); 
    }

}
