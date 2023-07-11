using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCard : MonoBehaviour
{
    private DeckBuilder deckBuilder;
    void Start()
    {
        StartCoroutine(enumerator());
    }
    IEnumerator enumerator()
    {
        yield return new WaitForSeconds(0.2f);
        deckBuilder = FindObjectOfType<DeckBuilder>();
        List<CardsInDeckBuilder> cards = new List<CardsInDeckBuilder>();
        foreach (CardsInDeckBuilder card in deckBuilder.cardsInDeckBuilder)
            if (!card.cover.activeSelf)
                cards.Add(card);
        GetComponent<CardCore>().card = cards[Random.Range(0, cards.Count)].card.card;
        GetComponent<CardCore>().SetData();
    }
    void Update()
    {
        
    }
}
