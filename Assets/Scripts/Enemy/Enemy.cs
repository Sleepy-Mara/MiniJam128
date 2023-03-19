using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Health
{
    public Strategy strategy;
    private Table table;
    public ThisCard card;

    public void MoveBackCards(int turn)
    {
        PlaceBackCards(turn);
    }
    public void PlaceBackCards(int turn)
    {
        if (strategy.turns[turn].cardPlacement0 != null)
        {
            var newCard = Instantiate(card);
            newCard.card = strategy.turns[turn].cardPlacement0;
            newCard.SetData();
            //place holder:
            table.SetCard(newCard.gameObject, transform);
        }
        if (strategy.turns[turn].cardPlacement1 != null)
        {
            var newCard = Instantiate(card);
            newCard.card = strategy.turns[turn].cardPlacement1;
            newCard.SetData();
            //place holder:
            table.SetCard(newCard.gameObject, transform);
        }
        if (strategy.turns[turn].cardPlacement1 != null)
        {
            var newCard = Instantiate(card);
            newCard.card = strategy.turns[turn].cardPlacement1;
            newCard.SetData();
            //place holder:
            table.SetCard(newCard.gameObject, transform);
        }
    }
    public void AttackFrontCards()
    {

    }
    public override void Defeat()
    {
        Debug.Log("Derrotaste al oponente, estas feliz?");
    }
}
