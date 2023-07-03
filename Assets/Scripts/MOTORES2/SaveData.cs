using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public bool firstPlay;
    public int currentCurrency;
    public int currentStamina;
    public string nextStaminaTime;
    public string lastStaminaTime;
    public int currentUnlockedLevels;
    public float musicVolume;
    public float sfxVolume;
    public List<SavedCards> currentUnlockedCards;
    public List<SavedCards> currentCardsInDeck;
}
[Serializable]
public class SavedCards
{
    public string card;
    public int cardAmount;
}
