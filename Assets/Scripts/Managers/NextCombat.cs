using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextCombat : MonoBehaviour
{
    [SerializeField] private Enemy[] enemies;
    private int enemy;
    private TurnManager turnManager;
    private Draw draw;
    private Table table;

    private void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();
        draw = FindObjectOfType<Draw>();
        table = FindObjectOfType<Table>();
    }
    public void ToNextCombat()
    {
        enemy++;
        if (enemy > enemies.Length)
        {
            Debug.Log("Ganaste");
            return;
        }
        turnManager.enemy = enemies[enemy];
        turnManager.turn = 0;
        draw.ResetDeckAndHand();
        table.ResetTable(enemies[enemy]);
    }
}
