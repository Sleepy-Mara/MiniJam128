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
    public string effectDesc;
    public Sprite effectSprite;
    public List<string> keywords = new List<string>()
    {
        "At the start of the turn",
        "When it's played",
        "At the end of the turn",
        "When it dies",
        "When it attacks",
        "draw",
        "deal damage",
        "/",
        "add",
        "deck",
        "hand",
        "Colonel toad",
        "Forg2",
        "Roger",
        "Rat"
    };

}
