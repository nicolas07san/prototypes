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
    [SerializeField] private GameObject _supportActionShieldIcon;
    [SerializeField] private GameObject _supportActionHealthIcon;

    [Header("Special Attack")]
    [SerializeField] private TMP_Text _specialAttackDmgText;
    [SerializeField] private Button _specialAttackButton;
    [SerializeField] private GameObject _specialAttackCombo;

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
        if (_card.isShield)
            _supportActionShieldIcon.SetActive(true);
        else
            _supportActionHealthIcon.SetActive(true);

        // Special Attack
        _specialAttackDmgText.text = _card.specialAttackDmg.ToString();
        for(int i = 0; i < _card.combo.Length; i++)
        {
            switch (_card.combo[i])
            {
                case (Card.Action.LightAttack):
                    Instantiate(_lightAttackIcon, _specialAttackCombo.transform);
                    break;
                case (Card.Action.HeavyAttack):
                    Instantiate(_heavyAttackIcon, _specialAttackCombo.transform);
                    break;
                case (Card.Action.SupportAction):
                    if(_card.isShield)
                        Instantiate(_shieldIcon, _specialAttackCombo.transform);
                    else
                        Instantiate(_healthIcon, _specialAttackCombo.transform);
                    break;
            }
        }

        //Base stats
        _healthText.text = _card.health.ToString();
        _shieldText.text = _card.shield.ToString(); 
    }

}
