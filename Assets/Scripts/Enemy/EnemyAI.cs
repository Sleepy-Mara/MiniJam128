using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : Enemy
{
    [SerializeField] private List<Cards> deck;
    private List<Cards> _actualDeck;
    [SerializeField] private List<Cards> bloodDeck;
    private List<Cards> _actualBloodDeck;
    private List<Cards> _hand;
    private int _mana;
    private new void Awake()
    {
        base.Awake();
        _actualDeck = deck;
        _actualBloodDeck = bloodDeck;
    }
    public override void MoveBackCards(int turn)
    {
        base.MoveBackCards(turn);
        DrawACard(null, turn);
        PlaceBackCards(turn);
    }
    public override void PlaceBackCards(int turn)
    {
        BestPlay bestPlay = CheckValueCards();
        foreach (Cards card in bestPlay.cards)
        {
            _table.EnemySetCard(System.Array.IndexOf(_table.enemyFront, bestPlay.positions[bestPlay.cards.IndexOf(card)]), card);
        }
    }
    private BestPlay CheckValueCards()
    {
        List<CardInfo> cards = new List<CardInfo>();
        int actualId = 0;
        foreach (Cards card in _hand)
        {
            actualId++;
            CardInfo cardInfo = new CardInfo();
            int actualValue = 0;
            if (!card.spell)
                foreach (var position in _table.enemyFront)
                {
                    if (_table.enemyBack[System.Array.IndexOf(_table.enemyFront, position)].card != null)
                        continue;
                    bool kill = false;
                    bool survive = false;
                    bool hasEffect = false;
                    bool activeEffect = false;
                    bool effectUtility = false;
                    bool effectValue = false;
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
                            hasEffect = true;
                            actualValue++;
                        }
                        else continue;
                        actualValue += CheckEffectValue(card, position, kill, survive);
                    }
                    cardInfo.mapPosition = position;
                }
            else
                actualValue += CheckEffectValue(card, null, false, true);
            cardInfo.card = card;
            cardInfo.value = actualValue;
            cardInfo.id = actualId;
            cards.Add(cardInfo);
        }
        List<BestPlay> bestPlays = ForeachCard(cards, 0, null);
        BestPlay trueBestPlay = null;
        foreach (BestPlay bestPlay in bestPlays)
        {
            if (trueBestPlay == null)
            {
                trueBestPlay = bestPlay;
                continue;
            }
            if (trueBestPlay.value < bestPlay.value)
                trueBestPlay = bestPlay;
        }
        return trueBestPlay;
    }
    private int CheckEffectValue(Cards card, MapPosition position, bool kill, bool survive)
    {
        int value = 0;
        var effect = card.effect;
        foreach(string effects in _effectManager.Conditions)
            if (!survive && effect.Contains(effects))
            {
                if (effect.Contains(_effectManager.Effects[0]))
                {
                    if (effect.Contains(_effectManager.Target[15]) && _actualDeck.Count > 0)
                        value++;
                    if (effect.Contains(_effectManager.Target[16]) && _actualBloodDeck.Count > 0)
                        value++;
                    if (effect.Contains(_effectManager.Target[17]) && (_actualBloodDeck.Count > 0 || _actualDeck.Count > 0))
                        value++;
                }
                if (effect.Contains(_effectManager.Conditions[1]))
                {
                    bool usefulEffect = false;
                    if (effect.Contains(_effectManager.Target[2]) || effect.Contains(_effectManager.Target[4]) || effect.Contains(_effectManager.Target[6]) || effect.Contains(_effectManager.Target[8]))
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
                    if (effect.Contains(_effectManager.Target[3]) || effect.Contains(_effectManager.Target[5]) || effect.Contains(_effectManager.Target[7]) || effect.Contains(_effectManager.Target[9]))
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
                    if (effect.Contains(_effectManager.Target[11]))
                        for (int i = 0; i < 50; i++)
                            if (effect.Contains(_effectManager.Target[11] + "_" + i))
                                if (FindObjectOfType<Player>().actualHealth - i <= 0)
                                    value += 1000;
                    if (effect.Contains(_effectManager.Target[12]))
                        for (int i = 0; i < 50; i++)
                            if (effect.Contains(_effectManager.Target[12] + "_" + i))
                                if (actualHealth - i <= 0)
                                    value -= 1000;
                    if (effect.Contains(_effectManager.Target[13]) && !kill)
                    {
                        for (int i = 0; i < 50; i++)
                            if (effect.Contains(_effectManager.Target[13] + "_" + i))
                                if (position.positionFacing.card.ActualLife - card.attack - i <= 0)
                                {
                                    kill = true;
                                    value++;
                                    if (effect.Contains(_effectManager.Conditions[1]) || effect.Contains(_effectManager.Conditions[2]) || effect.Contains(_effectManager.Conditions[3]) || effect.Contains(_effectManager.Conditions[7]))
                                        survive = true;
                                }
                        usefulEffect = true;
                    }
                    if (effect.Contains(_effectManager.Target[10]))
                    {
                        for (int i = 0; i < 50; i++)
                            if (effect.Contains(_effectManager.Target[10] + "_" + i))
                                if (card.life - position.positionFacing.card.ActualAttack - i <= 0)
                                {
                                    if (effect.Contains(_effectManager.Conditions[1]) || effect.Contains(_effectManager.Conditions[2]) || effect.Contains(_effectManager.Conditions[3]) || effect.Contains(_effectManager.Conditions[7]))
                                    {
                                        value--;
                                        survive = false;
                                    }
                                }
                        usefulEffect = true;
                    }
                    if ((!effect.Contains(_effectManager.Conditions[4]) || !survive) && (!effect.Contains(_effectManager.Conditions[4]) || kill))
                        if (usefulEffect)
                            value++;
                }
                if (effect.Contains(_effectManager.Conditions[2]))
                {
                    bool usefulEffect = true;
                    if (effect.Contains(_effectManager.Target[2]) || effect.Contains(_effectManager.Target[4]) || effect.Contains(_effectManager.Target[6]) || effect.Contains(_effectManager.Target[8]))
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
                    if (effect.Contains(_effectManager.Target[3]) || effect.Contains(_effectManager.Target[5]) || effect.Contains(_effectManager.Target[7]) || effect.Contains(_effectManager.Target[9]))
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
                    if (effect.Contains(_effectManager.Target[11]))
                        for (int i = 0; i < 50; i++)
                            if (effect.Contains(_effectManager.Target[11] + "_" + i))
                                if (FindObjectOfType<Player>().actualHealth + i > FindObjectOfType<Player>().maxHealth)
                                    usefulEffect = true;
                                else usefulEffect = false;
                    if (effect.Contains(_effectManager.Target[12]))
                        for (int i = 0; i < 50; i++)
                            if (effect.Contains(_effectManager.Target[12] + "_" + i))
                                if (actualHealth + i > maxHealth)
                                    usefulEffect = false;
                    if (effect.Contains(_effectManager.Target[13]) && !kill)
                    {
                        for (int i = 0; i < 50; i++)
                            if (effect.Contains(_effectManager.Target[13] + "_" + i))
                                if (position.positionFacing.card.ActualLife - card.attack + i > position.positionFacing.card.card.life)
                                {
                                    usefulEffect = true;
                                    //survive = true;
                                }
                                else usefulEffect = false;
                    }
                    if (effect.Contains(_effectManager.Target[10]))
                    {
                        for (int i = 0; i < 50; i++)
                            if (effect.Contains(_effectManager.Target[10] + "_" + i))
                                if (card.life - position.positionFacing.card.ActualAttack + i > card.life)
                                {
                                    usefulEffect = false;
                                    survive = true;
                                }
                    }
                    if ((!effect.Contains(_effectManager.Conditions[4]) || !survive) && (!effect.Contains(_effectManager.Conditions[4]) || kill))
                        if (usefulEffect)
                            value++;
                }
                if (effect.Contains(_effectManager.Conditions[4]))
                {
                    bool usefulEffect = false;
                    if (effect.Contains(_effectManager.Target[2]) || effect.Contains(_effectManager.Target[4]) || effect.Contains(_effectManager.Target[6]) || effect.Contains(_effectManager.Target[8]))
                    {
                        if (!kill && position.positionFacing.card != null && effect.Contains("-"))
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
                                if (!effect.Contains("-"))
                                    continue;
                                usefulEffect = true;
                            }
                        }
                    }
                    if (effect.Contains(_effectManager.Target[3]) || effect.Contains(_effectManager.Target[5]) || effect.Contains(_effectManager.Target[7]) || effect.Contains(_effectManager.Target[9]))
                    {
                        bool allyInBattle = false;
                        foreach (MapPosition allyCards in _table.enemyFront)
                        {
                            if (allyCards.card == null)
                                continue;
                            if (effect.Contains("-"))
                                continue;
                            allyInBattle = true;
                        }
                        if (!allyInBattle)
                            usefulEffect = true;
                    }
                    if (effect.Contains(_effectManager.Target[13]) && !kill)
                        if (effect.Contains("-"))
                            usefulEffect = true;
                    if (effect.Contains(_effectManager.Target[10]))
                        if (survive)
                            usefulEffect = true;
                    if ((!effect.Contains(_effectManager.Conditions[4]) || !survive) && (!effect.Contains(_effectManager.Conditions[4]) || kill))
                        if (usefulEffect)
                            value++;
                }
            }
        return value;
    }
    private List<BestPlay> ForeachCard(List<CardInfo> cards, int i, BestPlay lastBestPlays)
    {
        List<BestPlay> bestPlays = new List<BestPlay>();
        foreach (CardInfo card in cards)
        {
            BestPlay newBestPlay = new BestPlay();
            if (lastBestPlays != null)
            {
                if (lastBestPlays.leftoverMana - card.card.manaCost < 0 || lastBestPlays.leftoverHealth - card.card.healthCost <= 0)
                    continue;
                bool iContinue = false;
                foreach (int ids in lastBestPlays.ids)
                    if(ids == card.id)
                        iContinue = true;
                if (card.mapPosition != null)
                    foreach (MapPosition position in lastBestPlays.positions)
                        if (position == card.mapPosition)
                            iContinue = true;
                if (iContinue)
                    continue;
                newBestPlay = lastBestPlays;
            }
            else
            {
                if (_mana - card.card.manaCost < 0 || actualHealth - card.card.healthCost <= 0)
                    continue;
                newBestPlay.leftoverMana = _mana;
                newBestPlay.leftoverHealth = actualHealth;
            }
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
            _actualDeck.Add(card);
        else
            _actualBloodDeck.Add(card);
    }
    public void DrawACard(Cards card, int turn)
    {
        if (card != null && _actualDeck.Contains(card))
        {
            _hand.Add(card);
            _actualDeck.Remove(card);
            PlaceBackCards(turn);
            return;
        }
        int randomCard = Random.Range(0, _actualDeck.Count);
        _hand.Add(_actualDeck[randomCard]);
        _actualDeck.Remove(_actualDeck[randomCard]);
        PlaceBackCards(turn);
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
