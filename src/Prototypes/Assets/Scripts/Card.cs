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
    public int attack1Cost;
    public int attack1Dmg;

    [Header("Attack 2")]
    public string attack2Name;
    public int attack2Cost;
    public int attack2Dmg;

    [Header("Attack 3")]
    public string attack3Name;
    public int attack3Cost;
    public int attack3Dmg;

    [Header("Base Stats")]
    public int shield;
    public int health; 
}
