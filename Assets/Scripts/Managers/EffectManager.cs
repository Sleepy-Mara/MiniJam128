using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EffectManager : MonoBehaviour
{
    //[SerializeField]
    //private List<string> conditions = new List<string>()
    //{
    //    "start_turn", //0
    //    "played", //1
    //    "attacks", //2
    //    "damaged", //3
    //    "defeated", //4
    //    "defeats_enemy", //5
    //    "ally_creature_defeated", //6
    //    "end_turn", //7
    //    "buffed", //8
    //    "spell_played", //9
    //};
    //[HideInInspector]
    //public List<string> Conditions
    //{
    //    get { return conditions; }
    //}
    //[SerializeField]
    //private List<string> extraConditions = new List<string>()
    //{
    //    "until_next_turn", //0
    //    "until_end_turn" //1
    //};
    //[SerializeField]
    //private List<string> effects = new List<string>()
    //{
    //    "draw", //0
    //    "deal", //1
    //    "heal", //2
    //    "add", //3
    //    "give", //4
    //    "immune", //5
    //    "summon" //6
    //};
    //[HideInInspector]
    //public List<string> Effects
    //{
    //    get { return effects; }
    //}
    //[SerializeField]
    //private List<string> targets = new List<string>()
    //{
    //    "enemy_creature", //0
    //    "ally_creature", //1
    //    "random_enemy", //2
    //    "random_ally", //3
    //    "random_enemy_creature", //4
    //    "random_ally_creature", //5
    //    "random_enemy_creatures", //6
    //    "random_ally_creatures", //7
    //    "all_enemy_creatures", //8
    //    "all_ally_creatures", //9
    //    "itself", //10
    //    "enemy_player", //11
    //    "player", //12
    //    "creature_front", //13
    //    "hand", //14
    //    "deck", //15
    //    "life_deck", //16
    //    "either_deck", //17
    //    "random_creature", //18
    //    "random_spell", //19
    //    "enemy_creatures", //20
    //    "ally_creatures", //21
    //};
    //[HideInInspector]
    //public List<string> Target
    //{
    //    get { return targets; }
    //}
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
        bool checkEffect = true;
        for (int i = 0; i < effects.conditions.Count; i++)
            if (effects.conditions[i] == effect.conditions[1])
            {
                CheckEffect(caller, effects, i);
                checkEffect = false;
            }
        if (!checkEffect)
            CheckingEffect(caller);
    }
    public void CheckConditionIsPlayed(MonoBehaviour caller)
    {
        Effects effects = new Effects();
        if (caller.GetComponent<CardCore>())
            effects = caller.GetComponent<CardCore>().card.effects;
        bool checkEffect = true;
        for (int i = 0; i < effects.conditions.Count; i++)
            if (effects.conditions[i] == effect.conditions[2])
            {
                CheckEffect(caller, effects, i);
                checkEffect = false;
            }
        if (!checkEffect)
            CheckingEffect(caller);
    }
    public void CheckConditionAttack(MonoBehaviour caller)
    {
        Effects effects = new Effects();
        if (caller.GetComponent<CardCore>())
            effects = caller.GetComponent<CardCore>().card.effects;
        bool checkEffect = true;
        for (int i = 0; i < effects.conditions.Count; i++)
            if (effects.conditions[i] == effect.conditions[3])
            {
                CheckEffect(caller, effects, i);
                checkEffect = false;
            }
        if (!checkEffect)
            CheckingEffect(caller);
    }
    public void CheckConditionGetDamaged(MonoBehaviour caller)
    {
        Effects effects = new Effects();
        if (caller.GetComponent<CardCore>())
            effects = caller.GetComponent<CardCore>().card.effects;
        bool checkEffect = true;
        for (int i = 0; i < effects.conditions.Count; i++)
            if (effects.conditions[i] == effect.conditions[4])
            {
                CheckEffect(caller, effects, i);
                checkEffect = false;
            }
        if (!checkEffect)
            CheckingEffect(caller);
    }
    public void CheckConditionDefeated(MonoBehaviour caller)
    {
        Effects effects = new Effects();
        if (caller.GetComponent<CardCore>())
            effects = caller.GetComponent<CardCore>().card.effects;
        bool checkEffect = true;
        for (int i = 0; i < effects.conditions.Count; i++)
            if (effects.conditions[i] == effect.conditions[5])
            {
                CheckEffect(caller, effects, i);
                checkEffect = false;
            }
        if (!checkEffect)
            CheckingEffect(caller);
    }
    public void CheckConditionDefeatsAnEnemy(MonoBehaviour caller)
    {
        Effects effects = new Effects();
        if (caller.GetComponent<CardCore>())
            effects = caller.GetComponent<CardCore>().card.effects;
        bool checkEffect = true;
        for (int i = 0; i < effects.conditions.Count; i++)
            if (effects.conditions[i] == effect.conditions[6])
            {
                CheckEffect(caller, effects, i);
                checkEffect = false;
            }
        if (!checkEffect)
            CheckingEffect(caller);
    }
    public void CheckConditionAllyIsDefeated(MonoBehaviour caller)
    {
        Effects effects = new Effects();
        if (caller.GetComponent<CardCore>())
            effects = caller.GetComponent<CardCore>().card.effects;
        bool checkEffect = true;
        for (int i = 0; i < effects.conditions.Count; i++)
            if (effects.conditions[i] == effect.conditions[7])
            {
                CheckEffect(caller, effects, i);
                checkEffect = false;
            }
        if (!checkEffect)
            CheckingEffect(caller);
    }
    public void CheckConditionEndOfTurn(MonoBehaviour caller)
    {
        Effects effects = new Effects();
        if (caller.GetComponent<CardCore>())
            effects = caller.GetComponent<CardCore>().card.effects;
        bool checkEffect = true;
        for (int i = 0; i < effects.conditions.Count; i++)
            if (effects.conditions[i] == effect.conditions[8])
            {
                CheckEffect(caller, effects, i);
                checkEffect = false;
            }
        if (!checkEffect)
            CheckingEffect(caller);
    }
    public void CheckConditionGetBuffed(MonoBehaviour caller)
    {
        Effects effects = new Effects();
        if (caller.GetComponent<CardCore>())
            effects = caller.GetComponent<CardCore>().card.effects;
        bool checkEffect = true;
        for (int i = 0; i < effects.conditions.Count; i++)
            if (effects.conditions[i] == effect.conditions[9])
            {
                CheckEffect(caller, effects, i);
                checkEffect = false;
            }
        if (!checkEffect)
            CheckingEffect(caller);
    }
    public void CheckConditionSpellPlayed(MonoBehaviour caller)
    {
        Effects effects = new Effects();
        if (caller.GetComponent<CardCore>())
            effects = caller.GetComponent<CardCore>().card.effects;
        bool checkEffect = true;
        for (int i = 0; i < effects.conditions.Count; i++)
            if (effects.conditions[i] == effect.conditions[10])
            {
                CheckEffect(caller, effects, i);
                checkEffect = false;
            }
        if (!checkEffect)
            CheckingEffect(caller);
    }
    string GetEffect(MonoBehaviour caller)
    {
        string effect = "";
        if (caller.GetComponent<CardCore>())
            effect = caller.GetComponent<CardCore>().card.effect;
        if (caller.GetComponent<Health>())
            effect = caller.GetComponent<Health>().effect;
        return effect;
    }
    void SelectEffects(MonoBehaviour caller, string effect, string condition)
    {
        //if (caller.GetComponent<CardCore>())
        //    checkingEffect = true;
        //List<List<string>> allEffects = new List<List<string>>();
        //List<string> effectDescriptions = new List<string>();
        //List<string> tempEffect = new List<string>();
        //bool startTempEffect = false;
        //for (int i = 0; i < effect.Length; i++)
        //{
        //    if (effect[i].ToString() == ">")
        //    {
        //        if (string.Join("", tempEffect) == "/")
        //        {
        //            Debug.LogError(effectDescriptions.Count);
        //            foreach (string conditions in effectDescriptions)
        //                if (condition == conditions)
        //                {
        //                    effectDescriptions.Remove(conditions);
        //                    CheckEffect(caller, effectDescriptions);
        //                    break;
        //                }
        //            effectDescriptions.Clear();
        //        }
        //        else
        //        if (string.Join("", tempEffect) == "&")
        //        {
        //            bool checkCondition = false;
        //            Debug.LogError(effectDescriptions.Count);
        //            foreach (string conditions in effectDescriptions)
        //                if (condition == conditions)
        //                {
        //                    effectDescriptions.Remove(conditions);
        //                    CheckEffect(caller, effectDescriptions);
        //                    checkCondition = true;
        //                    break;
        //                }
        //            effectDescriptions.Clear();
        //            if (checkCondition)
        //                effectDescriptions.Add(condition);
        //        }
        //        else
        //            effectDescriptions.Add(string.Join("", tempEffect));
        //        tempEffect.Clear();
        //        startTempEffect = false;
        //    }
        //    if (startTempEffect)
        //        tempEffect.Add(effect[i].ToString());
        //    if (effect[i].ToString() == "<")
        //        startTempEffect = true;
        //}
        //foreach (string conditions in effectDescriptions)
        //    if (condition == conditions)
        //    {
        //        effectDescriptions.Remove(conditions);
        //        CheckEffect(caller, effectDescriptions);
        //        return;
        //    }
        //CheckingEffect(caller);
    }
    private void CheckEffect(MonoBehaviour caller, Effects newEffect, int numberEffect)
    {
        bool startTurn = false;
        bool endTurn = false;
        if (effect.extraConditions[1] == newEffect.extraConditions[numberEffect])
            startTurn = true;
        if (effect.extraConditions[2] == newEffect.extraConditions[numberEffect])
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
        for (int j = 0;  j < newEffect.x[numberEffect]; j++)
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
        //foreach (string target in targets)
        //    for (int i = 0; i < 5; i++)
        //        foreach (string effectNew in newEffect)
        //            if (effectNew == target + "_" + i.ToString())
        //                for (int j = 0; j < i; j++)
        //                {
        //                    bool creature = true;
        //                    bool spell = true;
        //                    foreach (string effect in newEffect)
        //                    {
        //                        if (effect == targets[18])
        //                            spell = false;
        //                        if (effect == targets[19])
        //                            creature = false;
        //                    }
        //                    if (target == targets[15])
        //                    {
        //                        if (caller.GetComponent<CardCore>()?.currentPosition.oponent == FindObjectOfType<Enemy>() || caller.GetComponent<Player>())
        //                            _draw.DrawACard(Draw.DeckType.Mana, creature, spell);
        //                        if ((caller.GetComponent<CardCore>()?.currentPosition.oponent == FindObjectOfType<Player>() || caller.GetComponent<Enemy>()) && FindObjectOfType<EnemyAI>().enabled)
        //                            FindObjectOfType<EnemyAI>().DrawACard(null, creature, spell, 0);
        //                    }
        //                    else if (target == targets[16])
        //                    {
        //                        if (caller.GetComponent<CardCore>()?.currentPosition.oponent == FindObjectOfType<Enemy>() || caller.GetComponent<Player>())
        //                            _draw.DrawACard(Draw.DeckType.Blood, creature, spell);
        //                        if ((caller.GetComponent<CardCore>()?.currentPosition.oponent == FindObjectOfType<Player>() || caller.GetComponent<Enemy>()) && FindObjectOfType<EnemyAI>().enabled)
        //                            FindObjectOfType<EnemyAI>().DrawACard(null, creature, spell, 0);
        //                    }
        //                    else if (target == targets[17])
        //                    {
        //                        if (caller.GetComponent<CardCore>()?.currentPosition.oponent == FindObjectOfType<Enemy>() || caller.GetComponent<Player>())
        //                        {
        //                            _draw.DrawACard(Draw.DeckType.Mana, creature, spell);
        //                            _draw.DrawACard(Draw.DeckType.Blood, creature, spell);
        //                        }
        //                        if ((caller.GetComponent<CardCore>()?.currentPosition.oponent == FindObjectOfType<Player>() || caller.GetComponent<Enemy>()) && FindObjectOfType<EnemyAI>().enabled)
        //                        {
        //                            FindObjectOfType<EnemyAI>().DrawACard(null, creature, spell, 0);
        //                            FindObjectOfType<EnemyAI>().DrawACard(null, creature, spell, 0);
        //                        }
        //                    }
        //                }
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
                        StartCoroutine(WaitCardSelect(caller, 1, startTurn, endTurn, newEffect.x[numberEffect], 0, 0, 0, false));
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
                        StartCoroutine(WaitCardSelect(caller, 1, startTurn, endTurn, newEffect.x[numberEffect], 0, 0, 0, false));
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
                            enemyHealth.ReceiveDamage(newEffect.x[numberEffect]);
                        else if (enemysAlive2[selected1].card != null)
                            enemysAlive2[selected1].card.ReceiveDamageEffect(newEffect.x[numberEffect], null, startTurn, endTurn);
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
                            allyHealth.ReceiveDamage(newEffect.x[numberEffect]);
                        else if (allysAlive2[selected2].card != null)
                            allysAlive2[selected2].card.ReceiveDamageEffect(newEffect.x[numberEffect], null, startTurn, endTurn);
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
                            enemysAlive3[selected3].card.ReceiveDamageEffect(newEffect.x[numberEffect], null, startTurn, endTurn);
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
                            allysAlive3[selected4].card.ReceiveDamageEffect(newEffect.x[numberEffect], null, startTurn, endTurn);
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
                                enemysAlive[selected].card.ReceiveDamageEffect(newEffect.x[numberEffect], null, startTurn, endTurn);
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
                                allysAlive[selected].card.ReceiveDamageEffect(newEffect.x[numberEffect], null, startTurn, endTurn);
                        }
                        break;
                    case 9:
                        foreach (MapPosition selected in enemyPositions)
                            if (selected.card != null)
                                selected.card.ReceiveDamageEffect(newEffect.x[numberEffect], null, startTurn, endTurn);
                        break;
                    case 10:
                        foreach (MapPosition selected in allyPositions)
                            if (selected.card != null)
                                selected.card.ReceiveDamageEffect(newEffect.x[numberEffect], null, startTurn, endTurn);
                        break;
                    case 11:
                        if (caller.GetComponent<Card>())
                            caller.GetComponent<Card>().ReceiveDamageEffect(newEffect.x[numberEffect], null, startTurn, endTurn);
                        if (caller.GetComponent<Health>())
                            caller.GetComponent<Health>().ReceiveDamage(newEffect.x[numberEffect]);
                        break;
                    case 12:
                        enemyHealth.ReceiveDamage(newEffect.x[numberEffect]);
                        break;
                    case 13:
                        allyHealth.ReceiveDamage(newEffect.x[numberEffect]);
                        break;
                    case 14:
                        caller.GetComponent<CardCore>()?.currentPosition.positionFacing.card.ReceiveDamageEffect(newEffect.x[numberEffect], null, startTurn, endTurn);
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
                        StartCoroutine(WaitCardSelect(caller, numberOfObjectives1, startTurn, endTurn, newEffect.x[numberEffect], 0, 0, 0, false));
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
                        StartCoroutine(WaitCardSelect(caller, numberOfObjectives2, startTurn, endTurn, newEffect.x[numberEffect], 0, 0, 0, false));
                        Debug.Log("Elige Aliado");
                        return;
                }
        //foreach (string target in targets)
        //    foreach (string effectNew in newEffect)
        //        if (effectNew.Contains(target))
        //        {
        //            MapPosition[] enemyPositions;
        //            MapPosition[] allyPositions;
        //            Health enemyHealth;
        //            Health allyHealth;
        //            if (caller.GetComponent<CardCore>()?.currentPosition.oponent == FindObjectOfType<Player>() || caller.GetComponent<Enemy>())
        //            {
        //                enemyPositions = _table.playerPositions;
        //                allyPositions = _table.enemyFront;
        //                enemyHealth = FindObjectOfType<Player>();
        //                allyHealth = FindObjectOfType<Enemy>();
        //            }
        //            else
        //            {
        //                enemyPositions = _table.enemyFront;
        //                allyPositions = _table.playerPositions;
        //                enemyHealth = FindObjectOfType<Enemy>();
        //                allyHealth = FindObjectOfType<Player>();
        //            }
        //            for (int i = 0; i < 5; i++)
        //                if (effectNew.Contains(target + "_" + i.ToString()))
        //                {
        //                    if (target == targets[13])
        //                        caller.GetComponent<CardCore>()?.currentPosition.positionFacing.card.ReceiveDamageEffect(i, null, startTurn, endTurn);
        //                    else if (target == targets[12])
        //                        allyHealth.ReceiveDamage(i);
        //                    else if (target == targets[11])
        //                        enemyHealth.ReceiveDamage(i);
        //                    else if (target == targets[10])
        //                    {
        //                        if (caller.GetComponent<Card>())
        //                            caller.GetComponent<Card>().ReceiveDamageEffect(i, null, startTurn, endTurn);
        //                        if (caller.GetComponent<Health>())
        //                            caller.GetComponent<Health>().ReceiveDamage(i);
        //                    }
        //                    else if (target == targets[7])
        //                    {
        //                        for (int j = 0; j < 3; j++)
        //                            foreach (string effect in newEffect)
        //                                if (effect.Contains(j.ToString() + "_" + target))
        //                                    for (int k = 0; k < j; k++)
        //                                    {
        //                                        var allysAlive = new List<MapPosition>();
        //                                        foreach (MapPosition enemy in allyPositions)
        //                                            if (enemy.card != null)
        //                                                if (enemy.card.ActualLife > 0)
        //                                                    allysAlive.Add(enemy);
        //                                        var selected = Random.Range(0, allysAlive.Count);
        //                                        if (allysAlive[selected].card != null)
        //                                            allysAlive[selected].card.ReceiveDamageEffect(i, null, startTurn, endTurn);
        //                                    }
        //                    }
        //                    else if (target == targets[6])
        //                    {
        //                        for (int j = 0; j < 3; j++)
        //                            foreach (string effect in newEffect)
        //                                if (effect.Contains(j.ToString() + "_" + target))
        //                                    for (int k = 0; k < j; k++)
        //                                    {
        //                                        var enemysAlive = new List<MapPosition>();
        //                                        foreach (MapPosition enemy in enemyPositions)
        //                                            if (enemy.card != null)
        //                                                if (enemy.card.ActualLife > 0)
        //                                                    enemysAlive.Add(enemy);
        //                                        var selected = Random.Range(0, enemysAlive.Count);
        //                                        if (enemysAlive[selected].card != null)
        //                                            enemysAlive[selected].card.ReceiveDamageEffect(i, null, startTurn, endTurn);
        //                                    }
        //                    }
        //                    else if (target == targets[9])
        //                    {
        //                        foreach (MapPosition selected in allyPositions)
        //                            if (selected.card != null)
        //                                selected.card.ReceiveDamageEffect(i, null, startTurn, endTurn);
        //                    }
        //                    else if (target == targets[8])
        //                    {
        //                        foreach (MapPosition selected in enemyPositions)
        //                            if (selected.card)
        //                                selected.card.ReceiveDamageEffect(i, null, startTurn, endTurn);
        //                    }
        //                    else if (target == targets[5])
        //                    {
        //                        var allysAlive = new List<MapPosition>();
        //                        foreach (MapPosition enemy in allyPositions)
        //                            if (enemy.card != null)
        //                                if (enemy.card.ActualLife > 0)
        //                                    allysAlive.Add(enemy);
        //                        var selected = Random.Range(0, allysAlive.Count);
        //                        if (allysAlive[selected].card != null)
        //                            allysAlive[selected].card.ReceiveDamageEffect(i, null, startTurn, endTurn);
        //                    }
        //                    else if (target == targets[4])
        //                    {
        //                        var enemysAlive = new List<MapPosition>();
        //                        foreach (MapPosition enemy in enemyPositions)
        //                            if (enemy.card != null)
        //                                if (enemy.card.ActualLife > 0)
        //                                    enemysAlive.Add(enemy);
        //                        var selected = Random.Range(0, enemysAlive.Count);
        //                        if (enemysAlive[selected].card != null)
        //                            enemysAlive[selected].card.ReceiveDamageEffect(i, null, startTurn, endTurn);
        //                    }
        //                    else if (target == targets[3])
        //                    {
        //                        var allysAlive = new List<MapPosition>();
        //                        foreach (MapPosition enemy in allyPositions)
        //                            if (enemy.card != null)
        //                                if (enemy.card.ActualLife > 0)
        //                                    allysAlive.Add(enemy);
        //                        var selected = Random.Range(0, allysAlive.Count + 1);
        //                        if (selected == allyPositions.Length)
        //                            allyHealth.ReceiveDamage(i);
        //                        else if (allysAlive[selected].card != null)
        //                            allysAlive[selected].card.ReceiveDamageEffect(i, null, startTurn, endTurn);
        //                    }
        //                    else if (target == targets[2])
        //                    {
        //                        var enemysAlive = new List<MapPosition>();
        //                        foreach (MapPosition enemy in enemyPositions)
        //                            if (enemy.card != null)
        //                                if (enemy.card.ActualLife > 0)
        //                                    enemysAlive.Add(enemy);
        //                        var selected = Random.Range(0, enemysAlive.Count + 1);
        //                        if (selected == enemyPositions.Length)
        //                            enemyHealth.ReceiveDamage(i);
        //                        else if (enemysAlive[selected].card != null)
        //                            enemysAlive[selected].card.ReceiveDamageEffect(i, null, startTurn, endTurn);
        //                    }
        //                    else if (target == targets[20])
        //                    {
        //                        for (int l = 0; l < 3; l++)
        //                            foreach (string effect in newEffect)
        //                                if (effect.Contains(l.ToString() + "_" + target))
        //                                {
        //                                    var enemysAlive = new List<Card>();
        //                                    foreach (MapPosition enemy in enemyPositions)
        //                                        if (enemy.card != null)
        //                                            if (enemy.card.ActualLife > 0)
        //                                                enemysAlive.Add(enemy.card);
        //                                    if (enemysAlive.Count == 0)
        //                                    {
        //                                        CheckingEffect(caller);
        //                                        return;
        //                                    }
        //                                    int numberOfObjectives = l;
        //                                    if (enemysAlive.Count < l)
        //                                        numberOfObjectives = enemysAlive.Count;
        //                                    waitForSelect = true;
        //                                    foreach (Card enemy in enemysAlive)
        //                                    {
        //                                        enemy.waitForSelect = true;
        //                                        enemy.StartCoroutine(enemy.CardSelected());
        //                                    }
        //                                    StartCoroutine(WaitCardSelect(caller, numberOfObjectives, startTurn, endTurn, i, 0, 0, 0, false));
        //                                    Debug.Log("Elige Enemigo");
        //                                    return;
        //                                }
        //                    }
        //                    else if (target == targets[21])
        //                    {
        //                        for (int l = 0; l < 3; l++)
        //                            foreach (string effect in newEffect)
        //                                if (effect.Contains(l.ToString() + "_" + target))
        //                                {
        //                                    var allysAlive = new List<Card>();
        //                                    foreach (MapPosition ally in allyPositions)
        //                                        if (ally.card != null)
        //                                            if (ally.card.ActualLife > 0)
        //                                                allysAlive.Add(ally.card);
        //                                    if (allysAlive.Count == 0)
        //                                    {
        //                                        CheckingEffect(caller);
        //                                        return;
        //                                    }
        //                                    int numberOfObjectives = l;
        //                                    if (allysAlive.Count < l)
        //                                        numberOfObjectives = allysAlive.Count;
        //                                    foreach (Card ally in allysAlive)
        //                                    {
        //                                        ally.waitForSelect = true;
        //                                        ally.StartCoroutine(ally.CardSelected());
        //                                    }
        //                                    StartCoroutine(WaitCardSelect(caller, numberOfObjectives, startTurn, endTurn, i, 0, 0, 0, false));
        //                                    Debug.Log("Elige Aliado");
        //                                    return;
        //                                }
        //                    }
        //                    else if (target == targets[0])
        //                    {
        //                        var enemysAlive = new List<Card>();
        //                        foreach (MapPosition enemy in enemyPositions)
        //                            if (enemy.card != null)
        //                                if (enemy.card.ActualLife > 0)
        //                                    enemysAlive.Add(enemy.card);
        //                        if (enemysAlive.Count == 0)
        //                        {
        //                            CheckingEffect(caller);
        //                            return;
        //                        }
        //                        waitForSelect = true;
        //                        foreach (Card enemy in enemysAlive)
        //                        {
        //                            enemy.waitForSelect = true;
        //                            enemy.StartCoroutine(enemy.CardSelected());
        //                        }
        //                        StartCoroutine(WaitCardSelect(caller, 1, startTurn, endTurn, i, 0, 0, 0, false));
        //                        Debug.Log("Elige Enemigo");
        //                        return;
        //                    }
        //                    else if (target == targets[1])
        //                    {
        //                        var allysAlive = new List<Card>();
        //                        foreach (MapPosition ally in allyPositions)
        //                            if (ally.card != null)
        //                                if (ally.card.ActualLife > 0)
        //                                    allysAlive.Add(ally.card);
        //                        if (allysAlive.Count == 0)
        //                        {
        //                            CheckingEffect(caller);
        //                            return;
        //                        }
        //                        waitForSelect = true;
        //                        foreach (Card ally in allysAlive)
        //                        {
        //                            ally.waitForSelect = true;
        //                            ally.StartCoroutine(ally.CardSelected());
        //                        }
        //                        StartCoroutine(WaitCardSelect(caller, 1, startTurn, endTurn, i, 0, 0, 0, false));
        //                        Debug.Log("Elige Aliado");
        //                        return;
        //                    }
        //                }
        //        }
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
                        StartCoroutine(WaitCardSelect(caller, 1, startTurn, endTurn, 0, newEffect.x[numberEffect], 0, 0, false));
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
                        StartCoroutine(WaitCardSelect(caller, 1, startTurn, endTurn, 0, newEffect.x[numberEffect], 0, 0, false));
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
                            enemyHealth.RestoreHealth(newEffect.x[numberEffect]);
                        else if (enemysAlive2[selected1].card != null)
                            enemysAlive2[selected1].card.HealEffect(newEffect.x[numberEffect], startTurn, endTurn);
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
                            allyHealth.RestoreHealth(newEffect.x[numberEffect]);
                        else if (allysAlive2[selected2].card != null)
                            allysAlive2[selected2].card.HealEffect(newEffect.x[numberEffect], startTurn, endTurn);
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
                            enemysAlive3[selected3].card.HealEffect(newEffect.x[numberEffect], startTurn, endTurn);
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
                            allysAlive3[selected4].card.HealEffect(newEffect.x[numberEffect], startTurn, endTurn);
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
                                enemysAlive[selected].card.HealEffect(newEffect.x[numberEffect], startTurn, endTurn);
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
                                allysAlive[selected].card.HealEffect(newEffect.x[numberEffect], startTurn, endTurn);
                        }
                        break;
                    case 9:
                        foreach (MapPosition selected in enemyPositions)
                            if (selected.card != null)
                                selected.card.HealEffect(newEffect.x[numberEffect], startTurn, endTurn);
                        break;
                    case 10:
                        foreach (MapPosition selected in allyPositions)
                            if (selected.card != null)
                                selected.card.HealEffect(newEffect.x[numberEffect], startTurn, endTurn);
                        break;
                    case 11:
                        if (caller.GetComponent<Card>())
                            caller.GetComponent<Card>().HealEffect(newEffect.x[numberEffect], startTurn, endTurn);
                        if (caller.GetComponent<Health>())
                            caller.GetComponent<Health>().RestoreHealth(newEffect.x[numberEffect]);
                        break;
                    case 12:
                        enemyHealth.RestoreHealth(newEffect.x[numberEffect]);
                        break;
                    case 13:
                        allyHealth.RestoreHealth(newEffect.x[numberEffect]);
                        break;
                    case 14:
                        caller.GetComponent<CardCore>()?.currentPosition.positionFacing.card.HealEffect(newEffect.x[numberEffect], startTurn, endTurn);
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
                        StartCoroutine(WaitCardSelect(caller, numberOfObjectives1, startTurn, endTurn, 0, newEffect.x[numberEffect], 0, 0, false));
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
                        StartCoroutine(WaitCardSelect(caller, numberOfObjectives2, startTurn, endTurn, 0, newEffect.x[numberEffect], 0, 0, false));
                        Debug.Log("Elige Aliado");
                        return;
                }
        //foreach (string target in targets)
        //    foreach (string effectNew in newEffect)
        //        if (effectNew.Contains(target))
        //            for (int i = 0; i < 5; i++)
        //                if (effectNew.Contains(target + "_" + i.ToString()))
        //                {
        //                    MapPosition[] enemyPositions;
        //                    MapPosition[] allyPositions;
        //                    Health enemyHealth;
        //                    Health allyHealth;
        //                    if (caller.GetComponent<CardCore>()?.currentPosition.oponent == FindObjectOfType<Player>() || caller.GetComponent<Enemy>())
        //                    {
        //                        enemyPositions = _table.playerPositions;
        //                        allyPositions = _table.enemyFront;
        //                        enemyHealth = FindObjectOfType<Player>();
        //                        allyHealth = FindObjectOfType<Enemy>();
        //                    }
        //                    else
        //                    {
        //                        enemyPositions = _table.enemyFront;
        //                        allyPositions = _table.playerPositions;
        //                        enemyHealth = FindObjectOfType<Enemy>();
        //                        allyHealth = FindObjectOfType<Player>();
        //                    }
        //                    if (target == targets[13])
        //                        caller.GetComponent<CardCore>()?.currentPosition.positionFacing.card.HealEffect(i, startTurn, endTurn);
        //                    else if (target == targets[12])
        //                        allyHealth.RestoreHealth(i);
        //                    else if (target == targets[11])
        //                        enemyHealth.RestoreHealth(i);
        //                    else if (target == targets[10])
        //                    {
        //                        if (caller.GetComponent<Card>())
        //                            caller.GetComponent<Card>().HealEffect(i, startTurn, endTurn);
        //                        if (caller.GetComponent<Health>())
        //                            caller.GetComponent<Health>().RestoreHealth(i);
        //                    }
        //                    else if (target == targets[7])
        //                    {
        //                        for (int j = 0; j < 3; j++)
        //                            foreach (string effect in newEffect)
        //                                if (effect.Contains(j.ToString() + "_" + target))
        //                                    for (int k = 0; k < j; k++)
        //                                    {
        //                                        var allysToHeal = new List<MapPosition>();
        //                                        foreach (MapPosition ally in allyPositions)
        //                                            if (ally.card != null)
        //                                                if (ally.card.ActualLife > 0 && ally.card.ActualLife < ally.card.card.life)
        //                                                    allysToHeal.Add(ally);
        //                                        int selected = 0;
        //                                        if (allysToHeal.Count > 0)
        //                                            selected = Random.Range(0, allysToHeal.Count);
        //                                        if (allysToHeal[selected].card != null)
        //                                            allysToHeal[selected].card.HealEffect(i, startTurn, endTurn);
        //                                    }
        //                    }
        //                    else if (target == targets[6])
        //                    {
        //                        for (int j = 0; j < 3; j++)
        //                            foreach (string effect in newEffect)
        //                                if (effect.Contains(j.ToString() + "_" + target))
        //                                    for (int k = 0; k < j; k++)
        //                                    {
        //                                        var enemysToHeal = new List<MapPosition>();
        //                                        foreach (MapPosition ally in enemyPositions)
        //                                            if (ally.card != null)
        //                                                if (ally.card.ActualLife > 0 && ally.card.ActualLife < ally.card.card.life)
        //                                                    enemysToHeal.Add(ally);
        //                                        int selected = 0;
        //                                        if (enemysToHeal.Count > 0)
        //                                            selected = Random.Range(0, enemysToHeal.Count);
        //                                        if (enemysToHeal[selected].card != null)
        //                                            enemysToHeal[selected].card.HealEffect(i, startTurn, endTurn);
        //                                    }
        //                    }
        //                    else if (target == targets[9])
        //                    {
        //                        foreach (MapPosition selected in allyPositions)
        //                            if (selected.card != null)
        //                                selected.card.HealEffect(i, startTurn, endTurn);
        //                    }
        //                    else if (target == targets[8])
        //                    {
        //                        foreach (MapPosition selected in enemyPositions)
        //                            if (selected.card)
        //                                selected.card.HealEffect(i, startTurn, endTurn);
        //                    }
        //                    else if (target == targets[5])
        //                    {
        //                        var allysToHeal = new List<MapPosition>();
        //                        foreach (MapPosition ally in allyPositions)
        //                            if (ally.card != null)
        //                                if (ally.card.ActualLife > 0 && ally.card.ActualLife < ally.card.card.life)
        //                                    allysToHeal.Add(ally);
        //                        int selected = 0;
        //                        if (allysToHeal.Count > 0)
        //                            selected = Random.Range(0, allysToHeal.Count);
        //                        if (allysToHeal[selected].card != null)
        //                            allysToHeal[selected].card.HealEffect(i, startTurn, endTurn);
        //                    }
        //                    else if (target == targets[4])
        //                    {
        //                        var enemysToHeal = new List<MapPosition>();
        //                        foreach (MapPosition ally in enemyPositions)
        //                            if (ally.card != null)
        //                                if (ally.card.ActualLife > 0 && ally.card.ActualLife < ally.card.card.life)
        //                                    enemysToHeal.Add(ally);
        //                        int selected = 0;
        //                        if (enemysToHeal.Count > 0)
        //                            selected = Random.Range(0, enemysToHeal.Count);
        //                        if (enemysToHeal[selected].card != null)
        //                            enemysToHeal[selected].card.HealEffect(i, startTurn, endTurn);
        //                    }
        //                    else if (target == targets[3])
        //                    {
        //                        var allysToHeal = new List<MapPosition>();
        //                        foreach (MapPosition ally in allyPositions)
        //                            if (ally.card != null)
        //                                if (ally.card.ActualLife > 0 && ally.card.ActualLife < ally.card.card.life)
        //                                    allysToHeal.Add(ally);
        //                        int selected = 0;
        //                        if (allysToHeal.Count > 0)
        //                            selected = Random.Range(0, allysToHeal.Count);
        //                        if (selected == allyPositions.Length)
        //                            allyHealth.RestoreHealth(i);
        //                        else if (allysToHeal[selected].card != null)
        //                            allysToHeal[selected].card.HealEffect(i, startTurn, endTurn);
        //                    }
        //                    else if (target == targets[2])
        //                    {
        //                        var enemysToHeal = new List<MapPosition>();
        //                        foreach (MapPosition ally in enemyPositions)
        //                            if (ally.card != null)
        //                                if (ally.card.ActualLife > 0 && ally.card.ActualLife < ally.card.card.life)
        //                                    enemysToHeal.Add(ally);
        //                        int selected = 0;
        //                        if (enemysToHeal.Count > 0)
        //                            selected = Random.Range(0, enemysToHeal.Count);
        //                        if (selected == enemyPositions.Length)
        //                            enemyHealth.RestoreHealth(i);
        //                        else if (enemysToHeal[selected].card != null)
        //                            enemysToHeal[selected].card.HealEffect(i, startTurn, endTurn);
        //                    }
        //                    else if (target == targets[20])
        //                    {
        //                        for (int l = 0; l < 3; l++)
        //                            foreach (string effect in newEffect)
        //                                if (effect.Contains(l.ToString() + "_" + target))
        //                                {
        //                                    var enemysAlive = new List<Card>();
        //                                    foreach (MapPosition enemy in enemyPositions)
        //                                        if (enemy.card != null)
        //                                            if (enemy.card.ActualLife > 0)
        //                                                enemysAlive.Add(enemy.card);
        //                                    if (enemysAlive.Count == 0)
        //                                    {
        //                                        CheckingEffect(caller);
        //                                        return;
        //                                    }
        //                                    int numberOfObjectives = l;
        //                                    if (enemysAlive.Count < l)
        //                                        numberOfObjectives = enemysAlive.Count;
        //                                    waitForSelect = true;
        //                                    foreach (Card enemy in enemysAlive)
        //                                    {
        //                                        enemy.waitForSelect = true;
        //                                        enemy.StartCoroutine(enemy.CardSelected());
        //                                    }
        //                                    StartCoroutine(WaitCardSelect(caller, numberOfObjectives, startTurn, endTurn, 0, i, 0, 0, false));
        //                                    Debug.Log("Elige Enemigo");
        //                                    return;
        //                                }
        //                    }
        //                    else if (target == targets[21])
        //                    {
        //                        for (int l = 0; l < 3; l++)
        //                            foreach (string effect in newEffect)
        //                                if (effect.Contains(l.ToString() + "_" + target))
        //                                {
        //                                    var allysAlive = new List<Card>();
        //                                    foreach (MapPosition ally in allyPositions)
        //                                        if (ally.card != null)
        //                                            if (ally.card.ActualLife > 0)
        //                                                allysAlive.Add(ally.card);
        //                                    if (allysAlive.Count == 0)
        //                                    {
        //                                        CheckingEffect(caller);
        //                                        return;
        //                                    }
        //                                    int numberOfObjectives = l;
        //                                    if (allysAlive.Count < l)
        //                                        numberOfObjectives = allysAlive.Count;
        //                                    waitForSelect = true;
        //                                    foreach (Card ally in allysAlive)
        //                                    {
        //                                        ally.waitForSelect = true;
        //                                        ally.StartCoroutine(ally.CardSelected());
        //                                    }
        //                                    StartCoroutine(WaitCardSelect(caller, numberOfObjectives, startTurn, endTurn, 0, i, 0, 0, false));
        //                                    Debug.Log("Elige Aliado");
        //                                    return;
        //                                }
        //                    }
        //                    else if (target == targets[0])
        //                    {
        //                        var enemysAlive = new List<Card>();
        //                        foreach (MapPosition enemy in enemyPositions)
        //                            if (enemy.card != null)
        //                                if (enemy.card.ActualLife > 0)
        //                                    enemysAlive.Add(enemy.card);
        //                        if (enemysAlive.Count == 0)
        //                        {
        //                            CheckingEffect(caller);
        //                            return;
        //                        }
        //                        waitForSelect = true;
        //                        foreach (Card enemy in enemysAlive)
        //                        {
        //                            enemy.waitForSelect = true;
        //                            enemy.StartCoroutine(enemy.CardSelected());
        //                        }
        //                        StartCoroutine(WaitCardSelect(caller, 1, startTurn, endTurn, 0, i, 0, 0, false));
        //                        Debug.Log("Elige Enemigo");
        //                        return;
        //                    }
        //                    else if (target == targets[1])
        //                    {
        //                        var allysAlive = new List<Card>();
        //                        foreach (MapPosition ally in allyPositions)
        //                            if (ally.card != null)
        //                                if (ally.card.ActualLife > 0)
        //                                    allysAlive.Add(ally.card);
        //                        if (allysAlive.Count == 0)
        //                        {
        //                            CheckingEffect(caller);
        //                            return;
        //                        }
        //                        waitForSelect = true;
        //                        foreach (Card ally in allysAlive)
        //                        {
        //                            ally.waitForSelect = true;
        //                            ally.StartCoroutine(ally.CardSelected());
        //                        }
        //                        StartCoroutine(WaitCardSelect(caller, 1, startTurn, endTurn, 0, i, 0, 0, false));
        //                        Debug.Log("Elige Aliado");
        //                        return;
        //                    }
        //                }
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
                        for (int j = 0; j < newEffect.x[numberEffect] && j < cardsToAdd1.Count; j++)
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
                        for (int j = 0; j < newEffect.x[numberEffect] && j < cardsToAdd2.Count; j++)
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
                        for (int j = 0; j < newEffect.x[numberEffect] && j < cardsToAdd3.Count; j++)
                        {
                            if (player)
                                _draw.AddATempCard(cardsToAdd3[j]);
                            else if (FindObjectOfType<EnemyAI>())
                                FindObjectOfType<EnemyAI>().hand.Add(cardsToAdd3[j]);
                        }
                        break;
                }
        //bool player = true;
        //if ((caller.GetComponent<CardCore>()?.currentPosition.oponent == FindObjectOfType<Player>() || caller.GetComponent<EnemyAI>()) && FindObjectOfType<EnemyAI>().enabled)
        //    player = false;
        //bool added = false;
        //foreach (string effectNew in newEffect)
        //{
        //    if (effectNew == targets[14])
        //    {
        //        List<Cards> listOfCards = new List<Cards>();
        //        List<Cards> cardsToAdd = new List<Cards>();
        //        foreach (string effect in newEffect)
        //        {
        //            if (effect == targets[18])
        //            {
        //                foreach (Cards cardToList in cards)
        //                    if (!cardToList.spell)
        //                        listOfCards.Add(cardToList);
        //                cardsToAdd.Add(listOfCards[Random.Range(0, listOfCards.Count)]);
        //            }
        //            else if (effect == targets[19])
        //            {
        //                foreach (Cards cardToList in cards)
        //                    if (cardToList.spell)
        //                        listOfCards.Add(cardToList);
        //                cardsToAdd.Add(listOfCards[Random.Range(0, listOfCards.Count)]);
        //            }
        //            else
        //            {
        //                listOfCards = cards;
        //                foreach (Cards cards in listOfCards)
        //                    foreach (string effect2 in newEffect)
        //                        if (effect2 == cards.name)
        //                            cardsToAdd.Add(cards);
        //            }
        //        }
        //        if (cardsToAdd.Count > 0)
        //        {
        //            var cards = cardsToAdd[Random.Range(0, cardsToAdd.Count)];
        //            for (int i = 0; i < 5; i++)
        //                foreach (string effect in newEffect)
        //                    if (effect == targets[14] + "_" + i.ToString())
        //                    {
        //                        for (int j = 0; j < i; j++)
        //                        {
        //                            if (player)
        //                            {
        //                                var addCard = Instantiate(newCard, _draw.transform);
        //                                addCard.card = cards;
        //                                addCard.GetComponent<Card>().SetData();
        //                                _draw.AddCardToHand(addCard);
        //                            } else if (FindObjectOfType<EnemyAI>())
        //                                FindObjectOfType<EnemyAI>().hand.Add(cards);
        //                        }
        //                        added = true;
        //                    }
        //            if (!added)
        //            {
        //                if (player)
        //                {
        //                    var addCard = Instantiate(newCard, _draw.transform);
        //                    addCard.card = cards;
        //                    addCard.GetComponent<Card>().SetData();
        //                    _draw.AddCardToHand(addCard);
        //                }
        //                else if (FindObjectOfType<EnemyAI>())
        //                    FindObjectOfType<EnemyAI>().hand.Add(cards);
        //            }
        //        }
        //    }
        //    else if (effectNew == targets[15])
        //    {
        //        List<Cards> listOfCards = new List<Cards>();
        //        List<Cards> cardsToAdd = new List<Cards>();
        //        foreach (string effect in newEffect)
        //        {
        //            if (effect == targets[18])
        //            {
        //                foreach (Cards cardToList in cards)
        //                    if (!cardToList.spell)
        //                        listOfCards.Add(cardToList);
        //                cardsToAdd.Add(listOfCards[Random.Range(0, listOfCards.Count)]);
        //            }
        //            else if (effect == targets[19])
        //            {
        //                foreach (Cards cardToList in cards)
        //                    if (cardToList.spell)
        //                        listOfCards.Add(cardToList);
        //                cardsToAdd.Add(listOfCards[Random.Range(0, listOfCards.Count)]);
        //            }
        //            else
        //            {
        //                listOfCards = cards;
        //                foreach (Cards cards in listOfCards)
        //                    foreach (string effect2 in newEffect)
        //                        if (effect2 == cards.name)
        //                            cardsToAdd.Add(cards);
        //            }
        //        }
        //        if (cardsToAdd.Count > 0)
        //        {
        //            var cards = cardsToAdd[Random.Range(0, cardsToAdd.Count)];
        //            for (int i = 0; i < 50; i++)
        //                foreach (string effect in newEffect)
        //                    if (effect == targets[15] + "_" + i.ToString())
        //                    {
        //                        for (int j = 0; j < i; j++)
        //                        {
        //                            if (player)
        //                                _draw.AddATempCard(cards);
        //                            else if (FindObjectOfType<EnemyAI>())
        //                                FindObjectOfType<EnemyAI>().hand.Add(cards);
        //                        }
        //                        added = true;
        //                    }
        //            if (!added)
        //            {
        //                if (player)
        //                    _draw.AddATempCard(cards);
        //                else if (FindObjectOfType<EnemyAI>())
        //                    FindObjectOfType<EnemyAI>().hand.Add(cards);
        //            }
        //        }
        //    }
        //    else if (effectNew == targets[16])
        //    {
        //        List<Cards> listOfCards = new List<Cards>();
        //        List<Cards> cardsToAdd = new List<Cards>();
        //        foreach (string effect in newEffect)
        //        {
        //            if (effect == targets[18])
        //            {
        //                foreach (Cards cardToList in cards)
        //                    if (!cardToList.spell)
        //                        listOfCards.Add(cardToList);
        //                cardsToAdd.Add(listOfCards[Random.Range(0, listOfCards.Count)]);
        //            }
        //            else if (effect == targets[19])
        //            {
        //                foreach (Cards cardToList in cards)
        //                    if (cardToList.spell)
        //                        listOfCards.Add(cardToList);
        //                cardsToAdd.Add(listOfCards[Random.Range(0, listOfCards.Count)]);
        //            }
        //            else
        //            {
        //                listOfCards = cards;
        //                foreach (Cards cards in listOfCards)
        //                    foreach (string effect2 in newEffect)
        //                        if (effect2 == cards.name)
        //                            cardsToAdd.Add(cards);
        //            }
        //        }
        //        if (cardsToAdd.Count > 0)
        //        {
        //            var cards = cardsToAdd[Random.Range(0, cardsToAdd.Count)];
        //            for (int i = 0; i < 50; i++)
        //                foreach (string effect in newEffect)
        //                    if (effect == targets[16] + "_" + i.ToString())
        //                    {
        //                        for (int j = 0; j < i; j++)
        //                        {
        //                            if (player)
        //                                _draw.AddATempCard(cards);
        //                            else if (FindObjectOfType<EnemyAI>())
        //                                FindObjectOfType<EnemyAI>().hand.Add(cards);
        //                        }
        //                        added = true;
        //                    }
        //            if (!added)
        //            {
        //                if (player)
        //                    _draw.AddATempCard(cards);
        //                else if (FindObjectOfType<EnemyAI>())
        //                    FindObjectOfType<EnemyAI>().hand.Add(cards);
        //            }
        //        }
        //    }
        //}
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
                            newEffect.x[numberEffect], newEffect.y[numberEffect], false));
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
                            newEffect.x[numberEffect], newEffect.y[numberEffect], false));
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
                            enemysAlive3[selected3].card.BuffEffect(newEffect.x[numberEffect], newEffect.y[numberEffect], startTurn, endTurn);
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
                            allysAlive3[selected4].card.BuffEffect(newEffect.x[numberEffect], newEffect.y[numberEffect], startTurn, endTurn);
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
                                enemysAlive[selected].card.BuffEffect(newEffect.x[numberEffect], newEffect.y[numberEffect], startTurn, endTurn);
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
                                allysAlive[selected].card.BuffEffect(newEffect.x[numberEffect], newEffect.y[numberEffect], startTurn, endTurn);
                        }
                        break;
                    case 9:
                        foreach (MapPosition selected in enemyPositions)
                            if (selected.card != null)
                                selected.card.BuffEffect(newEffect.x[numberEffect], newEffect.y[numberEffect], startTurn, endTurn);
                        break;
                    case 10:
                        foreach (MapPosition selected in allyPositions)
                            if (selected.card != null)
                                selected.card.BuffEffect(newEffect.x[numberEffect], newEffect.y[numberEffect], startTurn, endTurn);
                        break;
                    case 11:
                        if (caller.GetComponent<Card>())
                            caller.GetComponent<Card>().BuffEffect(newEffect.x[numberEffect], newEffect.y[numberEffect], startTurn, endTurn);
                        break;
                    case 14:
                        caller.GetComponent<CardCore>()?.currentPosition.positionFacing.card.BuffEffect(newEffect.x[numberEffect],
                            newEffect.y[numberEffect], startTurn, endTurn);
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
                            newEffect.x[numberEffect], newEffect.y[numberEffect], false));
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
                            newEffect.x[numberEffect], newEffect.y[numberEffect], false));
                        Debug.Log("Elige Aliado");
                        return;
                }
        //foreach (string target in targets)
        //    foreach (string effectNew in newEffect)
        //        if (effectNew.Contains(target))
        //            for (int i = 0; i < 5; i++)
        //                for (int j = 0; j < 5; j++)
        //                {
        //                    int attack = i;
        //                    int life = j;
        //                    bool mustContinue = true;
        //                    foreach (string effect in newEffect)
        //                    {
        //                        if (effect.Contains(target + "_+" + i.ToString() + "/+" + j.ToString()))
        //                        {
        //                            attack *= 1;
        //                            life *= 1;
        //                            mustContinue = false;
        //                        }
        //                        else if (effect.Contains(target + "_-" + i.ToString() + "/-" + j.ToString()))
        //                        {
        //                            attack *= -1;
        //                            life *= -1;
        //                            mustContinue = false;
        //                        }
        //                        else if (effect.Contains(target + "_+" + i.ToString() + "/-" + j.ToString()))
        //                        {
        //                            attack *= 1;
        //                            life *= -1;
        //                            mustContinue = false;
        //                        }
        //                        else if (effect.Contains(target + "_-" + i.ToString() + "/+" + j.ToString()))
        //                        {
        //                            attack *= -1;
        //                            life *= 1;
        //                            mustContinue = false;
        //                        }
        //                    }
        //                    if (mustContinue)
        //                        continue;
        //                    MapPosition[] enemyPositions;
        //                    MapPosition[] allyPositions;
        //                    if (caller.GetComponent<CardCore>()?.currentPosition.oponent == FindObjectOfType<Player>() || caller.GetComponent<Enemy>())
        //                    {
        //                        enemyPositions = _table.playerPositions;
        //                        allyPositions = _table.enemyFront;
        //                    }
        //                    else
        //                    {
        //                        enemyPositions = _table.enemyFront;
        //                        allyPositions = _table.playerPositions;
        //                    }
        //                    if (target == targets[13])
        //                    {
        //                        if (caller.GetComponent<CardCore>())
        //                            if (caller.GetComponent<CardCore>().currentPosition.positionFacing.card != null)
        //                                caller.GetComponent<CardCore>().currentPosition.positionFacing.card.BuffEffect(attack, life, startTurn, endTurn);
        //                    }
        //                    else if (target == targets[10] && caller.GetComponent<Card>())
        //                        caller.GetComponent<Card>().BuffEffect(attack, life, startTurn, endTurn);
        //                    else if (target == targets[7])
        //                    {
        //                        for (int l = 0; l < 3; l++)
        //                            foreach (string effect in newEffect)
        //                                if (effect.Contains(l.ToString() + "_" + target))
        //                                    for (int k = 0; k < l; k++)
        //                                    {
        //                                        var creatureToBuff = new List<MapPosition>();
        //                                        foreach (MapPosition creature in allyPositions)
        //                                            if (creature.card != null)
        //                                                if (creature.card.ActualLife > 0)
        //                                                    creatureToBuff.Add(creature);
        //                                        var selected = Random.Range(0, creatureToBuff.Count);
        //                                        if (creatureToBuff[selected].card != null)
        //                                            creatureToBuff[selected].card.BuffEffect(attack, life, startTurn, endTurn);
        //                                    }
        //                    }
        //                    else if (target == targets[6])
        //                    {
        //                        for (int l = 0; l < 3; l++)
        //                            foreach (string effect in newEffect)
        //                                if (effect.Contains(l.ToString() + "_" + target))
        //                                    for (int k = 0; k < l; k++)
        //                                    {
        //                                        var creatureToBuff = new List<MapPosition>();
        //                                        foreach (MapPosition creature in enemyPositions)
        //                                            if (creature.card != null)
        //                                                if (creature.card.ActualLife > 0)
        //                                                    creatureToBuff.Add(creature);
        //                                        var selected = Random.Range(0, creatureToBuff.Count);
        //                                        if (creatureToBuff[selected].card != null)
        //                                            creatureToBuff[selected].card.BuffEffect(attack, life, startTurn, endTurn);
        //                                    }
        //                    }
        //                    else if (target == targets[9])
        //                    {
        //                        foreach (MapPosition selected in allyPositions)
        //                            if (selected.card != null)
        //                                selected.card.BuffEffect(attack, life, startTurn, endTurn);
        //                    }
        //                    else if (target == targets[8])
        //                    {
        //                        foreach (MapPosition selected in enemyPositions)
        //                            if (selected.card != null)
        //                                selected.card.BuffEffect(attack, life, startTurn, endTurn);
        //                    }
        //                    else if (target == targets[5])
        //                    {
        //                        var creatureToBuff = new List<MapPosition>();
        //                        foreach (MapPosition creature in allyPositions)
        //                            if (creature.card != null)
        //                                if (creature.card.ActualLife > 0)
        //                                    creatureToBuff.Add(creature);
        //                        var selected = Random.Range(0, creatureToBuff.Count);
        //                        if(creatureToBuff.Count > 0)
        //                        if (creatureToBuff[selected].card != null)
        //                            creatureToBuff[selected].card.BuffEffect(attack, life, startTurn, endTurn);
        //                    }
        //                    else if (target == targets[4])
        //                    {
        //                        var creatureToBuff = new List<MapPosition>();
        //                        foreach (MapPosition creature in enemyPositions)
        //                            if (creature.card != null)
        //                                if (creature.card.ActualLife > 0)
        //                                    creatureToBuff.Add(creature);
        //                        var selected = Random.Range(0, creatureToBuff.Count);
        //                        if (creatureToBuff[selected].card != null)
        //                            creatureToBuff[selected].card.BuffEffect(attack, life, startTurn, endTurn);
        //                    }
        //                    else if (target == targets[20])
        //                    {
        //                        for (int l = 0; l < 3; l++)
        //                            foreach (string effect in newEffect)
        //                                if (effect.Contains(l.ToString() + "_" + target))
        //                                {
        //                                    var enemysAlive = new List<Card>();
        //                                    foreach (MapPosition enemy in enemyPositions)
        //                                        if (enemy.card != null)
        //                                            if (enemy.card.ActualLife > 0)
        //                                                enemysAlive.Add(enemy.card);
        //                                    if (enemysAlive.Count == 0)
        //                                    {
        //                                        CheckingEffect(caller);
        //                                        return;
        //                                    }
        //                                    int numberOfObjectives = l;
        //                                    if (enemysAlive.Count < l)
        //                                        numberOfObjectives = enemysAlive.Count;
        //                                    waitForSelect = true;
        //                                    foreach (Card enemy in enemysAlive)
        //                                    {
        //                                        enemy.waitForSelect = true;
        //                                        enemy.StartCoroutine(enemy.CardSelected());
        //                                    }
        //                                    StartCoroutine(WaitCardSelect(caller, numberOfObjectives, startTurn, endTurn, 0, 0, attack, life, false));
        //                                    Debug.Log("Elige Enemigo");
        //                                    return;
        //                                }
        //                    }
        //                    else if (target == targets[21])
        //                    {
        //                        for (int l = 0; l < 3; l++)
        //                            foreach (string effect in newEffect)
        //                                if (effect.Contains(l.ToString() + "_" + target))
        //                                {
        //                                    var allysAlive = new List<Card>();
        //                                    foreach (MapPosition ally in allyPositions)
        //                                        if (ally.card != null)
        //                                            if (ally.card.ActualLife > 0)
        //                                                allysAlive.Add(ally.card);
        //                                    if (allysAlive.Count == 0)
        //                                    {
        //                                        CheckingEffect(caller);
        //                                        return;
        //                                    }
        //                                    int numberOfObjectives = l;
        //                                    if (allysAlive.Count < l)
        //                                        numberOfObjectives = allysAlive.Count;
        //                                    waitForSelect = true;
        //                                    foreach (Card ally in allysAlive)
        //                                    {
        //                                        ally.waitForSelect = true;
        //                                        ally.StartCoroutine(ally.CardSelected());
        //                                    }
        //                                    StartCoroutine(WaitCardSelect(caller, numberOfObjectives, startTurn, endTurn, 0, 0, attack, life, false));
        //                                    Debug.Log("Elige Aliado");
        //                                    return;
        //                                }
        //                    }
        //                    else if (target == targets[0])
        //                    {
        //                        var enemysAlive = new List<Card>();
        //                        foreach (MapPosition enemy in enemyPositions)
        //                            if (enemy.card != null)
        //                                if (enemy.card.ActualLife > 0)
        //                                    enemysAlive.Add(enemy.card);
        //                        if (enemysAlive.Count == 0)
        //                        {
        //                            CheckingEffect(caller);
        //                            return;
        //                        }
        //                        waitForSelect = true;
        //                        foreach (Card enemy in enemysAlive)
        //                        {
        //                            enemy.waitForSelect = true;
        //                            enemy.StartCoroutine(enemy.CardSelected());
        //                        }
        //                        StartCoroutine(WaitCardSelect(caller, 1, startTurn, endTurn, 0, 0, attack, life, false));
        //                        Debug.Log("Elige Enemigo");
        //                        return;
        //                    }
        //                    else if (target == targets[1])
        //                    {
        //                        var allysAlive = new List<Card>();
        //                        foreach (MapPosition ally in allyPositions)
        //                            if (ally.card != null)
        //                                if (ally.card.ActualLife > 0)
        //                                    allysAlive.Add(ally.card);
        //                        if (allysAlive.Count == 0)
        //                        {
        //                            CheckingEffect(caller);
        //                            return;
        //                        }
        //                        waitForSelect = true;
        //                        foreach (Card ally in allysAlive)
        //                        {
        //                            ally.waitForSelect = true;
        //                            ally.StartCoroutine(ally.CardSelected());
        //                        }
        //                        StartCoroutine(WaitCardSelect(caller, 1, startTurn, endTurn, 0, 0, attack, life, false));
        //                        Debug.Log("Elige Aliado");
        //                        return;
        //                    }
        //                }
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
        //foreach (string target in targets)
        //    foreach (string effectNew in newEffect)
        //        if (effectNew.Contains(target))
        //        {
        //            MapPosition[] enemyPositions;
        //            MapPosition[] allyPositions;
        //            if (caller.GetComponent<CardCore>()?.currentPosition.oponent == FindObjectOfType<Player>() || caller.GetComponent<Enemy>())
        //            {
        //                enemyPositions = _table.playerPositions;
        //                allyPositions = _table.enemyFront;
        //            }
        //            else
        //            {
        //                enemyPositions = _table.enemyFront;
        //                allyPositions = _table.playerPositions;
        //            }
        //            if (target == targets[13])
        //                caller.GetComponent<CardCore>()?.currentPosition.positionFacing.card.ImmuneEffect(startTurn, endTurn);
        //            else if (target == targets[10] && caller.GetComponent<Card>())
        //                caller.GetComponent<Card>().ImmuneEffect(startTurn, endTurn);
        //            else if (target == targets[7])
        //            {
        //                for (int j = 0; j < 3; j++)
        //                    foreach (string effect in newEffect)
        //                        if (effect.Contains(j.ToString() + "_" + target))
        //                            for (int k = 0; k < j; k++)
        //                            {
        //                                var selected = Random.Range(0, allyPositions.Length - 1);
        //                                if (_table.playerPositions[selected].card != null)
        //                                    _table.playerPositions[selected].card.ImmuneEffect(startTurn, endTurn);
        //                            }
        //            }
        //            else if (target == targets[6])
        //            {
        //                for (int j = 0; j < 3; j++)
        //                    foreach (string effect in newEffect)
        //                        if (effect.Contains(j.ToString() + "_" + target))
        //                            for (int k = 0; k < j; k++)
        //                            {
        //                                var selected = Random.Range(0, enemyPositions.Length - 1);
        //                                if (_table.enemyFront[selected].card != null)
        //                                    _table.enemyFront[selected].card.ImmuneEffect(startTurn, endTurn);
        //                            }
        //            }
        //            else if (target == targets[9])
        //            {
        //                foreach (MapPosition selected in allyPositions)
        //                    if (selected.card != null)
        //                        selected.card.ImmuneEffect(startTurn, endTurn);
        //            }
        //            else if (target == targets[8])
        //            {
        //                foreach (MapPosition selected in enemyPositions)
        //                    if (selected.card != null)
        //                        selected.card.ImmuneEffect(startTurn, endTurn);
        //            }
        //            else if (target == targets[5])
        //            {
        //                var selected = Random.Range(0, allyPositions.Length - 1);
        //                if (_table.playerPositions[selected].card != null)
        //                    _table.playerPositions[selected].card.ImmuneEffect(startTurn, endTurn);
        //            }
        //            else if (target == targets[4])
        //            {
        //                var selected = Random.Range(0, enemyPositions.Length - 1);
        //                if (_table.enemyFront[selected].card != null)
        //                    _table.enemyFront[selected].card.ImmuneEffect(startTurn, endTurn);
        //            }
        //            else if (target == targets[20])
        //            {
        //                for (int l = 0; l < 3; l++)
        //                    foreach (string effect in newEffect)
        //                        if (effect.Contains(l.ToString() + "_" + target))
        //                        {
        //                            var enemysAlive = new List<Card>();
        //                            foreach (MapPosition enemy in enemyPositions)
        //                                if (enemy.card != null)
        //                                    if (enemy.card.ActualLife > 0)
        //                                        enemysAlive.Add(enemy.card);
        //                            if (enemysAlive.Count == 0)
        //                            {
        //                                CheckingEffect(caller);
        //                                return;
        //                            }
        //                            int numberOfObjectives = l;
        //                            if (enemysAlive.Count < l)
        //                                numberOfObjectives = enemysAlive.Count;
        //                            waitForSelect = true;
        //                            foreach (Card enemy in enemysAlive)
        //                            {
        //                                enemy.waitForSelect = true;
        //                                enemy.StartCoroutine(enemy.CardSelected());
        //                            }
        //                            StartCoroutine(WaitCardSelect(caller, numberOfObjectives, startTurn, endTurn, 0, 0, 0, 0, true));
        //                            Debug.Log("Elige Enemigo");
        //                            return;
        //                        }
        //            }
        //            else if (target == targets[21])
        //            {
        //                for (int l = 0; l < 3; l++)
        //                    foreach (string effect in newEffect)
        //                        if (effect.Contains(l.ToString() + "_" + target))
        //                        {
        //                            var allysAlive = new List<Card>();
        //                            foreach (MapPosition ally in allyPositions)
        //                                if (ally.card != null)
        //                                    if (ally.card.ActualLife > 0)
        //                                        allysAlive.Add(ally.card);
        //                            if (allysAlive.Count == 0)
        //                            {
        //                                CheckingEffect(caller);
        //                                return;
        //                            }
        //                            int numberOfObjectives = l;
        //                            if (allysAlive.Count < l)
        //                                numberOfObjectives = allysAlive.Count;
        //                            waitForSelect = true;
        //                            foreach (Card ally in allysAlive)
        //                            {
        //                                ally.waitForSelect = true;
        //                                ally.StartCoroutine(ally.CardSelected());
        //                            }
        //                            StartCoroutine(WaitCardSelect(caller, numberOfObjectives, startTurn, endTurn, 0, 0, 0, 0, true));
        //                            Debug.Log("Elige Aliado");
        //                            return;
        //                        }
        //            }
        //            else if (target == targets[0])
        //            {
        //                var enemysAlive = new List<Card>();
        //                foreach (MapPosition enemy in enemyPositions)
        //                    if (enemy.card != null)
        //                        if (enemy.card.ActualLife > 0)
        //                            enemysAlive.Add(enemy.card);
        //                if (enemysAlive.Count == 0)
        //                {
        //                    CheckingEffect(caller);
        //                    return;
        //                }
        //                waitForSelect = true;
        //                foreach (Card enemy in enemysAlive)
        //                {
        //                    enemy.waitForSelect = true;
        //                    enemy.StartCoroutine(enemy.CardSelected());
        //                }
        //                StartCoroutine(WaitCardSelect(caller, 1, startTurn, endTurn, 0, 0, 0, 0, true));
        //                Debug.Log("Elige Enemigo");
        //                return;
        //            }
        //            else if (target == targets[1])
        //            {
        //                var allysAlive = new List<Card>();
        //                foreach (MapPosition ally in allyPositions)
        //                    if (ally.card != null)
        //                        if (ally.card.ActualLife > 0)
        //                            allysAlive.Add(ally.card);
        //                if (allysAlive.Count == 0)
        //                {
        //                    CheckingEffect(caller);
        //                    return;
        //                }
        //                waitForSelect = true;
        //                foreach (Card ally in allysAlive)
        //                {
        //                    ally.waitForSelect = true;
        //                    ally.StartCoroutine(ally.CardSelected());
        //                }
        //                StartCoroutine(WaitCardSelect(caller, 1, startTurn, endTurn, 0, 0, 0, 0, true));
        //                Debug.Log("Elige Aliado");
        //                return;
        //            }
        //        }
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
                if (effectCards == cards.name)
                    selectedCards.Add(cards);
        if (selectedCards != null)
        {
            if (caller.GetComponent<CardCore>()?.currentPosition.oponent.GetComponent<Enemy>() || caller.GetComponent<Player>())
            {
                List<MapPosition> playerMap = new List<MapPosition>();
                foreach (MapPosition mapPosition in _table.playerPositions)
                    if (mapPosition.card == null)
                        playerMap.Add(mapPosition);
                for (int i = 0; i < newEffect.x[numberEffect]; i++)
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
                for (int i = 0; i < newEffect.x[numberEffect]; i++)
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
        //bool sumoned = false;
        //List<Cards> selectedCards = new List<Cards>();
        //foreach (Cards cards in cards)
        //    foreach (string effect in newEffect)
        //        if (effect == cards.name)
        //            selectedCards.Add(cards);
        //if (selectedCards != null)
        //{
        //    if (caller.GetComponent<CardCore>()?.currentPosition.oponent.GetComponent<Enemy>() || caller.GetComponent<Player>())
        //    {
        //        for (int i = 0; i < _table.enemyFront.Length; i++)
        //            foreach (string effect in newEffect)
        //                if (effect == i.ToString())
        //                {
        //                    for (int j = 0; j < i; j++)
        //                    {
        //                        var pos = Random.Range(0, _table.playerPositions.Length - 1);
        //                        GameObject sumonCard = Instantiate(newCard).gameObject;
        //                        sumonCard.GetComponent<Card>().card = selectedCards[Random.Range(0, selectedCards.Count)];
        //                        _table.SetCard(sumonCard, pos);
        //                        sumoned = true;
        //                    }
        //                }
        //    }
        //    else if (caller.GetComponent<CardCore>()?.currentPosition.oponent.GetComponent<Player>() || caller.GetComponent<Enemy>())
        //    {
        //        for (int i = 0; i < _table.enemyFront.Length; i++)
        //            foreach (string effect in newEffect)
        //                if (effect == i.ToString())
        //                {
        //                    for (int j = 0; j < i; j++)
        //                    {
        //                        var pos = Random.Range(0, _table.enemyFront.Length - 1);
        //                        GameObject sumonCard = Instantiate(newCard).gameObject;
        //                        sumonCard.GetComponent<Card>().card = selectedCards[Random.Range(0, selectedCards.Count)];
        //                        _table.EnemySpawnCard(pos, sumonCard);
        //                        sumoned = true;
        //                    }
        //                }
        //    }
        //}
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
        "none", //0
        "start_turn", //1
        "played", //2
        "attacks", //3
        "damaged", //4
        "defeated", //5
        "defeats_enemy", //6
        "ally_creature_defeated", //7
        "end_turn", //8
        "buffed", //9
        "spell_played", //10
    };
    public List<string> extraConditions = new List<string>()
    {
        "none", //0
        "until_next_turn", //1
        "until_end_turn" //2
    };
    public List<string> effects = new List<string>()
    {
        "none", //0
        "draw", //1
        "deal", //2
        "heal", //3
        "add", //4
        "give", //5
        "immune", //6
        "summon", //7
    };
    public List<string> targetsCreatures = new List<string>()
    {
        "none", //0
        "enemy_creature", //1
        "ally_creature", //2
        "random_enemy", //3
        "random_ally", //4
        "random_enemy_creature", //5
        "random_ally_creature", //6
        "random_enemy_creatures", //7
        "random_ally_creatures", //8
        "all_enemy_creatures", //9
        "all_ally_creatures", //10
        "itself", //11
        "enemy_player", //12
        "player", //13
        "creature_front", //14
        "enemy_creatures", //15
        "ally_creatures", //16
    };
    public List<string> targetsDecks = new List<string>()
    {
        "none", //0
        "hand", //1
        "deck", //2
        "life_deck", //3
        "either_deck", //4
    };
    public List<string> targetsCards = new List<string>()
    {
        "none", //0
        "random_creature", //1
        "random_spell", //2
        "random", //3
        "specific_card" //4
    };
    public List<string> targetsSummon = new List<string>()
    {
        "none", //0
        "player", //1
        "enemy", //2
    };
    public List<int> x = new List<int>();
    public List<int> y = new List<int>();
    public List<int> numberOfTargets = new List<int>();
    public List<List<string>> cards = new List<List<string>>();
}
