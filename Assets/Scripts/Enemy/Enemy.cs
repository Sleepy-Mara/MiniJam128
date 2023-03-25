using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Health
{
    public Strategy strategy;
    private Table table;
    public GameObject card;
    private TurnManager _turnManager;
    private EffectManager _effectManager;
    
    private void Start()
    {
        table = FindObjectOfType<Table>();
        _turnManager = FindObjectOfType<TurnManager>();
        _effectManager = FindObjectOfType<EffectManager>();
    }
    public void MoveBackCards(int turn)
    {
        foreach (MapPosition card in table.enemyFront)
            if (card.card != null)
                _effectManager.CheckConditionStartOfTurn(card.card);
        table.MoveEnemyCard();
        foreach (MapPosition card in table.enemyFront)
            if (card.card != null)
                _effectManager.CheckConditionIsPlayed(card.card);
        PlaceBackCards(turn);
    }
    public void PlaceBackCards(int turn)
    {
        if (turn < strategy.turns.Length)
        {
                Debug.Log("el enemigo pone cartas, turno " + turn);
            if (strategy.turns[turn].cardPlacement0 != null)
            {
                //var newCard = Instantiate(card).GetComponent<ThisCard>();
                //newCard.card = strategy.turns[turn].cardPlacement0;
                //newCard.SetData();
                table.EnemySetCard(0, strategy.turns[turn].cardPlacement0);
            }
            if (strategy.turns[turn].cardPlacement1 != null)
            {
                //var newCard = Instantiate(card).GetComponent<ThisCard>();
                //newCard.card = strategy.turns[turn].cardPlacement1;
                //newCard.SetData();
                table.EnemySetCard(1, strategy.turns[turn].cardPlacement1);
            }
            if (strategy.turns[turn].cardPlacement2 != null)
            {
                //var newCard = Instantiate(card).GetComponent<ThisCard>();
                //newCard.card = strategy.turns[turn].cardPlacement2;
                //newCard.SetData();
                table.EnemySetCard(2, strategy.turns[turn].cardPlacement2);
            }
        }
        AttackFrontCards();
    }
    public void AttackFrontCards()
    {
        foreach(MapPosition card in table.enemyFront)
            if (card.card != null)
                card.card.Attack();
        foreach (MapPosition card in table.enemyFront)
            if (card.card != null)
                _effectManager.CheckConditionEndOfTurn(card.card);
        _turnManager.StartTurn();
    }
    public override void Defeat()
    {
        FindObjectOfType<NextCombat>().ToNextCombat();
        Debug.Log("Derrotaste al oponente, estas feliz?");
    }
}
