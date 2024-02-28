using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    //public List<GameObject> drawThings;
    [HideInInspector] public List<GameObject> _cardsInHand = new List<GameObject>();
    public List<Cards> deck;
    public List<Cards> bloodDeck;
    [SerializeField]private List<Cards> _currentDeck = new List<Cards>();
    [HideInInspector] public List<Cards> CurrentDeck
    {
        get { return _currentDeck; }
        set { _currentDeck = value; }
    }
    [SerializeField] private List<Cards> _currentBloodDeck = new();
    [HideInInspector] public List<Cards> CurrentBloodDeck
    {
        get { return _currentBloodDeck; }
        set { _currentBloodDeck = value; }
    }
    public Transform handPos;
    public bool canDraw;
    public bool zoomingCard;
    private TurnManager _turnManager;
    public GameObject cardPrefab;
    public GameObject spellPrefab;
    public Transform[] handRange;
    public List<AudioClip> clips;
    //public GameObject audio;
    public Animator noCardsWindow;
    public GameObject manaDeckObject;
    public GameObject bloodDeckObject;

    [Header("Card Hand")]
    public float cardVerticalCurvature = 0.2f;
    public float cardLongitudeCurvature = 1f;
    public float cardHorizontalMove = 0f;
    public float cardVerticalMove = 0f;
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
        var tempDeck = new List<Cards>();
        var tempBloodDeck = new List<Cards>();
        for (int i = 0; i < deck.Count; i++)
            tempDeck.Add(deck[i]);
        for (int j = 0; j < bloodDeck.Count; j++)
            tempBloodDeck.Add(bloodDeck[j]);
        ShuffleDeck(tempDeck);
        ShuffleBloodDeck(tempBloodDeck);
    }
    public void ShuffleDeck(List<Cards> cardsToShuffle)
    {
        while (cardsToShuffle.Count > 0)
        {
            int deckCardNum = Random.Range(0, cardsToShuffle.Count);
            _currentDeck.Add(cardsToShuffle[deckCardNum]);
            cardsToShuffle.RemoveAt(deckCardNum);
        }
    }
    public void ShuffleBloodDeck(List<Cards> cardsToShuffle)
    {
        while (cardsToShuffle.Count > 0)
        {
            int bloodDeckCardNum = Random.Range(0, cardsToShuffle.Count);
            _currentBloodDeck.Add(cardsToShuffle[bloodDeckCardNum]);
            cardsToShuffle.RemoveAt(bloodDeckCardNum);
        }
    }
    public void AddACard(Cards card)
    {
        if(card.healthCost == 0)
        {
            deck.Add(card);
            _currentDeck.Add(card);
            ShuffleDeck(_currentDeck);
        }
        else
        {
            bloodDeck.Add(card);
            _currentBloodDeck.Add(card);
            ShuffleBloodDeck(_currentBloodDeck);
        }
    }
    public void AddATempCard(Cards card)
    {
        if (card.healthCost == 0)
        {
            _currentDeck.Add(card);
            ShuffleDeck(_currentDeck);
        }
        else
        {
            _currentBloodDeck.Add(card);
            ShuffleBloodDeck(_currentBloodDeck);
        }
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
        CardCore newCard;
        if (type == DeckType.Mana)
        {
            if (_currentDeck.Count <= 0)
                return;
            List<Cards> cardsToDraw = new List<Cards>();
            if (creature && spell)
                foreach (Cards card in _currentDeck)
                    cardsToDraw.Add(card);
            else
            {
                if (creature)
                    foreach (Cards card in _currentDeck)
                        if (!card.spell)
                            cardsToDraw.Add(card);
                if (spell)
                    foreach (Cards card in _currentDeck)
                        if (card.spell)
                            cardsToDraw.Add(card);
            }
            //var newCard = Instantiate(_actualDeck[drawedCard], handPos);
            if (cardsToDraw[0].spell)
                newCard = Instantiate(spellPrefab, transform).GetComponent<CardMagic>();
            else
                newCard = Instantiate(cardPrefab, transform).GetComponent<Card>();
            newCard.card = cardsToDraw[0];
            _currentDeck.Remove(cardsToDraw[0]);
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
            if (_currentBloodDeck[0].spell)
                newCard = Instantiate(spellPrefab, transform).GetComponent<CardMagic>();
            else
                newCard = Instantiate(cardPrefab, transform).GetComponent<Card>();
            newCard.card = cardsToDraw[0];
            _currentBloodDeck.Remove(cardsToDraw[0]);
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
        // a ver, quiero varias cosas:
        // 1- que las cartas esten mas arriba, se puede conseguir subiendo el objeto
        // 2- que se vean mejor los contenidos de las cartas
        // 3- que las cartas no se vean escalonadas, pero que no esten todas al mismo nivel
        //var newAudio = Instantiate(audio).GetComponent<AudioSource>();
        //newAudio.clip = clips[Random.Range(0, clips.Count)];
        //newAudio.Play();
        float distanceX = Mathf.Abs(handRange[0].localPosition.x) + Mathf.Abs(handRange[1].localPosition.x);
        float distanceZ = Mathf.Abs(handRange[0].localPosition.z) + Mathf.Abs(handRange[1].localPosition.z);
        float angle = Mathf.Abs(handRange[0].localEulerAngles.z - 360) + Mathf.Abs(handRange[1].localEulerAngles.z);
        angle /= (_cardsInHand.Count + 1);
        distanceX /= (_cardsInHand.Count + 1);
        distanceZ /= (_cardsInHand.Count + 1);
        for (int i = 0; i < _cardsInHand.Count; i++)
        {
            //tiene que ser un numero entre 0 y 1
            float f = cardVerticalCurvature * Mathf.Sin(Mathf.PI * ((i * 1.0f +1) / (_cardsInHand.Count+1)) * cardLongitudeCurvature + cardHorizontalMove) + cardVerticalMove;
            _cardsInHand[_cardsInHand.Count - i - 1].transform.SetPositionAndRotation(new Vector3(handRange[0].position.x + distanceX * (1 +i), handRange[0].position.y + (f), handRange[0].position.z + distanceZ * (1 + i)), 
                Quaternion.Euler(new Vector3(handRange[0].eulerAngles.x, handRange[0].eulerAngles.y, handRange[0].eulerAngles.z + angle * (i+1))));
        }
    }

    public void ResetDeckAndHand()
    {
        manaDeckObject.SetActive(true);
        //bloodDeckObject.SetActive(true);
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
