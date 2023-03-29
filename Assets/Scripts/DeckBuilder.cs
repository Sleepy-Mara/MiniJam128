using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeckBuilder : MonoBehaviour
{
    public List<CardsInDeckBuilder> cardsInDeckBuilder;
    public List<CardsInDeckBuilder> cardsInBloodDeckBuilder;
    public List<Card> cardsInDeck;
    static List<Card> savedCardsInDeck;
    public List<Card> cardsInBloodDeck;
    static List<Card> savedCardsInBloodDeck;
    private static DeckBuilder instance;
    [SerializeField] private int maxCardsInNormalDeck;
    [SerializeField] private int minCardsInNormalDeck;
    [SerializeField] private int maxCardsInBloodDeck;
    [SerializeField] private int minCardsInBloodDeck;
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
                cardsInDeck.Add(newCard);
                savedCardsInDeck.Add(newCard);
            }
        foreach (CardsInDeckBuilder card in cardsInBloodDeckBuilder)
            if (card.card.card.name == newCard.card.name)
            {
                cardsInBloodDeck.Add(newCard);
                savedCardsInBloodDeck.Add(newCard);
            }
    }
    public void UnselectedCard(Card newCard)
    {
        foreach (CardsInDeckBuilder card in cardsInDeckBuilder)
            if (card.card.card.name == newCard.card.name)
            {
                cardsInDeck.Remove(newCard);
                savedCardsInDeck.Remove(newCard);
            }
        foreach (CardsInDeckBuilder card in cardsInBloodDeckBuilder)
            if (card.card.card.name == newCard.card.name)
            {
                cardsInBloodDeck.Remove(newCard);
                savedCardsInBloodDeck.Remove(newCard);
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
