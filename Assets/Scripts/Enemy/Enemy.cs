using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Health
{
    public Strategy strategy;
    public GameObject card;
    protected Table _table;
    private TurnManager _turnManager;
    protected EffectManager _effectManager;

    private void Start()
    {
        _turnManager = FindObjectOfType<TurnManager>();
        _effectManager = FindObjectOfType<EffectManager>();
    }
    private new void Awake()
    {
        base.Awake();
        _table = FindObjectOfType<Table>();
    }
    virtual public void MoveBackCards(int turn)
    {
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
        PlaceBackCards(turn);
    }
    virtual public void PlaceBackCards(int turn)
    {
        if (turn < strategy.turns.Length)
        {
            if (strategy.turns[turn].cardPlacement0 != null)
            {
                //var newCard = Instantiate(card).GetComponent<ThisCard>();
                //newCard.card = strategy.turns[turn].cardPlacement0;
                //newCard.SetData();
                _table.EnemySetCard(0, strategy.turns[turn].cardPlacement0);
            }
            if (strategy.turns[turn].cardPlacement1 != null)
            {
                //var newCard = Instantiate(card).GetComponent<ThisCard>();
                //newCard.card = strategy.turns[turn].cardPlacement1;
                //newCard.SetData();
                _table.EnemySetCard(1, strategy.turns[turn].cardPlacement1);
            }
            if (strategy.turns[turn].cardPlacement2 != null)
            {
                //var newCard = Instantiate(card).GetComponent<ThisCard>();
                //newCard.card = strategy.turns[turn].cardPlacement2;
                //newCard.SetData();
                _table.EnemySetCard(2, strategy.turns[turn].cardPlacement2);
            }
        }
        AttackFrontCards();
    }
    public void AttackFrontCards()
    {
        StartCoroutine(AttackPhase());
    }
    IEnumerator AttackPhase()
    {
        foreach (MapPosition card in _table.enemyFront)
            if (card.card != null)
            {
                card.card.inAnimation = true;
                card.card.Attack();
                if (card.card.ActualAttack > 0)
                    yield return new WaitUntil(() => !card.card.inAnimation);
            }
        foreach (Card card in FindObjectsOfType<Card>())
            yield return new WaitUntil(() => !card.inAnimation);
        foreach (MapPosition card in _table.enemyFront)
            if (card.card != null)
            {
                card.card.EndTurn();
                card.card.checkingEffect = true;
                _effectManager.CheckConditionEndOfTurn(card.card);
                yield return new WaitUntil(() => !card.card.checkingEffect);
            }
        EndTurn();
        yield return new WaitForSeconds(1);
        _turnManager.StartTurn();
    }
    public override void Defeat()
    {
        FindObjectOfType<NextCombat>().ToNextCombat();
        FindObjectOfType<EnemyAI>().enabled = true;
        enabled = false;
    }
}
