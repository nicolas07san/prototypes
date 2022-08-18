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


    [Header("Base Stats")]
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private TMP_Text _shieldText;


    void Start()
    {
        _nameText.text = _card.characterName;

        _artworkImage.sprite = _card.artwork;

        ////Buttons
        //atk1Button.interactable = false;
        //atk2Button.interactable = false;
        //atk3Button.interactable = false;

        //// Attack 1
        //atk1NameText.text = card.attack1Name;
        //atk1CostText.text = card.attack1Cost.ToString();
        //atk1DmgText.text = card.attack1Dmg.ToString();
        
        //// Attack 2
        //atk2NameText.text = card.attack2Name;
        //atk2CostText.text = card.attack2Cost.ToString();
        //atk2DmgText.text = card.attack2Dmg.ToString();

        //// Attack 3
        //atk3NameText.text = card.attack3Name;
        //atk3CostText.text = card.attack3Cost.ToString();
        //atk3DmgText.text = card.attack3Dmg.ToString();

        ////Base stats
        //healthText.text = card.health.ToString();
        //shieldText.text = card.shield.ToString(); 
    }

}
