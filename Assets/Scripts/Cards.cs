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
    public virtual void PlayEffect()
    {
        Debug.Log("la carta " + cardName + " no tiene efecto");
    }
    public virtual void TurnStart()
    {

    }
    public virtual void TurnEnd()
    {

    }

}
