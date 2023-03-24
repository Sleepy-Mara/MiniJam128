using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NextCombat : MonoBehaviour
{
    public EnemyData[] enemies;
    public TextMeshProUGUI wonCombatText;
    [SerializeField] private Enemy _enemy;
    public GameObject wonCombat;
    public GameObject endTurnButton;
    public GameObject gameVictory;
    private int enemyNum;
    private TurnManager turnManager;
    private Draw draw;
    private Table table;
    public GameObject initialMenu;
    private AudioPlayer audioPlayer;
    public RectTransform[] rewardsRange;
    public Transform cardsPlacing;
    public GameObject cardPrefab;
    private List<GameObject> cardsToDelete = new();

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
        enemies[enemyNum].enemyCharacter.SetActive(true);
        _enemy.strategy = enemies[enemyNum].strategy;
        audioPlayer.Play("Music" + enemyNum);
        initialMenu.SetActive(false);
        turnManager.StartBattle();
    }
    public void ToNextCombat()
    {
        float distance = Mathf.Abs(rewardsRange[0].localPosition.x) + Mathf.Abs(rewardsRange[1].localPosition.x);
        distance /= (enemies[enemyNum].rewards.Length + 1);
        for (int i = 0; i < enemies[enemyNum].rewards.Length; i++)
        {
            draw.AddACard(enemies[enemyNum].rewards[i]);
            CardCore card = Instantiate(cardPrefab, cardsPlacing).GetComponent<CardCore>();
            card.card = enemies[enemyNum].rewards[i];
            card.SetData();
            //card.GetComponent<Animator>().runtimeAnimatorController = card.tableAnimator;
            //card.playerCard = false;
            card.transform.localPosition = new Vector3(rewardsRange[0].localPosition.x + distance * (1 + i), rewardsRange[0].localPosition.y);
            cardsToDelete.Add(card.gameObject);
        }
        wonCombatText.text = enemies[enemyNum].wonCombatDescription;
        audioPlayer.StopPlaying("Music" + enemyNum);
        enemyNum++;
        table.ResetTable();
        table.player.RestartPlayer();
        draw.ResetDeckAndHand();
        if (enemyNum == enemies.Length)
        {
            Debug.Log("Ganaste");
            audioPlayer.Play("Credits");
            gameVictory.SetActive(true);
            //agregar victoria de verdad xD
            return;
        }
        audioPlayer.Play("Music" + enemyNum);
        _enemy.strategy = enemies[enemyNum].strategy;
        _enemy.RestoreHealth(10);
        endTurnButton.SetActive(false);
        wonCombat.SetActive(true);
    }
    public void SendNext()
    {
        enemies[enemyNum - 1].enemyCharacter.SetActive(false);
        enemies[enemyNum].enemyCharacter.SetActive(true);
        wonCombat.SetActive(false);
        turnManager.turn = 0;
        turnManager.StartBattle();
        endTurnButton.SetActive(true);
        foreach (GameObject card in cardsToDelete)
            Destroy(card);
        cardsToDelete.Clear();
    }
}
