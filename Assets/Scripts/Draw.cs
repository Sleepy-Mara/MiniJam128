using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    public List<GameObject> _cards = new List<GameObject>();
    public GameObject card;
    public Transform handPos;
    void Start()
    {
        
    }

    public void DrawACard()
    {
        var newCard = Instantiate(card, handPos);
        _cards.Add(newCard);
        for(int i = 0; i < _cards.Count; i++)
        {
            Debug.Log(_cards.Count);
            //Debug.Log(90 / (_cards.Count + 1) * (i + 1));
            _cards[_cards.Count - i - 1].transform.rotation = handPos.rotation;
            _cards[_cards.Count - i - 1].transform.position += new Vector3(0, 0, 0 - 0.001f * i);
            _cards[_cards.Count - i - 1].transform.Rotate(0, 0, 90 / (_cards.Count + 1) * (i + 1));
        }
    }
}
