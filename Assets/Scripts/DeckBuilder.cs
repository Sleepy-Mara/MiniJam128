using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using Unity.VisualScripting;

public class DeckBuilder : MonoBehaviour
{
    [SerializeField] private CardsInDeckBuilder cardPrefab;
    public List<CardsInDeckBuilder> cardsInDeckBuilder = new List<CardsInDeckBuilder>();
    [HideInInspector] public List<CardsInDeckBuilder> cardsInBloodDeckBuilder = new List<CardsInDeckBuilder>();
    private List<CardsInDeckBuilder> cardsInDeck = new List<CardsInDeckBuilder>();
    private List<CardsInDeckBuilder> cardsInBloodDeck = new List<CardsInDeckBuilder>();
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
    [SerializeField] private Filter filter;
    private List<CardsInDeckBuilder> currentCardOrder;
    private void Awake()
    {
        json = FindObjectOfType<SaveWithJson>();
        LoadCards();
        currentCardOrder = new List<CardsInDeckBuilder>();
    }
    private void Start()
    {
        deck = FindObjectOfType<Draw>();
        foreach (CardsInDeckBuilder cards in cardsInDeckBuilder)
                cards.cover.SetActive(true);
        foreach (CardsInDeckBuilder cards in cardsInDeckBuilder)
            for (int i = 0; i < json.SaveData.currentUnlockedCards.Count; i++)
                if (cards.card.card.name.Contains("_" + json.SaveData.currentUnlockedCards[i].card + "_") ||
                    cards.card.card.name == json.SaveData.currentUnlockedCards[i].card)
                {
                    UnlockCard(cards.card.card, json.SaveData.currentUnlockedCards[i].cardAmount);
                }
        foreach (CardsInDeckBuilder cards in cardsInDeckBuilder)
            for (int i = 0; i < json.SaveData.currentCardsInDeck.Count; i++)
                if (cards.card.card.name.Contains("_" + json.SaveData.currentCardsInDeck[i].card + "_") ||
                    cards.card.card.name == json.SaveData.currentCardsInDeck[i].card)
                {
                    for (int j = 0; j < json.SaveData.currentCardsInDeck[i].cardAmount; j++)
                    {
                        SelectCard(cards);
                    }
                }
        ReloadDeck();
        ChangeOrderOfCards();
    }
    public void LoadCards()
    {
        foreach (Cards card in CardList())
        {
            var newCard = Instantiate(cardPrefab, buildDeck.transform);
            newCard.name = card.name;
            newCard.card.card = card;
            newCard.card.SetData();
            cardsInDeckBuilder.Add(newCard);
        }
    }
    private List<Cards> CardList()
    {
        List<Cards> retirnGeneratedCards = new List<Cards>();
        var tempCards = new List<Cards>();
        foreach (var asset in AssetDatabase.FindAssets("SOC_"))
        {
            var path = AssetDatabase.GUIDToAssetPath(asset);
            if (path.Contains("/Tokens"))
                continue;
            var objects = AssetDatabase.LoadAssetAtPath(path, typeof(Cards));
            tempCards.Add(objects.ConvertTo<Cards>());
        }
        int j = tempCards.Count;
        int lastId = -1;
        for (int i = 0; i < j; i++)
        {
            int acrualId = 0;
            foreach (Cards cards in tempCards)
            {
                int.TryParse(cards.id, out int x);
                if (x > acrualId)
                    acrualId = x;
            }
            Cards cardToRemove = null;
            foreach (Cards cards in tempCards)
            {
                int.TryParse(cards.id, out int y);
                if (y <= acrualId && y > lastId)
                {
                    acrualId = y;
                    cardToRemove = cards;
                }
            }
            if (cardToRemove == null)
                break;
            retirnGeneratedCards.Add(cardToRemove);
            lastId = acrualId;
        }
        return retirnGeneratedCards;
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
        if (deck == null)
            return;
        List<Cards> cardsInDeckTemp = new List<Cards>();
        List<Cards> cardsInBloodDeckTemp = new List<Cards>();
        foreach (var card in cardsInDeck)
            cardsInDeckTemp.Add(card.card.card);
        foreach (var card in cardsInBloodDeck)
            cardsInBloodDeckTemp.Add(card.card.card);
        if (deck.manaDeckObject != null)
        {
            if (cardsInDeck.Count <= 0)
                deck.manaDeckObject.SetActive(false);
            else deck.manaDeckObject.SetActive(true);
        }
        if (deck.bloodDeckObject != null)
        {
            if (cardsInBloodDeck.Count <= 0)
                deck.bloodDeckObject.SetActive(false);
            else deck.bloodDeckObject.SetActive(true);
        }
        deck.deck = new List<Cards>();
        deck.bloodDeck = new List<Cards>();
        deck.deck = cardsInDeckTemp;
        deck.bloodDeck = cardsInBloodDeckTemp;
        deck.ReloadActualDecks();
    }
    public void OpenDeckBuilder()
    {
        foreach (CardsInDeckBuilder cards in cardsInDeckBuilder)
            cards.cover.SetActive(true);
        deckBuilderLayout.SetActive(true);
        ResetDeckBuilder();
        foreach (CardsInDeckBuilder cards in cardsInDeckBuilder)
            for (int i = 0; i < json.SaveData.currentUnlockedCards.Count; i++)
                if (cards.card.card.name.Contains("_" + json.SaveData.currentUnlockedCards[i].card + "_") || 
                    cards.card.card.name == json.SaveData.currentUnlockedCards[i].card)
                {
                    UnlockCard(cards.card.card, json.SaveData.currentUnlockedCards[i].cardAmount);
                }
        foreach (CardsInDeckBuilder cards in cardsInDeckBuilder)
            for (int i = 0; i < json.SaveData.currentCardsInDeck.Count; i++)
                if (cards.card.card.name.Contains("_" + json.SaveData.currentCardsInDeck[i].card + "_") ||
                    cards.card.card.name == json.SaveData.currentCardsInDeck[i].card)
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
            card.NumberOfCards = - card.NumberOfCards;
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
    public void ChangeFilter(int newFilter)
    {
        filter = (Filter)newFilter;
        ChangeOrderOfCards();
    }
    private void ChangeOrderOfCards()
    {
        switch (filter)
        {
            case Filter.Original:
                foreach (var card in cardsInDeckBuilder)
                {
                    card.transform.SetSiblingIndex(cardsInDeckBuilder.IndexOf(card));
                    currentCardOrder.Add(card);
                }
                break;
            case Filter.Name:
                List<CardsInDeckBuilder> cardsName = new List<CardsInDeckBuilder>();
                List<string> cardsNames = new List<string>();
                foreach (CardsInDeckBuilder card in cardsInDeckBuilder)
                    cardsNames.Add(card.card.card.name);
                cardsNames.Sort();
                foreach (string cardName in cardsNames)
                    foreach (CardsInDeckBuilder card in cardsInDeckBuilder)
                        if (cardName == card.card.card.name)
                            cardsName.Add(card);
                foreach (var card in cardsName)
                    card.transform.SetSiblingIndex(cardsName.IndexOf(card));
                break;
            case Filter.ManaCost:
                List<CardsInDeckBuilder> cardsManaCost = new List<CardsInDeckBuilder>();
                while (cardsManaCost.Count < cardsInDeckBuilder.Count)
                {
                    CardsInDeckBuilder lastFirstCard = null;
                    foreach (var card in cardsInDeckBuilder)
                    {
                        if (cardsManaCost.Contains(card) || card.card.card.healthCost > 0)
                            continue;
                        if(lastFirstCard == null)
                        {
                            lastFirstCard = card;
                            continue;
                        }
                        if (card.card.card.manaCost < lastFirstCard.card.card.manaCost)
                            lastFirstCard = card;
                    }
                    cardsManaCost.Add(lastFirstCard);
                    foreach (var card in cardsInDeckBuilder)
                    {
                        if (cardsManaCost.Contains(card) || card.card.card.manaCost > 0)
                            continue;
                        if (lastFirstCard == null)
                        {
                            lastFirstCard = card;
                            continue;
                        }
                        if (card.card.card.healthCost < lastFirstCard.card.card.healthCost)
                            lastFirstCard = card;
                    }
                    cardsManaCost.Add(lastFirstCard);
                }
                foreach (var card in cardsManaCost)
                    card.transform.SetSiblingIndex(cardsManaCost.IndexOf(card));
                break;
            case Filter.LifeCost:
                List<CardsInDeckBuilder> cardsLifeCost = new List<CardsInDeckBuilder>();
                while (cardsLifeCost.Count < cardsInDeckBuilder.Count)
                {
                    CardsInDeckBuilder lastFirstCard = null;
                    foreach (var card in cardsInDeckBuilder)
                    {
                        if (cardsLifeCost.Contains(card) || card.card.card.manaCost > 0)
                            continue;
                        if (lastFirstCard == null)
                        {
                            lastFirstCard = card;
                            continue;
                        }
                        if (card.card.card.healthCost < lastFirstCard.card.card.healthCost)
                            lastFirstCard = card;
                    }
                    cardsLifeCost.Add(lastFirstCard);
                    foreach (var card in cardsInDeckBuilder)
                    {
                        if (cardsLifeCost.Contains(card) || card.card.card.healthCost > 0)
                            continue;
                        if (lastFirstCard == null)
                        {
                            lastFirstCard = card;
                            continue;
                        }
                        if (card.card.card.manaCost < lastFirstCard.card.card.manaCost)
                            lastFirstCard = card;
                    }
                    cardsLifeCost.Add(lastFirstCard);
                }
                foreach (var card in cardsLifeCost)
                    card.transform.SetSiblingIndex(cardsLifeCost.IndexOf(card));
                break;
            case Filter.Damage:
                List<CardsInDeckBuilder> cardsDamage = new List<CardsInDeckBuilder>();
                while (cardsDamage.Count < cardsInDeckBuilder.Count)
                {
                    CardsInDeckBuilder lastFirstCard = null;
                    foreach (var card in cardsInDeckBuilder)
                    {
                        if (cardsDamage.Contains(card))
                            continue;
                        if (lastFirstCard == null)
                        {
                            lastFirstCard = card;
                            continue;
                        }
                        if (card.card.card.attack > lastFirstCard.card.card.attack)
                            lastFirstCard = card;
                    }
                    cardsDamage.Add(lastFirstCard);
                }
                foreach (var card in cardsDamage)
                    card.transform.SetSiblingIndex(cardsDamage.IndexOf(card));
                break;
            case Filter.Health:
                List<CardsInDeckBuilder> cardsHealth = new List<CardsInDeckBuilder>();
                while (cardsHealth.Count < cardsInDeckBuilder.Count)
                {
                    CardsInDeckBuilder lastFirstCard = null;
                    foreach (var card in cardsInDeckBuilder)
                    {
                        if (cardsHealth.Contains(card))
                            continue;
                        if (lastFirstCard == null)
                        {
                            lastFirstCard = card;
                            continue;
                        }
                        if (card.card.card.life > lastFirstCard.card.card.life)
                            lastFirstCard = card;
                    }
                    cardsHealth.Add(lastFirstCard);
                }
                foreach (var card in cardsHealth)
                    card.transform.SetSiblingIndex(cardsHealth.IndexOf(card));
                break;
            case Filter.Effect:
                List<string> effects = new Effects().effects;
                List<CardsInDeckBuilder> cardsEffect = new List<CardsInDeckBuilder>();
                List<CardsInDeckBuilder> cardsWhitoutEffect = new List<CardsInDeckBuilder>();
                foreach (string effect in effects)
                    foreach (var card in cardsInDeckBuilder)
                        if (card.card.card.hasEffect)
                        {
                            if (card.card.card.effect.Contains(effect) && !cardsEffect.Contains(card))
                                cardsEffect.Add(card);
                        } else cardsWhitoutEffect.Add(card);
                foreach (var card in cardsWhitoutEffect)
                    cardsEffect.Add(card);
                foreach (var card in cardsEffect)
                    card.transform.SetSiblingIndex(cardsEffect.IndexOf(card));
                break;
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
        SaveData saveData = json.SaveData;
        saveData.currentCardsInDeck.Clear();
        foreach (CardsInDeckBuilder card in cardsInDeck)
        {
            SavedCards savedCards = new SavedCards() { card = card.card.card.name, cardAmount = card.NumberOfCards };
            saveData.currentCardsInDeck.Add(savedCards);
        }
        saveData.currentUnlockedCards.Clear();
        foreach (CardsInDeckBuilder card in cardsInDeckBuilder)
        {
            if (!card.cover.activeSelf)
            {
                SavedCards savedCards = new SavedCards() { card = card.card.card.name, cardAmount = card.NumberOfCards };
                saveData.currentUnlockedCards.Add(savedCards);
            }
        }
        json.SaveData = saveData;
    }
}
public enum Filter
{
    Original,
    Name,
    ManaCost,
    LifeCost,
    Damage,
    Health,
    Effect,
}
