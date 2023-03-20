using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardPos : MonoBehaviour, IPointerDownHandler
{
    private CardManager _cardManager;
    private Table _table;
    public GameObject enemy;
    public Transform positionFacing;
    public int positionNum;

    private void Start()
    {
        _cardManager = FindObjectOfType<CardManager>();
        _table = FindObjectOfType<Table>();
    }
    public void SelectThisPosition()
    {
        if(_cardManager.placeCards == true)
        {
            Debug.Log("aaa");
            List<GameObject> newCardsInHand = new List<GameObject>();
            foreach(GameObject card in _cardManager.draw._cardsInHand)
            {
                if (card != _cardManager.cardToPlace)
                {
                    newCardsInHand.Add(card);
                    Debug.Log("Added" + card.name);
                }else Debug.Log("Ignored" + card.name);
            }
            _cardManager.draw._cardsInHand.Clear();
            _cardManager.draw._cardsInHand = newCardsInHand;
            _cardManager.draw.AdjustHand();
            //_cardManager.cardToPlace.GetComponentInChildren<ThisCard>().inTable = true;
            _table.SetCard(_cardManager.cardToPlace, positionNum);
            _table.player.SpendMana(_cardManager.cardToPlace.GetComponent<ThisCard>().card.manaCost);
            _table.player.SpendHealth(_cardManager.cardToPlace.GetComponent<ThisCard>().card.healthCost);
            _cardManager.placeCards = false;
            _cardManager.cardToPlace = null;
        }

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (_cardManager.placeCards == true && _cardManager.cardToPlace != null)
            SelectThisPosition();
    }
    //private void OnMouseDown()
    //{
    //    if (_cardManager.placeCards == true && _cardManager.cardToPlace != null)
    //        SelectThisPosition();
    //}
    //public void SelectPosition()
    //{
    //    if (_cardManager.placeCards == true && _cardManager.cardToPlace != null)
    //        SelectThisPosition();
    //}
}
