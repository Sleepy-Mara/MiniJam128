using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    }
    public void UnlockCard(Card newCard)
    {
        foreach (CardsInDeckBuilder card in cardsInDeckBuilder)
            if (card.card.card.name == newCard.card.name)
            {
                card.numberOfCards++;
                card.numberText.text = card.numberOfCards.ToString();
                card.mysteryCard.SetActive(false);
            }
        foreach (CardsInDeckBuilder card in cardsInBloodDeckBuilder)
            if (card.card.card.name == newCard.card.name)
            {
                card.numberOfCards++;
                card.numberText.text = card.numberOfCards.ToString();
                card.mysteryCard.SetActive(false);
            }
    }
    public void SelectCard(Card newCard)
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public class CardsInDeckBuilder : ScriptableObject
{
    [HideInInspector]
    public int numberOfCards;
    public TextMeshProUGUI numberText;
    public GameObject mysteryCard;
    public Card card;
}
