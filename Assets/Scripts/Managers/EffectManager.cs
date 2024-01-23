using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField]
    private List<string> conditions = new List<string>()
    {
        "start_turn", //0
        "played", //1
        "attacks", //2
        "damaged", //3
        "defeated", //4
        "defeats_enemy", //5
        "ally_creature_defeated", //6
        "end_turn", //7
        "buffed", //8
        "spell_played", //9
    };
    [HideInInspector]
    public List<string> Conditions
    {
        get { return conditions; }
    }
    [SerializeField]
    private List<string> extraConditions = new List<string>()
    {
        "until_next_turn", //0
        "until_end_turn" //1
    };
    [SerializeField]
    private List<string> effects = new List<string>()
    {
        "draw", //0
        "deal", //1
        "heal", //2
        "add", //3
        "give", //4
        "immune", //5
        "summon" //6
    };
    [HideInInspector]
    public List<string> Effects
    {
        get { return effects; }
    }
    [SerializeField]
    private List<string> targets = new List<string>()
    {
        "enemy_creature", //0
        "ally_creature", //1
        "random_enemy", //2
        "random_ally", //3
        "random_enemy_creature", //4
        "random_ally_creature", //5
        "random_enemy_creatures", //6
        "random_ally_creatures", //7
        "all_enemy_creatures", //8
        "all_ally_creatures", //9
        "itself", //10
        "enemy_player", //11
        "player", //12
        "creature_front", //13
        "hand", //14
        "deck", //15
        "life_deck", //16
        "either_deck", //17
        "random_creature", //18
        "random_spell" //19
    };
    [HideInInspector]
    public List<string> Target
    {
        get { return targets; }
    }
    [SerializeField]
    private List<Cards> cards;
    [SerializeField]
    private Card newCard;
    private Table _table;
    private Draw _draw;
    private void Awake()
    {
        _table = FindObjectOfType<Table>();
        _draw = FindObjectOfType<Draw>();
    }
    #region CheckingsConditions
    public void CheckConditionStartOfTurn(MonoBehaviour caller)
    {
        SelectEffects(caller, GetEffect(caller), conditions[0]);
    }
    public void CheckConditionIsPlayed(MonoBehaviour caller)
    {
        SelectEffects(caller, GetEffect(caller), conditions[1]);
    }
    public void CheckConditionAttack(MonoBehaviour caller)
    {
        SelectEffects(caller, GetEffect(caller), conditions[2]);
    }
    public void CheckConditionGetDamaged(MonoBehaviour caller)
    {
        SelectEffects(caller, GetEffect(caller), conditions[3]);
    }
    public void CheckConditionDefeated(MonoBehaviour caller)
    {
        SelectEffects(caller, GetEffect(caller), conditions[4]);
    }
    public void CheckConditionDefeatsAnEnemy(MonoBehaviour caller)
    {
        SelectEffects(caller, GetEffect(caller), conditions[5]);
    }
    public void CheckConditionAllyIsDefeated(MonoBehaviour caller)
    {
        SelectEffects(caller, GetEffect(caller), conditions[6]);
    }
    public void CheckConditionEndOfTurn(MonoBehaviour caller)
    {
        SelectEffects(caller, GetEffect(caller), conditions[7]);
    }
    public void CheckConditionSpellPlayed(MonoBehaviour caller)
    {
        SelectEffects(caller, GetEffect(caller), conditions[8]);
    }
    public void CheckConditionGetBuffed(MonoBehaviour caller)
    {
        SelectEffects(caller, GetEffect(caller), conditions[9]);
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
        List<List<string>> allEffects = new List<List<string>>();
        List<string> effectDescriptions = new List<string>();
        List<string> tempEffect = new List<string>();
        bool startTempEffect = false;
        for (int i = 0; i < effect.Length; i++)
        {
            if (effect[i].ToString() == ">")
            {
                if (string.Join("", tempEffect) == "/")
                {
                    Debug.LogError(effectDescriptions.Count);
                    foreach (string conditions in effectDescriptions)
                        if (condition == conditions)
                        {
                            effectDescriptions.Remove(conditions);
                            CheckEffect(caller, effectDescriptions);
                            break;
                        }
                    effectDescriptions.Clear();
                }
                else
                if (string.Join("", tempEffect) == "&")
                {
                    bool checkCondition = false;
                    Debug.LogError(effectDescriptions.Count);
                    foreach (string conditions in effectDescriptions)
                        if (condition == conditions)
                        {
                            effectDescriptions.Remove(conditions);
                            CheckEffect(caller, effectDescriptions);
                            checkCondition = true;
                            break;
                        }
                    effectDescriptions.Clear();
                    if (checkCondition)
                        effectDescriptions.Add(condition);
                }
                else
                    effectDescriptions.Add(string.Join("", tempEffect));
                tempEffect.Clear();
                startTempEffect = false;
            }
            if (startTempEffect)
                tempEffect.Add(effect[i].ToString());
            if (effect[i].ToString() == "<")
                startTempEffect = true;
        }
        foreach (string conditions in effectDescriptions)
            if (condition == conditions)
            {
                effectDescriptions.Remove(conditions);
                CheckEffect(caller, effectDescriptions);
                return;
            }
        if (caller.GetComponent<CardCore>())
            caller.GetComponent<CardCore>().checkingEffect = false;
    }
    private void CheckEffect(MonoBehaviour caller, List<string> newEffect)
    {
        bool startTurn = false;
        bool endTurn = false;
        foreach(string extraCondition in newEffect)
        {
            if (extraCondition.Contains(extraConditions[0]))
                startTurn = true;
            if (extraCondition.Contains(extraConditions[1]))
                endTurn = true;
        }
        foreach (string effect in newEffect)
        {
            if (effect.Contains(effects[0]))
                DrawEffect(caller, newEffect, startTurn, endTurn);
            if (effect.Contains(effects[1]))
                DealDamageEffect(caller, newEffect, startTurn, endTurn);
            if (effect.Contains(effects[2]))
                HealEffect(caller, newEffect, startTurn, endTurn);
            if (effect.Contains(effects[3]))
                AddEffect(caller, newEffect, startTurn, endTurn);
            if (effect.Contains(effects[4]))
                GiveEffect(caller, newEffect, startTurn, endTurn);
            if (effect.Contains(effects[5]))
                ImmuneEffect(caller, newEffect, startTurn, endTurn);
            if (effect.Contains(effects[6]))
                SumonEffect(caller, newEffect, startTurn, endTurn);
        }
    }
    #endregion
    private void DrawEffect(MonoBehaviour caller, List<string> newEffect, bool startTurn, bool endTurn)
    {
        #region Draw
        foreach (string target in targets)
            for (int i = 0; i < 5; i++)
                foreach (string effectNew in newEffect)
                    if (effectNew == target + "_" + i.ToString())
                        for (int j = 0; j < i; j++)
                        {
                            bool creature = true;
                            bool spell = true;
                            foreach (string effect in newEffect)
                            {
                                if (effect == targets[18])
                                    spell = false;
                                if (effect == targets[19])
                                    creature = false;
                            }
                            if (target == targets[15])
                            {
                                if (caller.GetComponent<CardCore>()?.currentPosition.oponent == FindObjectOfType<Enemy>() || caller.GetComponent<Player>())
                                    _draw.DrawACard(Draw.DeckType.Mana, creature, spell);
                                if ((caller.GetComponent<CardCore>()?.currentPosition.oponent == FindObjectOfType<Player>() || caller.GetComponent<Enemy>()) && FindObjectOfType<EnemyAI>().enabled)
                                    FindObjectOfType<EnemyAI>().DrawACard(null, creature, spell, 0);
                            }
                            else if (target == targets[16])
                            {
                                if (caller.GetComponent<CardCore>()?.currentPosition.oponent == FindObjectOfType<Enemy>() || caller.GetComponent<Player>())
                                    _draw.DrawACard(Draw.DeckType.Blood, creature, spell);
                                if ((caller.GetComponent<CardCore>()?.currentPosition.oponent == FindObjectOfType<Player>() || caller.GetComponent<Enemy>()) && FindObjectOfType<EnemyAI>().enabled)
                                    FindObjectOfType<EnemyAI>().DrawACard(null, creature, spell, 0);
                            }
                            else if (target == targets[17])
                            {
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
                            }
                        }
        if (caller.GetComponent<CardCore>())
            caller.GetComponent<CardCore>().checkingEffect = false;
        #endregion
    }
    private void DealDamageEffect(MonoBehaviour caller, List<string> newEffect, bool startTurn, bool endTurn)
    {
        #region DealDamage
        foreach (string target in targets)
            foreach (string effectNew in newEffect)
                if (effectNew.Contains(target))
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
                    for (int i = 0; i < 5; i++)
                        if (effectNew == target + "_" + i.ToString())
                        {
                            if (target == targets[13])
                                caller.GetComponent<CardCore>()?.currentPosition.positionFacing.card.ReceiveDamageEffect(i, null, startTurn, endTurn);
                            else if (target == targets[12])
                                allyHealth.ReceiveDamage(i);
                            else if (target == targets[11])
                                enemyHealth.ReceiveDamage(i);
                            else if (target == targets[10])
                            {
                                if (caller.GetComponent<Card>())
                                    caller.GetComponent<Card>().ReceiveDamageEffect(i, null, startTurn, endTurn);
                                if (caller.GetComponent<Health>())
                                    caller.GetComponent<Health>().ReceiveDamage(i);
                            }
                            else if (target == targets[7])
                            {
                                for (int j = 0; j < 3; j++)
                                    foreach (string effect in newEffect)
                                        if (effect == target + "_" + j.ToString())
                                            for (int k = 0; k < j; k++)
                                            {
                                                var allysAlive = new List<MapPosition>();
                                                foreach (MapPosition enemy in allyPositions)
                                                    if (enemy.card != null)
                                                        if (enemy.card.ActualLife > 0)
                                                            allysAlive.Add(enemy);
                                                var selected = Random.Range(0, allysAlive.Count);
                                                if (allysAlive[selected].card != null)
                                                    allysAlive[selected].card.ReceiveDamageEffect(i, null, startTurn, endTurn);
                                            }
                            }
                            else if (target == targets[6])
                            {
                                for (int j = 0; j < 3; j++)
                                    foreach (string effect in newEffect)
                                        if (effect == target + "_" + j.ToString())
                                            for (int k = 0; k < j; k++)
                                            {
                                                var enemysAlive = new List<MapPosition>();
                                                foreach (MapPosition enemy in enemyPositions)
                                                    if (enemy.card != null)
                                                        if (enemy.card.ActualLife > 0)
                                                            enemysAlive.Add(enemy);
                                                var selected = Random.Range(0, enemysAlive.Count);
                                                if (enemysAlive[selected].card != null)
                                                    enemysAlive[selected].card.ReceiveDamageEffect(i, null, startTurn, endTurn);
                                            }
                            }
                            else if (target == targets[9])
                            {
                                foreach (MapPosition selected in allyPositions)
                                    if (selected.card != null)
                                        selected.card.ReceiveDamageEffect(i, null, startTurn, endTurn);
                            }
                            else if (target == targets[8])
                            {
                                foreach (MapPosition selected in enemyPositions)
                                    if (selected.card)
                                        selected.card.ReceiveDamageEffect(i, null, startTurn, endTurn);
                            }
                            else if (target == targets[5])
                            {
                                var allysAlive = new List<MapPosition>();
                                foreach (MapPosition enemy in allyPositions)
                                    if (enemy.card != null)
                                        if (enemy.card.ActualLife > 0)
                                            allysAlive.Add(enemy);
                                var selected = Random.Range(0, allysAlive.Count);
                                if (allysAlive[selected].card != null)
                                    allysAlive[selected].card.ReceiveDamageEffect(i, null, startTurn, endTurn);
                            }
                            else if (target == targets[4])
                            {
                                var enemysAlive = new List<MapPosition>();
                                foreach (MapPosition enemy in enemyPositions)
                                    if (enemy.card != null)
                                        if (enemy.card.ActualLife > 0)
                                            enemysAlive.Add(enemy);
                                var selected = Random.Range(0, enemysAlive.Count);
                                if (enemysAlive[selected].card != null)
                                    enemysAlive[selected].card.ReceiveDamageEffect(i, null, startTurn, endTurn);
                            }
                            else if (target == targets[3])
                            {
                                var allysAlive = new List<MapPosition>();
                                foreach (MapPosition enemy in allyPositions)
                                    if (enemy.card != null)
                                        if (enemy.card.ActualLife > 0)
                                            allysAlive.Add(enemy);
                                var selected = Random.Range(0, allysAlive.Count + 1);
                                if (selected == allyPositions.Length)
                                    allyHealth.ReceiveDamage(i);
                                else if (allysAlive[selected].card != null)
                                    allysAlive[selected].card.ReceiveDamageEffect(i, null, startTurn, endTurn);
                            }
                            else if (target == targets[2])
                            {
                                var enemysAlive = new List<MapPosition>();
                                foreach (MapPosition enemy in enemyPositions)
                                    if (enemy.card != null)
                                        if (enemy.card.ActualLife > 0)
                                            enemysAlive.Add(enemy);
                                var selected = Random.Range(0, enemysAlive.Count + 1);
                                if (selected == enemyPositions.Length)
                                    enemyHealth.ReceiveDamage(i);
                                else if (enemysAlive[selected].card != null)
                                    enemysAlive[selected].card.ReceiveDamageEffect(i, null, startTurn, endTurn);
                            }
                            else if (target == targets[0])
                            {
                                Debug.Log("Elige Enemigo");
                            }
                            else if (target == targets[1])
                            {
                                Debug.Log("Elige Aliado");
                            }
                        }
                }
        if (caller.GetComponent<CardCore>())
            caller.GetComponent<CardCore>().checkingEffect = false;
        #endregion
    }
    private void HealEffect(MonoBehaviour caller, List<string> newEffect, bool startTurn, bool endTurn)
    {
        #region Heal
        foreach (string target in targets)
            foreach (string effectNew in newEffect)
                if (effectNew.Contains(target))
                    for (int i = 0; i < 5; i++)
                        if (effectNew == target + "_" + i.ToString())
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
                            if (target == targets[13])
                                caller.GetComponent<CardCore>()?.currentPosition.positionFacing.card.HealEffect(i, startTurn, endTurn);
                            else if (target == targets[12])
                                allyHealth.RestoreHealth(i);
                            else if (target == targets[11])
                                enemyHealth.RestoreHealth(i);
                            else if (target == targets[10])
                            {
                                if (caller.GetComponent<Card>())
                                    caller.GetComponent<Card>().HealEffect(i, startTurn, endTurn);
                                if (caller.GetComponent<Health>())
                                    caller.GetComponent<Health>().RestoreHealth(i);
                            }
                            else if (target == targets[7])
                            {
                                for (int j = 0; j < 3; j++)
                                    foreach (string effect in newEffect)
                                        if (effect == target + "_" + j.ToString())
                                            for (int k = 0; k < j; k++)
                                            {
                                                var allysToHeal = new List<MapPosition>();
                                                foreach (MapPosition ally in allyPositions)
                                                    if (ally.card != null)
                                                        if (ally.card.ActualLife > 0 && ally.card.ActualLife < ally.card.card.life)
                                                            allysToHeal.Add(ally);
                                                int selected = 0;
                                                if (allysToHeal.Count > 0)
                                                    selected = Random.Range(0, allysToHeal.Count);
                                                if (allysToHeal[selected].card != null)
                                                    allysToHeal[selected].card.HealEffect(i, startTurn, endTurn);
                                            }
                            }
                            else if (target == targets[6])
                            {
                                for (int j = 0; j < 3; j++)
                                    foreach (string effect in newEffect)
                                        if (effect == target + "_" + j.ToString())
                                            for (int k = 0; k < j; k++)
                                            {
                                                var enemysToHeal = new List<MapPosition>();
                                                foreach (MapPosition ally in enemyPositions)
                                                    if (ally.card != null)
                                                        if (ally.card.ActualLife > 0 && ally.card.ActualLife < ally.card.card.life)
                                                            enemysToHeal.Add(ally);
                                                int selected = 0;
                                                if (enemysToHeal.Count > 0)
                                                    selected = Random.Range(0, enemysToHeal.Count);
                                                if (enemysToHeal[selected].card != null)
                                                    enemysToHeal[selected].card.HealEffect(i, startTurn, endTurn);
                                            }
                            }
                            else if (target == targets[9])
                            {
                                foreach (MapPosition selected in allyPositions)
                                    if (selected.card != null)
                                        selected.card.HealEffect(i, startTurn, endTurn);
                            }
                            else if (target == targets[8])
                            {
                                foreach (MapPosition selected in enemyPositions)
                                    if (selected.card)
                                        selected.card.HealEffect(i, startTurn, endTurn);
                            }
                            else if (target == targets[5])
                            {
                                var allysToHeal = new List<MapPosition>();
                                foreach (MapPosition ally in allyPositions)
                                    if (ally.card != null)
                                        if (ally.card.ActualLife > 0 && ally.card.ActualLife < ally.card.card.life)
                                            allysToHeal.Add(ally);
                                int selected = 0;
                                if (allysToHeal.Count > 0)
                                    selected = Random.Range(0, allysToHeal.Count);
                                if (allysToHeal[selected].card != null)
                                    allysToHeal[selected].card.HealEffect(i, startTurn, endTurn);
                            }
                            else if (target == targets[4])
                            {
                                var enemysToHeal = new List<MapPosition>();
                                foreach (MapPosition ally in enemyPositions)
                                    if (ally.card != null)
                                        if (ally.card.ActualLife > 0 && ally.card.ActualLife < ally.card.card.life)
                                            enemysToHeal.Add(ally);
                                int selected = 0;
                                if (enemysToHeal.Count > 0)
                                    selected = Random.Range(0, enemysToHeal.Count);
                                if (enemysToHeal[selected].card != null)
                                    enemysToHeal[selected].card.HealEffect(i, startTurn, endTurn);
                            }
                            else if (target == targets[3])
                            {
                                var allysToHeal = new List<MapPosition>();
                                foreach (MapPosition ally in allyPositions)
                                    if (ally.card != null)
                                        if (ally.card.ActualLife > 0 && ally.card.ActualLife < ally.card.card.life)
                                            allysToHeal.Add(ally);
                                int selected = 0;
                                if (allysToHeal.Count > 0)
                                    selected = Random.Range(0, allysToHeal.Count);
                                if (selected == allyPositions.Length)
                                    allyHealth.RestoreHealth(i);
                                else if (allysToHeal[selected].card != null)
                                    allysToHeal[selected].card.HealEffect(i, startTurn, endTurn);
                            }
                            else if (target == targets[2])
                            {
                                var enemysToHeal = new List<MapPosition>();
                                foreach (MapPosition ally in enemyPositions)
                                    if (ally.card != null)
                                        if (ally.card.ActualLife > 0 && ally.card.ActualLife < ally.card.card.life)
                                            enemysToHeal.Add(ally);
                                int selected = 0;
                                if (enemysToHeal.Count > 0)
                                    selected = Random.Range(0, enemysToHeal.Count);
                                if (selected == enemyPositions.Length)
                                    enemyHealth.RestoreHealth(i);
                                else if (enemysToHeal[selected].card != null)
                                    enemysToHeal[selected].card.HealEffect(i, startTurn, endTurn);
                            }
                            else if (target == targets[0])
                            {
                                Debug.Log("Elige Enemigo");
                            }
                            else if (target == targets[1])
                            {
                                Debug.Log("Elige Aliado");
                            }
                        }
        if (caller.GetComponent<CardCore>())
            caller.GetComponent<CardCore>().checkingEffect = false;
        #endregion
    }
    private void AddEffect(MonoBehaviour caller, List<string> newEffect, bool startTurn, bool endTurn)
    {
        #region Add
        bool player = true;
        if ((caller.GetComponent<CardCore>()?.currentPosition.oponent == FindObjectOfType<Player>() || caller.GetComponent<EnemyAI>()) && FindObjectOfType<EnemyAI>().enabled)
            player = false;
        bool added = false;
        foreach (string effectNew in newEffect)
        {
            if (effectNew == targets[14])
            {
                List<Cards> listOfCards = new List<Cards>();
                List<Cards> cardsToAdd = new List<Cards>();
                foreach (string effect in newEffect)
                {
                    if (effect == targets[18])
                    {
                        foreach (Cards cardToList in cards)
                            if (!cardToList.spell)
                                listOfCards.Add(cardToList);
                        cardsToAdd.Add(listOfCards[Random.Range(0, listOfCards.Count)]);
                    }
                    else if (effect == targets[19])
                    {
                        foreach (Cards cardToList in cards)
                            if (cardToList.spell)
                                listOfCards.Add(cardToList);
                        cardsToAdd.Add(listOfCards[Random.Range(0, listOfCards.Count)]);
                    }
                    else
                    {
                        listOfCards = cards;
                        foreach (Cards cards in listOfCards)
                            foreach (string effect2 in newEffect)
                                if (effect2 == cards.name)
                                    cardsToAdd.Add(cards);
                    }
                }
                if (cardsToAdd.Count > 0)
                {
                    var cards = cardsToAdd[Random.Range(0, cardsToAdd.Count)];
                    for (int i = 0; i < 5; i++)
                        foreach (string effect in newEffect)
                            if (effect == targets[14] + "_" + i.ToString())
                            {
                                for (int j = 0; j < i; j++)
                                {
                                    if (player)
                                    {
                                        var addCard = Instantiate(newCard, _draw.transform);
                                        addCard.card = cards;
                                        addCard.GetComponent<Card>().SetData();
                                        _draw.AddCardToHand(addCard);
                                    } else if (FindObjectOfType<EnemyAI>())
                                        FindObjectOfType<EnemyAI>().hand.Add(cards);
                                }
                                added = true;
                            }
                    if (!added)
                    {
                        if (player)
                        {
                            var addCard = Instantiate(newCard, _draw.transform);
                            addCard.card = cards;
                            addCard.GetComponent<Card>().SetData();
                            _draw.AddCardToHand(addCard);
                        }
                        else if (FindObjectOfType<EnemyAI>())
                            FindObjectOfType<EnemyAI>().hand.Add(cards);
                    }
                }
            }
            else if (effectNew == targets[15])
            {
                List<Cards> listOfCards = new List<Cards>();
                List<Cards> cardsToAdd = new List<Cards>();
                foreach (string effect in newEffect)
                {
                    if (effect == targets[18])
                    {
                        foreach (Cards cardToList in cards)
                            if (!cardToList.spell)
                                listOfCards.Add(cardToList);
                        cardsToAdd.Add(listOfCards[Random.Range(0, listOfCards.Count)]);
                    }
                    else if (effect == targets[19])
                    {
                        foreach (Cards cardToList in cards)
                            if (cardToList.spell)
                                listOfCards.Add(cardToList);
                        cardsToAdd.Add(listOfCards[Random.Range(0, listOfCards.Count)]);
                    }
                    else
                    {
                        listOfCards = cards;
                        foreach (Cards cards in listOfCards)
                            foreach (string effect2 in newEffect)
                                if (effect2 == cards.name)
                                    cardsToAdd.Add(cards);
                    }
                }
                if (cardsToAdd.Count > 0)
                {
                    var cards = cardsToAdd[Random.Range(0, cardsToAdd.Count)];
                    for (int i = 0; i < 50; i++)
                        foreach (string effect in newEffect)
                            if (effect == targets[15] + "_" + i.ToString())
                            {
                                for (int j = 0; j < i; j++)
                                {
                                    if (player)
                                        _draw.AddATempCard(cards);
                                    else if (FindObjectOfType<EnemyAI>())
                                        FindObjectOfType<EnemyAI>().hand.Add(cards);
                                }
                                added = true;
                            }
                    if (!added)
                    {
                        if (player)
                            _draw.AddATempCard(cards);
                        else if (FindObjectOfType<EnemyAI>())
                            FindObjectOfType<EnemyAI>().hand.Add(cards);
                    }
                }
            }
            else if (effectNew == targets[16])
            {
                List<Cards> listOfCards = new List<Cards>();
                List<Cards> cardsToAdd = new List<Cards>();
                foreach (string effect in newEffect)
                {
                    if (effect == targets[18])
                    {
                        foreach (Cards cardToList in cards)
                            if (!cardToList.spell)
                                listOfCards.Add(cardToList);
                        cardsToAdd.Add(listOfCards[Random.Range(0, listOfCards.Count)]);
                    }
                    else if (effect == targets[19])
                    {
                        foreach (Cards cardToList in cards)
                            if (cardToList.spell)
                                listOfCards.Add(cardToList);
                        cardsToAdd.Add(listOfCards[Random.Range(0, listOfCards.Count)]);
                    }
                    else
                    {
                        listOfCards = cards;
                        foreach (Cards cards in listOfCards)
                            foreach (string effect2 in newEffect)
                                if (effect2 == cards.name)
                                    cardsToAdd.Add(cards);
                    }
                }
                if (cardsToAdd.Count > 0)
                {
                    var cards = cardsToAdd[Random.Range(0, cardsToAdd.Count)];
                    for (int i = 0; i < 50; i++)
                        foreach (string effect in newEffect)
                            if (effect == targets[16] + "_" + i.ToString())
                            {
                                for (int j = 0; j < i; j++)
                                {
                                    if (player)
                                        _draw.AddATempCard(cards);
                                    else if (FindObjectOfType<EnemyAI>())
                                        FindObjectOfType<EnemyAI>().hand.Add(cards);
                                }
                                added = true;
                            }
                    if (!added)
                    {
                        if (player)
                            _draw.AddATempCard(cards);
                        else if (FindObjectOfType<EnemyAI>())
                            FindObjectOfType<EnemyAI>().hand.Add(cards);
                    }
                }
            }
        }
        if (caller.GetComponent<CardCore>())
            caller.GetComponent<CardCore>().checkingEffect = false;
        #endregion
    }
    private void GiveEffect(MonoBehaviour caller, List<string> newEffect, bool startTurn, bool endTurn)
    {
        #region Give
        foreach (string target in targets)
            foreach (string effectNew in newEffect)
                if (effectNew.Contains(target))
                    for (int i = 0; i < 5; i++)
                        for (int j = 0; j < 5; j++)
                        {
                            int attack = i;
                            int life = j;
                            bool mustContinue = true;
                            foreach (string effect in newEffect)
                            {
                                if (effect == target + "_+" + i.ToString() + "/+" + j.ToString())
                                {
                                    attack *= 1;
                                    life *= 1;
                                    mustContinue = false;
                                }
                                else if (effect == target + "_-" + i.ToString() + "/-" + j.ToString())
                                {
                                    attack *= -1;
                                    life *= -1;
                                    mustContinue = false;
                                }
                                else if (effect == target + "_+" + i.ToString() + "/-" + j.ToString())
                                {
                                    attack *= 1;
                                    life *= -1;
                                    mustContinue = false;
                                }
                                else if (effect == target + "_-" + i.ToString() + "/+" + j.ToString())
                                {
                                    attack *= -1;
                                    life *= 1;
                                    mustContinue = false;
                                }
                            }
                            if (mustContinue)
                                continue;
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
                            if (target == targets[13])
                            {
                                if (caller.GetComponent<CardCore>())
                                    if (caller.GetComponent<CardCore>().currentPosition.positionFacing.card != null)
                                        caller.GetComponent<CardCore>().currentPosition.positionFacing.card.BuffEffect(attack, life, startTurn, endTurn);
                            }
                            else if (target == targets[10] && caller.GetComponent<Card>())
                                caller.GetComponent<Card>().BuffEffect(attack, life, startTurn, endTurn);
                            else if (target == targets[7])
                            {
                                for (int l = 0; l < 3; l++)
                                    foreach (string effect in newEffect)
                                        if (effect == target + "_" + l.ToString())
                                            for (int k = 0; k < l; k++)
                                            {
                                                var creatureToBuff = new List<MapPosition>();
                                                foreach (MapPosition creature in allyPositions)
                                                    if (creature.card != null)
                                                        if (creature.card.ActualLife > 0)
                                                            creatureToBuff.Add(creature);
                                                var selected = Random.Range(0, creatureToBuff.Count);
                                                if (creatureToBuff[selected].card != null)
                                                    creatureToBuff[selected].card.BuffEffect(attack, life, startTurn, endTurn);
                                            }
                            }
                            else if (target == targets[6])
                            {
                                for (int l = 0; l < 3; l++)
                                    foreach (string effect in newEffect)
                                        if (effect == target + "_" + l.ToString())
                                            for (int k = 0; k < l; k++)
                                            {
                                                var creatureToBuff = new List<MapPosition>();
                                                foreach (MapPosition creature in enemyPositions)
                                                    if (creature.card != null)
                                                        if (creature.card.ActualLife > 0)
                                                            creatureToBuff.Add(creature);
                                                var selected = Random.Range(0, creatureToBuff.Count);
                                                if (creatureToBuff[selected].card != null)
                                                    creatureToBuff[selected].card.BuffEffect(attack, life, startTurn, endTurn);
                                            }
                            }
                            else if (target == targets[9])
                            {
                                foreach (MapPosition selected in allyPositions)
                                    if (selected.card != null)
                                        selected.card.BuffEffect(attack, life, startTurn, endTurn);
                            }
                            else if (target == targets[8])
                            {
                                foreach (MapPosition selected in enemyPositions)
                                    if (selected.card != null)
                                        selected.card.BuffEffect(attack, life, startTurn, endTurn);
                            }
                            else if (target == targets[5])
                            {
                                var creatureToBuff = new List<MapPosition>();
                                foreach (MapPosition creature in allyPositions)
                                    if (creature.card != null)
                                        if (creature.card.ActualLife > 0)
                                            creatureToBuff.Add(creature);
                                var selected = Random.Range(0, creatureToBuff.Count);
                                if(creatureToBuff.Count > 0)
                                if (creatureToBuff[selected].card != null)
                                    creatureToBuff[selected].card.BuffEffect(attack, life, startTurn, endTurn);
                            }
                            else if (target == targets[4])
                            {
                                var creatureToBuff = new List<MapPosition>();
                                foreach (MapPosition creature in enemyPositions)
                                    if (creature.card != null)
                                        if (creature.card.ActualLife > 0)
                                            creatureToBuff.Add(creature);
                                var selected = Random.Range(0, creatureToBuff.Count);
                                if (creatureToBuff[selected].card != null)
                                    creatureToBuff[selected].card.BuffEffect(attack, life, startTurn, endTurn);
                            }
                            else if (target == targets[0])
                            {
                                Debug.Log("Elige Enemigo");
                            }
                            else if (target == targets[1])
                            {
                                Debug.Log("Elige Aliado");
                            }
                        }
        if (caller.GetComponent<CardCore>())
            caller.GetComponent<CardCore>().checkingEffect = false;
        #endregion
    }
    private void ImmuneEffect(MonoBehaviour caller, List<string> newEffect, bool startTurn, bool endTurn)
    {
        #region Inmune
        foreach (string target in targets)
            foreach (string effectNew in newEffect)
                if (effectNew.Contains(target))
                {
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
                    if (target == targets[13])
                        caller.GetComponent<CardCore>()?.currentPosition.positionFacing.card.ImmuneEffect(startTurn, endTurn);
                    else if (target == targets[10] && caller.GetComponent<Card>())
                        caller.GetComponent<Card>().ImmuneEffect(startTurn, endTurn);
                    else if (target == targets[7])
                    {
                        for (int j = 0; j < 3; j++)
                            foreach (string effect in newEffect)
                                if (effect == target + "_" + j.ToString())
                                    for (int k = 0; k < j; k++)
                                    {
                                        var selected = Random.Range(0, allyPositions.Length - 1);
                                        if (_table.playerPositions[selected].card != null)
                                            _table.playerPositions[selected].card.ImmuneEffect(startTurn, endTurn);
                                    }
                    }
                    else if (target == targets[6])
                    {
                        for (int j = 0; j < 3; j++)
                            foreach (string effect in newEffect)
                                if (effect == target + "_" + j.ToString())
                                    for (int k = 0; k < j; k++)
                                    {
                                        var selected = Random.Range(0, enemyPositions.Length - 1);
                                        if (_table.enemyFront[selected].card != null)
                                            _table.enemyFront[selected].card.ImmuneEffect(startTurn, endTurn);
                                    }
                    }
                    else if (target == targets[9])
                    {
                        foreach (MapPosition selected in allyPositions)
                            if (selected.card != null)
                                selected.card.ImmuneEffect(startTurn, endTurn);
                    }
                    else if (target == targets[8])
                    {
                        foreach (MapPosition selected in enemyPositions)
                            if (selected.card != null)
                                selected.card.ImmuneEffect(startTurn, endTurn);
                    }
                    else if (target == targets[5])
                    {
                        var selected = Random.Range(0, allyPositions.Length - 1);
                        if (_table.playerPositions[selected].card != null)
                            _table.playerPositions[selected].card.ImmuneEffect(startTurn, endTurn);
                    }
                    else if (target == targets[4])
                    {
                        var selected = Random.Range(0, enemyPositions.Length - 1);
                        if (_table.enemyFront[selected].card != null)
                            _table.enemyFront[selected].card.ImmuneEffect(startTurn, endTurn);
                    }
                    else if (target == targets[0])
                    {
                        Debug.Log("Elige Enemigo");
                    }
                    else if (target == targets[1])
                    {
                        Debug.Log("Elige Aliado");
                    }
                }
        if (caller.GetComponent<CardCore>())
            caller.GetComponent<CardCore>().checkingEffect = false;
        #endregion
    }
    private void SumonEffect(MonoBehaviour caller, List<string> newEffect, bool startTurn, bool endTurn)
    {
        #region Sumon
        bool sumoned = false;
        List<Cards> selectedCards = new List<Cards>();
        foreach (Cards cards in cards)
            foreach (string effect in newEffect)
                if (effect == cards.name)
                    selectedCards.Add(cards);
        if (selectedCards != null)
        {
            if (caller.GetComponent<CardCore>()?.currentPosition.oponent.GetComponent<Enemy>() || caller.GetComponent<Player>())
            {
                for (int i = 0; i < _table.enemyFront.Length; i++)
                    foreach (string effect in newEffect)
                        if (effect == i.ToString())
                        {
                            for (int j = 0; j < i; j++)
                            {
                                var pos = Random.Range(0, _table.playerPositions.Length - 1);
                                GameObject sumonCard = Instantiate(newCard).gameObject;
                                sumonCard.GetComponent<Card>().card = selectedCards[Random.Range(0, selectedCards.Count)];
                                _table.SetCard(sumonCard, pos);
                                sumoned = true;
                            }
                        }
            }
            else if (caller.GetComponent<CardCore>()?.currentPosition.oponent.GetComponent<Player>() || caller.GetComponent<Enemy>())
            {
                for (int i = 0; i < _table.enemyFront.Length; i++)
                    foreach (string effect in newEffect)
                        if (effect == i.ToString())
                        {
                            for (int j = 0; j < i; j++)
                            {
                                var pos = Random.Range(0, _table.enemyFront.Length - 1);
                                GameObject sumonCard = Instantiate(newCard).gameObject;
                                sumonCard.GetComponent<Card>().card = selectedCards[Random.Range(0, selectedCards.Count)];
                                _table.EnemySpawnCard(pos, sumonCard);
                                sumoned = true;
                            }
                        }
            }
        }
        if (caller.GetComponent<CardCore>())
            caller.GetComponent<CardCore>().checkingEffect = false;
        #endregion
    }
}
