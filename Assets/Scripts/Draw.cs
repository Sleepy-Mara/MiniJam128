using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    private List<GameObject> _cardsInHand = new List<GameObject>();
    public List<GameObject> deck;
    private List<GameObject> _actualDeck = new List<GameObject>();
    public Transform handPos;
    void Start()
    {
        for (int i = 0; i < deck.Count; i++)
            _actualDeck.Add(deck[i]);
    }

    public void AddACard(GameObject card)
    {
        deck.Add(card);
        _actualDeck.Add(card);
    }

    public void DrawACard()
    {
        if (_actualDeck.Count <= 0)
        {
            Debug.Log("perdiste gay");
            return;
        }
        var drawedCard = Random.Range(0, _actualDeck.Count);
        var newCard = Instantiate(_actualDeck[drawedCard], handPos);
        _cardsInHand.Add(newCard);
        _actualDeck.Remove(_actualDeck[drawedCard]);
        for (int i = 0; i < _cardsInHand.Count; i++)
        {
            _cardsInHand[_cardsInHand.Count - i - 1].transform.rotation = handPos.rotation;
            _cardsInHand[_cardsInHand.Count - i - 1].transform.position += new Vector3(0, 0, 0 - 0.001f * i);
            _cardsInHand[_cardsInHand.Count - i - 1].transform.Rotate(0, 0, 90 / (_cardsInHand.Count + 1) * (i + 1));
        }
    }
}
