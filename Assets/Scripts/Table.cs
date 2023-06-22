using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    private bool _inTable;
    private Draw _draw;
    private EffectManager _effectManager;
    public MapPosition[] playerPositions;
    public MapPosition[] enemyFront;
    public MapPosition[] enemyBack;
    [HideInInspector] public Player player;
    private Enemy enemy;
    public GameObject cardPrefab;
    public List<AudioClip> clips;
    //public GameObject audio;
    [SerializeField]private AudioSource audioSource;

    [HideInInspector] public List<Card> myCards = new List<Card>();

    void Start()
    {
        _draw = FindObjectOfType<Draw>();
        _effectManager = FindObjectOfType<EffectManager>();
        player = FindObjectOfType<Player>();
        enemy = FindObjectOfType<Enemy>();
        //audioSource = GetComponent<AudioSource>();
        StartSet();
        //foreach (MapPosition position in mapPositions)
        //    foreach (MapPosition mapPosition in mapPositions)
        //        if (position.cardPos.gameObject.GetComponent<CardPos>().positionFacing == mapPosition.cardPos)
        //            position.positionFacing = mapPosition;
    }
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void StartSet()
    {
        for (int i = 0; i < playerPositions.Length; i++)
        {
            playerPositions[i].positionFacing = enemyFront[i];
            playerPositions[i].oponent = enemy;
            playerPositions[i].cardPos.isPlayable = true;
            enemyFront[i].positionFacing = playerPositions[i];
            enemyFront[i].oponent = player;
            enemyBack[i].nextPosition = enemyFront[i];
        }
    }
    public void SetCard(GameObject card, int place)
    {
        FindObjectOfType<CameraManager>().HandCamera();
        //var newAudio = Instantiate(audio).GetComponent<AudioSource>();
        audioSource.clip = clips[Random.Range(0, clips.Count)];
        audioSource.Play();
        card.GetComponent<Animator>().runtimeAnimatorController = card.GetComponent<Card>().tableAnimator;
        card.transform.SetParent(playerPositions[place].cardPos.transform);
        card.transform.SetPositionAndRotation(playerPositions[place].cardPos.transform.position, playerPositions[place].cardPos.transform.rotation);
        playerPositions[place].card = card.GetComponent<Card>();
        card.GetComponent<Card>().currentPosition = playerPositions[place];
        card.GetComponent<Card>().playerCard = true;
        //foreach (MapPosition positions in mapPositions)
        //    if (positions.cardPos == pos)
        //        positions.card = card.GetComponent<ThisCard>();
        myCards.Add(card.GetComponent<Card>());
        _effectManager.CheckConditionIsPlayed(card.GetComponent<Card>());
    }
    public void EnemySetCard(int place, Cards cardType)
    {
        if (enemyBack[place].card == null)
        {
            //var newAudio = Instantiate(audio).GetComponent<AudioSource>();
            audioSource.clip = clips[Random.Range(0, clips.Count)];
            audioSource.Play();
            Card newCard = Instantiate(cardPrefab, enemyBack[place].cardPos.transform).GetComponent<Card>();
            newCard.GetComponent<Animator>().runtimeAnimatorController = newCard.tableAnimator;
            Debug.Log("Se seteo la carta " + cardType.cardName + " en " + enemyBack[place].cardPos.name);
            newCard.card = cardType;
            newCard.currentPosition = enemyBack[place];
            newCard.SetData();
            newCard.transform.SetPositionAndRotation(enemyBack[place].cardPos.transform.position, enemyBack[place].cardPos.transform.rotation);
            newCard.playerCard = false;
            enemyBack[place].card = newCard;
        }
    }
    public void EnemySpawnCard(int place, GameObject card)
    {
        if (enemyFront[place].card == null)
        {
            audioSource.clip = clips[Random.Range(0, clips.Count)];
            audioSource.Play();
            Card newCard = card.GetComponent<Card>();
            newCard.playerCard = false;
            card.GetComponent<Animator>().runtimeAnimatorController = newCard.tableAnimator;
            card.transform.SetParent(enemyFront[place].cardPos.transform);
            card.transform.SetPositionAndRotation(enemyFront[place].cardPos.transform.position, enemyFront[place].cardPos.transform.rotation);
            newCard.currentPosition = enemyFront[place];
            newCard.SetData();
            enemyFront[place].card = newCard;
        }
        //que spawnee las cartas en la fila de en frente
    }
    public void MoveEnemyCard()
    {
        Debug.Log("MoveEnemyCard");
        for (int i = 0; i < enemyBack.Length; i++)
            if(enemyBack[i].card != null && enemyFront[i].card == null)
            {
                //var newAudio = Instantiate(audio).GetComponent<AudioSource>();
                audioSource.clip = clips[Random.Range(0, clips.Count)];
                audioSource.Play();
                Card card = enemyBack[i].card;
                enemyBack[i].card = null;
                card.transform.SetParent(enemyFront[i].cardPos.transform);
                card.transform.SetPositionAndRotation(enemyFront[i].cardPos.transform.position, enemyFront[i].cardPos.transform.rotation);
                card.currentPosition = enemyFront[i];
                enemyFront[i].card = card;
            }
    }
    public void ResetTable()
    {
        for (int i = 0; i < enemyBack.Length; i++)
        {
            if (enemyBack[i].card != null)
            {
                Destroy(enemyBack[i].card.gameObject);
                enemyBack[i].card = null;
            }
        }
        for (int j = 0; j < enemyFront.Length; j++)
        {
            if (enemyFront[j].card != null)
            {
                Destroy(enemyFront[j].card.gameObject);
                enemyFront[j].card = null;
            }
        }
        for (int k = 0; k < playerPositions.Length; k++)
        {
            if (playerPositions[k].card != null)
            {
                Destroy(playerPositions[k].card.gameObject);
                playerPositions[k].card = null;
            }
        }
        //foreach (var card in enemyBack)
        //    if (card.card != null)
        //        Destroy(card.card);
        //foreach (var card in enemyFront)
        //    if (card.card != null)
        //        Destroy(card.card);
        //foreach (var card in playerPositions)
        //    if (card.card != null)
        //        Destroy(card.card);
        //StartSet();
    }
}
