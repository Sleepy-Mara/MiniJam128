using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EffectManager : MonoBehaviour
{
    private Effects effect = new Effects();
    [SerializeField]
    private List<Cards> cards;
    [SerializeField]
    private Card newCard;
    private Table _table;
    private Draw _draw;
    [HideInInspector]
    public bool waitForSelect;
    [HideInInspector]
    public bool checkingEffect;
    [HideInInspector]
    public Card selectedCard;
    private void Awake()
    {
        _table = FindObjectOfType<Table>();
        _draw = FindObjectOfType<Draw>();
    }
    #region CheckingsConditions
    public void CheckConditionStartOfTurn(MonoBehaviour caller)
    {
        Effects effects = new Effects();
        if (caller.GetComponent<CardCore>())
            effects = caller.GetComponent<CardCore>().card.effects;
        bool checkEffect = false;
        for (int i = 0; i < effects.conditions.Count; i++)
            if (effects.conditions[i] == effect.conditions[1])
            {
                if (!CheckExtraConditions(caller, effects, i))
                    continue;
                CheckEffect(caller, effects, i);
                checkEffect = true;
            }
        if (!checkEffect)
            CheckingEffect(caller);
    }
    public void CheckConditionIsPlayed(MonoBehaviour caller)
    {
        Effects effects = new Effects();
        if (caller.GetComponent<CardCore>())
            effects = caller.GetComponent<CardCore>().card.effects;
        bool checkEffect = false;
        for (int i = 0; i < effects.conditions.Count; i++)
            if (effects.conditions[i] == effect.conditions[2])
            {
                if (!CheckExtraConditions(caller, effects, i))
                    continue;
                CheckEffect(caller, effects, i);
                checkEffect = true;
            }
        if (!checkEffect)
            CheckingEffect(caller);
    }
    public void CheckConditionAttack(MonoBehaviour caller)
    {
        Effects effects = new Effects();
        if (caller.GetComponent<CardCore>())
            effects = caller.GetComponent<CardCore>().card.effects;
        bool checkEffect = false;
        for (int i = 0; i < effects.conditions.Count; i++)
            if (effects.conditions[i] == effect.conditions[3])
            {
                if (!CheckExtraConditions(caller, effects, i))
                    continue;
                CheckEffect(caller, effects, i);
                checkEffect = true;
            }
        if (!checkEffect)
            CheckingEffect(caller);
    }
    public void CheckConditionGetDamaged(MonoBehaviour caller)
    {
        Effects effects = new Effects();
        if (caller.GetComponent<CardCore>())
            effects = caller.GetComponent<CardCore>().card.effects;
        bool checkEffect = false;
        for (int i = 0; i < effects.conditions.Count; i++)
            if (effects.conditions[i] == effect.conditions[4])
            {
                if (!CheckExtraConditions(caller, effects, i))
                    continue;
                CheckEffect(caller, effects, i);
                checkEffect = true;
            }
        if (!checkEffect)
            CheckingEffect(caller);
    }
    public void CheckConditionDefeated(MonoBehaviour caller)
    {
        Effects effects = new Effects();
        if (caller.GetComponent<CardCore>())
            effects = caller.GetComponent<CardCore>().card.effects;
        bool checkEffect = false;
        for (int i = 0; i < effects.conditions.Count; i++)
            if (effects.conditions[i] == effect.conditions[5])
            {
                if (!CheckExtraConditions(caller, effects, i))
                    continue;
                CheckEffect(caller, effects, i);
                checkEffect = true;
            }
        if (!checkEffect)
            CheckingEffect(caller);
    }
    public void CheckConditionDefeatsAnEnemy(MonoBehaviour caller)
    {
        Effects effects = new Effects();
        if (caller.GetComponent<CardCore>())
            effects = caller.GetComponent<CardCore>().card.effects;
        bool checkEffect = false;
        for (int i = 0; i < effects.conditions.Count; i++)
            if (effects.conditions[i] == effect.conditions[6])
            {
                if (!CheckExtraConditions(caller, effects, i))
                    continue;
                CheckEffect(caller, effects, i);
                checkEffect = true;
            }
        if (!checkEffect)
            CheckingEffect(caller);
    }
    public void CheckConditionAllyIsDefeated(MonoBehaviour caller)
    {
        Effects effects = new Effects();
        if (caller.GetComponent<CardCore>())
            effects = caller.GetComponent<CardCore>().card.effects;
        bool checkEffect = false;
        for (int i = 0; i < effects.conditions.Count; i++)
            if (effects.conditions[i] == effect.conditions[7])
            {
                if (!CheckExtraConditions(caller, effects, i))
                    continue;
                CheckEffect(caller, effects, i);
                checkEffect = true;
            }
        if (!checkEffect)
            CheckingEffect(caller);
    }
    public void CheckConditionEndOfTurn(MonoBehaviour caller)
    {
        Effects effects = new Effects();
        if (caller.GetComponent<CardCore>())
            effects = caller.GetComponent<CardCore>().card.effects;
        bool checkEffect = false;
        for (int i = 0; i < effects.conditions.Count; i++)
            if (effects.conditions[i] == effect.conditions[8])
            {
                if (!CheckExtraConditions(caller, effects, i))
                    continue;
                CheckEffect(caller, effects, i);
                checkEffect = true;
            }
        if (!checkEffect)
            CheckingEffect(caller);
    }
    public void CheckConditionGetBuffed(MonoBehaviour caller)
    {
        Effects effects = new Effects();
        if (caller.GetComponent<CardCore>())
            effects = caller.GetComponent<CardCore>().card.effects;
        bool checkEffect = false;
        for (int i = 0; i < effects.conditions.Count; i++)
            if (effects.conditions[i] == effect.conditions[9])
            {
                if (!CheckExtraConditions(caller, effects, i))
                    continue;
                CheckEffect(caller, effects, i);
                checkEffect = true;
            }
        if (!checkEffect)
            CheckingEffect(caller);
    }
    public void CheckConditionSpellPlayed(MonoBehaviour caller)
    {
        Effects effects = new Effects();
        if (caller.GetComponent<CardCore>())
            effects = caller.GetComponent<CardCore>().card.effects;
        bool checkEffect = false;
        for (int i = 0; i < effects.conditions.Count; i++)
            if (effects.conditions[i] == effect.conditions[10])
            {
                if (!CheckExtraConditions(caller, effects, i))
                    continue;
                CheckEffect(caller, effects, i);
                checkEffect = true;
            }
        if (!checkEffect)
            CheckingEffect(caller);
    }
    private bool CheckExtraConditions(MonoBehaviour caller, Effects newEffect, int numberEffect)
    {
        MapPosition[] enemyPositions;
        MapPosition[] allyPositions;
        Health enemyHealth;
        Health allyHealth;
        if (caller.GetComponent<CardCore>()?.currentPosition.oponent == FindObjectOfType<Player>() || caller.GetComponent<Enemy>())
        {
            enemyPositions = _table.playerPositions;
            allyPositions = _table.enemyFront;
            enemyHealth = FindObjectOfType<Player>();
            allyHealth = FindObjectOfType<Enemy>();
        }
        else
        {
            enemyPositions = _table.enemyFront;
            allyPositions = _table.playerPositions;
            enemyHealth = FindObjectOfType<Enemy>();
            allyHealth = FindObjectOfType<Player>();
        }
        bool fulfillsExtraCondition = true;
        foreach (string extraCondition in newEffect.extraConditions[numberEffect].extraConditions)
            for (int i = 0; i < effect.extraConditions[0].extraConditions.Count; i++)
            {
                if (extraCondition == effect.extraConditions[0].extraConditions[i])
                {
                    switch (i)
                    {
                        case 1:
                            if (allyHealth.GetComponent<Player>())
                            {
                                int j = 0;
                                List<string> ignore = new List<string>();
                                foreach (GameObject card in FindObjectOfType<Draw>()._cardsInHand)
                                    if (newEffect.extraConditions[numberEffect].cards[i].Contains(card.GetComponent<CardCore>().card.scriptableName) &&
                                        !ignore.Contains(card.GetComponent<CardCore>().card.scriptableName))
                                    {
                                        ignore.Add(card.GetComponent<CardCore>().card.scriptableName);
                                        j++;
                                        if (j > newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                            break;
                                    }
                                if (j < newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                    fulfillsExtraCondition = false;
                            }
                            if (allyHealth.GetComponent<EnemyAI>())
                            {
                                int j = 0;
                                List<string> ignore = new List<string>();
                                foreach (Cards card in allyHealth.GetComponent<EnemyAI>().hand)
                                    if (newEffect.extraConditions[numberEffect].cards[i].Contains(card.scriptableName) &&
                                        !ignore.Contains(card.scriptableName))
                                    {
                                        ignore.Add(card.scriptableName);
                                        j++;
                                        if (j > newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                            break;
                                    }
                                if (j < newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                    fulfillsExtraCondition = false;
                            }
                            break;
                        case 2:
                            if (enemyHealth.GetComponent<Player>())
                            {
                                int j = 0;
                                List<string> ignore = new List<string>();
                                foreach (GameObject card in FindObjectOfType<Draw>()._cardsInHand)
                                    if (newEffect.extraConditions[numberEffect].cards[i].Contains(card.GetComponent<CardCore>().card.scriptableName) &&
                                        !ignore.Contains(card.GetComponent<CardCore>().card.scriptableName))
                                    {
                                        ignore.Add(card.GetComponent<CardCore>().card.scriptableName);
                                        j++;
                                        if (j > newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                            break;
                                    }
                                if (j < newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                    fulfillsExtraCondition = false;
                            }
                            if (enemyHealth.GetComponent<EnemyAI>())
                            {
                                int j = 0;
                                List<string> ignore = new List<string>();
                                foreach (Cards card in enemyHealth.GetComponent<EnemyAI>().hand)
                                    if (newEffect.extraConditions[numberEffect].cards[i].Contains(card.scriptableName) &&
                                        !ignore.Contains(card.scriptableName))
                                    {
                                        ignore.Add(card.scriptableName);
                                        j++;
                                        if (j > newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                            break;
                                    }
                                if (j < newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                    fulfillsExtraCondition = false;
                            }
                            break;
                        case 3:
                            if (allyHealth.GetComponent<Player>())
                            {
                                if (FindObjectOfType<Draw>()._cardsInHand.Count != newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                    fulfillsExtraCondition = false;
                            }
                            if (allyHealth.GetComponent<EnemyAI>())
                            {
                                if (allyHealth.GetComponent<EnemyAI>().hand.Count != newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                    fulfillsExtraCondition = false;
                            }
                            break;
                        case 4:
                            if (enemyHealth.GetComponent<Player>())
                            {
                                if (FindObjectOfType<Draw>()._cardsInHand.Count != newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                    fulfillsExtraCondition = false;
                            }
                            if (enemyHealth.GetComponent<EnemyAI>())
                            {
                                if (enemyHealth.GetComponent<EnemyAI>().hand.Count != newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                    fulfillsExtraCondition = false;
                            }
                            break;
                        case 5:
                            if (allyHealth.GetComponent<Player>())
                            {
                                if (newEffect.extraConditions[numberEffect].extraConditionsInt[i] < 0)
                                    if (FindObjectOfType<Draw>()._cardsInHand.Count > newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                        fulfillsExtraCondition = false;
                                    else if (FindObjectOfType<Draw>()._cardsInHand.Count < newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                        fulfillsExtraCondition = false;
                            }
                            if (allyHealth.GetComponent<EnemyAI>())
                            {
                                if (newEffect.extraConditions[numberEffect].extraConditionsInt[i] < 0)
                                    if (allyHealth.GetComponent<EnemyAI>().hand.Count > newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                        fulfillsExtraCondition = false;
                                    else if (allyHealth.GetComponent<EnemyAI>().hand.Count < newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                        fulfillsExtraCondition = false;
                            }
                            break;
                        case 6:
                            if (enemyHealth.GetComponent<Player>())
                            {
                                if (newEffect.extraConditions[numberEffect].extraConditionsInt[i] < 0)
                                    if (FindObjectOfType<Draw>()._cardsInHand.Count > newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                        fulfillsExtraCondition = false;
                                    else if (FindObjectOfType<Draw>()._cardsInHand.Count < newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                        fulfillsExtraCondition = false;
                            }
                            if (enemyHealth.GetComponent<EnemyAI>())
                            {
                                if (newEffect.extraConditions[numberEffect].extraConditionsInt[i] < 0)
                                    if (enemyHealth.GetComponent<EnemyAI>().hand.Count > newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                        fulfillsExtraCondition = false;
                                    else if (enemyHealth.GetComponent<EnemyAI>().hand.Count < newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                        fulfillsExtraCondition = false;
                            }
                            break;
                        case 7:
                            if (allyHealth.GetComponent<Player>())
                            {
                                int j = 0;
                                List<string> ignore = new List<string>();
                                foreach (Cards card in FindObjectOfType<Draw>().CurrentDeck)
                                    if (newEffect.extraConditions[numberEffect].cards[i].Contains(card.scriptableName) &&
                                        !ignore.Contains(card.scriptableName))
                                    {
                                        ignore.Add(card.scriptableName);
                                        j++;
                                        if (j > newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                            break;
                                    }
                                if (j < newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                    fulfillsExtraCondition = false;
                            }
                            if (allyHealth.GetComponent<EnemyAI>())
                            {
                                int j = 0;
                                List<string> ignore = new List<string>();
                                foreach (Cards card in allyHealth.GetComponent<EnemyAI>().CurrentDeck)
                                    if (newEffect.extraConditions[numberEffect].cards[i].Contains(card.scriptableName) &&
                                        !ignore.Contains(card.scriptableName))
                                    {
                                        ignore.Add(card.scriptableName);
                                        j++;
                                        if (j > newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                            break;
                                    }
                                if (j < newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                    fulfillsExtraCondition = false;
                            }
                            break;
                        case 8:
                            if (enemyHealth.GetComponent<Player>())
                            {
                                int j = 0;
                                List<string> ignore = new List<string>();
                                foreach (Cards card in FindObjectOfType<Draw>().CurrentDeck)
                                    if (newEffect.extraConditions[numberEffect].cards[i].Contains(card.scriptableName) &&
                                        !ignore.Contains(card.scriptableName))
                                    {
                                        ignore.Add(card.scriptableName);
                                        j++;
                                        if (j > newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                            break;
                                    }
                                if (j < newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                    fulfillsExtraCondition = false;
                            }
                            if (enemyHealth.GetComponent<EnemyAI>())
                            {
                                int j = 0;
                                List<string> ignore = new List<string>();
                                foreach (Cards card in enemyHealth.GetComponent<EnemyAI>().CurrentDeck)
                                    if (newEffect.extraConditions[numberEffect].cards[i].Contains(card.scriptableName) &&
                                        !ignore.Contains(card.scriptableName))
                                    {
                                        ignore.Add(card.scriptableName);
                                        j++;
                                        if (j > newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                            break;
                                    }
                                if (j < newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                    fulfillsExtraCondition = false;
                            }
                            break;
                        case 9:
                            if (allyHealth.GetComponent<Player>())
                            {
                                if (FindObjectOfType<Draw>().CurrentDeck.Count != newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                    fulfillsExtraCondition = false;
                            }
                            if (allyHealth.GetComponent<EnemyAI>())
                            {
                                if (allyHealth.GetComponent<EnemyAI>().CurrentDeck.Count != newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                    fulfillsExtraCondition = false;
                            }
                            break;
                        case 10:
                            if (enemyHealth.GetComponent<Player>())
                            {
                                if (FindObjectOfType<Draw>().CurrentDeck.Count != newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                    fulfillsExtraCondition = false;
                            }
                            if (enemyHealth.GetComponent<EnemyAI>())
                            {
                                if (enemyHealth.GetComponent<EnemyAI>().CurrentDeck.Count != newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                    fulfillsExtraCondition = false;
                            }
                            break;
                        case 11:
                            if (allyHealth.GetComponent<Player>())
                            {
                                if (newEffect.extraConditions[numberEffect].extraConditionsInt[i] < 0)
                                    if (FindObjectOfType<Draw>().CurrentDeck.Count > newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                        fulfillsExtraCondition = false;
                                    else if (FindObjectOfType<Draw>().CurrentDeck.Count < newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                        fulfillsExtraCondition = false;
                            }
                            if (allyHealth.GetComponent<EnemyAI>())
                            {
                                if (newEffect.extraConditions[numberEffect].extraConditionsInt[i] < 0)
                                    if (allyHealth.GetComponent<EnemyAI>().CurrentDeck.Count > newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                        fulfillsExtraCondition = false;
                                    else if (allyHealth.GetComponent<EnemyAI>().CurrentDeck.Count < newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                        fulfillsExtraCondition = false;
                            }
                            break;
                        case 12:
                            if (enemyHealth.GetComponent<Player>())
                            {
                                if (newEffect.extraConditions[numberEffect].extraConditionsInt[i] < 0)
                                    if (FindObjectOfType<Draw>().CurrentDeck.Count > newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                        fulfillsExtraCondition = false;
                                    else if (FindObjectOfType<Draw>().CurrentDeck.Count < newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                        fulfillsExtraCondition = false;
                            }
                            if (enemyHealth.GetComponent<EnemyAI>())
                            {
                                if (newEffect.extraConditions[numberEffect].extraConditionsInt[i] < 0)
                                    if (enemyHealth.GetComponent<EnemyAI>().CurrentDeck.Count > newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                        fulfillsExtraCondition = false;
                                    else if (enemyHealth.GetComponent<EnemyAI>().CurrentDeck.Count < newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                        fulfillsExtraCondition = false;
                            }
                            break;
                        case 13:
                            if (allyHealth.GetComponent<Player>())
                            {
                                int j = 0;
                                List<string> ignore = new List<string>();
                                foreach (MapPosition card in _table.playerPositions)
                                    if (newEffect.extraConditions[numberEffect].cards[i].Contains(card.card.card.scriptableName) &&
                                        !ignore.Contains(card.card.card.scriptableName))
                                    {
                                        ignore.Add(card.card.card.scriptableName);
                                        j++;
                                        if (j > newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                            break;
                                    }
                                if (j < newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                    fulfillsExtraCondition = false;
                            }
                            if (allyHealth.GetComponent<EnemyAI>())
                            {
                                int j = 0;
                                List<string> ignore = new List<string>();
                                foreach (MapPosition card in _table.enemyFront)
                                    if (newEffect.extraConditions[numberEffect].cards[i].Contains(card.card.card.scriptableName) &&
                                        !ignore.Contains(card.card.card.scriptableName))
                                    {
                                        ignore.Add(card.card.card.scriptableName);
                                        j++;
                                        if (j > newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                            break;
                                    }
                                if (j < newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                    fulfillsExtraCondition = false;
                            }
                            break;
                        case 14:
                            if (enemyHealth.GetComponent<Player>())
                            {
                                int j = 0;
                                List<string> ignore = new List<string>();
                                foreach (MapPosition card in _table.playerPositions)
                                    if (newEffect.extraConditions[numberEffect].cards[i].Contains(card.card.card.scriptableName) &&
                                        !ignore.Contains(card.card.card.scriptableName))
                                    {
                                        ignore.Add(card.card.card.scriptableName);
                                        j++;
                                        if (j > newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                            break;
                                    }
                                if (j < newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                    fulfillsExtraCondition = false;
                            }
                            if (enemyHealth.GetComponent<EnemyAI>())
                            {
                                int j = 0;
                                List<string> ignore = new List<string>();
                                foreach (MapPosition card in _table.enemyFront)
                                    if (newEffect.extraConditions[numberEffect].cards[i].Contains(card.card.card.scriptableName) &&
                                        !ignore.Contains(card.card.card.scriptableName))
                                    {
                                        ignore.Add(card.card.card.scriptableName);
                                        j++;
                                        if (j > newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                            break;
                                    }
                                if (j < newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                    fulfillsExtraCondition = false;
                            }
                            break;
                        case 15:
                            if (allyHealth.GetComponent<Player>())
                            {
                                int j = 0;
                                foreach (MapPosition card in _table.playerPositions)
                                    if (card.card != null)
                                        j++;
                                if (j != newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                    fulfillsExtraCondition = false;
                            }
                            if (allyHealth.GetComponent<EnemyAI>())
                            {
                                int j = 0;
                                foreach (MapPosition card in _table.enemyFront)
                                    if (card.card != null)
                                        j++;
                                if (j != newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                    fulfillsExtraCondition = false;
                            }
                            break;
                        case 16:
                            if (enemyHealth.GetComponent<Player>())
                            {
                                int j = 0;
                                foreach (MapPosition card in _table.playerPositions)
                                    if (card.card != null)
                                        j++;
                                if (j != newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                    fulfillsExtraCondition = false;
                            }
                            if (enemyHealth.GetComponent<EnemyAI>())
                            {
                                int j = 0;
                                foreach (MapPosition card in _table.enemyFront)
                                    if (card.card != null)
                                        j++;
                                if (j != newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                    fulfillsExtraCondition = false;
                            }
                            break;
                        case 17:
                            if (allyHealth.GetComponent<Player>())
                            {
                                int j = 0;
                                foreach (MapPosition card in _table.playerPositions)
                                    if (card.card != null)
                                        j++;
                                if (newEffect.extraConditions[numberEffect].extraConditionsInt[i] < 0)
                                    if (j > newEffect.extraConditions[numberEffect].extraConditionsInt[i] * -1)
                                        fulfillsExtraCondition = false;
                                else if (j < newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                        fulfillsExtraCondition = false;
                            }
                            if (allyHealth.GetComponent<EnemyAI>())
                            {
                                int j = 0;
                                foreach (MapPosition card in _table.enemyFront)
                                    if (card.card != null)
                                        j++;
                                if (newEffect.extraConditions[numberEffect].extraConditionsInt[i] < 0)
                                    if (j > newEffect.extraConditions[numberEffect].extraConditionsInt[i] * -1)
                                        fulfillsExtraCondition = false;
                                    else if (j < newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                        fulfillsExtraCondition = false;
                            }
                            break;
                        case 18:
                            if (enemyHealth.GetComponent<Player>())
                            {
                                int j = 0;
                                foreach (MapPosition card in _table.playerPositions)
                                    if (card.card != null)
                                        j++;
                                if (newEffect.extraConditions[numberEffect].extraConditionsInt[i] < 0)
                                    if (j > newEffect.extraConditions[numberEffect].extraConditionsInt[i] * -1)
                                        fulfillsExtraCondition = false;
                                    else if (j < newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                        fulfillsExtraCondition = false;
                            }
                            if (enemyHealth.GetComponent<EnemyAI>())
                            {
                                int j = 0;
                                foreach (MapPosition card in _table.enemyFront)
                                    if (card.card != null)
                                        j++;
                                if (newEffect.extraConditions[numberEffect].extraConditionsInt[i] < 0)
                                    if (j > newEffect.extraConditions[numberEffect].extraConditionsInt[i] * -1)
                                        fulfillsExtraCondition = false;
                                    else if (j < newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                        fulfillsExtraCondition = false;
                            }
                            break;
                        case 19:
                            if (allyHealth.currentHealth != newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                fulfillsExtraCondition = false;
                            break;
                        case 20:
                            if (enemyHealth.currentHealth != newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                fulfillsExtraCondition = false;
                            break;
                        case 21:
                            if (newEffect.extraConditions[numberEffect].extraConditionsInt[i] < 0)
                                if (allyHealth.currentHealth > newEffect.extraConditions[numberEffect].extraConditionsInt[i] * -1)
                                    fulfillsExtraCondition = false;
                                else if (allyHealth.currentHealth < newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                    fulfillsExtraCondition = false;
                            break;
                        case 22:
                            if (newEffect.extraConditions[numberEffect].extraConditionsInt[i] < 0)
                                if (enemyHealth.currentHealth > newEffect.extraConditions[numberEffect].extraConditionsInt[i] * -1)
                                    fulfillsExtraCondition = false;
                                else if (enemyHealth.currentHealth < newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                    fulfillsExtraCondition = false;
                            break;
                        case 23:
                            if (allyHealth.currentMana != newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                fulfillsExtraCondition = false;
                            break;
                        case 24:
                            if (enemyHealth.currentMana != newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                fulfillsExtraCondition = false;
                            break;
                        case 25:
                            if (newEffect.extraConditions[numberEffect].extraConditionsInt[i] < 0)
                                if (allyHealth.currentMana > newEffect.extraConditions[numberEffect].extraConditionsInt[i] * -1)
                                    fulfillsExtraCondition = false;
                                else if (allyHealth.currentMana < newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                    fulfillsExtraCondition = false;
                            break;
                        case 26:
                            if (newEffect.extraConditions[numberEffect].extraConditionsInt[i] < 0)
                                if (enemyHealth.currentMana > newEffect.extraConditions[numberEffect].extraConditionsInt[i] * -1)
                                    fulfillsExtraCondition = false;
                                else if (enemyHealth.currentMana < newEffect.extraConditions[numberEffect].extraConditionsInt[i])
                                    fulfillsExtraCondition = false;
                            break;
                    }
                }
            }
        return fulfillsExtraCondition;
    }
    private void CheckEffect(MonoBehaviour caller, Effects newEffect, int numberEffect)
    {
        bool startTurn = false;
        bool endTurn = false;
        if (effect.tempEffect[1] == newEffect.tempEffect[numberEffect])
            startTurn = true;
        if (effect.tempEffect[2] == newEffect.tempEffect[numberEffect])
            endTurn = true;
        for (int i = 0; i < effect.effects.Count; i++)
            if (effect.effects[i] == newEffect.effects[numberEffect])
                switch (i)
                {
                    case 1:
                        DrawEffect(caller, newEffect, numberEffect, startTurn, endTurn);
                        break;
                    case 2:
                        DealDamageEffect(caller, newEffect, numberEffect, startTurn, endTurn);
                        break;
                    case 3:
                        HealEffect(caller, newEffect, numberEffect, startTurn, endTurn);
                        break;
                    case 4:
                        AddEffect(caller, newEffect, numberEffect, startTurn, endTurn);
                        break;
                    case 5:
                        GiveEffect(caller, newEffect, numberEffect, startTurn, endTurn);
                        break;
                    case 6:
                        ImmuneEffect(caller, newEffect, numberEffect, startTurn, endTurn);
                        break;
                    case 7:
                        SumonEffect(caller, newEffect, numberEffect, startTurn, endTurn);
                        break;
                    case 8:
                        ManaEffect(caller, newEffect, numberEffect, startTurn, endTurn);
                        break;
                }
    }
    #endregion
    private void DrawEffect(MonoBehaviour caller, Effects newEffect, int numberEffect, bool startTurn, bool endTurn)
    {
        #region Draw
        bool creature = true;
        bool spell = true;
        if (effect.targetsCards[1] == newEffect.targetsCards[numberEffect])
            creature = false;
        if (effect.targetsCards[2] == newEffect.targetsCards[numberEffect])
            spell = false;
        for (int j = 0;  j < newEffect.values[numberEffect][0]; j++)
            for (int i = 0; i < effect.targetsDecks.Count; i++)
                if (effect.targetsDecks[i] == newEffect.targetsDecks[numberEffect])
                    switch (i)
                    {
                        case 2:
                            if (caller.GetComponent<CardCore>()?.currentPosition.oponent == FindObjectOfType<Enemy>() || caller.GetComponent<Player>())
                                _draw.DrawACard(Draw.DeckType.Mana, creature, spell);
                            if ((caller.GetComponent<CardCore>()?.currentPosition.oponent == FindObjectOfType<Player>() || caller.GetComponent<Enemy>()) && FindObjectOfType<EnemyAI>().enabled)
                                FindObjectOfType<EnemyAI>().DrawACard(null, creature, spell, 0);
                            break;
                        case 3:
                            if (caller.GetComponent<CardCore>()?.currentPosition.oponent == FindObjectOfType<Enemy>() || caller.GetComponent<Player>())
                                _draw.DrawACard(Draw.DeckType.Blood, creature, spell);
                            if ((caller.GetComponent<CardCore>()?.currentPosition.oponent == FindObjectOfType<Player>() || caller.GetComponent<Enemy>()) && FindObjectOfType<EnemyAI>().enabled)
                                FindObjectOfType<EnemyAI>().DrawACard(null, creature, spell, 0);
                            break;
                        case 4:
                            if (caller.GetComponent<CardCore>()?.currentPosition.oponent == FindObjectOfType<Enemy>() || caller.GetComponent<Player>())
                            {
                                _draw.DrawACard(Draw.DeckType.Mana, creature, spell);
                                _draw.DrawACard(Draw.DeckType.Blood, creature, spell);
                            }
                            if ((caller.GetComponent<CardCore>()?.currentPosition.oponent == FindObjectOfType<Player>() || caller.GetComponent<Enemy>()) && FindObjectOfType<EnemyAI>().enabled)
                            {
                                FindObjectOfType<EnemyAI>().DrawACard(null, creature, spell, 0);
                                FindObjectOfType<EnemyAI>().DrawACard(null, creature, spell, 0);
                            }
                            break;
                    }
        CheckingEffect(caller);
        #endregion
    }
    private void DealDamageEffect(MonoBehaviour caller, Effects newEffect, int numberEffect, bool startTurn, bool endTurn)
    {
        #region DealDamage
        MapPosition[] enemyPositions;
        MapPosition[] allyPositions;
        Health enemyHealth;
        Health allyHealth;
        if (caller.GetComponent<CardCore>()?.currentPosition.oponent == FindObjectOfType<Player>() || caller.GetComponent<Enemy>())
        {
            enemyPositions = _table.playerPositions;
            allyPositions = _table.enemyFront;
            enemyHealth = FindObjectOfType<Player>();
            allyHealth = FindObjectOfType<Enemy>();
        }
        else
        {
            enemyPositions = _table.enemyFront;
            allyPositions = _table.playerPositions;
            enemyHealth = FindObjectOfType<Enemy>();
            allyHealth = FindObjectOfType<Player>();
        }
        for (int i = 0; i < effect.targetsCreatures.Count; i++)
            if (effect.targetsCreatures[i] == newEffect.targetsCreatures[numberEffect])
                switch (i)
                {
                    case 1:
                        var enemysAlive1 = new List<Card>();
                        foreach (MapPosition enemy in enemyPositions)
                            if (enemy.card != null)
                                if (enemy.card.ActualLife > 0)
                                    enemysAlive1.Add(enemy.card);
                        if (enemysAlive1.Count == 0)
                        {
                            CheckingEffect(caller);
                            return;
                        }
                        waitForSelect = true;
                        foreach (Card enemy in enemysAlive1)
                        {
                            enemy.waitForSelect = true;
                            enemy.StartCoroutine(enemy.CardSelected());
                        }
                        StartCoroutine(WaitCardSelect(caller, 1, startTurn, endTurn, newEffect.values[numberEffect][0], 0, 0, 0, false));
                        Debug.Log("Elige Enemigo");
                        return;
                    case 2:
                        var allysAlive1 = new List<Card>();
                        foreach (MapPosition ally in allyPositions)
                            if (ally.card != null)
                                if (ally.card.ActualLife > 0)
                                    allysAlive1.Add(ally.card);
                        if (allysAlive1.Count == 0)
                        {
                            CheckingEffect(caller);
                            return;
                        }
                        waitForSelect = true;
                        foreach (Card ally in allysAlive1)
                        {
                            ally.waitForSelect = true;
                            ally.StartCoroutine(ally.CardSelected());
                        }
                        StartCoroutine(WaitCardSelect(caller, 1, startTurn, endTurn, newEffect.values[numberEffect][0], 0, 0, 0, false));
                        Debug.Log("Elige Aliado");
                        return;
                    case 3:
                        var enemysAlive2 = new List<MapPosition>();
                        foreach (MapPosition enemy in enemyPositions)
                            if (enemy.card != null)
                                if (enemy.card.ActualLife > 0)
                                    enemysAlive2.Add(enemy);
                        int selected1 = 0;
                        if (enemysAlive2.Count > 0)
                            selected1 = Random.Range(0, enemysAlive2.Count + 1);
                        if (selected1 == enemyPositions.Length)
                            enemyHealth.ReceiveDamage(newEffect.values[numberEffect][0], startTurn, endTurn);
                        else if (enemysAlive2[selected1].card != null)
                            enemysAlive2[selected1].card.ReceiveDamageEffect(newEffect.values[numberEffect][0], null, startTurn, endTurn);
                        break;
                    case 4:
                        var allysAlive2 = new List<MapPosition>();
                        foreach (MapPosition enemy in allyPositions)
                            if (enemy.card != null)
                                if (enemy.card.ActualLife > 0)
                                    allysAlive2.Add(enemy);
                        int selected2 = 0;
                        if (allysAlive2.Count > 0)
                            selected2 = Random.Range(0, allysAlive2.Count + 1);
                        if (selected2 == allyPositions.Length)
                            allyHealth.ReceiveDamage(newEffect.values[numberEffect][0], startTurn, endTurn);
                        else if (allysAlive2[selected2].card != null)
                            allysAlive2[selected2].card.ReceiveDamageEffect(newEffect.values[numberEffect][0], null, startTurn, endTurn);
                        break;
                    case 5:
                        var enemysAlive3 = new List<MapPosition>();
                        foreach (MapPosition enemy in enemyPositions)
                            if (enemy.card != null)
                                if (enemy.card.ActualLife > 0)
                                    enemysAlive3.Add(enemy);
                        int selected3 = 0;
                        if (enemysAlive3.Count > 0)
                            selected3 = Random.Range(0, enemysAlive3.Count);
                        if (enemysAlive3[selected3].card != null)
                            enemysAlive3[selected3].card.ReceiveDamageEffect(newEffect.values[numberEffect][0], null, startTurn, endTurn);
                        break;
                    case 6:
                        var allysAlive3 = new List<MapPosition>();
                        foreach (MapPosition enemy in allyPositions)
                            if (enemy.card != null)
                                if (enemy.card.ActualLife > 0)
                                    allysAlive3.Add(enemy);
                        int selected4 = 0;
                        if (allysAlive3.Count > 0)
                            selected4 = Random.Range(0, allysAlive3.Count);
                        if (allysAlive3[selected4].card != null)
                            allysAlive3[selected4].card.ReceiveDamageEffect(newEffect.values[numberEffect][0], null, startTurn, endTurn);
                        break;
                    case 7:
                        for (int j = 0; j < newEffect.numberOfTargets[numberEffect]; j++)
                        {
                            var enemysAlive = new List<MapPosition>();
                            foreach (MapPosition enemy in enemyPositions)
                                if (enemy.card != null)
                                    if (enemy.card.ActualLife > 0)
                                        enemysAlive.Add(enemy);
                            int selected = 0;
                            if (enemysAlive.Count > 0)
                                selected = Random.Range(0, enemysAlive.Count);
                            if (enemysAlive[selected].card != null)
                                enemysAlive[selected].card.ReceiveDamageEffect(newEffect.values[numberEffect][0], null, startTurn, endTurn);
                        }
                        break;
                    case 8:
                        for (int j = 0; j < newEffect.numberOfTargets[numberEffect]; j++)
                        {
                            var allysAlive = new List<MapPosition>();
                            foreach (MapPosition enemy in allyPositions)
                                if (enemy.card != null)
                                    if (enemy.card.ActualLife > 0)
                                        allysAlive.Add(enemy);
                            int selected = 0;
                            if (allysAlive.Count > 0)
                                selected = Random.Range(0, allysAlive.Count);
                            if (allysAlive[selected].card != null)
                                allysAlive[selected].card.ReceiveDamageEffect(newEffect.values[numberEffect][0], null, startTurn, endTurn);
                        }
                        break;
                    case 9:
                        foreach (MapPosition selected in enemyPositions)
                            if (selected.card != null)
                                selected.card.ReceiveDamageEffect(newEffect.values[numberEffect][0], null, startTurn, endTurn);
                        break;
                    case 10:
                        foreach (MapPosition selected in allyPositions)
                            if (selected.card != null)
                                selected.card.ReceiveDamageEffect(newEffect.values[numberEffect][0], null, startTurn, endTurn);
                        break;
                    case 11:
                        if (caller.GetComponent<Card>())
                            caller.GetComponent<Card>().ReceiveDamageEffect(newEffect.values[numberEffect][0], null, startTurn, endTurn);
                        if (caller.GetComponent<Health>())
                            caller.GetComponent<Health>().ReceiveDamage(newEffect.values[numberEffect][0], startTurn, endTurn);
                        break;
                    case 12:
                        enemyHealth.ReceiveDamage(newEffect.values[numberEffect][0], startTurn, endTurn);
                        break;
                    case 13:
                        allyHealth.ReceiveDamage(newEffect.values[numberEffect][0], startTurn, endTurn);
                        break;
                    case 14:
                        caller.GetComponent<CardCore>()?.currentPosition.positionFacing.card.ReceiveDamageEffect(newEffect.values[numberEffect][0], null, startTurn, endTurn);
                        break;
                    case 15:
                        var enemysAlive4 = new List<Card>();
                        foreach (MapPosition enemy in enemyPositions)
                            if (enemy.card != null)
                                if (enemy.card.ActualLife > 0)
                                    enemysAlive4.Add(enemy.card);
                        if (enemysAlive4.Count == 0)
                        {
                            CheckingEffect(caller);
                            return;
                        }
                        int numberOfObjectives1 = newEffect.numberOfTargets[numberEffect];
                        if (enemysAlive4.Count < newEffect.numberOfTargets[numberEffect])
                            numberOfObjectives1 = enemysAlive4.Count;
                        waitForSelect = true;
                        foreach (Card enemy in enemysAlive4)
                        {
                            enemy.waitForSelect = true;
                            enemy.StartCoroutine(enemy.CardSelected());
                        }
                        StartCoroutine(WaitCardSelect(caller, numberOfObjectives1, startTurn, endTurn, newEffect.values[numberEffect][0], 0, 0, 0, false));
                        Debug.Log("Elige Enemigo");
                        return;
                    case 16:
                        var allysAlive4 = new List<Card>();
                        foreach (MapPosition ally in allyPositions)
                            if (ally.card != null)
                                if (ally.card.ActualLife > 0)
                                    allysAlive4.Add(ally.card);
                        if (allysAlive4.Count == 0)
                        {
                            CheckingEffect(caller);
                            return;
                        }
                        int numberOfObjectives2 = newEffect.numberOfTargets[numberEffect];
                        if (allysAlive4.Count < newEffect.numberOfTargets[numberEffect])
                            numberOfObjectives2 = allysAlive4.Count;
                        foreach (Card ally in allysAlive4)
                        {
                            ally.waitForSelect = true;
                            ally.StartCoroutine(ally.CardSelected());
                        }
                        StartCoroutine(WaitCardSelect(caller, numberOfObjectives2, startTurn, endTurn, newEffect.values[numberEffect][0], 0, 0, 0, false));
                        Debug.Log("Elige Aliado");
                        return;
                }
        CheckingEffect(caller);
        #endregion
    }
    private void HealEffect(MonoBehaviour caller, Effects newEffect, int numberEffect, bool startTurn, bool endTurn)
    {
        #region Heal
        MapPosition[] enemyPositions;
        MapPosition[] allyPositions;
        Health enemyHealth;
        Health allyHealth;
        if (caller.GetComponent<CardCore>()?.currentPosition.oponent == FindObjectOfType<Player>() || caller.GetComponent<Enemy>())
        {
            enemyPositions = _table.playerPositions;
            allyPositions = _table.enemyFront;
            enemyHealth = FindObjectOfType<Player>();
            allyHealth = FindObjectOfType<Enemy>();
        }
        else
        {
            enemyPositions = _table.enemyFront;
            allyPositions = _table.playerPositions;
            enemyHealth = FindObjectOfType<Enemy>();
            allyHealth = FindObjectOfType<Player>();
        }
        for (int i = 0; i < effect.targetsCreatures.Count; i++)
            if (effect.targetsCreatures[i] == newEffect.targetsCreatures[numberEffect])
                switch (i)
                {
                    case 1:
                        var enemysAlive1 = new List<Card>();
                        foreach (MapPosition enemy in enemyPositions)
                            if (enemy.card != null)
                                if (enemy.card.ActualLife > 0 && enemy.card.ActualLife < enemy.card.card.life)
                                    enemysAlive1.Add(enemy.card);
                        if (enemysAlive1.Count == 0)
                        {
                            CheckingEffect(caller);
                            return;
                        }
                        waitForSelect = true;
                        foreach (Card enemy in enemysAlive1)
                        {
                            enemy.waitForSelect = true;
                            enemy.StartCoroutine(enemy.CardSelected());
                        }
                        StartCoroutine(WaitCardSelect(caller, 1, startTurn, endTurn, 0, newEffect.values[numberEffect][0], 0, 0, false));
                        Debug.Log("Elige Enemigo");
                        return;
                    case 2:
                        var allysAlive1 = new List<Card>();
                        foreach (MapPosition ally in allyPositions)
                            if (ally.card != null)
                                if (ally.card.ActualLife > 0 && ally.card.ActualLife < ally.card.card.life)
                                    allysAlive1.Add(ally.card);
                        if (allysAlive1.Count == 0)
                        {
                            CheckingEffect(caller);
                            return;
                        }
                        waitForSelect = true;
                        foreach (Card ally in allysAlive1)
                        {
                            ally.waitForSelect = true;
                            ally.StartCoroutine(ally.CardSelected());
                        }
                        StartCoroutine(WaitCardSelect(caller, 1, startTurn, endTurn, 0, newEffect.values[numberEffect][0], 0, 0, false));
                        Debug.Log("Elige Aliado");
                        return;
                    case 3:
                        var enemysAlive2 = new List<MapPosition>();
                        foreach (MapPosition enemy in enemyPositions)
                            if (enemy.card != null)
                                if (enemy.card.ActualLife > 0 && enemy.card.ActualLife < enemy.card.card.life)
                                    enemysAlive2.Add(enemy);
                        int selected1 = 0;
                        if (enemysAlive2.Count > 0)
                            selected1 = Random.Range(0, enemysAlive2.Count + 1);
                        if (selected1 == enemyPositions.Length)
                            enemyHealth.RestoreHealth(newEffect.values[numberEffect][0], startTurn, endTurn);
                        else if (enemysAlive2[selected1].card != null)
                            enemysAlive2[selected1].card.HealEffect(newEffect.values[numberEffect][0], startTurn, endTurn);
                        break;
                    case 4:
                        var allysAlive2 = new List<MapPosition>();
                        foreach (MapPosition ally in allyPositions)
                            if (ally.card != null)
                                if (ally.card.ActualLife > 0 && ally.card.ActualLife < ally.card.card.life)
                                    allysAlive2.Add(ally);
                        int selected2 = 0;
                        if (allysAlive2.Count > 0)
                            selected2 = Random.Range(0, allysAlive2.Count + 1);
                        if (selected2 == allyPositions.Length)
                            allyHealth.RestoreHealth(newEffect.values[numberEffect][0], startTurn, endTurn);
                        else if (allysAlive2[selected2].card != null)
                            allysAlive2[selected2].card.HealEffect(newEffect.values[numberEffect][0], startTurn, endTurn);
                        break;
                    case 5:
                        var enemysAlive3 = new List<MapPosition>();
                        foreach (MapPosition enemy in enemyPositions)
                            if (enemy.card != null)
                                if (enemy.card.ActualLife > 0 && enemy.card.ActualLife < enemy.card.card.life)
                                    enemysAlive3.Add(enemy);
                        int selected3 = 0;
                        if (enemysAlive3.Count > 0)
                            selected3 = Random.Range(0, enemysAlive3.Count);
                        if (enemysAlive3[selected3].card != null)
                            enemysAlive3[selected3].card.HealEffect(newEffect.values[numberEffect][0], startTurn, endTurn);
                        break;
                    case 6:
                        var allysAlive3 = new List<MapPosition>();
                        foreach (MapPosition ally in allyPositions)
                            if (ally.card != null)
                                if (ally.card.ActualLife > 0 && ally.card.ActualLife < ally.card.card.life)
                                    allysAlive3.Add(ally);
                        int selected4 = 0;
                        if (allysAlive3.Count > 0)
                            selected4 = Random.Range(0, allysAlive3.Count);
                        if (allysAlive3[selected4].card != null)
                            allysAlive3[selected4].card.HealEffect(newEffect.values[numberEffect][0], startTurn, endTurn);
                        break;
                    case 7:
                        for (int j = 0; j < newEffect.numberOfTargets[numberEffect]; j++)
                        {
                            var enemysAlive = new List<MapPosition>();
                            foreach (MapPosition enemy in enemyPositions)
                                if (enemy.card != null)
                                    if (enemy.card.ActualLife > 0 && enemy.card.ActualLife < enemy.card.card.life)
                                        enemysAlive.Add(enemy);
                            int selected = 0;
                            if (enemysAlive.Count > 0)
                                selected = Random.Range(0, enemysAlive.Count);
                            if (enemysAlive[selected].card != null)
                                enemysAlive[selected].card.HealEffect(newEffect.values[numberEffect][0], startTurn, endTurn);
                        }
                        break;
                    case 8:
                        for (int j = 0; j < newEffect.numberOfTargets[numberEffect]; j++)
                        {
                            var allysAlive = new List<MapPosition>();
                            foreach (MapPosition ally in allyPositions)
                                if (ally.card != null)
                                    if (ally.card.ActualLife > 0 && ally.card.ActualLife < ally.card.card.life)
                                        allysAlive.Add(ally);
                            int selected = 0;
                            if (allysAlive.Count > 0)
                                selected = Random.Range(0, allysAlive.Count);
                            if (allysAlive[selected].card != null)
                                allysAlive[selected].card.HealEffect(newEffect.values[numberEffect][0], startTurn, endTurn);
                        }
                        break;
                    case 9:
                        foreach (MapPosition selected in enemyPositions)
                            if (selected.card != null)
                                selected.card.HealEffect(newEffect.values[numberEffect][0], startTurn, endTurn);
                        break;
                    case 10:
                        foreach (MapPosition selected in allyPositions)
                            if (selected.card != null)
                                selected.card.HealEffect(newEffect.values[numberEffect][0], startTurn, endTurn);
                        break;
                    case 11:
                        if (caller.GetComponent<Card>())
                            caller.GetComponent<Card>().HealEffect(newEffect.values[numberEffect][0], startTurn, endTurn);
                        if (caller.GetComponent<Health>())
                            caller.GetComponent<Health>().RestoreHealth(newEffect.values[numberEffect][0], startTurn, endTurn);
                        break;
                    case 12:
                        enemyHealth.RestoreHealth(newEffect.values[numberEffect][0], startTurn, endTurn);
                        break;
                    case 13:
                        allyHealth.RestoreHealth(newEffect.values[numberEffect][0], startTurn, endTurn);
                        break;
                    case 14:
                        caller.GetComponent<CardCore>()?.currentPosition.positionFacing.card.HealEffect(newEffect.values[numberEffect][0], startTurn, endTurn);
                        break;
                    case 15:
                        var enemysAlive4 = new List<Card>();
                        foreach (MapPosition enemy in enemyPositions)
                            if (enemy.card != null)
                                if (enemy.card.ActualLife > 0 && enemy.card.ActualLife < enemy.card.card.life)
                                    enemysAlive4.Add(enemy.card);
                        if (enemysAlive4.Count == 0)
                        {
                            CheckingEffect(caller);
                            return;
                        }
                        int numberOfObjectives1 = newEffect.numberOfTargets[numberEffect];
                        if (enemysAlive4.Count < newEffect.numberOfTargets[numberEffect])
                            numberOfObjectives1 = enemysAlive4.Count;
                        waitForSelect = true;
                        foreach (Card enemy in enemysAlive4)
                        {
                            enemy.waitForSelect = true;
                            enemy.StartCoroutine(enemy.CardSelected());
                        }
                        StartCoroutine(WaitCardSelect(caller, numberOfObjectives1, startTurn, endTurn, 0, newEffect.values[numberEffect][0], 0, 0, false));
                        Debug.Log("Elige Enemigo");
                        return;
                    case 16:
                        var allysAlive4 = new List<Card>();
                        foreach (MapPosition ally in allyPositions)
                            if (ally.card != null)
                                if (ally.card.ActualLife > 0 && ally.card.ActualLife < ally.card.card.life)
                                    allysAlive4.Add(ally.card);
                        if (allysAlive4.Count == 0)
                        {
                            CheckingEffect(caller);
                            return;
                        }
                        int numberOfObjectives2 = newEffect.numberOfTargets[numberEffect];
                        if (allysAlive4.Count < newEffect.numberOfTargets[numberEffect])
                            numberOfObjectives2 = allysAlive4.Count;
                        foreach (Card ally in allysAlive4)
                        {
                            ally.waitForSelect = true;
                            ally.StartCoroutine(ally.CardSelected());
                        }
                        StartCoroutine(WaitCardSelect(caller, numberOfObjectives2, startTurn, endTurn, 0, newEffect.values[numberEffect][0], 0, 0, false));
                        Debug.Log("Elige Aliado");
                        return;
                }
        CheckingEffect(caller);
        #endregion
    }
    private void AddEffect(MonoBehaviour caller, Effects newEffect, int numberEffect, bool startTurn, bool endTurn)
    {
        #region Add
        bool player = true;
        if ((caller.GetComponent<CardCore>()?.currentPosition.oponent == FindObjectOfType<Player>() || caller.GetComponent<EnemyAI>()) && FindObjectOfType<EnemyAI>().enabled)
            player = false;
        for (int i = 0; i < effect.targetsDecks.Count; i++)
            if (effect.targetsDecks[i] == newEffect.targetsDecks[numberEffect])
                switch (i)
                {
                    case 1:
                        List<Cards> cardsToAdd1 = new List<Cards>();
                        cardsToAdd1 = TypeOfCardsToAdd(cardsToAdd1, newEffect, numberEffect);
                        for (int j = 0; j < newEffect.values[numberEffect][0] && j < cardsToAdd1.Count; j++)
                        {
                            if (player)
                            {
                                var addCard = Instantiate(newCard, _draw.transform);
                                addCard.card = cardsToAdd1[j];
                                addCard.GetComponent<Card>().SetData();
                                _draw.AddCardToHand(addCard);
                            }
                            else if (FindObjectOfType<EnemyAI>())
                                FindObjectOfType<EnemyAI>().hand.Add(cardsToAdd1[j]);
                        }
                        break;
                    case 2:
                        List<Cards> cardsToAdd2 = new List<Cards>();
                        cardsToAdd2 = TypeOfCardsToAdd(cardsToAdd2, newEffect, numberEffect);
                        for (int j = 0; j < newEffect.values[numberEffect][0] && j < cardsToAdd2.Count; j++)
                        {
                            if (player)
                                _draw.AddATempCard(cardsToAdd2[j]);
                            else if (FindObjectOfType<EnemyAI>())
                                FindObjectOfType<EnemyAI>().hand.Add(cardsToAdd2[j]);
                        }
                        break;
                    case 3:
                        List<Cards> cardsToAdd3 = new List<Cards>();
                        cardsToAdd3 = TypeOfCardsToAdd(cardsToAdd3, newEffect, numberEffect);
                        for (int j = 0; j < newEffect.values[numberEffect][0] && j < cardsToAdd3.Count; j++)
                        {
                            if (player)
                                _draw.AddATempCard(cardsToAdd3[j]);
                            else if (FindObjectOfType<EnemyAI>())
                                FindObjectOfType<EnemyAI>().hand.Add(cardsToAdd3[j]);
                        }
                        break;
                }
        CheckingEffect(caller);
        #endregion
    }
    private void GiveEffect(MonoBehaviour caller, Effects newEffect, int numberEffect, bool startTurn, bool endTurn)
    {
        #region Give
        MapPosition[] enemyPositions;
        MapPosition[] allyPositions;
        if (caller.GetComponent<CardCore>()?.currentPosition.oponent == FindObjectOfType<Player>() || caller.GetComponent<Enemy>())
        {
            enemyPositions = _table.playerPositions;
            allyPositions = _table.enemyFront;
        }
        else
        {
            enemyPositions = _table.enemyFront;
            allyPositions = _table.playerPositions;
        }
        for (int i = 0; i < effect.targetsCreatures.Count; i++)
            if (effect.targetsCreatures[i] == newEffect.targetsCreatures[numberEffect])
                switch (i)
                {
                    case 1:
                        var enemysAlive1 = new List<Card>();
                        foreach (MapPosition enemy in enemyPositions)
                            if (enemy.card != null)
                                if (enemy.card.ActualLife > 0)
                                    enemysAlive1.Add(enemy.card);
                        if (enemysAlive1.Count == 0)
                        {
                            CheckingEffect(caller);
                            return;
                        }
                        waitForSelect = true;
                        foreach (Card enemy in enemysAlive1)
                        {
                            enemy.waitForSelect = true;
                            enemy.StartCoroutine(enemy.CardSelected());
                        }
                        StartCoroutine(WaitCardSelect(caller, 1, startTurn, endTurn, 0, 0, 
                            newEffect.values[numberEffect][0], newEffect.values[numberEffect][1], false));
                        Debug.Log("Elige Enemigo");
                        return;
                    case 2:
                        var allysAlive1 = new List<Card>();
                        foreach (MapPosition ally in allyPositions)
                            if (ally.card != null)
                                if (ally.card.ActualLife > 0)
                                    allysAlive1.Add(ally.card);
                        if (allysAlive1.Count == 0)
                        {
                            CheckingEffect(caller);
                            return;
                        }
                        waitForSelect = true;
                        foreach (Card ally in allysAlive1)
                        {
                            ally.waitForSelect = true;
                            ally.StartCoroutine(ally.CardSelected());
                        }
                        StartCoroutine(WaitCardSelect(caller, 1, startTurn, endTurn, 0, 0,
                            newEffect.values[numberEffect][0], newEffect.values[numberEffect][1], false));
                        Debug.Log("Elige Aliado");
                        return;
                    case 5:
                        var enemysAlive3 = new List<MapPosition>();
                        foreach (MapPosition enemy in enemyPositions)
                            if (enemy.card != null)
                                if (enemy.card.ActualLife > 0)
                                    enemysAlive3.Add(enemy);
                        int selected3 = 0;
                        if (enemysAlive3.Count > 0)
                            selected3 = Random.Range(0, enemysAlive3.Count);
                        if (enemysAlive3[selected3].card != null)
                            enemysAlive3[selected3].card.BuffEffect(newEffect.values[numberEffect][0], newEffect.values[numberEffect][1], startTurn, endTurn);
                        break;
                    case 6:
                        var allysAlive3 = new List<MapPosition>();
                        foreach (MapPosition ally in allyPositions)
                            if (ally.card != null)
                                if (ally.card.ActualLife > 0)
                                    allysAlive3.Add(ally);
                        int selected4 = 0;
                        if (allysAlive3.Count > 0)
                            selected4 = Random.Range(0, allysAlive3.Count);
                        if (allysAlive3[selected4].card != null)
                            allysAlive3[selected4].card.BuffEffect(newEffect.values[numberEffect][0], newEffect.values[numberEffect][1], startTurn, endTurn);
                        break;
                    case 7:
                        for (int j = 0; j < newEffect.numberOfTargets[numberEffect]; j++)
                        {
                            var enemysAlive = new List<MapPosition>();
                            foreach (MapPosition enemy in enemyPositions)
                                if (enemy.card != null)
                                    if (enemy.card.ActualLife > 0)
                                        enemysAlive.Add(enemy);
                            int selected = 0;
                            if (enemysAlive.Count > 0)
                                selected = Random.Range(0, enemysAlive.Count);
                            if (enemysAlive[selected].card != null)
                                enemysAlive[selected].card.BuffEffect(newEffect.values[numberEffect][0], newEffect.values[numberEffect][1], startTurn, endTurn);
                        }
                        break;
                    case 8:
                        for (int j = 0; j < newEffect.numberOfTargets[numberEffect]; j++)
                        {
                            var allysAlive = new List<MapPosition>();
                            foreach (MapPosition ally in allyPositions)
                                if (ally.card != null)
                                    if (ally.card.ActualLife > 0)
                                        allysAlive.Add(ally);
                            int selected = 0;
                            if (allysAlive.Count > 0)
                                selected = Random.Range(0, allysAlive.Count);
                            if (allysAlive[selected].card != null)
                                allysAlive[selected].card.BuffEffect(newEffect.values[numberEffect][0], newEffect.values[numberEffect][1], startTurn, endTurn);
                        }
                        break;
                    case 9:
                        foreach (MapPosition selected in enemyPositions)
                            if (selected.card != null)
                                selected.card.BuffEffect(newEffect.values[numberEffect][0], newEffect.values[numberEffect][1], startTurn, endTurn);
                        break;
                    case 10:
                        foreach (MapPosition selected in allyPositions)
                            if (selected.card != null)
                                selected.card.BuffEffect(newEffect.values[numberEffect][0], newEffect.values[numberEffect][1], startTurn, endTurn);
                        break;
                    case 11:
                        if (caller.GetComponent<Card>())
                            caller.GetComponent<Card>().BuffEffect(newEffect.values[numberEffect][0], newEffect.values[numberEffect][1], startTurn, endTurn);
                        break;
                    case 14:
                        caller.GetComponent<CardCore>()?.currentPosition.positionFacing.card.BuffEffect(newEffect.values[numberEffect][0],
                            newEffect.values[numberEffect][1], startTurn, endTurn);
                        break;
                    case 15:
                        var enemysAlive4 = new List<Card>();
                        foreach (MapPosition enemy in enemyPositions)
                            if (enemy.card != null)
                                if (enemy.card.ActualLife > 0)
                                    enemysAlive4.Add(enemy.card);
                        if (enemysAlive4.Count == 0)
                        {
                            CheckingEffect(caller);
                            return;
                        }
                        int numberOfObjectives1 = newEffect.numberOfTargets[numberEffect];
                        if (enemysAlive4.Count < newEffect.numberOfTargets[numberEffect])
                            numberOfObjectives1 = enemysAlive4.Count;
                        waitForSelect = true;
                        foreach (Card enemy in enemysAlive4)
                        {
                            enemy.waitForSelect = true;
                            enemy.StartCoroutine(enemy.CardSelected());
                        }
                        StartCoroutine(WaitCardSelect(caller, numberOfObjectives1, startTurn, endTurn, 0, 0,
                            newEffect.values[numberEffect][0], newEffect.values[numberEffect][1], false));
                        Debug.Log("Elige Enemigo");
                        return;
                    case 16:
                        var allysAlive4 = new List<Card>();
                        foreach (MapPosition ally in allyPositions)
                            if (ally.card != null)
                                if (ally.card.ActualLife > 0)
                                    allysAlive4.Add(ally.card);
                        if (allysAlive4.Count == 0)
                        {
                            CheckingEffect(caller);
                            return;
                        }
                        int numberOfObjectives2 = newEffect.numberOfTargets[numberEffect];
                        if (allysAlive4.Count < newEffect.numberOfTargets[numberEffect])
                            numberOfObjectives2 = allysAlive4.Count;
                        foreach (Card ally in allysAlive4)
                        {
                            ally.waitForSelect = true;
                            ally.StartCoroutine(ally.CardSelected());
                        }
                        StartCoroutine(WaitCardSelect(caller, numberOfObjectives2, startTurn, endTurn, 0, 0,
                            newEffect.values[numberEffect][0], newEffect.values[numberEffect][1], false));
                        Debug.Log("Elige Aliado");
                        return;
                }
        CheckingEffect(caller);
        #endregion
    }
    private void ImmuneEffect(MonoBehaviour caller, Effects newEffect, int numberEffect, bool startTurn, bool endTurn)
    {
        #region Inmune
        MapPosition[] enemyPositions;
        MapPosition[] allyPositions;
        if (caller.GetComponent<CardCore>()?.currentPosition.oponent == FindObjectOfType<Player>() || caller.GetComponent<Enemy>())
        {
            enemyPositions = _table.playerPositions;
            allyPositions = _table.enemyFront;
        }
        else
        {
            enemyPositions = _table.enemyFront;
            allyPositions = _table.playerPositions;
        }
        for (int i = 0; i < effect.targetsCreatures.Count; i++)
            if (effect.targetsCreatures[i] == newEffect.targetsCreatures[numberEffect])
                switch (i)
                {
                    case 1:
                        var enemysAlive1 = new List<Card>();
                        foreach (MapPosition enemy in enemyPositions)
                            if (enemy.card != null)
                                if (enemy.card.ActualLife > 0)
                                    enemysAlive1.Add(enemy.card);
                        if (enemysAlive1.Count == 0)
                        {
                            CheckingEffect(caller);
                            return;
                        }
                        waitForSelect = true;
                        foreach (Card enemy in enemysAlive1)
                        {
                            enemy.waitForSelect = true;
                            enemy.StartCoroutine(enemy.CardSelected());
                        }
                        StartCoroutine(WaitCardSelect(caller, 1, startTurn, endTurn, 0, 0, 0, 0, true));
                        Debug.Log("Elige Enemigo");
                        return;
                    case 2:
                        var allysAlive1 = new List<Card>();
                        foreach (MapPosition ally in allyPositions)
                            if (ally.card != null)
                                if (ally.card.ActualLife > 0)
                                    allysAlive1.Add(ally.card);
                        if (allysAlive1.Count == 0)
                        {
                            CheckingEffect(caller);
                            return;
                        }
                        waitForSelect = true;
                        foreach (Card ally in allysAlive1)
                        {
                            ally.waitForSelect = true;
                            ally.StartCoroutine(ally.CardSelected());
                        }
                        StartCoroutine(WaitCardSelect(caller, 1, startTurn, endTurn, 0, 0, 0, 0, true));
                        Debug.Log("Elige Aliado");
                        return;
                    case 5:
                        var enemysAlive3 = new List<MapPosition>();
                        foreach (MapPosition enemy in enemyPositions)
                            if (enemy.card != null)
                                if (enemy.card.ActualLife > 0)
                                    enemysAlive3.Add(enemy);
                        int selected3 = 0;
                        if (enemysAlive3.Count > 0)
                            selected3 = Random.Range(0, enemysAlive3.Count);
                        if (enemysAlive3[selected3].card != null)
                            enemysAlive3[selected3].card.ImmuneEffect(startTurn, endTurn);
                        break;
                    case 6:
                        var allysAlive3 = new List<MapPosition>();
                        foreach (MapPosition ally in allyPositions)
                            if (ally.card != null)
                                if (ally.card.ActualLife > 0)
                                    allysAlive3.Add(ally);
                        int selected4 = 0;
                        if (allysAlive3.Count > 0)
                            selected4 = Random.Range(0, allysAlive3.Count);
                        if (allysAlive3[selected4].card != null)
                            allysAlive3[selected4].card.ImmuneEffect(startTurn, endTurn);
                        break;
                    case 7:
                        for (int j = 0; j < newEffect.numberOfTargets[numberEffect]; j++)
                        {
                            var enemysAlive = new List<MapPosition>();
                            foreach (MapPosition enemy in enemyPositions)
                                if (enemy.card != null)
                                    if (enemy.card.ActualLife > 0)
                                        enemysAlive.Add(enemy);
                            int selected = 0;
                            if (enemysAlive.Count > 0)
                                selected = Random.Range(0, enemysAlive.Count);
                            if (enemysAlive[selected].card != null)
                                enemysAlive[selected].card.ImmuneEffect(startTurn, endTurn);
                        }
                        break;
                    case 8:
                        for (int j = 0; j < newEffect.numberOfTargets[numberEffect]; j++)
                        {
                            var allysAlive = new List<MapPosition>();
                            foreach (MapPosition ally in allyPositions)
                                if (ally.card != null)
                                    if (ally.card.ActualLife > 0)
                                        allysAlive.Add(ally);
                            int selected = 0;
                            if (allysAlive.Count > 0)
                                selected = Random.Range(0, allysAlive.Count);
                            if (allysAlive[selected].card != null)
                                allysAlive[selected].card.ImmuneEffect(startTurn, endTurn);
                        }
                        break;
                    case 9:
                        foreach (MapPosition selected in enemyPositions)
                            if (selected.card != null)
                                selected.card.ImmuneEffect(startTurn, endTurn);
                        break;
                    case 10:
                        foreach (MapPosition selected in allyPositions)
                            if (selected.card != null)
                                selected.card.ImmuneEffect(startTurn, endTurn);
                        break;
                    case 11:
                        if (caller.GetComponent<Card>())
                            caller.GetComponent<Card>().ImmuneEffect(startTurn, endTurn);
                        break;
                    case 14:
                        caller.GetComponent<CardCore>()?.currentPosition.positionFacing.card.ImmuneEffect(startTurn, endTurn);
                        break;
                    case 15:
                        var enemysAlive4 = new List<Card>();
                        foreach (MapPosition enemy in enemyPositions)
                            if (enemy.card != null)
                                if (enemy.card.ActualLife > 0)
                                    enemysAlive4.Add(enemy.card);
                        if (enemysAlive4.Count == 0)
                        {
                            CheckingEffect(caller);
                            return;
                        }
                        int numberOfObjectives1 = newEffect.numberOfTargets[numberEffect];
                        if (enemysAlive4.Count < newEffect.numberOfTargets[numberEffect])
                            numberOfObjectives1 = enemysAlive4.Count;
                        waitForSelect = true;
                        foreach (Card enemy in enemysAlive4)
                        {
                            enemy.waitForSelect = true;
                            enemy.StartCoroutine(enemy.CardSelected());
                        }
                        StartCoroutine(WaitCardSelect(caller, numberOfObjectives1, startTurn, endTurn, 0, 0, 0, 0, true));
                        Debug.Log("Elige Enemigo");
                        return;
                    case 16:
                        var allysAlive4 = new List<Card>();
                        foreach (MapPosition ally in allyPositions)
                            if (ally.card != null)
                                if (ally.card.ActualLife > 0)
                                    allysAlive4.Add(ally.card);
                        if (allysAlive4.Count == 0)
                        {
                            CheckingEffect(caller);
                            return;
                        }
                        int numberOfObjectives2 = newEffect.numberOfTargets[numberEffect];
                        if (allysAlive4.Count < newEffect.numberOfTargets[numberEffect])
                            numberOfObjectives2 = allysAlive4.Count;
                        foreach (Card ally in allysAlive4)
                        {
                            ally.waitForSelect = true;
                            ally.StartCoroutine(ally.CardSelected());
                        }
                        StartCoroutine(WaitCardSelect(caller, numberOfObjectives2, startTurn, endTurn, 0, 0, 0, 0, true));
                        Debug.Log("Elige Aliado");
                        return;
                }
        CheckingEffect(caller);
        #endregion
    }
    private void SumonEffect(MonoBehaviour caller, Effects newEffect, int numberEffect, bool startTurn, bool endTurn)
    {
        #region Sumon
        bool sumoned = false;
        List<Cards> selectedCards = new List<Cards>();
        foreach (Cards cards in cards)
            foreach (string effectCards in newEffect.cards[numberEffect])
                if (effectCards == cards.scriptableName)
                    selectedCards.Add(cards);
        if (selectedCards != null)
        {
            if (caller.GetComponent<CardCore>()?.currentPosition.oponent.GetComponent<Enemy>() || caller.GetComponent<Player>())
            {
                List<MapPosition> playerMap = new List<MapPosition>();
                foreach (MapPosition mapPosition in _table.playerPositions)
                    if (mapPosition.card == null)
                        playerMap.Add(mapPosition);
                for (int i = 0; i < newEffect.values[numberEffect][0]; i++)
                {
                    if (playerMap.Count < 0)
                        return;
                    var pos = Random.Range(0, playerMap.Count + 1);
                    pos = System.Array.IndexOf(_table.playerPositions, playerMap[pos]);
                    GameObject sumonCard = Instantiate(newCard).gameObject;
                    sumonCard.GetComponent<Card>().card = selectedCards[Random.Range(0, selectedCards.Count)];
                    _table.SetCard(sumonCard, pos);
                    sumoned = true;
                }
            }
            else if (caller.GetComponent<CardCore>()?.currentPosition.oponent.GetComponent<Player>() || caller.GetComponent<Enemy>())
            {
                List<MapPosition> enemyMap = new List<MapPosition>();
                foreach (MapPosition mapPosition in _table.enemyFront)
                    if (mapPosition.card == null)
                        enemyMap.Add(mapPosition);
                for (int i = 0; i < newEffect.values[numberEffect][0]; i++)
                {
                    if (enemyMap.Count < 0)
                        return;
                    var pos = Random.Range(0, enemyMap.Count + 1);
                    pos = System.Array.IndexOf(_table.enemyFront, enemyMap[pos]);
                    GameObject sumonCard = Instantiate(newCard).gameObject;
                    sumonCard.GetComponent<Card>().card = selectedCards[Random.Range(0, selectedCards.Count)];
                    _table.EnemySpawnCard(pos, sumonCard);
                    sumoned = true;
                }
            }
        }
        CheckingEffect(caller);
        #endregion
    }
    private void ManaEffect(MonoBehaviour caller, Effects newEffect, int numberEffect, bool startTurn, bool endTurn)
    {
        #region Mana
        Health enemyMana;
        Health allyMana;
        if (caller.GetComponent<CardCore>()?.currentPosition.oponent == FindObjectOfType<Player>() || caller.GetComponent<Enemy>())
        {
            enemyMana = FindObjectOfType<Player>();
            allyMana = FindObjectOfType<Enemy>();
        }
        else
        {
            enemyMana = FindObjectOfType<Enemy>();
            allyMana = FindObjectOfType<Player>();
        }
        for (int i = 0; i < effect.targetsCreatures.Count; i++)
            if (effect.targetsPlayers[i] == newEffect.targetsPlayers[numberEffect])
                switch (i)
                {
                    case 1:
                        allyMana.ManaEffect(newEffect.values[numberEffect][0], startTurn, endTurn);
                        return;
                    case 2:
                        enemyMana.ManaEffect(newEffect.values[numberEffect][0], startTurn, endTurn);
                        return;
                }
        CheckingEffect(caller);
        #endregion
    }
    IEnumerator WaitCardSelect(MonoBehaviour caller, int numberOfObjectives, bool startTurn, bool endTurn, int damage, 
        int heal, int attack, int life, bool inmune)
    {
        ArrowToSelect arrow = FindAnyObjectByType<ArrowToSelect>();
        arrow.origin = Camera.main.WorldToScreenPoint(caller.transform.position);
        for (int i = 0; i < numberOfObjectives; i++)
        {
            while (selectedCard == null)
            {
                arrow.Arrow();
                yield return new WaitForSeconds(0.01f);
            }
            arrow.Hide();
            yield return new WaitUntil(() => (selectedCard != null));
            if (damage > 0)
                selectedCard.ReceiveDamageEffect(damage, null, startTurn, endTurn);
            if (heal > 0)
                selectedCard.HealEffect(heal, startTurn, endTurn);
            if (attack > 0 || life > 0)
                selectedCard.BuffEffect(attack, life, startTurn, endTurn);
            if (inmune)
                selectedCard.ImmuneEffect(startTurn, endTurn);
            selectedCard = null;
        }
        waitForSelect = false;
        selectedCard = null;
        CheckingEffect(caller);
    }
    private List<Cards> TypeOfCardsToAdd(List<Cards> cardsToAdd, Effects newEffect, int numberEffect)
    {
        List<Cards> listOfCards = new List<Cards>();
        for (int i = 0; i < effect.targetsCards.Count; i++)
            if (effect.targetsCards[i] == newEffect.targetsCards[numberEffect])
                switch (i)
                {
                    case 1:
                        foreach (Cards cardToList in cards)
                            if (!cardToList.spell)
                                listOfCards.Add(cardToList);
                        cardsToAdd.Add(listOfCards[Random.Range(0, listOfCards.Count)]);
                        break;
                    case 2:
                        foreach (Cards cardToList in cards)
                            if (cardToList.spell)
                                listOfCards.Add(cardToList);
                        cardsToAdd.Add(listOfCards[Random.Range(0, listOfCards.Count)]);
                        break;
                    case 3:
                        foreach (Cards cardToList in cards)
                            listOfCards.Add(cardToList);
                        cardsToAdd.Add(listOfCards[Random.Range(0, listOfCards.Count)]);
                        break;
                    case 4:
                        listOfCards = cards;
                        foreach (Cards cards in listOfCards)
                            foreach (string effect2 in newEffect.cards[numberEffect])
                                if (effect2 == cards.name)
                                    cardsToAdd.Add(cards);
                        break;
                }
        return cardsToAdd;
    }
    private void CheckingEffect(MonoBehaviour caller)
    {
        if (caller.GetComponent<CardCore>())
        {
            caller.GetComponent<CardCore>().checkingEffect = false;
            checkingEffect = false;
        }
    }
}
[System.Serializable]
public class Effects
{
    public List<string> conditions = new List<string>()
    {
        "None", //0
        "Start_turn", //1
        "Played", //2
        "Attacks", //3
        "Damaged", //4
        "Defeated", //5
        "Defeats_enemy", //6
        "Ally_creature_defeated", //7
        "End_turn", //8
        "Buffed", //9
        "Spell_played", //10
    };
    public List<ExtraConditions> extraConditions = new List<ExtraConditions>()
    {
        new ExtraConditions()
    };
    public List<string> tempEffect = new List<string>()
    {
        "None", //0
        "Until_next_turn", //1
        "Until_end_turn", //2
    };
    public List<string> effects = new List<string>()
    {
        "None", //0
        "Draw", //1
        "Deal_damage", //2
        "Heal", //3
        "Add", //4
        "Give_stats", //5
        "Immune", //6
        "Summon ", //7
        "Mana", //8
        "Give_stats_in_hand", //9
    };
    public List<string> targetsCreatures = new List<string>()
    {
        "None", //0
        "Enemy_creature", //1
        "Ally_creature", //2
        "Random_enemy", //3
        "Random_ally", //4
        "Random_enemy_creature", //5
        "Random_ally_creature", //6
        "Random_enemy_creatures", //7
        "Random_ally_creatures", //8
        "All_enemy_creatures", //9
        "All_ally_creatures", //10
        "Itself", //11
        "Enemy_player", //12
        "Player", //13
        "Creature_front", //14
        "Enemy_creatures", //15
        "Ally_creatures", //16
    };
    public List<string> targetsDecks = new List<string>()
    {
        "None", //0
        "Hand", //1
        "Deck", //2
        "Life_deck", //3
        "Either_deck", //4
    };
    public List<string> targetsCards = new List<string>()
    {
        "None", //0
        "Random_creature", //1
        "Random_spell", //2
        "Random", //3
        "Specific_card", //4
        "All_creatures", //5
        "All_spells", //6
        "All_cards", //7
    };
    public List<string> targetsPlayers = new List<string>()
    {
        "None", //0
        "Player", //1
        "Enemy", //2
    };
    public List<List<int>> values = new List<List<int>>();
    public List<int> numberOfTargets = new List<int>();
    public List<List<string>> cards = new List<List<string>>();
    public List<Effects> effectsToGive = new List<Effects>();
}
[System.Serializable]
public class ExtraConditions
{
    public List<string> extraConditions = new List<string>()
    {
        "None", //0
        "Have_specifics_cards_in_hand", //1
        "Enemy_have_specifics_cards_in_hand", //2
        "Have_x_cards_in_hand", //3
        "Enemy_have_x_cards_in_hand", //4
        "Have_at_least_x_cards_in_hand", //5
        "Enemy_have_at_least_x_cards_in_hand", //6
        "Have_specifics_cards_in_deck", //7
        "Enemy_have_specifics_cards_in_deck", //8
        "Have_x_cards_in_deck", //9
        "Enemy_have_x_cards_in_deck", //10
        "Have_at_least_x_cards_in_deck", //11
        "Enemy_have_at_least_x_cards_in_deck", //12
        "Have_specifics_cards_played", //13
        "Enemy_have_specifics_cards_played", //14
        "Have_x_cards_played", //15
        "Enemy_have_x_cards_played", //16
        "Have_at_least_x_cards_played", //17
        "Enemy_have_at_least_x_cards_played", //18
        "Have_x_life", //19
        "Enemy_have_x_life", //20
        "Have_at_least_x_life", //21
        "Enemy_have_at_least_x_life", //22
        "Have_x_mana", //23
        "Enemy_have_x_mana", //24
        "Have_at_least_x_mana", //25
        "Enemy_have_at_least_x_mana", //26
    };
    public List<int> extraConditionsInt = new List<int>();
    public List<List<string>> cards = new List<List<string>>();
}
