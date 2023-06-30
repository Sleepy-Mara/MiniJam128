using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPacks : MonoBehaviour
{
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
        currencyManager.Currency = -cost;
        for (int i = 0; i < numberOfCardsInPack; i++)
            FindObjectOfType<DeckBuilder>().UnlockCard(cardsInPack[Random.Range(0, cardsInPack.Count)], 1);
    }
}
