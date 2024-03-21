using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card")]
public class Cards : ScriptableObject
{
    public string scriptableName;
    public string id;
    public List<string> cardName;
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
    public Effects effects;
}
