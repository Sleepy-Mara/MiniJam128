using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeckBuilder : MonoBehaviour
{
    public List<CardsInDeckBuilder> cardsInDeckBuilder;
    public List<CardsInDeckBuilder> cardsInBloodDeckBuilder;
    public List<Cards> cardsInDeck;
    static List<Cards> savedCardsInDeck;
    public List<Cards> cardsInBloodDeck;
    static List<Cards> savedCardsInBloodDeck;
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
    private Camera camera;
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
        if (deck != null)
        {
            deck.deck = cardsInDeck;
            deck.bloodDeck = cardsInBloodDeck;
        }
        camera = FindObjectOfType<Camera>();
        Debug.Log(camera.pixelWidth);
        Debug.Log(camera.pixelHeight);
        GetComponentInChildren<GridLayoutGroup>().spacing = new Vector2(GetComponentInChildren<GridLayoutGroup>().spacing.x * ((float)camera.pixelWidth / (float)originalCameraWidth),
            GetComponentInChildren<GridLayoutGroup>().spacing.y * ((float)camera.pixelHeight / (float)originalCameraHeight));
        GetComponentInChildren<GridLayoutGroup>().cellSize = new Vector2(GetComponentInChildren<GridLayoutGroup>().cellSize.x * ((float)camera.pixelWidth / (float)originalCameraWidth),
            GetComponentInChildren<GridLayoutGroup>().cellSize.y * ((float)camera.pixelHeight / (float)originalCameraHeight));
        actualCameraHeight = camera.pixelHeight;
        actualCameraWidth = camera.pixelWidth;
    }
    public void UnlockCard(Card newCard)
    {
        foreach (CardsInDeckBuilder card in cardsInDeckBuilder)
            if (card.card.card.name == newCard.card.name)
            {
                card.NumberOfCards = 1;
                card.mysteryCard.SetActive(false);
            }
        foreach (CardsInDeckBuilder card in cardsInBloodDeckBuilder)
            if (card.card.card.name == newCard.card.name)
            {
                card.NumberOfCards = 1;
                card.mysteryCard.SetActive(false);
            }
    }
    public void SelectCard(CardCore newCard)
    {
        foreach (CardsInDeckBuilder card in cardsInDeckBuilder)
            if (card.card.card.name == newCard.card.name)
            {
                cardsInDeck.Add(newCard.card);
                savedCardsInDeck.Add(newCard.card);
                if (deck != null)
                    deck.deck.Add(newCard.card);
            }
        foreach (CardsInDeckBuilder card in cardsInBloodDeckBuilder)
            if (card.card.card.name == newCard.card.name)
            {
                cardsInBloodDeck.Add(newCard.card);
                savedCardsInBloodDeck.Add(newCard.card);
                if (deck != null)
                    deck.bloodDeck.Add(newCard.card);
            }
    }
    public void UnselectedCard(Card newCard)
    {
        foreach (CardsInDeckBuilder card in cardsInDeckBuilder)
            if (card.card.card.name == newCard.card.name)
            {
                cardsInDeck.Remove(newCard.card);
                savedCardsInDeck.Remove(newCard.card);
                if (deck != null)
                    deck.deck.Remove(newCard.card);
            }
        foreach (CardsInDeckBuilder card in cardsInBloodDeckBuilder)
            if (card.card.card.name == newCard.card.name)
            {
                cardsInBloodDeck.Remove(newCard.card);
                savedCardsInBloodDeck.Remove(newCard.card);
                if (deck != null)
                    deck.bloodDeck.Remove(newCard.card);
            }
    }
    private void Update()
    {
        if (actualCameraHeight != camera.pixelHeight || actualCameraWidth != camera.pixelWidth)
        {
            GetComponentInChildren<GridLayoutGroup>().spacing = new Vector2(GetComponentInChildren<GridLayoutGroup>().spacing.x * ((float)camera.pixelWidth / (float)actualCameraWidth),
                GetComponentInChildren<GridLayoutGroup>().spacing.y * ((float)camera.pixelHeight / (float)actualCameraHeight));
            GetComponentInChildren<GridLayoutGroup>().cellSize = new Vector2(GetComponentInChildren<GridLayoutGroup>().cellSize.x * ((float)camera.pixelWidth / (float)actualCameraWidth),
                GetComponentInChildren<GridLayoutGroup>().cellSize.y * ((float)camera.pixelHeight / (float)actualCameraHeight));
            actualCameraHeight = camera.pixelHeight;
            actualCameraWidth = camera.pixelWidth;
        }
    }
}
