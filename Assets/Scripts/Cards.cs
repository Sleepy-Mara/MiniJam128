using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

[CreateAssetMenu(menuName = "Objects/Card")]
public class Cards : ScriptableObject
{
    public string cardName;
    public int life;
    public int attack;
    public int manaCost;
    public int healthCost;
    public CardEffect[] effects;
    public Sprite sprite;
}
