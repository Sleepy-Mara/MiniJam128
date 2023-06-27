using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public int currentCurrency;
    public int currentStamina;
    public string nextStaminaTime;
    public string lastStaminaTime;
    public int currentUnlockedLevels;
    public List<SavedCards> currentUnlockedCards;
    public List<SavedCards> currentCardsInDeck;
}
[Serializable]
public class SavedCards
{
    public Cards card;
    public int cardAmount;
}
