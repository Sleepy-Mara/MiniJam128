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

    [HideInInspector] public List<ThisCard> myCards = new List<ThisCard>();

    void Start()
    {
        _draw = FindObjectOfType<Draw>();
        for (int i = 0; i < playerPositions.Length; i++)
        {
            playerPositions[i].positionFacing = enemyFront[i];
            playerPositions[i].oponent = enemy;
            enemyFront[i].positionFacing = playerPositions[i];
            enemyFront[i].oponent = player;
            enemyBack[i].nextPosition = enemyFront[i];
        }
        //foreach (MapPosition position in mapPositions)
        //    foreach (MapPosition mapPosition in mapPositions)
        //        if (position.cardPos.gameObject.GetComponent<CardPos>().positionFacing == mapPosition.cardPos)
        //            position.positionFacing = mapPosition;
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
    public void EnemySetCard(ThisCard card, int place)
    {
        if (enemyFront[place] == null)
        {
            card.transform.SetPositionAndRotation(enemyBack[place].cardPos.transform.position + new Vector3(0, 0.01f, 0), enemyBack[place].cardPos.transform.rotation);
            enemyBack[place].card = card;
        }
    }
    public void MoveEnemyCard()
    {
        for (int i = 0; i < enemyBack.Length; i++)
            if(enemyBack[i].card != null && enemyFront[i].card == null)
            {
                var card = enemyBack[i].card;
                enemyBack[i] = null;
                card.transform.SetPositionAndRotation(enemyFront[i].cardPos.transform.position + new Vector3(0, 0.01f, 0), enemyFront[i].cardPos.transform.rotation);
                enemyFront[i].card = card;
            }
    }
}
