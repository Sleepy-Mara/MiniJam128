using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyAI : Enemy
{
    [HideInInspector] public List<Cards> deck;
    private List<Cards> _currentDeck;
    public List<Cards> CurrentDeck
    {
        get { return _currentDeck; }
    }
    [SerializeField] private List<Cards> bloodDeck;
    private List<Cards> _currentBloodDeck;
    [HideInInspector] public List<Cards> hand;
    [SerializeField] private int cardsInHand;
    [SerializeField] private int maxMana;
    [SerializeField] private int startMana;
    private int _mana;
    private new void Awake()
    {
        base.Awake();
        _table = FindObjectOfType<Table>();
    }
    public void StartCombat(List<Cards> deck)
    {
        hand = new List<Cards>();
        _currentDeck = new List<Cards>();
        _mana = startMana;
        foreach (Cards card in deck)
            _currentDeck.Add(card);
        _currentBloodDeck = bloodDeck;
        for (int i = 0; i < cardsInHand; i++)
            DrawACard(null, false, false, 0);
    }
    public override void MoveBackCards(int turn)
    {
        if (_mana < maxMana)
            _mana++;
        currentMana = _mana;
        foreach (MapPosition card in _table.enemyFront)
            if (card.card != null)
            {
                card.card.StartTurn();
                _effectManager.CheckConditionStartOfTurn(card.card);
            }
        StartTurn();
        _table.MoveEnemyCard();
        foreach (MapPosition card in _table.enemyFront)
            if (card.card != null)
                if (!card.card.played)
                {
                    _effectManager.CheckConditionIsPlayed(card.card);
                    card.card.played = true;
                }
        DrawACard(null, false, false, turn);
        PlaceBackCards(turn);
    }
    public override void PlaceBackCards(int turn)
    {
        bool canPlay = false;
        foreach (var pos in _table.enemyBack)
            if (pos.card == null)
                canPlay = true;
        if (!canPlay)
        {
            AttackFrontCards();
            return;
        }
        if (hand.Count > 0)
        {
            BestPlay bestPlay = CheckValueCards();
            if (bestPlay != null)
            foreach (Cards card in bestPlay.cards)
            {
                _table.EnemySetCard(System.Array.IndexOf(_table.enemyFront, bestPlay.positions[bestPlay.cards.IndexOf(card)]), card);
                hand.Remove(card);
            }
        }
        AttackFrontCards();
    }
    private BestPlay CheckValueCards()
    {
        List<CardInfo> cards = new List<CardInfo>();
        int actualId = 0;
        foreach (Cards card in hand)
        {
            actualId++;
            CardInfo cardInfo = new CardInfo();
            int actualValue = 0;
            if (!card.spell)
                foreach (var position in _table.enemyFront)
                {
                    cardInfo = new CardInfo();
                    actualValue = 0;
                    if (_table.enemyBack[System.Array.IndexOf(_table.enemyFront, position)].card != null)
                        continue;
                    if (position.card != null)
                        actualValue -= 3;
                    bool kill = false;
                    bool survive = false;
                    foreach (var playerCards in _table.playerPositions)
                    {
                        if (playerCards.positionFacing == position)
                        {
                            if (playerCards.card != null)
                            {
                                if (playerCards.card.ActualLife <= card.attack)
                                {
                                    kill = true;
                                    actualValue++;
                                }
                                if (playerCards.card.ActualAttack < card.life)
                                {
                                    survive = true;
                                    actualValue++;
                                }
                            }
                            else
                            {
                                survive = true;
                                actualValue++;
                            }
                        }
                        if (card.hasEffect)
                        {
                            actualValue++;
                        }
                        else continue;
                        actualValue += CheckEffectValue(card, position, kill, survive);
                    }
                    cardInfo.mapPosition = position;
                    cardInfo.card = card;
                    cardInfo.value = actualValue;
                    cardInfo.id = actualId;
                    cards.Add(cardInfo);
                }
            else
            {
                actualValue += CheckEffectValue(card, null, false, true);
                cardInfo.card = card;
                cardInfo.value = actualValue;
                cardInfo.id = actualId;
                cards.Add(cardInfo);
            }
        }
        List<BestPlay> bestPlays = ForeachCard(cards, 0, null);
        if (bestPlays.Count > 0)
        {
            List<BestPlay> trueBestPlay = new List<BestPlay>();
            foreach (BestPlay bestPlay in bestPlays)
            {
                for (int i = 0; i < bestPlay.cards.Count; i++)
                    Debug.Log(bestPlay.cards[i].cardName[0] + " - " + bestPlay.positions[i].positionNum + " - " + bestPlay.value);
                Debug.Log("-------------------------------------------------");
                if (trueBestPlay.Count <= 0)
                {
                    trueBestPlay.Add(bestPlay);
                    continue;
                }
                else if (trueBestPlay[0].value < bestPlay.value)
                {
                    trueBestPlay.Clear();
                    trueBestPlay.Add(bestPlay);
                }
                else if (trueBestPlay[0].value == bestPlay.value)
                    trueBestPlay.Add(bestPlay);
            }
            return trueBestPlay[Random.Range(0, trueBestPlay.Count)];
        }
        return null;
    }
    private int CheckEffectValue(Cards card, MapPosition position, bool kill, bool survive)
    {
        int value = 0;
        Effects effect = card.effects;
        foreach (string effects in new Effects().conditions)
            for (int j = 0; j < effect.conditions.Count; j++)
                if (!survive && effect.conditions[j] == effects)
                {
                    if (effect.effects[j] == new Effects().effects[1])
                    {
                        if (effect.targetsDecks[j] == new Effects().targetsDecks[2] && _currentDeck.Count > 0)
                            value++;
                        if (effect.targetsDecks[j] == new Effects().targetsDecks[3] && _currentBloodDeck.Count > 0)
                            value++;
                        if (effect.targetsDecks[j] == new Effects().targetsDecks[4] && (_currentBloodDeck.Count > 0 || _currentDeck.Count > 0))
                            value++;
                    }
                    if (effect.effects[j] == new Effects().effects[2])
                    {
                        bool usefulEffect = false;
                        if (effect.targetsCreatures[j] == new Effects().targetsCreatures[3] || effect.targetsCreatures[j] == new Effects().targetsCreatures[5] ||
                            effect.targetsCreatures[j] == new Effects().targetsCreatures[7] || effect.targetsCreatures[j] == new Effects().targetsCreatures[9])
                        {
                            if (!kill && position.positionFacing.card != null)
                                usefulEffect = true;
                            else
                            {
                                foreach (MapPosition playerCards in _table.playerPositions)
                                {
                                    if (playerCards.card == null)
                                        continue;
                                    if (position != null)
                                        if (playerCards != position.positionFacing)
                                            continue;
                                    usefulEffect = true;
                                }
                            }
                        }
                        if (effect.targetsCreatures[j] == new Effects().targetsCreatures[4] || effect.targetsCreatures[j] == new Effects().targetsCreatures[6] ||
                            effect.targetsCreatures[j] == new Effects().targetsCreatures[8] || effect.targetsCreatures[j] == new Effects().targetsCreatures[10])
                        {
                            bool allyInBattle = false;
                            foreach (MapPosition allyCards in _table.enemyFront)
                            {
                                if (allyCards.card == null)
                                    continue;
                                allyInBattle = true;
                            }
                            if (!allyInBattle)
                                usefulEffect = true;
                        }
                        if (effect.targetsCreatures[j] == new Effects().targetsCreatures[12])
                            if (FindObjectOfType<Player>().currentHealth - effect.values[j][0] <= 0)
                                value += 1000;
                        if (effect.targetsCreatures[j] == new Effects().targetsCreatures[13])
                            if (currentHealth - effect.values[j][0] <= 0)
                                value -= 1000;
                        if (effect.targetsCreatures[j] == new Effects().targetsCreatures[14] && !kill)
                        {
                            if (position.positionFacing.card.ActualLife - card.attack - effect.values[j][0] <= 0)
                            {
                                kill = true;
                                value++;
                                if (effect.conditions[j] == new Effects().conditions[2] || effect.conditions[j] == new Effects().conditions[3] ||
                                    effect.conditions[j] == new Effects().conditions[4] || effect.conditions[j] == new Effects().conditions[8])
                                    survive = true;
                            }
                            usefulEffect = true;
                        }
                        if (effect.targetsCreatures[j] == new Effects().targetsCreatures[11])
                        {
                            if (card.life - position.positionFacing.card.ActualAttack - effect.values[j][0] <= 0)
                            {
                                if (effect.conditions[j] == new Effects().conditions[2] || effect.conditions[j] == new Effects().conditions[3] ||
                                    effect.conditions[j] == new Effects().conditions[4] || effect.conditions[j] == new Effects().conditions[8])
                                {
                                    value--;
                                    survive = false;
                                }
                            }
                            usefulEffect = true;
                        }
                        if ((effect.conditions[j] != new Effects().conditions[4] || !survive) && (effect.conditions[j] != new Effects().conditions[4] || kill))
                            if (usefulEffect)
                                value++;
                    }
                    if (effect.effects[j] == new Effects().effects[3])
                    {
                        bool usefulEffect = true;
                        if (effect.targetsCreatures[j] == new Effects().targetsCreatures[3] || effect.targetsCreatures[j] == new Effects().targetsCreatures[5] ||
                            effect.targetsCreatures[j] == new Effects().targetsCreatures[7] || effect.targetsCreatures[j] == new Effects().targetsCreatures[9])
                        {
                            if (!kill && position.positionFacing.card != null)
                                usefulEffect = false;
                            else
                            {
                                foreach (MapPosition playerCards in _table.playerPositions)
                                {
                                    if (playerCards.card == null)
                                        continue;
                                    if (position != null)
                                        if (playerCards != position.positionFacing)
                                            continue;
                                    if (playerCards.card.ActualLife >= playerCards.card.card.life)
                                        continue;
                                    usefulEffect = false;
                                }
                            }
                        }
                        if (effect.targetsCreatures[j] == new Effects().targetsCreatures[4] || effect.targetsCreatures[j] == new Effects().targetsCreatures[6] ||
                            effect.targetsCreatures[j] == new Effects().targetsCreatures[8] || effect.targetsCreatures[j] == new Effects().targetsCreatures[10])
                        {
                            bool allyInBattle = false;
                            foreach (MapPosition allyCards in _table.enemyFront)
                            {
                                if (allyCards.card == null)
                                    continue;
                                if (allyCards.card.ActualLife >= allyCards.card.card.life)
                                    continue;
                                allyInBattle = true;
                            }
                            if (!allyInBattle)
                                usefulEffect = false;
                        }
                        if (effect.targetsCreatures[j] == new Effects().targetsCreatures[12])
                            if (FindObjectOfType<Player>().currentHealth + effect.values[j][0] > FindObjectOfType<Player>().maxHealth)
                                usefulEffect = true;
                            else usefulEffect = false;
                        if (effect.targetsCreatures[j] == new Effects().targetsCreatures[13])
                            if (currentHealth + effect.values[j][0] > maxHealth)
                                usefulEffect = false;
                        if (effect.targetsCreatures[j] == new Effects().targetsCreatures[14])
                        {
                            if (position.positionFacing.card.ActualLife - card.attack + effect.values[j][0] > position.positionFacing.card.card.life)
                            {
                                usefulEffect = true;
                                //survive = true;
                            }
                            else usefulEffect = false;
                        }
                        if (effect.targetsCreatures[j] == new Effects().targetsCreatures[11])
                        {
                            if (card.life - position.positionFacing.card.ActualAttack + effect.values[j][0] > card.life)
                            {
                                usefulEffect = false;
                                survive = true;
                            }
                        }
                        if ((effect.conditions[j] != new Effects().conditions[4] || !survive) && (effect.conditions[j] != new Effects().conditions[4] || kill))
                            if (usefulEffect)
                                value++;
                    }
                    if (effect.effects[j] == new Effects().effects[5])
                    {
                        bool usefulEffect = false;
                        if (effect.targetsCreatures[j] == new Effects().targetsCreatures[3] || effect.targetsCreatures[j] == new Effects().targetsCreatures[5] ||
                            effect.targetsCreatures[j] == new Effects().targetsCreatures[7] || effect.targetsCreatures[j] == new Effects().targetsCreatures[9])
                        {
                            if (!kill && position.positionFacing.card != null && (effect.values[j][0] < 0 || effect.values[j][1] < 0))
                                usefulEffect = true;
                            else
                            {
                                foreach (MapPosition playerCards in _table.playerPositions)
                                {
                                    if (playerCards.card == null)
                                        continue;
                                    if (position != null)
                                        if (playerCards != position.positionFacing)
                                            continue;
                                    if (effect.values[j][0] >= 0 || effect.values[j][1] >= 0)
                                        continue;
                                    usefulEffect = true;
                                }
                            }
                        }
                        if (effect.targetsCreatures[j] == new Effects().targetsCreatures[4] || effect.targetsCreatures[j] == new Effects().targetsCreatures[6] ||
                            effect.targetsCreatures[j] == new Effects().targetsCreatures[8] || effect.targetsCreatures[j] == new Effects().targetsCreatures[10])
                        {
                            bool allyInBattle = false;
                            foreach (MapPosition allyCards in _table.enemyFront)
                            {
                                if (allyCards.card == null)
                                    continue;
                                if (effect.values[j][0] < 0 || effect.values[j][1] < 0)
                                    continue;
                                allyInBattle = true;
                            }
                            if (!allyInBattle)
                                usefulEffect = true;
                        }
                        if (effect.targetsCreatures[j] == new Effects().targetsCreatures[14] && !kill)
                            if (effect.values[j][0] < 0 || effect.values[j][1] < 0)
                                usefulEffect = true;
                        if (effect.targetsCreatures[j] == new Effects().targetsCreatures[11])
                            if (survive)
                                usefulEffect = true;
                        if ((effect.conditions[j] != new Effects().conditions[4] || !survive) && (effect.conditions[j] != new Effects().conditions[4] || kill))
                            if (usefulEffect)
                                value++;
                    }
                }
        //foreach (string effects in new Effects().conditions)
        //    for (int i =0; i< effects.; i++)
        //    if (!survive && effect.Contains(effects))
        //    {
        //        if (effect.Contains(_effectManager.Effects[0]))
        //        {
        //            if (effect.Contains(_effectManager.Target[15]) && _currentDeck.Count > 0)
        //                value++;
        //            if (effect.Contains(_effectManager.Target[16]) && _currentBloodDeck.Count > 0)
        //                value++;
        //            if (effect.Contains(_effectManager.Target[17]) && (_currentBloodDeck.Count > 0 || _currentDeck.Count > 0))
        //                value++;
        //        }
        //        if (effect.Contains(_effectManager.Conditions[1]))
        //        {
        //            bool usefulEffect = false;
        //            if (effect.Contains(_effectManager.Target[2]) || effect.Contains(_effectManager.Target[4]) || effect.Contains(_effectManager.Target[6]) || effect.Contains(_effectManager.Target[8]))
        //            {
        //                if (!kill && position.positionFacing.card != null)
        //                    usefulEffect = true;
        //                else
        //                {
        //                    foreach (MapPosition playerCards in _table.playerPositions)
        //                    {
        //                        if (playerCards.card == null)
        //                            continue;
        //                        if (position != null)
        //                            if (playerCards != position.positionFacing)
        //                                continue;
        //                        usefulEffect = true;
        //                    }
        //                }
        //            }
        //            if (effect.Contains(_effectManager.Target[3]) || effect.Contains(_effectManager.Target[5]) || effect.Contains(_effectManager.Target[7]) || effect.Contains(_effectManager.Target[9]))
        //            {
        //                bool allyInBattle = false;
        //                foreach (MapPosition allyCards in _table.enemyFront)
        //                {
        //                    if (allyCards.card == null)
        //                        continue;
        //                    allyInBattle = true;
        //                }
        //                if (!allyInBattle)
        //                    usefulEffect = true;
        //            }
        //            if (effect.Contains(_effectManager.Target[11]))
        //                for (int i = 0; i < 50; i++)
        //                    if (effect.Contains(_effectManager.Target[11] + "_" + i))
        //                        if (FindObjectOfType<Player>().currentHealth - i <= 0)
        //                            value += 1000;
        //            if (effect.Contains(_effectManager.Target[12]))
        //                for (int i = 0; i < 50; i++)
        //                    if (effect.Contains(_effectManager.Target[12] + "_" + i))
        //                        if (currentHealth - i <= 0)
        //                            value -= 1000;
        //            if (effect.Contains(_effectManager.Target[13]) && !kill)
        //            {
        //                for (int i = 0; i < 50; i++)
        //                    if (effect.Contains(_effectManager.Target[13] + "_" + i))
        //                        if (position.positionFacing.card.ActualLife - card.attack - i <= 0)
        //                        {
        //                            kill = true;
        //                            value++;
        //                            if (effect.Contains(_effectManager.Conditions[1]) || effect.Contains(_effectManager.Conditions[2]) || effect.Contains(_effectManager.Conditions[3]) || effect.Contains(_effectManager.Conditions[7]))
        //                                survive = true;
        //                        }
        //                usefulEffect = true;
        //            }
        //            if (effect.Contains(_effectManager.Target[10]))
        //            {
        //                for (int i = 0; i < 50; i++)
        //                    if (effect.Contains(_effectManager.Target[10] + "_" + i))
        //                        if (card.life - position.positionFacing.card.ActualAttack - i <= 0)
        //                        {
        //                            if (effect.Contains(_effectManager.Conditions[1]) || effect.Contains(_effectManager.Conditions[2]) || effect.Contains(_effectManager.Conditions[3]) || effect.Contains(_effectManager.Conditions[7]))
        //                            {
        //                                value--;
        //                                survive = false;
        //                            }
        //                        }
        //                usefulEffect = true;
        //            }
        //            if ((!effect.Contains(_effectManager.Conditions[4]) || !survive) && (!effect.Contains(_effectManager.Conditions[4]) || kill))
        //                if (usefulEffect)
        //                    value++;
        //        }
        //        if (effect.Contains(_effectManager.Conditions[2]))
        //        {
        //            bool usefulEffect = true;
        //            if (effect.Contains(_effectManager.Target[2]) || effect.Contains(_effectManager.Target[4]) || effect.Contains(_effectManager.Target[6]) || effect.Contains(_effectManager.Target[8]))
        //            {
        //                if (!kill && position.positionFacing.card != null)
        //                    usefulEffect = false;
        //                else
        //                {
        //                    foreach (MapPosition playerCards in _table.playerPositions)
        //                    {
        //                        if (playerCards.card == null)
        //                            continue;
        //                        if (position != null)
        //                            if (playerCards != position.positionFacing)
        //                                continue;
        //                        if (playerCards.card.ActualLife >= playerCards.card.card.life)
        //                            continue;
        //                        usefulEffect = false;
        //                    }
        //                }
        //            }
        //            if (effect.Contains(_effectManager.Target[3]) || effect.Contains(_effectManager.Target[5]) || effect.Contains(_effectManager.Target[7]) || effect.Contains(_effectManager.Target[9]))
        //            {
        //                bool allyInBattle = false;
        //                foreach (MapPosition allyCards in _table.enemyFront)
        //                {
        //                    if (allyCards.card == null)
        //                        continue;
        //                    if (allyCards.card.ActualLife >= allyCards.card.card.life)
        //                        continue;
        //                    allyInBattle = true;
        //                }
        //                if (!allyInBattle)
        //                    usefulEffect = false;
        //            }
        //            if (effect.Contains(_effectManager.Target[11]))
        //                for (int i = 0; i < 50; i++)
        //                    if (effect.Contains(_effectManager.Target[11] + "_" + i))
        //                        if (FindObjectOfType<Player>().currentHealth + i > FindObjectOfType<Player>().maxHealth)
        //                            usefulEffect = true;
        //                        else usefulEffect = false;
        //            if (effect.Contains(_effectManager.Target[12]))
        //                for (int i = 0; i < 50; i++)
        //                    if (effect.Contains(_effectManager.Target[12] + "_" + i))
        //                        if (currentHealth + i > maxHealth)
        //                            usefulEffect = false;
        //            if (effect.Contains(_effectManager.Target[13]) && !kill)
        //            {
        //                for (int i = 0; i < 50; i++)
        //                    if (effect.Contains(_effectManager.Target[13] + "_" + i))
        //                        if (position.positionFacing.card.ActualLife - card.attack + i > position.positionFacing.card.card.life)
        //                        {
        //                            usefulEffect = true;
        //                            //survive = true;
        //                        }
        //                        else usefulEffect = false;
        //            }
        //            if (effect.Contains(_effectManager.Target[10]))
        //            {
        //                for (int i = 0; i < 50; i++)
        //                    if (effect.Contains(_effectManager.Target[10] + "_" + i))
        //                        if (card.life - position.positionFacing.card.ActualAttack + i > card.life)
        //                        {
        //                            usefulEffect = false;
        //                            survive = true;
        //                        }
        //            }
        //            if ((!effect.Contains(_effectManager.Conditions[4]) || !survive) && (!effect.Contains(_effectManager.Conditions[4]) || kill))
        //                if (usefulEffect)
        //                    value++;
        //        }
        //        if (effect.Contains(_effectManager.Conditions[4]))
        //        {
        //            bool usefulEffect = false;
        //            if (effect.Contains(_effectManager.Target[2]) || effect.Contains(_effectManager.Target[4]) || effect.Contains(_effectManager.Target[6]) || effect.Contains(_effectManager.Target[8]))
        //            {
        //                if (!kill && position.positionFacing.card != null && effect.Contains("-"))
        //                    usefulEffect = true;
        //                else
        //                {
        //                    foreach (MapPosition playerCards in _table.playerPositions)
        //                    {
        //                        if (playerCards.card == null)
        //                            continue;
        //                        if (position != null)
        //                            if (playerCards != position.positionFacing)
        //                                continue;
        //                        if (!effect.Contains("-"))
        //                            continue;
        //                        usefulEffect = true;
        //                    }
        //                }
        //            }
        //            if (effect.Contains(_effectManager.Target[3]) || effect.Contains(_effectManager.Target[5]) || effect.Contains(_effectManager.Target[7]) || effect.Contains(_effectManager.Target[9]))
        //            {
        //                bool allyInBattle = false;
        //                foreach (MapPosition allyCards in _table.enemyFront)
        //                {
        //                    if (allyCards.card == null)
        //                        continue;
        //                    if (effect.Contains("-"))
        //                        continue;
        //                    allyInBattle = true;
        //                }
        //                if (!allyInBattle)
        //                    usefulEffect = true;
        //            }
        //            if (effect.Contains(_effectManager.Target[13]) && !kill)
        //                if (effect.Contains("-"))
        //                    usefulEffect = true;
        //            if (effect.Contains(_effectManager.Target[10]))
        //                if (survive)
        //                    usefulEffect = true;
        //            if ((!effect.Contains(_effectManager.Conditions[4]) || !survive) && (!effect.Contains(_effectManager.Conditions[4]) || kill))
        //                if (usefulEffect)
        //                    value++;
        //        }
        //    }
        return value;
    }
    private List<BestPlay> ForeachCard(List<CardInfo> cards, int i, BestPlay lastBestPlays)
    {
        List<BestPlay> bestPlays = new List<BestPlay>();
        foreach (CardInfo card in cards)
        {
            bool iContinue = false;
            BestPlay newBestPlay = new BestPlay();
            if (lastBestPlays != null)
            {
                if (lastBestPlays.leftoverMana - card.card.manaCost < 0 || lastBestPlays.leftoverHealth - card.card.healthCost <= 0)
                    continue;
                foreach (int ids in lastBestPlays.ids)
                    if(ids == card.id)
                        iContinue = true;
                if (card.mapPosition != null)
                    foreach (MapPosition position in lastBestPlays.positions)
                        if (position == card.mapPosition)
                            iContinue = true;
                newBestPlay = lastBestPlays;
            }
            else
            {
                if (currentMana - card.card.manaCost < 0 || currentHealth - card.card.healthCost <= 0)
                    continue;
                newBestPlay.leftoverMana = currentMana;
                newBestPlay.leftoverHealth = currentHealth;
            }
            if (iContinue)
                continue;
            if (card.mapPosition != null)
            {
                newBestPlay.positions.Add(card.mapPosition);
                newBestPlay.cards.Add(card.card);
            }
            else 
                newBestPlay.spells.Add(card.card);
            newBestPlay.ids.Add(card.id);
            newBestPlay.value += card.value;
            newBestPlay.leftoverMana -= card.card.manaCost;
            newBestPlay.leftoverHealth -= card.card.healthCost;
            bestPlays.Add(newBestPlay);
            if (i < cards.Count)
            {
                i++;
                foreach (BestPlay newestBestPlay in ForeachCard(cards, i, newBestPlay))
                    bestPlays.Add(newestBestPlay);
            }
        }
        return bestPlays;
    }
    public void AddCard(Cards card)
    {
        if (card.healthCost == 0)
            _currentDeck.Add(card);
        else
            _currentBloodDeck.Add(card);
    }
    public void DrawACard(Cards card, bool creature, bool spell, int turn)
    {
        if (_currentDeck.Count <= 0)
            return;
        List<Cards> cardsToDraw = new List<Cards>();
        if (creature)
        {
            foreach (Cards cards in _currentDeck)
                if (!cards.spell)
                    cardsToDraw.Add(cards);
        }
        else if (spell)
        {
            foreach (Cards cards in _currentDeck)
                if (cards.spell)
                    cardsToDraw.Add(cards);
        }
        else cardsToDraw = _currentDeck;
        if (card != null && _currentDeck.Contains(card))
        {
            hand.Add(card);
            _currentDeck.Remove(card);
            return;
        }
        int randomCard = Random.Range(0, _currentDeck.Count);
        hand.Add(cardsToDraw[randomCard]);
        _currentDeck.Remove(cardsToDraw[randomCard]);
    }
}
[System.Serializable]
public class BestPlay
{
    public List<Cards> cards = new List<Cards>();
    public List<Cards> spells = new List<Cards>();
    public int value;
    public int leftoverMana;
    public int leftoverHealth;
    public List<MapPosition> positions = new List<MapPosition>();
    public List<int> ids = new List<int>();
}
[System.Serializable]
public class CardInfo
{
    public MapPosition mapPosition;
    public Cards card;
    public int value;
    public int id;
}
