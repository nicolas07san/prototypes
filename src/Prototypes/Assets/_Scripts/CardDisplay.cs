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
        _nameText.text = _card.CharacterName;

        _artworkImage.sprite = _card.Artwork;

        // Light Attack
        _lightAttackCostText.text = _card.LightAttackCost.ToString();
        _lightAttackDmgText.text = _card.LightAttackDmg.ToString();

        // Heavy Atttack
        _heavyAttackCostText.text = _card.HeavyAttackCost.ToString();
        _heavyAttackDmgText.text = _card.HeavyAttackDmg.ToString();

        // Support Action
        _supportActionCostText.text = _card.SupportActionCost.ToString();
        _supportActionAmountText.text = _card.SupportActionAmount.ToString();
        if (_card.IsShield)
            _supportActionShieldIcon.SetActive(true);
        else
            _supportActionHealthIcon.SetActive(true);

        // Special Attack
        _specialAttackDmgText.text = _card.SpecialAttackDmg.ToString();
        for(int i = 0; i < _card.Combo.Length; i++)
        {
            switch (_card.Combo[i])
            {
                case (Card.Action.LightAttack):
                    Instantiate(_lightAttackIcon, _specialAttackCombo.transform);
                    break;
                case (Card.Action.HeavyAttack):
                    Instantiate(_heavyAttackIcon, _specialAttackCombo.transform);
                    break;
                case (Card.Action.SupportAction):
                    if(_card.IsShield)
                        Instantiate(_shieldIcon, _specialAttackCombo.transform);
                    else
                        Instantiate(_healthIcon, _specialAttackCombo.transform);
                    break;
            }
        }

        _specialAttackButton.interactable = false;

        // Base stats
        _healthText.text = _card.Health.ToString();
        _shieldText.text = _card.Shield.ToString(); 
    }

}
