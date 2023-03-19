using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Strategy strategy;
    public BattleMap battleMap;
    private Table table;
    public ThisCard card;

    public void MoveBackCards(int turn)
    {
        PlaceBackCards(turn);
    }
    public void PlaceBackCards(int turn)
    {
        if (strategy.turns[turn].cardpPlacement0 != null)
        {
            var newCard = Instantiate(card);
            newCard.card = strategy.turns[turn].cardpPlacement0;
            newCard.SetData();
            //place holder:
            table.SetCard(newCard.gameObject, transform);
        }
        if (strategy.turns[turn].cardpPlacement1 != null)
        {
            var newCard = Instantiate(card);
            newCard.card = strategy.turns[turn].cardpPlacement1;
            newCard.SetData();
            //place holder:
            table.SetCard(newCard.gameObject, transform);
        }
        if (strategy.turns[turn].cardpPlacement1 != null)
        {
            var newCard = Instantiate(card);
            newCard.card = strategy.turns[turn].cardpPlacement1;
            newCard.SetData();
            //place holder:
            table.SetCard(newCard.gameObject, transform);
        }
    }
    public void AttackFrontCards()
    {

    }
}
