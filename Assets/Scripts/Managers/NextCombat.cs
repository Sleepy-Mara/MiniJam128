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
    [HideInInspector]
    public int enemyNum;
    private List<GameObject> cardsToDelete = new();
    [HideInInspector] public Enemy enemy;
    protected TurnManager _turnManager;
    private Draw _draw;
    private Table _table;
    private CameraManager _cameraManager;
    private LanguageManager _languageManager;
    protected AudioPlayer _audioPlayer;
    private SaveWithJson json;
    public GameObject demoScreen;
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
        // Esto es un arreglo provisional, no recuerdo bien como funciona este script, pero quizás haya que 
        // cambiarlo porque ya no haga falta que haga las cosas de esta manera.
        // CREO que estaba hecho así porque el canvas se guardaba entre escenas? Ni idea
        if(scene.buildIndex == 0)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            return;
        }
        _cameraManager = FindObjectOfType<CameraManager>();
        if (enemy == null)
            enemy = FindObjectOfType<Enemy>();
        _turnManager = FindObjectOfType<TurnManager>();
        //Debug.Log("Se busco TurnManager, se encontro " + _turnManager.name);
        _turnManager.enemy = enemy;
        _draw = FindObjectOfType<Draw>();
        _draw.Start();
        _table = FindObjectOfType<Table>();
        _languageManager = FindObjectOfType<LanguageManager>();
        _audioPlayer = GetComponent<AudioPlayer>();
        if (enemyNum == enemies.Length - 1)
        {
            GameObject.Find("CapybaraBoy").SetActive(true);
            StartGame();
        }
    }
    private void Start()
    {
        _turnManager = FindObjectOfType<TurnManager>();
        _turnManager.enemy = enemy;
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
        introCombatText.text = enemies[enemyNum].introCombatMessage[_languageManager.languageNumber];
        introCombat.SetActive(true);
        endTurnButton.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        endTurnButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(_turnManager.EndTurn);
    }
    public void ToNextCombat()
    {
        FindObjectOfType<CurrencyManager>().Currency = enemies[enemyNum].reward;
        wonCombatText.text = enemies[enemyNum].wonCombatMessage[_languageManager.languageNumber];
        if (_cameraManager != null)
            _cameraManager.HandCamera();
        else
            FindObjectOfType<CameraManager>().HandCamera();
        _audioPlayer.StopPlaying("Music" + enemyNum);
        enemyNum++;

        //QUITAR ESTE OTRO IF ACORDATE CATALINA LA PUTA MADRE

        if (enemyNum < 4)

        //AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
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
    public virtual void StartCombat()
    {
        if (FindObjectOfType<EnemyAI>())
        {
            enemy.GetComponent<EnemyAI>().StartCombat(enemies[enemyNum].enemyDeck.deck);
        }
        else
            enemy.strategy = enemies[enemyNum].strategy;
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

        // QUITAR ESTE IF

        if (enemyNum == 4)
        {
            demoScreen.SetActive(true);
            return;
        }

        // QUITAR ESTE IF

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
        introCombatText.text = enemies[enemyNum].introCombatMessage[_languageManager.languageNumber];
    }
    // este es para el boton de derrota
    public void RestartCombat()
    {
        if (FindObjectOfType<EnemyAI>())
        {
            enemy.GetComponent<EnemyAI>().StartCombat(enemies[enemyNum].enemyDeck.deck);
        }
        else
            enemy.strategy = enemies[enemyNum].strategy;
        _table.player.RestartPlayer();
        _turnManager.StartBattle();
        lostCombat.SetActive(false);
        endTurnButton.SetActive(true);
    }
    public void Defeat()
    {
        ResetCombat();
        _cameraManager.HandCamera();
        endTurnButton.SetActive(false);
        lostCombatText.text = enemies[enemyNum].lostCombatMessage[_languageManager.languageNumber];
        lostCombat.SetActive(true);
    }
    private void ResetCombat()
    {
        _table.ResetTable();
        _table.player.RestartPlayer();
        _draw.ResetDeckAndHand();
        enemy.RestoreHealth(10, false, false);
        _turnManager.turn = 0;
        endTurnButton.SetActive(false);
    }
    public void SetEnemyNum(int i)
    {
        enemyNum = i;
    }
}
