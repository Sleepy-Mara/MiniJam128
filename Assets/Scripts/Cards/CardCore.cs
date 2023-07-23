using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class CardCore : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public Cards card;
    public MapPosition currentPosition;
    protected int currentLife;
    protected int currentAttack;
    [SerializeField] protected TextMeshPro nameText;
    [SerializeField] protected TextMeshPro attackText;
    [SerializeField] protected TextMeshPro lifeText;
    [SerializeField] protected TextMeshPro manaCostText;
    [SerializeField] protected TextMeshPro healthCostText;
    [SerializeField] protected TextMeshPro effectText;
    [SerializeField] protected SpriteRenderer image;
    //[SerializeField] protected Canvas canvas;

    [HideInInspector]
    public bool playerCard;
    public bool checkingEffect;

    protected EffectManager _effectManager;
    protected CardManager _cardManager;
    protected TurnManager _turnManager;
    protected Table _table;
    protected Draw _draw;

    private void Awake()
    {
        if (card != null)
            SetData();
        _table = FindObjectOfType<Table>();
        _draw = FindObjectOfType<Draw>();
        _effectManager = FindObjectOfType<EffectManager>();
        _cardManager = FindObjectOfType<CardManager>();
        _turnManager = FindObjectOfType<TurnManager>();
    }

    public virtual void SetData()
    {
        currentLife = card.life;
        currentAttack = card.attack;
        image.sprite = card.sprite;
        attackText.text = card.attack.ToString();
        lifeText.text = currentLife.ToString();
        manaCostText.text = card.manaCost.ToString();
        healthCostText.text = card.healthCost.ToString();
        UpdateLanguage(FindObjectOfType<LanguageManager>().languageNumber);
    }
    public virtual void OnEndDrag(PointerEventData eventData)
    {
        //print("OnEndDrag");
    }
    public virtual void OnDrop(PointerEventData eventData)
    {
        //print("OnDrop");
    }
    public virtual void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
    }
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter");
        ZoomIn();
    }
    public virtual void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("OnPointerExit");
        if (eventData.fullyExited)
        {
            ZoomOut();
        }
    }
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown");
        if (_draw == null)
            return;
        if (eventData.button == PointerEventData.InputButton.Left && (currentPosition.cardPos == null || playerCard))
        {
            if (_turnManager.CanPlayCards())
            {
                if (_table.player.EnoughMana(card.manaCost) && _table.player.EnoughHealth(card.healthCost))
                {
                    ZoomOut();
                    _cardManager.canZoom = false;
                    SelectCard();
                    //_cardManager.PlaceCards(gameObject);
                }
            }
        }
    }
    protected virtual void SelectCard()
    {
        Debug.LogError("Cuidado, por algun motivo estas intentando invocar una carta que solo contiene CardCore!");
    }
    #region Zoom
    private void ZoomIn()
    {
        if (_draw == null)
        {
            GetComponent<Animator>().SetBool("Zoomed", true);
        }
        else if (!_draw.zoomingCard && _cardManager.canZoom)
        {
            GetComponent<Animator>().SetBool("Zoomed", true);
            _draw.zoomingCard = true;
        }
    }
    private void ZoomOut()
    {
        GetComponent<Animator>().SetBool("Zoomed", false);
        if (_draw != null)
            _draw.zoomingCard = false;
    }
    #endregion
    public virtual void UpdateLanguage(int languageNumber)
    {
        nameText.text = card.cardName[languageNumber];
        if (card.hasEffect)
        {
            effectText.enabled = true;
            effectText.text = card.effectDesc[languageNumber];
        }
        else
            effectText.enabled = false;
    }
}
