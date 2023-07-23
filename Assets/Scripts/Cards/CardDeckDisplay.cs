using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDeckDisplay : CardDisplay
{
    public override void SetData()
    {
        manaCostText.text = card.manaCost.ToString();
        healthCostText.text = card.healthCost.ToString();
        UpdateLanguage(FindObjectOfType<LanguageManager>().languageNumber);
    }
    public override void UpdateLanguage(int languageNumber)
    {
        nameText.text = card.cardName[languageNumber];
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
    }
}
