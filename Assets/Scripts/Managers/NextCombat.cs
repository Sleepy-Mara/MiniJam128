using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NextCombat : MonoBehaviour
{
    [SerializeField] private Strategy[] enemies;
    public GameObject[] enemyCharacters;
    [TextArea(1, 4)]
    public string[] wonCombatDescription;
    public TextMeshProUGUI wonCombatText;
    [SerializeField] private Enemy _enemy;
    public GameObject wonCombat;
    public GameObject gameVictory;
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
        enemyCharacters[enemyNum].SetActive(true);
    }
    public void ToNextCombat()
    {
        enemyCharacters[enemyNum].SetActive(false);
        enemyNum++;
        if (enemyNum == enemies.Length)
        {
            Debug.Log("Ganaste");
            gameVictory.SetActive(true);
            //agregar victoria de verdad xD
            return;
        }
        enemyCharacters[enemyNum].SetActive(true);
        wonCombatText.text = wonCombatDescription[enemyNum - 1];
        draw.ResetDeckAndHand();
        _enemy.strategy = enemies[enemyNum];
        _enemy.RestoreHealth(10);
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
