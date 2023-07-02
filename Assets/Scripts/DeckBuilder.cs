using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeckBuilder : MonoBehaviour
{
    public List<CardsInDeckBuilder> cardsInDeckBuilder;
    public List<CardsInDeckBuilder> cardsInBloodDeckBuilder;
    public List<CardsInDeckBuilder> cardsInDeck;
    static List<CardsInDeckBuilder> savedCardsInDeck;
    public List<CardsInDeckBuilder> cardsInBloodDeck;
    static List<CardsInDeckBuilder> savedCardsInBloodDeck;
    private static DeckBuilder instance;
    [SerializeField] private int maxCardsInNormalDeck;
    [SerializeField] private int minCardsInNormalDeck;
    [SerializeField] private int maxCardsInBloodDeck;
    [SerializeField] private int minCardsInBloodDeck;
    [SerializeField] private Draw deck;
    [SerializeField] private int originalCameraHeight;
    [SerializeField] private int originalCameraWidth;
    private int currentCameraHeight;
    private int currentCameraWidth;
    private Camera mainCamera;
    [SerializeField] private GridLayoutGroup buildDeck;
    [SerializeField] private GridLayoutGroup currentDeck;
    [SerializeField] private GameObject cardInDeck;
    [SerializeField] private GameObject deckBuilderLayout;
    [SerializeField] private Animator minCardsInNormalDeckWarning;
    [SerializeField] private Animator maxCardsInNormalDeckWarning;
    [SerializeField] private Animator minCardsInBloodDeckWarning;
    [SerializeField] private Animator maxCardsInBloodDeckWarning;
    private SaveWithJson json;
    private void Awake()
    {
        json = FindObjectOfType<SaveWithJson>();
        foreach (CardsInDeckBuilder cards in cardsInDeckBuilder)
            cards.cover.SetActive(true);
    }
    private void Start()
    {
    }
    public void UnlockCard(Cards newCard, int number)
    {
        foreach (CardsInDeckBuilder card in cardsInDeckBuilder)
            if (card.card.card.name == newCard.name)
            {
                card.numberText.gameObject.SetActive(true);
                card.NumberOfCards = number;
                card.cover.SetActive(false);
                bool alreadyUnlocked = false;
                SaveData saveData = json.SaveData;
                for (int i = 0; i < json.SaveData.currentUnlockedCards.Count; i++)
                    if(newCard.name == json.SaveData.currentUnlockedCards[i].card)
                    {
                        saveData.currentUnlockedCards[i].cardAmount = card.NumberOfCards;
                        alreadyUnlocked = true;
                    }
                if(!alreadyUnlocked)
                {
                    SavedCards savedCards = new SavedCards() { card = newCard.name, cardAmount = card.NumberOfCards };
                    saveData.currentUnlockedCards.Add(savedCards);
                }
                json.SaveData = saveData;
            }
        foreach (CardsInDeckBuilder card in cardsInBloodDeckBuilder)
            if (card.card.card.name == newCard.name)
            {
                card.numberText.gameObject.SetActive(true);
                card.NumberOfCards = number;
                card.cover.SetActive(false);
                bool alreadyUnlocked = false;
                SaveData saveData = json.SaveData;
                for (int i = 0; i < json.SaveData.currentUnlockedCards.Count; i++)
                    if (newCard.name == json.SaveData.currentUnlockedCards[i].card)
                    {
                        saveData.currentUnlockedCards[i].cardAmount = card.NumberOfCards;
                        alreadyUnlocked = true;
                    }
                if (!alreadyUnlocked)
                {
                    SavedCards savedCards = new SavedCards() { card = newCard.name, cardAmount = card.NumberOfCards };
                    saveData.currentUnlockedCards.Add(savedCards);
                }
                json.SaveData = saveData;
            }
    }
    public void SelectCard(CardsInDeckBuilder selectedCard)
    {
        foreach (CardsInDeckBuilder card in cardsInDeckBuilder)
            if (card.card.card.name == selectedCard.card.card.name)
            {
                Debug.Log("aaaa");
                bool inDeck = false;
                CardsInDeckBuilder newCardInDeck = null;
                if(cardsInDeck.Count > 0)
                    foreach (CardsInDeckBuilder cards in cardsInDeck)
                        if (cards.card.card.name == selectedCard.card.card.name)
                        {
                            inDeck = true;
                            newCardInDeck = cards;
                        }
                if (!inDeck)
                {
                    CardsInDeckBuilder newCard = Instantiate(cardInDeck, currentDeck.transform).GetComponent<CardsInDeckBuilder>();
                    newCard.numberText.gameObject.SetActive(true);
                    newCard.card.card = selectedCard.card.card;
                    newCard.card.SetData();
                    newCard.inDeck = true;
                    newCard.NumberOfCards = 1;
                    newCardInDeck = newCard;
                }
                else
                    newCardInDeck.NumberOfCards = 1;
                cardsInDeck.Add(newCardInDeck);
                if (deck != null)
                    deck.deck.Add(newCardInDeck.card.card);
                break;
            }
        foreach (CardsInDeckBuilder card in cardsInBloodDeckBuilder)
            if (card.card.card.name == selectedCard.card.card.name)
            {
                Debug.Log("eeee");
                bool inDeck = false;
                CardsInDeckBuilder newCardInDeck = null;
                if (cardsInBloodDeck.Count > 0)
                    foreach (CardsInDeckBuilder cards in cardsInBloodDeck)
                        if (cards.card.card.name == selectedCard.card.card.name)
                        {
                            inDeck = true;
                            cards.NumberOfCards = 1;
                            newCardInDeck = cards;
                        }
                if (!inDeck)
                {
                    CardsInDeckBuilder newCard = Instantiate(cardInDeck, currentDeck.transform).GetComponent<CardsInDeckBuilder>();
                    newCard.numberText.gameObject.SetActive(true);
                    newCard.card.card = selectedCard.card.card;
                    newCard.inDeck = true;
                    newCard.NumberOfCards = 1;
                    newCardInDeck = newCard;
                }
                cardsInBloodDeck.Add(newCardInDeck);
                if (deck != null)
                    deck.bloodDeck.Add(newCardInDeck.card.card);
                break;
            }
    }
    public void UnselectedCard(CardsInDeckBuilder selectedCard)
    {
        foreach (CardsInDeckBuilder card in cardsInDeck)
            if (card.card.card.name == selectedCard.card.card.name)
            {
                foreach (CardsInDeckBuilder cards in cardsInDeckBuilder)
                    if (cards.card.card.name == selectedCard.card.card.name)
                        cards.NumberOfCards = 1;
                cardsInDeck.Remove(selectedCard);
                if (deck != null)
                    deck.deck.Remove(selectedCard.card.card);
                if (selectedCard.NumberOfCards < 1)
                    Destroy(selectedCard.gameObject);
                return;
            }
        foreach (CardsInDeckBuilder card in cardsInBloodDeckBuilder)
            if (card.card.card.name == selectedCard.card.card.name)
            {
                foreach (CardsInDeckBuilder cards in cardsInDeckBuilder) 
                    if (cards.card.card.name == selectedCard.card.card.name)
                        cards.NumberOfCards = 1;
                cardsInBloodDeck.Remove(selectedCard);
                if (deck != null)
                    deck.bloodDeck.Remove(selectedCard.card.card);
                if (selectedCard.NumberOfCards < 1)
                    Destroy(selectedCard.gameObject);
                return;
            }
    }
    private void ReloadDeck()
    {
        List<Cards> cardsInDeckTemp = new List<Cards>();
        List<Cards> cardsInBloodDeckTemp = new List<Cards>();
        foreach (var card in cardsInDeck)
            cardsInDeckTemp.Add(card.card.card);
        foreach (var card in cardsInBloodDeck)
            cardsInBloodDeckTemp.Add(card.card.card);
        if (deck != null)
        {
            if (cardsInDeck.Count <= 0)
                deck.manaDeckObject.SetActive(false);
            else deck.manaDeckObject.SetActive(true);
            if (cardsInBloodDeck.Count <= 0)
                deck.bloodDeckObject.SetActive(false);
            else deck.bloodDeckObject.SetActive(true);
            deck.deck = new List<Cards>();
            deck.bloodDeck = new List<Cards>();
            deck.deck = cardsInDeckTemp;
            deck.bloodDeck = cardsInBloodDeckTemp;
            deck.ReloadActualDecks();
        }
    }
    public void OpenDeckBuilder()
    {
        deckBuilderLayout.SetActive(true);
        foreach (CardsInDeckBuilder cards in cardsInDeckBuilder)
            for (int i = 0; i < json.SaveData.currentUnlockedCards.Count; i++)
                if (json.SaveData.currentUnlockedCards[i].card == cards.card.card.name)
                {
                    UnlockCard(cards.card.card, json.SaveData.currentUnlockedCards[i].cardAmount);
                }
        foreach (CardsInDeckBuilder cards in cardsInDeckBuilder)
            for (int i = 0; i < json.SaveData.currentCardsInDeck.Count; i++)
                if (json.SaveData.currentCardsInDeck[i].card == cards.card.card.name)
                {
                    for (int j = 0; j < json.SaveData.currentCardsInDeck[i].cardAmount; j++)
                    {
                        SelectCard(cards);
                    }
                }
        ReloadDeck();
    }
    public void CloseDeckBuilder()
    {
        deckBuilderLayout.SetActive(false);
        ResetDeckBuilder();
    }
    void ResetDeckBuilder()
    {
        foreach (CardsInDeckBuilder card in cardsInDeckBuilder)
        {
            card.NumberOfCards = - card.NumberOfCards;
            card.numberText.gameObject.SetActive(false);
            card.cover.SetActive(true);
        }
        foreach (CardsInDeckBuilder card in cardsInBloodDeckBuilder)
        {
            card.NumberOfCards = -card.NumberOfCards;
            card.numberText.gameObject.SetActive(false);
            card.cover.SetActive(true);
        }
        int x = cardsInDeck.Count, y = cardsInBloodDeck.Count;
        for (int i = 0; i < x; i++)
        {
            var card = cardsInDeck[0];
            cardsInDeck.Remove(card);
            Destroy(card.gameObject);
        }
        for (int i = 0; i < y; i++)
        {
            var card = cardsInBloodDeck[0];
            cardsInBloodDeck.Remove(card);
            Destroy(card.gameObject);
        }
    }
    public void SaveData()
    {
        if (cardsInDeck.Count > maxCardsInNormalDeck)
        {
            maxCardsInNormalDeckWarning.SetTrigger("Activate");
            return;
        }
        if (cardsInDeck.Count < minCardsInNormalDeck)
        {
            minCardsInNormalDeckWarning.SetTrigger("Activate");
            return;
        }
        if (cardsInBloodDeck.Count > maxCardsInBloodDeck)
        {
            maxCardsInBloodDeckWarning.SetTrigger("Activate");
            return;
        }
        if (cardsInBloodDeck.Count < minCardsInBloodDeck)
        {
            minCardsInBloodDeckWarning.SetTrigger("Activate");
            return;
        }
        ReloadDeck();
        foreach (CardsInDeckBuilder card in cardsInDeck)
        {
            if (card.cover.activeSelf)
                continue;
            bool alreadyUnlocked = false;
            SaveData saveData = json.SaveData;
            for (int i = 0; i < json.SaveData.currentCardsInDeck.Count; i++)
                if (card.card.card.name == json.SaveData.currentCardsInDeck[i].card)
                {
                    saveData.currentCardsInDeck[i].cardAmount = card.NumberOfCards;
                    alreadyUnlocked = true;
                }
            if (!alreadyUnlocked)
            {
                SavedCards savedCards = new SavedCards() { card = card.card.card.name, cardAmount = card.NumberOfCards };
                saveData.currentCardsInDeck.Add(savedCards);
            }
            json.SaveData = saveData;
        }
        foreach (CardsInDeckBuilder card in cardsInDeckBuilder)
        {
            if (card.cover.activeSelf)
                continue;
            bool alreadyUnlocked = false;
            SaveData saveData = json.SaveData;
            for (int i = 0; i < json.SaveData.currentUnlockedCards.Count; i++)
                if (card.card.card.name == json.SaveData.currentUnlockedCards[i].card)
                {
                    saveData.currentUnlockedCards[i].cardAmount = card.NumberOfCards;
                    alreadyUnlocked = true;
                }
            if (!alreadyUnlocked)
            {
                SavedCards savedCards = new SavedCards() { card = card.card.card.name, cardAmount = card.NumberOfCards };
                saveData.currentUnlockedCards.Add(savedCards);
            }
            json.SaveData = saveData;
        }
    }
}
