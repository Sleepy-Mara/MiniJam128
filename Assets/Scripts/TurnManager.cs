using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private int turn;
    [SerializeField] private BattleMap battleMap;
    [SerializeField] private Player player;
    private Draw draw;
    private Table table;
    [SerializeField] private int cardsInHandStart;

    private void Start()
    {
        draw = FindObjectOfType<Draw>();
        table = FindObjectOfType<Table>();
        StartBattle();
    }

    public void StartBattle()
    {
        for (int i = 0; i < cardsInHandStart; i++)
            draw.DrawACard();
        //hacer cosas como agarrar cartas iniciales, setear enemigo? y eso
        StartTurn();
    }
    public void StartTurn()
    {
        turn++;
        player.RestoreMana();
        draw.canDraw = true;
        //algun efecto de comienzo de turno
    }
    public void PlayableTurn()
    {
        var cards = FindObjectsOfType<Card>();
        foreach (Card card in cards)
            card.canPlay = true;
    }
    public void EndTurn()
    {
        var cards = FindObjectsOfType<Card>();
        foreach (Card card in cards)
            card.canPlay = false;
        foreach (ThisCard thisCard in table.myCards)
            thisCard.Attack();
        //foreach (MapPosition position in battleMap.playerPositions)
        //{
        //    if(position.card != null)
        //        position.card.Attack();
        //}
        //algun efecto de fin de turno quizas
        //que actue el oponente
    }
}
