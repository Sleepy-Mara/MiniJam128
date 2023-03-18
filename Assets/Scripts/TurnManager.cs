using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private int turn;
    [SerializeField] private BattleMap battleMap;
    [SerializeField] private Player player;
    public void StartBattle()
    {
        //hacer cosas como agarrar cartas iniciales, setear enemigo? y eso
        StartTurn();
    }
    public void StartTurn()
    {
        turn++;
        player.RestoreMana();
        //algun efecto de comienzo de turno
    }
    public void EndTurn()
    {
        foreach (MapPosition position in battleMap.playerPositions)
        {
            position.card.Attack();
        }
        //algun efecto de fin de turno quizas
    }
}
