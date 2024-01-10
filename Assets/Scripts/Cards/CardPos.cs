using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardPos : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private CardManager _cardManager;
    private Table _table;
    public GameObject enemy;
    public Transform positionFacing;
    public int positionNum;
    public bool isPlayable;

    [SerializeField] private GameObject selectViewer;

    private void Start()
    {
        _cardManager = FindObjectOfType<CardManager>();
        _table = FindObjectOfType<Table>();
    }
    public void SelectThisPosition(GameObject cardToPlace)
    {
        //if(_cardManager.placeCards == true)
        //{
        if (_table.playerPositions[positionNum].card != null || !isPlayable)
        {
            FindObjectOfType<Draw>().AdjustHand();
            FindObjectOfType<CameraManager>().HandCamera();
            return;
        }
        cardToPlace.GetComponent<Card>().played = true;
        List<GameObject> newCardsInHand = new List<GameObject>();
        foreach (GameObject card in _cardManager.draw._cardsInHand)
        {
            // preguntar para que sirve este codigo
            if (card != cardToPlace)
                newCardsInHand.Add(card);
        }
        _cardManager.draw._cardsInHand.Clear();
        _cardManager.draw._cardsInHand = newCardsInHand;
        _cardManager.draw.AdjustHand();
        //_cardManager.cardToPlace.GetComponentInChildren<ThisCard>().inTable = true;
        _table.SetCard(cardToPlace, positionNum);
        _table.player.SpendMana(cardToPlace.GetComponent<Card>().card.manaCost);
        _table.player.SpendHealth(cardToPlace.GetComponent<Card>().card.healthCost);
        _cardManager.EndPlacing();
        //}

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (_cardManager.placeCards == true && _cardManager.cardToPlace != null && isPlayable)
                SelectThisPosition(_cardManager.cardToPlace);
        }
        else
            _cardManager.CancelPlacing();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //print("AAAAAAAAAAAAAAAAAA");
        if(selectViewer != null)
            selectViewer.SetActive(_cardManager.placeCards);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if(selectViewer != null)
            selectViewer.SetActive(false);
    }
}
