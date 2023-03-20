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
    public RectTransform[] handRange;
    public List<AudioClip> clips;
    public GameObject audio;
    public Animator noCardsWindow;
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

    public void CanDraw()
    {
        if(_actualDeck.Count <= 0)
        {
            turnManager.canEndTurn = true;
        }
        canDraw = true;
    }
    public void PlayerDraw()
    {
        if (canDraw)
        {
            canDraw = false;
            turnManager.canEndTurn = true;
            DrawACard();
            turnManager.PlayableTurn();
        }
    }

    public void DrawACard()
    {
        if (_actualDeck.Count <= 0)
        {
            Debug.Log("perdiste gay");
            noCardsWindow.SetTrigger("Activate");
            return;
        }
        var drawedCard = Random.Range(0, _actualDeck.Count);
        //var newCard = Instantiate(_actualDeck[drawedCard], handPos);
        var newCard = Instantiate(cardPrefab, transform).GetComponent<ThisCard>();
        newCard.card = _actualDeck[drawedCard];
        newCard.SetData();
        _cardsInHand.Add(newCard.gameObject);
        _actualDeck.Remove(_actualDeck[drawedCard]);
        AdjustHand();
    }

    public void AddCardToHand(ThisCard newCard)
    {
        newCard.SetData();
        _cardsInHand.Add(newCard.gameObject);
        AdjustHand();
    }

    public void AdjustHand()
    {
        FindObjectOfType<CameraManager>().HandCamera();
        var newAudio = Instantiate(audio).GetComponent<AudioSource>();
        newAudio.clip = clips[Random.Range(0, clips.Count)];
        newAudio.Play();
        float distance = Mathf.Abs(handRange[0].position.x) + Mathf.Abs(handRange[1].position.x);
        distance /= (_cardsInHand.Count + 1);
        for (int i = 0; i < _cardsInHand.Count; i++)
        {
            _cardsInHand[i].transform.SetPositionAndRotation(new Vector3(handRange[1].position.x + distance * (1 +i), handRange[0].position.y, handRange[0].position.z), handRange[0].rotation);
            //_cardsInHand[_cardsInHand.Count - i - 1].transform.rotation = handPos.rotation;
            //_cardsInHand[_cardsInHand.Count - i - 1].transform.position += new Vector3(0, 0, 0 - 0.001f * i);
            //_cardsInHand[_cardsInHand.Count - i - 1].transform.Rotate(0, 0, 90 / (_cardsInHand.Count + 1) * (i + 1));
        }
    }

    public void ResetDeckAndHand()
    {
        _actualDeck = new List<Cards>();
        for (int i = 0; i < deck.Count; i++)
            _actualDeck.Add(deck[i]);
        while (_cardsInHand.Count > 0)
        {
            Destroy(_cardsInHand[0].gameObject);
            _cardsInHand.RemoveAt(0);
        }
        //for (int j = 0; j < _cardsInHand.Count; j++)
        //{
        //    Destroy(_cardsInHand[j].gameObject);
        //    _cardsInHand.RemoveAt(j);
        //}
        //foreach (var card in _cardsInHand)
        //{
        //    _cardsInHand.Remove(card);
        //    Destroy(card);
        //}
    }
}
