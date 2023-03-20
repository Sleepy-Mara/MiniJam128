using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NextCombat : MonoBehaviour
{
    [SerializeField] private Strategy[] enemies;
    [TextArea(1, 4)]
    public string[] wonCombatText;
    public TextMeshProUGUI wonText;
    [SerializeField] private Enemy _enemy;
    public GameObject wonCombat;
    private int enemyNum;
    private TurnManager turnManager;
    private Draw draw;
    private Table table;

    private void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();
        turnManager.enemy = _enemy;
        draw = FindObjectOfType<Draw>();
        table = FindObjectOfType<Table>();
        _enemy.strategy = enemies[enemyNum];
    }
    public void ToNextCombat()
    {
        enemyNum++;
        if (enemyNum > enemies.Length)
        {
            Debug.Log("Ganaste");
            //agregar victoria de verdad xD
            return;
        }
        draw.ResetDeckAndHand();
        _enemy.strategy = enemies[enemyNum];
        _enemy.RestoreHealth(10);
        wonText.text = wonCombatText[enemyNum];
        wonCombat.SetActive(true);
    }
    public void SendNext()
    {
        table.player.RestartPlayer();
        wonCombat.SetActive(false);
        table.ResetTable();
        turnManager.turn = 0;
        turnManager.StartBattle();
    }
}
