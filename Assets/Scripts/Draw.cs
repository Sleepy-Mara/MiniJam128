using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    public List<GameObject> drawThings;
    [HideInInspector] public List<GameObject> _cardsInHand = new List<GameObject>();
    public List<Cards> deck;
    public List<Cards> bloodDeck;
    [SerializeField]private List<Cards> _currentDeck = new List<Cards>();
    [SerializeField] private List<Cards> _currentBloodDeck = new();
    public Transform handPos;
    public bool canDraw;
    public bool zoomingCard;
    private TurnManager _turnManager;
    public GameObject cardPrefab;
    public GameObject spellPrefab;
    public RectTransform[] handRange;
    public List<AudioClip> clips;
    public GameObject audio;
    public Animator noCardsWindow;
    public GameObject manaDeckObject;
    public GameObject bloodDeckObject;
    public enum DeckType { Mana, Blood}
    private void Awake()
    {
        ReloadActualDecks();
    }
    public void Start()
    {
        _turnManager = FindObjectOfType<TurnManager>();
    }
    public void ReloadActualDecks()
    {
        _currentDeck = new List<Cards>();
        _currentBloodDeck = new List<Cards>();
        for (int i = 0; i < deck.Count; i++)
            _currentDeck.Add(deck[i]);
        for (int j = 0; j < bloodDeck.Count; j++)
            _currentBloodDeck.Add(bloodDeck[j]);
    }
    public void AddACard(Cards card)
    {
        if(card.healthCost == 0)
        {
            deck.Add(card);
            _currentDeck.Add(card);
        }
        else
        {
            bloodDeck.Add(card);
            _currentBloodDeck.Add(card);
        }
    }
    public void AddATempCard(Cards card)
    {
        if (card.healthCost == 0)
            _currentDeck.Add(card);
        else
            _currentBloodDeck.Add(card);
    }

    public void CanDraw()
    {
        if(_currentDeck.Count <= 0 && _currentBloodDeck.Count <= 0)
        {
            _turnManager.canEndTurn = true;
            _turnManager.PlayableTurn();
        }
        canDraw = true;
    }
    public void PlayerDraw(DeckType type)
    {
        canDraw = false;
        _turnManager.canEndTurn = true;
        DrawACard(type, true, true);
        _turnManager.PlayableTurn();
    }

    public void DrawACard(DeckType type, bool creature, bool spell)
    {
        if (_currentDeck.Count <= 0 && _currentBloodDeck.Count <= 0)
        {
            if (noCardsWindow == null)
                noCardsWindow = GameObject.Find("NoMoreCards").GetComponent<Animator>();
            noCardsWindow.SetTrigger("Activate");
            return;
        }
        int drawedCard;
        CardCore newCard;
        if (type == DeckType.Mana)
        {
            if (_currentDeck.Count <= 0)
                return;
            List<Cards> cardsToDraw = new List<Cards>();
            if (creature)
                foreach (Cards card in _currentDeck)
                    if (!card.spell)
                        cardsToDraw.Add(card);
            if (spell)
                foreach (Cards card in _currentDeck)
                    if (card.spell)
                        cardsToDraw.Add(card);
            drawedCard = Random.Range(0, cardsToDraw.Count);
            //var newCard = Instantiate(_actualDeck[drawedCard], handPos);
            if (cardsToDraw[drawedCard].spell)
                newCard = Instantiate(spellPrefab, transform).GetComponent<CardMagic>();
            else
                newCard = Instantiate(cardPrefab, transform).GetComponent<Card>();
            newCard.card = cardsToDraw[drawedCard];
            _currentDeck.Remove(cardsToDraw[drawedCard]);
            Debug.Log(_currentDeck.Count);
            if (_currentDeck.Count < 1)
                manaDeckObject.SetActive(false);
        }
        else
        {
            if (_currentBloodDeck.Count <= 0)
                return;
            List<Cards> cardsToDraw = new List<Cards>();
            if (creature)
                foreach (Cards card in _currentBloodDeck)
                    if (!card.spell)
                        cardsToDraw.Add(card);
            if (spell)
                foreach (Cards card in _currentBloodDeck)
                    if (card.spell)
                        cardsToDraw.Add(card);
            drawedCard = Random.Range(0, cardsToDraw.Count);
            if (_currentBloodDeck[drawedCard].spell)
                newCard = Instantiate(spellPrefab, transform).GetComponent<CardMagic>();
            else
                newCard = Instantiate(cardPrefab, transform).GetComponent<Card>();
            newCard.card = cardsToDraw[drawedCard];
            _currentBloodDeck.Remove(cardsToDraw[drawedCard]);
            Debug.Log(_currentBloodDeck.Count);
            if (_currentBloodDeck.Count < 1)
                bloodDeckObject.SetActive(false);
        }
        AddCardToHand(newCard);
    }

    public void AddCardToHand(CardCore newCard)
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
            _cardsInHand[_cardsInHand.Count - i - 1].transform.SetPositionAndRotation(new Vector3(handRange[1].position.x + distance * (1 +i), handRange[0].position.y, handRange[0].position.z), handRange[0].rotation);
        }
    }

    public void ResetDeckAndHand()
    {
        manaDeckObject.SetActive(true);
        bloodDeckObject.SetActive(true);
        _currentDeck = new List<Cards>();
        for (int i = 0; i < deck.Count; i++)
            _currentDeck.Add(deck[i]);
        _currentBloodDeck = new();
        for (int j = 0; j < bloodDeck.Count; j++)
            _currentBloodDeck.Add(bloodDeck[j]);
        while (_cardsInHand.Count > 0)
        {
            Destroy(_cardsInHand[0].gameObject);
            _cardsInHand.RemoveAt(0);
        }
    }
}
