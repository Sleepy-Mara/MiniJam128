using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    public List<GameObject> drawThings;
    [HideInInspector] public List<GameObject> _cardsInHand = new List<GameObject>();
    public static List<Cards> savedDeck;
    public static List<Cards> savedBloodDeck;
    public List<Cards> deck;
    public List<Cards> bloodDeck;
    private static Draw instance;
    [SerializeField]private List<Cards> _actualDeck = new List<Cards>();
    [SerializeField] private List<Cards> _actualBloodDeck = new();
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
        if (instance == null)
        {
            instance = this;
            savedDeck = deck;
            savedBloodDeck = bloodDeck;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
        deck = savedDeck;
        bloodDeck = savedBloodDeck;
        ReloadActualDecks();
    }
    public void Start()
    {
        _turnManager = FindObjectOfType<TurnManager>();
    }
    public void ReloadActualDecks()
    {
        _actualDeck = new List<Cards>();
        _actualBloodDeck = new List<Cards>();
        for (int i = 0; i < deck.Count; i++)
            _actualDeck.Add(deck[i]);
        for (int j = 0; j < bloodDeck.Count; j++)
            _actualBloodDeck.Add(bloodDeck[j]);
    }
    public void AddACard(Cards card)
    {
        if(card.healthCost == 0)
        {
            deck.Add(card);
            _actualDeck.Add(card);
        }
        else
        {
            bloodDeck.Add(card);
            _actualBloodDeck.Add(card);
        }
    }
    public void AddATempCard(Cards card)
    {
        if (card.healthCost == 0)
            _actualDeck.Add(card);
        else
            _actualBloodDeck.Add(card);
    }

    public void CanDraw()
    {
        if(_actualDeck.Count <= 0 && _actualBloodDeck.Count <= 0)
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
        if (_actualDeck.Count <= 0 && _actualBloodDeck.Count <= 0)
        {
            noCardsWindow.SetTrigger("Activate");
            return;
        }
        int drawedCard;
        CardCore newCard;
        if (type == DeckType.Mana)
        {
            if (_actualDeck.Count <= 0)
                return;
            List<Cards> cardsToDraw = new List<Cards>();
            if (creature)
                foreach (Cards card in _actualDeck)
                    if (!card.spell)
                        cardsToDraw.Add(card);
            if (spell)
                foreach (Cards card in _actualDeck)
                    if (card.spell)
                        cardsToDraw.Add(card);
            drawedCard = Random.Range(0, cardsToDraw.Count);
            //var newCard = Instantiate(_actualDeck[drawedCard], handPos);
            if (cardsToDraw[drawedCard].spell)
                newCard = Instantiate(spellPrefab, transform).GetComponent<CardMagic>();
            else
                newCard = Instantiate(cardPrefab, transform).GetComponent<Card>();
            newCard.card = cardsToDraw[drawedCard];
            _actualDeck.Remove(cardsToDraw[drawedCard]);
            Debug.Log(_actualDeck.Count);
            if (_actualDeck.Count < 1)
                manaDeckObject.SetActive(false);
        }
        else
        {
            if (_actualBloodDeck.Count <= 0)
                return;
            List<Cards> cardsToDraw = new List<Cards>();
            if (creature)
                foreach (Cards card in _actualBloodDeck)
                    if (!card.spell)
                        cardsToDraw.Add(card);
            if (spell)
                foreach (Cards card in _actualBloodDeck)
                    if (card.spell)
                        cardsToDraw.Add(card);
            drawedCard = Random.Range(0, cardsToDraw.Count);
            if (_actualBloodDeck[drawedCard].spell)
                newCard = Instantiate(spellPrefab, transform).GetComponent<CardMagic>();
            else
                newCard = Instantiate(cardPrefab, transform).GetComponent<Card>();
            newCard.card = cardsToDraw[drawedCard];
            _actualBloodDeck.Remove(cardsToDraw[drawedCard]);
            Debug.Log(_actualBloodDeck.Count);
            if (_actualBloodDeck.Count < 1)
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
        _actualDeck = new List<Cards>();
        for (int i = 0; i < deck.Count; i++)
            _actualDeck.Add(deck[i]);
        _actualBloodDeck = new();
        for (int j = 0; j < bloodDeck.Count; j++)
            _actualBloodDeck.Add(bloodDeck[j]);
        while (_cardsInHand.Count > 0)
        {
            Destroy(_cardsInHand[0].gameObject);
            _cardsInHand.RemoveAt(0);
        }
    }
}
