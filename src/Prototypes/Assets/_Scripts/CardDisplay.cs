using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    [field:SerializeField]public Card Card{private set; get;}

    [SerializeField]private TMP_Text _nameText;

    [SerializeField]private Image _artworkImage;

    [Header("Light Attack")]
    [SerializeField] private TMP_Text _lightAttackCostText;
    [SerializeField] private TMP_Text _lightAttackDmgText;
    [field:SerializeField] public Button LightAttackButton{private set; get;}

    [Header("Heavy Attack")]
    [SerializeField] private TMP_Text _heavyAttackCostText;
    [SerializeField] private TMP_Text _heavyAttackDmgText;
    [field:SerializeField] public Button HeavyAttackButton{private set; get;}

    [Header("Support Action")]
    [SerializeField] private TMP_Text _supportActionCostText;
    [SerializeField] private TMP_Text _supportActionAmountText;
    [SerializeField] private GameObject _supportActionShieldIcon;
    [SerializeField] private GameObject _supportActionHealthIcon;
    [field:SerializeField] public Button SupportActionButton {private set; get;}

    [Header("Special Attack")]
    [SerializeField] private TMP_Text _specialAttackDmgText;
    [field:SerializeField] public GameObject SpecialAttackCombo {private set; get;}
    [field:SerializeField] public Button SpecialAttackButton {private set; get;}

    // [Header("Base Stats")]
    // [SerializeField] private TMP_Text _healthText;
    // [SerializeField] private TMP_Text _shieldText;

    [field:Header("Particles VFX")]
    [field:SerializeField] public ParticleSystem LightAttackVFX {private set; get;}
    [field:SerializeField] public ParticleSystem HeavyAttackVFX {private set; get;}
    [field:SerializeField] public ParticleSystem ShieldVFX {private set; get;}
    [field:SerializeField] public ParticleSystem HealVFX {private set; get;}

    [Header("FX")]
    [SerializeField] private ShakeableTransform _shakeableTransform;

    [Header("Icons")]
    [SerializeField] private Image _lightAttackIcon;
    [SerializeField] private Image _heavyAttackIcon;
    [SerializeField] private Image _shieldIcon;
    [SerializeField] private Image _healthIcon;


    void Start()
    {
        _nameText.text = Card.CharacterName;

        _artworkImage.sprite = Card.Artwork;

        // Light Attack
        _lightAttackCostText.text = Card.LightAttackCost.ToString();
        _lightAttackDmgText.text = Card.LightAttackDmg.ToString();

        // Heavy Atttack
        _heavyAttackCostText.text = Card.HeavyAttackCost.ToString();
        _heavyAttackDmgText.text = Card.HeavyAttackDmg.ToString();

        // Support Action
        _supportActionCostText.text = Card.SupportActionCost.ToString();
        _supportActionAmountText.text = Card.SupportActionValue.ToString();
        if (Card.IsShield)
            _supportActionShieldIcon.SetActive(true);
        else
            _supportActionHealthIcon.SetActive(true);

        // Special Attack
        _specialAttackDmgText.text = Card.SpecialAttackDmg.ToString();
        if(SpecialAttackCombo.transform.childCount < Card.Combo.Length)
        {
            for(int i = 0; i < Card.Combo.Length; i++)
            {
                switch (Card.Combo[i])
                {
                    case (Card.Action.LightAttack):
                        Instantiate(_lightAttackIcon, SpecialAttackCombo.transform);
                        break;
                    case (Card.Action.HeavyAttack):
                        Instantiate(_heavyAttackIcon, SpecialAttackCombo.transform);
                        break;
                    case (Card.Action.SupportAction):
                        if(Card.IsShield)
                            Instantiate(_shieldIcon, SpecialAttackCombo.transform);
                        else
                            Instantiate(_healthIcon, SpecialAttackCombo.transform);
                        break;
                }
            }
        }
        

        LightAttackButton.interactable = false;
        HeavyAttackButton.interactable = false;
        SupportActionButton.interactable = false;
        SpecialAttackButton.interactable = false;

        // Base stats
        // _healthText.text = Card.Health.ToString();
        // _shieldText.text = Card.Shield.ToString(); 
    }

    public void Shake(float intensity){
        _shakeableTransform.InduceStress(intensity);
    }

}
