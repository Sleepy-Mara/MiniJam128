using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
    private SaveWithJson json;
    [HideInInspector] public int EnemyReward
    {
        get => enemies[enemyNum-1].reward;
        set { }
    }

    private void Awake()
    {
        json=FindObjectOfType<SaveWithJson>();
        if (FindObjectOfType<LevelSelected>())
            SetEnemyNum(FindObjectOfType<LevelSelected>().Level);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _enemy = FindObjectOfType<Enemy>();
        _turnManager = FindObjectOfType<TurnManager>();
        Debug.Log("Se busco TurnManager, se encontro " + _turnManager.name);
        _turnManager.enemy = _enemy;
        _draw = FindObjectOfType<Draw>();
        _draw.Start();
        _table = FindObjectOfType<Table>();
        _audioPlayer = GetComponent<AudioPlayer>();
        if (enemyNum == enemies.Length - 1)
        {
            GameObject.Find("CapybaraBoy").SetActive(true);
            StartGame();
        }
    }
    private void Start()
    {
        _enemy = FindObjectOfType<Enemy>();
        _turnManager = FindObjectOfType<TurnManager>();
        _turnManager.enemy = _enemy;
        _draw = FindObjectOfType<Draw>();
        _table = FindObjectOfType<Table>();
        _audioPlayer = GetComponent<AudioPlayer>();
        if (enemyNum == enemies.Length - 1)
        {
            enemies[enemyNum].enemyCharacter = GameObject.Find("CapybaraBoy");
            StartGame();
            return;
        }
        if (json.SaveData.firstPlay)
            initialMenu.SetActive(true);
        else StartGame();
        SaveData saveData = json.SaveData;
        saveData.firstPlay = false;
        json.SaveData = saveData;
    }
    public void StartGame()
    {
        if (enemies[enemyNum].enemyCharacter != null)
            enemies[enemyNum].enemyCharacter.SetActive(true);
        initialMenu.SetActive(false);
        introCombatText.text = enemies[enemyNum].introCombatMessage;
        introCombat.SetActive(true);
        endTurnButton.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        endTurnButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(_turnManager.EndTurn);
    }
    public void ToNextCombat()
    {
        float distance = Mathf.Abs(rewardsRange[0].localPosition.x) + Mathf.Abs(rewardsRange[1].localPosition.x);
        distance /= (enemies[enemyNum].rewards.Length + 1);
        FindObjectOfType<CurrencyManager>().Currency = enemies[enemyNum].reward;
        //for (int i = 0; i < enemies[enemyNum].rewards.Length; i++)
        //{
            //_draw.AddACard(enemies[enemyNum].rewards[i]);
            //CardCore card = Instantiate(cardPrefab, cardsPlacing).GetComponent<CardCore>();
            //card.card = enemies[enemyNum].rewards[i];
            //card.SetData();
            //card.transform.localPosition = new Vector3(rewardsRange[0].localPosition.x + distance * (1 + i), rewardsRange[0].localPosition.y);
            //cardsToDelete.Add(card.gameObject);
        //}
        wonCombatText.text = enemies[enemyNum].wonCombatMessage;
        _audioPlayer.StopPlaying("Music" + enemyNum);
        enemyNum++;
        if (json.SaveData.currentUnlockedLevels < enemyNum && enemyNum != enemies.Length -1)
        {
            SaveData saveData = json.SaveData;
            saveData.currentUnlockedLevels = enemyNum;
            json.SaveData = saveData;
        }
        ResetCombat();
        endTurnButton.SetActive(false);
        wonCombat.SetActive(true);
    }
    // este es para el boton de introduccion
    public void StartCombat()
    {
        if (FindObjectOfType<EnemyAI>())
            _enemy.GetComponent<EnemyAI>().deck = enemies[enemyNum].enemyDeck.deck;
        else
            _enemy.strategy = enemies[enemyNum].strategy;
        _audioPlayer.Play("Music" + enemyNum);
        _turnManager.StartBattle();
        introCombat.SetActive(false);
        endTurnButton.SetActive(true);
    }
    // este es para el boton de victoria
    public void EndCombat()
    {
        ResetCombat();
        wonCombat.SetActive(false);
        if (enemyNum == enemies.Length)
        {
            FindObjectOfType<ChangeSceneManager>().ChangeScene("Credits");
            return;
        }
        enemies[enemyNum - 1].enemyCharacter.SetActive(false);
        foreach (GameObject card in cardsToDelete)
            Destroy(card);
        cardsToDelete.Clear();
        if (enemyNum == enemies.Length - 1)
        {
            FindObjectOfType<ChangeSceneManager>().ChangeScene("Credits");
            //FindObjectOfType<ChangeSceneManager>().ChangeScene("PruebaNoche");
            return;
        }
        enemies[enemyNum].enemyCharacter.SetActive(true);
        introCombat.SetActive(true);
        introCombatText.text = enemies[enemyNum].introCombatMessage;
    }
    // este es para el boton de derrota
    public void RestartCombat()
    {
        _table.player.RestartPlayer();
        _turnManager.StartBattle();
        lostCombat.SetActive(false);
        endTurnButton.SetActive(true);
    }
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
    public void SetEnemyNum(int i)
    {
        enemyNum = i;
    }
}
