using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "ScriptableObjects/Card")]
public class Card : ScriptableObject
{
    [field:SerializeField]public string CharacterName {get; private set;}
    [field:SerializeField]public Sprite Artwork{get; private set;}

    [field:Header("Light Attack")]
    [field:SerializeField]public int LightAttackCost {get; private set;}
    [field:SerializeField]public int LightAttackDmg {get; private set;}

    [field:Header("Heavy Attack")]
    [field:SerializeField]public int HeavyAttackCost {get; private set;}
    [field:SerializeField]public int HeavyAttackDmg {get; private set;}

    [field:Header("Support Action")]
    [field:SerializeField]public bool IsShield {get; private set;}
    [field:SerializeField]public int SupportActionCost {get; private set;}   
    [field:SerializeField]public int SupportActionValue {get; private set;}

    [field:Header("Special Attack")]
    [field:SerializeField]public int SpecialAttackDmg {get; private set;}
    [field:SerializeField]public Action[] Combo {get; private set;}

    [field:Header("Base Stats")]
    [field:SerializeField]public int Health {get; private set;}
    [field:SerializeField]public int Shield {get; private set;}

    public enum Action
    {
        LightAttack,
        HeavyAttack,
        SupportAction,
        SpecialAttack
    }
}
