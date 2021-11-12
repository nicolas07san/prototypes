using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string characterName;

    public Sprite artwork;

    [Header("Attack 1")]
    public string attack1Name;
    public int attack1Dmg;
    public int attack1Cost;

    [Header("Attack 2")]
    public string attack2Name;
    public int attack2Dmg;
    public int attack2Cost;

    [Header("Attack 3")]
    public string attack3Name;
    public int attack3Dmg;
    public int attack3Cost;

    [Header("Base Stats")]
    public int health;
    public int shield;
}
