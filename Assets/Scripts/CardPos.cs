using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPos : MonoBehaviour
{
    private CardManager _cardManager;
    private BattleMap _battleMap;
    public int position;

    private void Start()
    {
        _cardManager = FindObjectOfType<CardManager>();
        _battleMap = FindObjectOfType<BattleMap>();
    }
    public void SelectThisPosition()
    {
        Debug.Log("aaa");
        if(_cardManager.placeCards == true)
        {
            _cardManager.cardToPlace.GetComponent<Card>().enabled = false;
            _battleMap.SetCard(_cardManager.cardToPlace, position);
        }

    }

    private void OnMouseDown()
    {
        SelectThisPosition();
    }
}
