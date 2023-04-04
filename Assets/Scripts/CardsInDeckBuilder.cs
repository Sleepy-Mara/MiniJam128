using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardsInDeckBuilder : MonoBehaviour
{
    private int numberOfCards;
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
