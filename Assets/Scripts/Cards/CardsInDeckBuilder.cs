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
    public GameObject cover;
    public CardDisplay card;
    [HideInInspector]
    public bool inDeck;
    public int inDeckFromStart;
    // Start is called before the first frame update
    void Awake()
    {
        if (inDeckFromStart > 0)
            return;
        for (int i = 0; i < inDeckFromStart; i++)
            SelectThis();
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
