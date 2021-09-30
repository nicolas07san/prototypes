using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string characterName;

    public Sprite artwork;

    public string attack1Name;
    public int attack1Dmg;
    public int attack1Cost;

    public int health;
    public int shield;
}
