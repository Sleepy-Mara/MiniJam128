using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NextCombat : MonoBehaviour
{
    [SerializeField] private Strategy[] enemies;
    public GameObject[] enemyCharacters;
    public Cards[] enemyRewards;
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
    public GameObject initialMenu;
    private AudioPlayer audioPlayer;

    private void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();
        turnManager.enemy = _enemy;
        draw = FindObjectOfType<Draw>();
        table = FindObjectOfType<Table>();
        audioPlayer = GetComponent<AudioPlayer>();
        initialMenu.SetActive(true);
    }
    public void StartGame()
    {
        enemyCharacters[enemyNum].SetActive(true);
        _enemy.strategy = enemies[enemyNum];
        audioPlayer.Play("Music" + enemyNum);
        initialMenu.SetActive(false);
        turnManager.StartBattle();
    }
    public void ToNextCombat()
    {
        enemyCharacters[enemyNum].SetActive(false);
        draw.AddACard(enemyRewards[enemyNum]);
        wonCombatText.text = wonCombatDescription[enemyNum];
        audioPlayer.StopPlaying("Music" + enemyNum);
        enemyNum++;
        if (enemyNum == enemies.Length)
        {
            Debug.Log("Ganaste");
            audioPlayer.Play("Credits");
            gameVictory.SetActive(true);
            //agregar victoria de verdad xD
            return;
        }
        audioPlayer.Play("Music" + enemyNum);
        enemyCharacters[enemyNum].SetActive(true);
        draw.ResetDeckAndHand();
        _enemy.strategy = enemies[enemyNum];
        table.ResetTable();
        wonCombat.SetActive(true);
        table.player.RestartPlayer();
    }
    public void SendNext()
    {
        wonCombat.SetActive(false);
        turnManager.turn = 0;
        turnManager.StartBattle();
    }
}
