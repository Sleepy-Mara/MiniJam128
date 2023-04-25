using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardMagic : CardCore, IPointerDownHandler
{
    //aca habia un codigo 

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("a");
            if (_turnManager.CanPlayCards())
            {
                Debug.Log("c");
                if (_table.player.EnoughMana(card.manaCost) && _table.player.EnoughHealth(card.healthCost))
                {
                    Debug.Log("d");
                    //_cardManager.PlaceCards(gameObject);
                    StartCoroutine(PlayEffect());
                }
            }
    }

    IEnumerator PlayEffect()
    {
        foreach (MapPosition card in FindObjectOfType<Table>().playerPositions)
            _effectManager.CheckConditionSpellPlayed(card.card);
        _table.player.SpendMana(card.manaCost);
        _table.player.SpendHealth(card.healthCost);
        List<GameObject> newCardsInHand = new List<GameObject>();
        foreach (GameObject card in _cardManager.draw._cardsInHand)
        {
            // preguntar para que sirve este codigo
            if (card != gameObject)
            {
                newCardsInHand.Add(card);
                Debug.Log("Added" + card.name);
            }
            else Debug.Log("Ignored" + card.name);
        }
        _cardManager.draw._cardsInHand.Clear();
        _cardManager.draw._cardsInHand = newCardsInHand;
        _cardManager.draw.AdjustHand();
        checkingEffect = true;
        _effectManager.CheckConditionIsPlayed(this);
        yield return new WaitUntil(() => checkingEffect == false);
        foreach (Cemetery cemetery in FindObjectsOfType<Cemetery>())
        {
            if (cemetery.player)
                cemetery.AddCard(card);
        }
        FindObjectOfType<CardToCemeteryAnimation>().AddCard(card, null, true);
        Destroy(gameObject);
    }
}
