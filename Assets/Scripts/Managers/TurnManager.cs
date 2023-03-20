using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [HideInInspector] public int turn;
    [SerializeField] private Player player;
    public Enemy enemy;
    private Draw draw;
    private Table table;
    public bool canPlayCards;
    public bool canEndTurn;
    [SerializeField] private int cardsInHandStart;

    private void Start()
    {
        draw = FindObjectOfType<Draw>();
        table = FindObjectOfType<Table>();
        StartBattle();
    }

    public void StartBattle()
    {
        Debug.Log("Empieza el combate");
        enemy.MoveBackCards(turn);
        for (int i = 0; i < cardsInHandStart; i++)
            draw.DrawACard();
        //hacer cosas como agarrar cartas iniciales, setear enemigo? y eso
    }
    public void StartTurn()
    {
        canEndTurn = false;
        turn++;
        Debug.Log("Empieza el turno " + turn);
        player.RestoreMana();
        draw.canDraw = true;
        foreach (ThisCard thisCard in table.myCards)
            thisCard.OnTurnStart();
        //algun efecto de comienzo de turno
    }
    public void PlayableTurn()
    {
        Debug.Log("deberias poder jugar");
        canPlayCards = true;
        foreach (ThisCard thisCard in table.myCards)
            thisCard.OnPlayEffect();
        //var cards = FindObjectsOfType<Card>();
        //foreach (Card card in cards)
        //    card.canPlay = true;
    }
    public void EndTurn()
    {
        if (canEndTurn)
        {
            canPlayCards = false;
            foreach (ThisCard thisCard in table.myCards)
                thisCard.Attack();
            foreach (ThisCard thisCard in table.myCards)
                thisCard.OnTurnEnd();
            enemy.MoveBackCards(turn);
        }
        //var cards = FindObjectsOfType<Card>();
        //foreach (Card card in cards)
        //    card.canPlay = false;
        //foreach (MapPosition position in battleMap.playerPositions)
        //{
        //    if(position.card != null)
        //        position.card.Attack();
        //}
        //algun efecto de fin de turno quizas
        //que actue el oponente
    }
}
