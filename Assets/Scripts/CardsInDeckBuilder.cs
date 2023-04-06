using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardsInDeckBuilder : MonoBehaviour
{
    [SerializeField] private int numberOfCards;
    [HideInInspector]
    public int NumberOfCards
    {
        get { return numberOfCards; }
        set 
        {
            numberOfCards += value;
            numberText.text = numberOfCards.ToString();
        }
    }
    public TextMeshProUGUI numberText;
    public GameObject mysteryCard;
    public CardCore card;
    [HideInInspector]
    public bool inDeck;
    public bool inDeckFromStart;
    // Start is called before the first frame update
    void Start()
    {
        if (inDeckFromStart)
            FindObjectOfType<DeckBuilder>().SelectCard(this);
        numberText.text = NumberOfCards.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectThis()
    {
        if (NumberOfCards < 1)
            return;
        NumberOfCards = -1;
        if (!inDeck)
            FindObjectOfType<DeckBuilder>().SelectCard(this);
        if (inDeck)
            FindObjectOfType<DeckBuilder>().UnselectedCard(this);
    }
}
