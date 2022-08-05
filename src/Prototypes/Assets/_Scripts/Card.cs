using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string characterName;

    public Sprite artwork;

    [Header("Light Attack")]
    public int lightAttackCost;
    public int lightAttackDmg;

    [Header("Heavy Attack")]
    public int heavyAttackCost;
    public int heavyAttackDmg;

    [Header("Support Action")]
    public bool isShield;
    public int supportActionCost;   
    public int supportActionAmount;

    [Header("Special Attack")]
    public int specialAttackDmg;
    public Action[] combo;

    [Header("Base Stats")]
    public int health;
    public int shield;

    public enum Action
    {
        LightAttack,
        HeavyAttack,
        SupportAction
    }
}
