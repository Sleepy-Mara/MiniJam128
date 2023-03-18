using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private CardManager _cardManager;
    public GameObject me;
    private GameObject _showedCard;
    [HideInInspector] public bool inTable;
    [HideInInspector] public bool canPlay;
    void Start()
    {
        _cardManager = FindObjectOfType<CardManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        if (_showedCard == null && !inTable)
        {
            _showedCard = Instantiate(me, new Vector3(10000, 10000, 10000), transform.rotation);
            _cardManager.ShowCard(_showedCard);
        }
    }

    private void OnMouseExit()
    {
        _cardManager.HideCard(_showedCard);
        _showedCard = null;
    }

    private void OnMouseDown()
    {
        if (!inTable && canPlay)
            _cardManager.PlaceCards(gameObject.transform.parent.gameObject);
    }
}
