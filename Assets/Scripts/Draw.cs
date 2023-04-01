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
    private List<Cards> _actualDeck = new List<Cards>();
    private List<Cards> _actualBloodDeck = new();
    public Transform handPos;
    public bool canDraw;
    public bool zoomingCard;
    private TurnManager _turnManager;
    public GameObject cardPrefab;
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
    }
    public void Start()
    {
        manaDeckObject = GameObject.Find("Deck");
        bloodDeckObject = GameObject.Find("BloodDeck");
        _turnManager = FindObjectOfType<TurnManager>();
        for (int i = 0; i < deck.Count; i++)
            _actualDeck.Add(deck[i]);
        for (int j = 0; j < bloodDeck.Count; j++)
        {
            _actualBloodDeck.Add(bloodDeck[j]);
        }
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
        DrawACard(type);
        _turnManager.PlayableTurn();
    }

    public void DrawACard(DeckType type)
    {
        if (_actualDeck.Count <= 0 && _actualBloodDeck.Count <= 0)
        {
            noCardsWindow.SetTrigger("Activate");
            return;
        }
        var newCard = Instantiate(cardPrefab, transform).GetComponent<Card>();
        int drawedCard;
        if (type == DeckType.Mana)
        {
            drawedCard = Random.Range(0, _actualDeck.Count);
            //var newCard = Instantiate(_actualDeck[drawedCard], handPos);
            newCard.card = _actualDeck[drawedCard];
            _actualDeck.Remove(_actualDeck[drawedCard]);
            if (_actualDeck.Count == 0)
                manaDeckObject.SetActive(false);
        }
        else
        {
            drawedCard = Random.Range(0, _actualBloodDeck.Count);
            newCard.card = _actualBloodDeck[drawedCard];
            _actualBloodDeck.RemoveAt(drawedCard);
            if (_actualBloodDeck.Count == 0)
                bloodDeckObject.SetActive(false);
        }
        AddCardToHand(newCard);
    }

    public void AddCardToHand(Card newCard)
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
