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
    private int actualCameraHeight;
    private int actualCameraWidth;
    private Camera mainCamera;
    [SerializeField] private GridLayoutGroup buildDeck;
    [SerializeField] private GridLayoutGroup actualDeck;
    [SerializeField] private GameObject cardInDeck;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            savedCardsInDeck = cardsInDeck;
            savedCardsInBloodDeck = cardsInBloodDeck;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
        cardsInDeck = savedCardsInDeck;
        cardsInBloodDeck = savedCardsInBloodDeck;
        List<Cards> cardsInDeckTemp = new List<Cards>();
        List<Cards> cardsInBloodDeckTemp = new List<Cards>();
        foreach (var card in cardsInDeck)
            cardsInDeckTemp.Add(card.card.card);
        foreach (var card in cardsInBloodDeck)
            cardsInBloodDeckTemp.Add(card.card.card);
        if (deck != null)
        {
            deck.deck = cardsInDeckTemp;
            deck.bloodDeck = cardsInBloodDeckTemp;
        }
        mainCamera = FindObjectOfType<Camera>();
        Debug.Log(mainCamera.pixelWidth);
        Debug.Log(mainCamera.pixelHeight);
        buildDeck.spacing = new Vector2(buildDeck.spacing.x * ((float)mainCamera.pixelWidth / (float)originalCameraWidth),
            buildDeck.spacing.y * ((float)mainCamera.pixelHeight / (float)originalCameraHeight));
        buildDeck.cellSize = new Vector2(buildDeck.cellSize.x * ((float)mainCamera.pixelWidth / (float)originalCameraWidth),
            buildDeck.cellSize.y * ((float)mainCamera.pixelHeight / (float)originalCameraHeight));
        actualDeck.spacing = new Vector2(actualDeck.spacing.x * ((float)mainCamera.pixelWidth / (float)originalCameraWidth),
            actualDeck.spacing.y * ((float)mainCamera.pixelHeight / (float)originalCameraHeight));
        actualDeck.cellSize = new Vector2(actualDeck.cellSize.x * ((float)mainCamera.pixelWidth / (float)originalCameraWidth),
            actualDeck.cellSize.y * ((float)mainCamera.pixelHeight / (float)originalCameraHeight));
        actualCameraHeight = mainCamera.pixelHeight;
        actualCameraWidth = mainCamera.pixelWidth;
    }
    public void UnlockCard(Card newCard)
    {
        foreach (CardsInDeckBuilder card in cardsInDeckBuilder)
            if (card.card.card.cardName == newCard.card.cardName)
            {
                card.NumberOfCards = 1;
                card.mysteryCard.SetActive(false);
            }
        foreach (CardsInDeckBuilder card in cardsInBloodDeckBuilder)
            if (card.card.card.cardName == newCard.card.cardName)
            {
                card.NumberOfCards = 1;
                card.mysteryCard.SetActive(false);
            }
    }
    public void SelectCard(CardsInDeckBuilder selectedCard)
    {
        Debug.Log("aaaa");
        foreach (CardsInDeckBuilder card in cardsInDeckBuilder)
            if (card.card.card.cardName == selectedCard.card.card.cardName)
            {
                bool inDeck = false;
                CardsInDeckBuilder newCardInDeck = null;
                if(cardsInDeck.Count > 0)
                    foreach (CardsInDeckBuilder cards in cardsInDeck)
                        if (cards.card.card.cardName == selectedCard.card.card.cardName)
                        {
                            inDeck = true;
                            newCardInDeck = cards;
                        }
                if (!inDeck)
                {
                    CardsInDeckBuilder newCard = Instantiate(cardInDeck, actualDeck.transform).GetComponent<CardsInDeckBuilder>();
                    newCard.card.card = selectedCard.card.card;
                    newCard.card.SetData();
                    newCard.inDeck = true;
                    newCard.NumberOfCards = 1;
                    newCardInDeck = newCard;
                }
                else
                    newCardInDeck.NumberOfCards = 1;
                cardsInDeck.Add(newCardInDeck);
                savedCardsInDeck.Add(newCardInDeck);
                if (deck != null)
                    deck.deck.Add(newCardInDeck.card.card);
            }
        foreach (CardsInDeckBuilder card in cardsInBloodDeckBuilder)
            if (card.card.card.cardName == selectedCard.card.card.cardName)
            {
                bool inDeck = false;
                CardsInDeckBuilder newCardInDeck = null;
                if (cardsInBloodDeck.Count > 0)
                    foreach (CardsInDeckBuilder cards in cardsInBloodDeck)
                        if (cards.card.card.cardName == selectedCard.card.card.cardName)
                        {
                            inDeck = true;
                            cards.NumberOfCards = 1;
                            newCardInDeck = cards;
                        }
                if (!inDeck)
                {
                    CardsInDeckBuilder newCard = Instantiate(cardInDeck, actualDeck.transform).GetComponent<CardsInDeckBuilder>();
                    newCard.card.card = selectedCard.card.card;
                    newCard.inDeck = true;
                    newCard.NumberOfCards = 1;
                    newCardInDeck = newCard;
                }
                cardsInDeck.Add(newCardInDeck);
                savedCardsInDeck.Add(newCardInDeck);
                if (deck != null)
                    deck.bloodDeck.Add(newCardInDeck.card.card);
            }
    }
    public void UnselectedCard(CardsInDeckBuilder selectedCard)
    {
        foreach (CardsInDeckBuilder card in cardsInDeck)
            if (card.card.card.cardName == selectedCard.card.card.cardName)
            {
                foreach (CardsInDeckBuilder cards in cardsInDeckBuilder)
                    if (cards.card.card.cardName == selectedCard.card.card.cardName)
                        cards.NumberOfCards = 1;
                cardsInDeck.Remove(selectedCard);
                savedCardsInDeck.Remove(selectedCard);
                if (deck != null)
                    deck.deck.Remove(selectedCard.card.card);
                if (selectedCard.NumberOfCards < 1)
                    Destroy(selectedCard.gameObject);
                return;
            }
        foreach (CardsInDeckBuilder card in cardsInBloodDeckBuilder)
            if (card.card.card.cardName == selectedCard.card.card.cardName)
            {
                foreach (CardsInDeckBuilder cards in cardsInDeckBuilder) 
                    if (cards.card.card.cardName == selectedCard.card.card.cardName)
                        cards.NumberOfCards = 1;
                cardsInBloodDeck.Remove(selectedCard);
                savedCardsInBloodDeck.Remove(selectedCard);
                if (deck != null)
                    deck.bloodDeck.Remove(selectedCard.card.card);
                if (selectedCard.NumberOfCards < 1)
                    Destroy(selectedCard.gameObject);
                return;
            }
    }
    private void Update()
    {
        if (actualCameraHeight != mainCamera.pixelHeight || actualCameraWidth != mainCamera.pixelWidth)
        {
            buildDeck.spacing = new Vector2(buildDeck.spacing.x * ((float)mainCamera.pixelWidth / (float)actualCameraWidth),
                buildDeck.spacing.y * ((float)mainCamera.pixelHeight / (float)actualCameraHeight));
            buildDeck.cellSize = new Vector2(buildDeck.cellSize.x * ((float)mainCamera.pixelWidth / (float)actualCameraWidth),
                buildDeck.cellSize.y * ((float)mainCamera.pixelHeight / (float)actualCameraHeight));
            actualDeck.spacing = new Vector2(actualDeck.spacing.x * ((float)mainCamera.pixelWidth / (float)actualCameraWidth),
                actualDeck.spacing.y * ((float)mainCamera.pixelHeight / (float)actualCameraHeight));
            actualDeck.cellSize = new Vector2(actualDeck.cellSize.x * ((float)mainCamera.pixelWidth / (float)actualCameraWidth),
                actualDeck.cellSize.y * ((float)mainCamera.pixelHeight / (float)actualCameraHeight));
            actualCameraHeight = mainCamera.pixelHeight;
            actualCameraWidth = mainCamera.pixelWidth;
        }
    }
}
