using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField]
    private List<string> conditions = new List<string>()
    {
        "At the start of the turn",
        "When it's played",
        "When it attacks",
        "When it get damaged",
        "When it's defeated",
        "At the end of the turn",
        "For this turn"
    };
    [SerializeField]
    private List<string> effects = new List<string>()
    {
        "draw",
        "deal damage",
        "heal",
        "add",
        "immune",
        "summon"
    };
    [SerializeField]
    private List<string> targets = new List<string>()
    {
        "enemy creature",
        "ally creature",
        "random enemy",
        "random ally",
        "random enemy creature",
        "random ally creature",
        "random enemy creatures",
        "random ally creatures",
        "all enemy creatures",
        "all ally creatures",
        "itself"
    };
    [SerializeField]
    private List<Cards> cards;
    public void CheckConditionStartOfTurn(Cards card)
    {
        if (card.effectDesc.Contains(conditions[0]))
            CheckEffect(card);
    }
    public void CheckConditionIsPlayed(Cards card)
    {
        if (card.effectDesc.Contains(conditions[1]))
            CheckEffect(card);
    }
    public void CheckConditionAttack(Cards card)
    {
        if (card.effectDesc.Contains(conditions[2]))
            CheckEffect(card);
    }
    public void CheckConditionGetDamaged(Cards card)
    {
        if (card.effectDesc.Contains(conditions[3]))
            CheckEffect(card);
    }
    public void CheckConditionDefeated(Cards card)
    {
        if (card.effectDesc.Contains(conditions[4]))
            CheckEffect(card);
    }
    public void CheckConditionEndOfTurn(Cards card)
    {
        if (card.effectDesc.Contains(conditions[5]))
            CheckEffect(card);
    }
    public void CheckConditionForThisTurn(Cards card)
    {
        if (card.effectDesc.Contains(conditions[6]))
            CheckEffect(card);
    }
    private void CheckEffect(Cards card)
    {
        foreach(string effect in effects)
            if (card.effectDesc.Contains(effect))
            {
                if (effect == effects[0])
                    DrawEffect(card);
                if (effect == effects[1])
                    DealDamageEffect(card);
                if (effect == effects[2])
                    HealEffect(card);
                if (effect == effects[3])
                    AddEffect(card);
                if (effect == effects[4])
                    InmuneEffect(card);
                if (effect == effects[5])
                    SumonEffect(card);
            }
    }
    private void DrawEffect(Cards card)
    {
        for (int i = 0; i < 50; i++)
            if (card.effectDesc.Contains(effects[i]))
                for (int j = 0; j < i; j++)
                    FindObjectOfType<Draw>().DrawACard();
    }
    private void DealDamageEffect(Cards card)
    {

    }
    private void HealEffect(Cards card)
    {

    }
    private void AddEffect(Cards card)
    {

    }
    private void InmuneEffect(Cards card)
    {

    }
    private void SumonEffect(Cards card)
    {

    }
}
