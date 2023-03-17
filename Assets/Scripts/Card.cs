using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public CardManager _cardManager;
    public GameObject me;
    public GameObject showedCard;
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
        if (showedCard == null)
        {
            showedCard = Instantiate(me, new Vector3(10000, 10000, 10000), transform.rotation);
            _cardManager.ShowCard(showedCard);
        }
    }

    private void OnMouseExit()
    {
        _cardManager.HideCard(showedCard);
        showedCard = null;
    }
}
