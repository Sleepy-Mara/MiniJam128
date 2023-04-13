using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardMagic : CardCore, IPointerDownHandler
{
    //aca habia un codigo 

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && (playerCard))
        {
            if (_turnManager.CanPlayCards())
            {
                if (_table.player.EnoughMana(card.manaCost) && _table.player.EnoughHealth(card.healthCost))
                {
                    //_cardManager.PlaceCards(gameObject);
                }
            }
        }
    }
}
