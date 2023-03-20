using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    private bool _inTable;
    private Draw _draw;
    public MapPosition[] playerPositions;
    public MapPosition[] enemyFront;
    public MapPosition[] enemyBack;
    [HideInInspector] public Player player;
    private Enemy enemy;
    public GameObject cardPrefab;
    public List<AudioClip> clips;
    public GameObject audio;

    [HideInInspector] public List<ThisCard> myCards = new List<ThisCard>();

    void Start()
    {
        _draw = FindObjectOfType<Draw>();
        player = FindObjectOfType<Player>();
        enemy = FindObjectOfType<Enemy>();
        StartSet();
        //foreach (MapPosition position in mapPositions)
        //    foreach (MapPosition mapPosition in mapPositions)
        //        if (position.cardPos.gameObject.GetComponent<CardPos>().positionFacing == mapPosition.cardPos)
        //            position.positionFacing = mapPosition;
    }
    void StartSet()
    {
        for (int i = 0; i < playerPositions.Length; i++)
        {
            playerPositions[i].positionFacing = enemyFront[i];
            playerPositions[i].oponent = enemy;
            enemyFront[i].positionFacing = playerPositions[i];
            enemyFront[i].oponent = player;
            enemyBack[i].nextPosition = enemyFront[i];
        }
    }
    public void SetCard(GameObject card, int place)
    {
        var newAudio = Instantiate(audio).GetComponent<AudioSource>();
        newAudio.clip = clips[Random.Range(0, clips.Count)];
        newAudio.Play();
        card.GetComponent<Animator>().runtimeAnimatorController = card.GetComponent<ThisCard>().tableAnimator;
        card.transform.SetParent(playerPositions[place].cardPos.transform);
        card.transform.SetPositionAndRotation(playerPositions[place].cardPos.transform.position, playerPositions[place].cardPos.transform.rotation);
        playerPositions[place].card = card.GetComponent<ThisCard>();
        card.GetComponent<ThisCard>().actualPosition = playerPositions[place];
        //foreach (MapPosition positions in mapPositions)
        //    if (positions.cardPos == pos)
        //        positions.card = card.GetComponent<ThisCard>();
        myCards.Add(card.GetComponent<ThisCard>());
    }
    public void EnemySetCard(int place, Cards cardType)
    {
        if (enemyBack[place].card == null)
        {
            var newAudio = Instantiate(audio).GetComponent<AudioSource>();
            newAudio.clip = clips[Random.Range(0, clips.Count)];
            newAudio.Play();
            ThisCard newCard = Instantiate(cardPrefab, enemyBack[place].cardPos.transform).GetComponent<ThisCard>();
            newCard.GetComponent<Animator>().runtimeAnimatorController = newCard.GetComponent<ThisCard>().tableAnimator;
            Debug.Log("Se seteo la carta " + cardType.cardName + " en " + enemyBack[place].cardPos.name);
            newCard.card = cardType;
            newCard.actualPosition = enemyBack[place];
            newCard.SetData();
            newCard.GetComponent<RectTransform>().SetPositionAndRotation(enemyBack[place].cardPos.GetComponent<RectTransform>().position, enemyBack[place].cardPos.transform.rotation);
            enemyBack[place].card = newCard;
        }
    }
    public void MoveEnemyCard()
    {
        Debug.Log("MoveEnemyCard");
        for (int i = 0; i < enemyBack.Length; i++)
            if(enemyBack[i].card != null && enemyFront[i].card == null)
            {
                var newAudio = Instantiate(audio).GetComponent<AudioSource>();
                newAudio.clip = clips[Random.Range(0, clips.Count)];
                newAudio.Play();
                ThisCard card = enemyBack[i].card;
                enemyBack[i].card = null;
                card.transform.SetParent(enemyFront[i].cardPos.transform);
                card.transform.SetPositionAndRotation(enemyFront[i].cardPos.transform.position, enemyFront[i].cardPos.transform.rotation);
                card.actualPosition = enemyFront[i];
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
