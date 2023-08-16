using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMagic : CardCore
{
    // modificar esto para que haya que moverlo a cualquier parte del escenario
    protected override void SelectCard()
    {
        StartCoroutine(PlayEffect());
        _cardManager.EndPlacing();
    }

    IEnumerator PlayEffect()
    {
        foreach (MapPosition card in FindObjectOfType<Table>().playerPositions)
            _effectManager.CheckConditionSpellPlayed(this);
        _table.player.SpendMana(card.manaCost);
        _table.player.SpendHealth(card.healthCost);
        List<GameObject> newCardsInHand = new List<GameObject>();
        foreach (GameObject card in _cardManager.draw._cardsInHand)
        {
            if (card != gameObject)
                newCardsInHand.Add(card);
        }
        _cardManager.draw._cardsInHand.Clear();
        _cardManager.draw._cardsInHand = newCardsInHand;
        _cardManager.draw.AdjustHand();
        checkingEffect = true;
        _effectManager.CheckConditionIsPlayed(this);
        yield return new WaitUntil(() => checkingEffect == false);
        //foreach (Cemetery cemetery in FindObjectsOfType<Cemetery>())
        //{
        //    if (cemetery.player)
        //        cemetery.AddCard(card);
        //}
        //FindObjectOfType<CardToCemeteryAnimation>().AddCard(card, null, true);
        Destroy(gameObject);
    }
}
