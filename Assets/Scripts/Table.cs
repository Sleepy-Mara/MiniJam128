using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    private bool _inTable;
    private Draw _draw;
    public List<MapPosition> mapPositions;
    public MapPosition[] playerPositions;
    public MapPosition[] enemyFront;
    public MapPosition[] enemyBack;
    private Player player;
    private Enemy enemy;
    public GameObject cardPrefab;

    [HideInInspector] public List<ThisCard> myCards = new List<ThisCard>();

    void Start()
    {
        _draw = FindObjectOfType<Draw>();
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
    public void SetCard(GameObject card, Transform position)
    {
        card.transform.position = position.position + new Vector3(0, 0.01f, 0);
        card.transform.rotation = Quaternion.Euler(90, 0, 0);
        foreach (MapPosition positions in mapPositions)
            if (positions.cardPos == position)
                positions.card = card.GetComponent<ThisCard>();
        myCards.Add(card.GetComponent<ThisCard>());
    }
    public void EnemySetCard(int place, Cards cardType)
    {
        if (enemyBack[place].card == null)
        {
            var newCard = Instantiate(cardPrefab, enemyBack[place].cardPos.transform).GetComponent<ThisCard>();
            Debug.Log("Se seteo la carta " + cardType.cardName + " en " + enemyBack[place].cardPos.name);
            newCard.card = cardType;
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
                var card = enemyBack[i].card;
                enemyBack[i] = null;
                card.transform.SetParent(enemyFront[i].card.transform);
                card.transform.SetPositionAndRotation(enemyFront[i].cardPos.transform.position + new Vector3(0, 0.01f, 0), enemyFront[i].cardPos.transform.rotation);
                enemyFront[i].card = card;
            }
    }
    public void ResetTable(Enemy newEnemy)
    {
        enemy = newEnemy;
        foreach (var card in enemyBack)
            if (card.card != null)
                Destroy(card.card);
        foreach (var card in enemyFront)
            if (card.card != null)
                Destroy(card.card);
        foreach (var card in playerPositions)
            if (card.card != null)
                Destroy(card.card);
        StartSet();
    }
}
