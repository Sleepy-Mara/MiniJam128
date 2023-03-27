using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NextCombat : MonoBehaviour
{
    public EnemyData[] enemies;
    public GameObject introCombat;
    public TextMeshProUGUI introCombatText;
    public GameObject wonCombat;
    public TextMeshProUGUI wonCombatText;
    public GameObject lostCombat;
    public TextMeshProUGUI lostCombatText;
    public GameObject endTurnButton;
    public GameObject gameVictory;
    public GameObject initialMenu;
    public RectTransform[] rewardsRange;
    public Transform cardsPlacing;
    public GameObject cardPrefab;
    private int enemyNum;
    private List<GameObject> cardsToDelete = new();
    private Enemy _enemy;
    private TurnManager _turnManager;
    private Draw _draw;
    private Table _table;
    private AudioPlayer _audioPlayer;

    private void Start()
    {
        _enemy = FindObjectOfType<Enemy>();
        _turnManager = FindObjectOfType<TurnManager>();
        _turnManager.enemy = _enemy;
        _draw = FindObjectOfType<Draw>();
        _table = FindObjectOfType<Table>();
        _audioPlayer = GetComponent<AudioPlayer>();
        initialMenu.SetActive(true);
    }
    public void StartGame()
    {
        enemies[enemyNum].enemyCharacter.SetActive(true);
        initialMenu.SetActive(false);
        introCombatText.text = enemies[enemyNum].introCombatMessage;
        introCombat.SetActive(true);
    }
    public void ToNextCombat()
    {
        float distance = Mathf.Abs(rewardsRange[0].localPosition.x) + Mathf.Abs(rewardsRange[1].localPosition.x);
        distance /= (enemies[enemyNum].rewards.Length + 1);
        for (int i = 0; i < enemies[enemyNum].rewards.Length; i++)
        {
            _draw.AddACard(enemies[enemyNum].rewards[i]);
            CardCore card = Instantiate(cardPrefab, cardsPlacing).GetComponent<CardCore>();
            card.card = enemies[enemyNum].rewards[i];
            card.SetData();
            card.transform.localPosition = new Vector3(rewardsRange[0].localPosition.x + distance * (1 + i), rewardsRange[0].localPosition.y);
            cardsToDelete.Add(card.gameObject);
        }
        wonCombatText.text = enemies[enemyNum].wonCombatMessage;
        _audioPlayer.StopPlaying("Music" + enemyNum);
        enemyNum++;
        ResetCombat();
        endTurnButton.SetActive(false);
        wonCombat.SetActive(true);
    }
    // este es para el boton de introduccion
    public void StartCombat()
    {
        _enemy.strategy = enemies[enemyNum].strategy;
        _audioPlayer.Play("Music" + enemyNum);
        _turnManager.StartBattle();
        introCombat.SetActive(false);
        endTurnButton.SetActive(true);
    }
    // este es para el boton de victoria
    public void EndCombat()
    {
        enemies[enemyNum - 1].enemyCharacter.SetActive(false);
        wonCombat.SetActive(false);
        if (enemyNum == enemies.Length)
        {
            Debug.Log("Ganaste");
            _audioPlayer.Play("Credits");
            gameVictory.SetActive(true);
            //agregar victoria de verdad xD
            return;
        }
        enemies[enemyNum].enemyCharacter.SetActive(true);
        introCombatText.text = enemies[enemyNum].introCombatMessage;
        introCombat.SetActive(true);
        foreach (GameObject card in cardsToDelete)
            Destroy(card);
        cardsToDelete.Clear();
    }
    // este es para el boton de derrota
    public void RestartCombat()
    {
        _table.player.RestartPlayer();
        _turnManager.StartBattle();
        lostCombat.SetActive(false);
        endTurnButton.SetActive(true);
    }
    //public void SendNext()
    //{
    //    enemies[enemyNum - 1].enemyCharacter.SetActive(false);
    //    wonCombat.SetActive(false);
    //    if (enemyNum == enemies.Length)
    //    {
    //        Debug.Log("Ganaste");
    //        _audioPlayer.Play("Credits");
    //        gameVictory.SetActive(true);
    //        //agregar victoria de verdad xD
    //        return;
    //    }
    //    enemies[enemyNum].enemyCharacter.SetActive(true);
    //    _audioPlayer.Play("Music" + enemyNum);
    //    _turnManager.turn = 0;
    //    _turnManager.StartBattle();
    //    endTurnButton.SetActive(true);
    //    _enemy.strategy = enemies[enemyNum].strategy;
    //    foreach (GameObject card in cardsToDelete)
    //        Destroy(card);
    //    cardsToDelete.Clear();
    //}
    public void Defeat()
    {
        ResetCombat();
        endTurnButton.SetActive(false);
        lostCombatText.text = enemies[enemyNum].lostCombatMessage;
        lostCombat.SetActive(true);
    }
    private void ResetCombat()
    {
        _table.ResetTable();
        _table.player.RestartPlayer();
        _draw.ResetDeckAndHand();
        _enemy.RestoreHealth(10);
        _turnManager.turn = 0;
        endTurnButton.SetActive(false);
    }
}
