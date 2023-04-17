using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Normal")]
public class Cards : ScriptableObject
{
    public string cardName;
    public int life;
    public int attack;
    public int attackToPlayer;
    public int manaCost;
    public int healthCost;
    public Sprite sprite;
    public bool hasEffect;
    [TextArea(1, 4)]
    public string effect;
    public Sprite effectSprite;
    [TextArea(1, 4)]
    public List<string> effectDesc;
    public bool spell;
}
