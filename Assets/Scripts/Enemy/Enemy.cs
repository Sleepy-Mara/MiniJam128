using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Health
{
    public Strategy strategy;
    private Table table;
    public GameObject card;

    private void Start()
    {
        table = FindObjectOfType<Table>();
    }
    public void MoveBackCards(int turn)
    {
        table.MoveEnemyCard();
        PlaceBackCards(turn);
    }
    public void PlaceBackCards(int turn)
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
        AttackFrontCards();
    }
    public void AttackFrontCards()
    {
        foreach(MapPosition card in table.enemyFront)
            if (card.card != null)
                card.card.Attack();
    }
    public override void Defeat()
    {
        Debug.Log("Derrotaste al oponente, estas feliz?");
    }
}
