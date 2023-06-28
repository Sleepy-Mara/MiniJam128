using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPacks : MonoBehaviour
{
    [SerializeField] private int numberOfCardsInPack;
    [SerializeField] private List<Cards> cardsInPack;
    public void OpenPack()
    {
        for (int i = 0; i < numberOfCardsInPack; i++)
            FindObjectOfType<DeckBuilder>().UnlockCard(cardsInPack[Random.Range(0, cardsInPack.Count)], 1);
    }
}
