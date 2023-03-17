using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public Transform showCard;
    private bool _placeCards;
    private GameObject _cardToPlace;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_placeCards)
        {
            if(Input.GetMouseButtonDown(1))
            {
                _placeCards = false;
                return;
            }
        }
    }

    public void ShowCard(GameObject card)
    {
        card.GetComponentInChildren<Card>().enabled = false;
        card.transform.position = showCard.position;
        card.transform.rotation = showCard.rotation;
        card.transform.localScale = card.transform.localScale * 2;
    }

    public void HideCard(GameObject card)
    {
        Destroy(card);
    }

    public void PlaceCards(GameObject card)
    {
        _cardToPlace = card;
        _placeCards = true;
    }
}
