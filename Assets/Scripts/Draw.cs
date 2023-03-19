using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    public List<GameObject> drawThings;
    [HideInInspector] public List<GameObject> _cardsInHand = new List<GameObject>();
    public List<Cards> deck;
    private List<Cards> _actualDeck = new List<Cards>();
    public Transform handPos;
    public bool canDraw;
    private TurnManager turnManager;
    public GameObject cardPrefab;
    void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();
        for (int i = 0; i < deck.Count; i++)
            _actualDeck.Add(deck[i]);
    }

    public void AddACard(Cards card)
    {
        deck.Add(card);
        _actualDeck.Add(card);
    }

    public void PlayerDraw()
    {
        if (canDraw)
        {
            canDraw = false;
            DrawACard();
            turnManager.PlayableTurn();
        }
    }

    public void DrawACard()
    {
        if (_actualDeck.Count <= 0)
        {
            Debug.Log("perdiste gay");
            return;
        }
        var drawedCard = Random.Range(0, _actualDeck.Count);
        //var newCard = Instantiate(_actualDeck[drawedCard], handPos);
        var newCard = Instantiate(cardPrefab, handPos);
        newCard.GetComponent<ThisCard>().card = _actualDeck[drawedCard];
        _cardsInHand.Add(newCard);
        _actualDeck.Remove(_actualDeck[drawedCard]);
        AdjustHand();
    }

    public void AdjustHand()
    {
        for (int i = 0; i < _cardsInHand.Count; i++)
        {
            _cardsInHand[_cardsInHand.Count - i - 1].transform.rotation = handPos.rotation;
            _cardsInHand[_cardsInHand.Count - i - 1].transform.position += new Vector3(0, 0, 0 - 0.001f * i);
            _cardsInHand[_cardsInHand.Count - i - 1].transform.Rotate(0, 0, 90 / (_cardsInHand.Count + 1) * (i + 1));
        }
    }

    public void ResetDeckAndHand()
    {
        _actualDeck = new List<Cards>();
        for (int i = 0; i < deck.Count; i++)
            _actualDeck.Add(deck[i]);
        foreach (var card in _cardsInHand)
        {
            _cardsInHand.Remove(card);
            Destroy(card);
        }
    }
}
