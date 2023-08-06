using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPacks : MonoBehaviour
{
    [SerializeField] private Transform listOfUnlocked;
    [SerializeField] private GameObject card;
    [SerializeField] private int numberOfCardsInPack;
    [SerializeField] private List<Cards> cardsInPack;
    [SerializeField] int cost;
    private CurrencyManager currencyManager;
    private void Awake()
    {
        currencyManager = FindObjectOfType<CurrencyManager>();
    }
    public void OpenPack()
    {
        if (currencyManager.Currency < cost)
            return;
        SaveWithJson json = FindObjectOfType<SaveWithJson>();
        SaveData saveData = json.SaveData;
        saveData.currentCurrency = currencyManager.Currency - cost;
        json.SaveData = saveData;
        currencyManager.Currency = -cost;
        foreach(CardDisplay card in listOfUnlocked.GetComponentsInChildren<CardDisplay>())
            Destroy(card.gameObject);
        for (int i = 0; i < numberOfCardsInPack; i++)
        {
            var unlockedCard = cardsInPack[Random.Range(0, cardsInPack.Count)];
            CardDisplay newCard = Instantiate(card, listOfUnlocked).GetComponent<CardDisplay>();
            newCard.card = unlockedCard;
            newCard.SetData();
            FindObjectOfType<DeckBuilder>().UnlockCard(unlockedCard, 1);
        }
    }
}
