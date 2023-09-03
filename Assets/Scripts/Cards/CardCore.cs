using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class CardCore : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler
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

    private float _mouseZCoord;
    [SerializeField] private float speed = 5;
    protected bool _onDrag;

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
    private void Update()
    {
        if (_onDrag)
        {
            Vector3 direction = (GetMouseWorldPos()) - transform.position;
            while (direction.magnitude > 0.01f)
            {
                direction = direction * Time.deltaTime * speed;
                transform.position += direction;
            }
        }
    }
    public virtual void SetData()
    {
        currentLife = card.life;
        currentAttack = card.attack;
        image.sprite = card.sprite;
        attackText.text = card.attack.ToString();
        lifeText.text = currentLife.ToString();
        if (card.manaCost > 0)
            manaCostText.text = card.manaCost.ToString();
        else
            manaCostText.transform.parent.gameObject.SetActive(false);
        if (card.healthCost > 0)
            healthCostText.text = card.healthCost.ToString();
        else
            healthCostText.transform.parent.gameObject.SetActive(false);
        UpdateLanguage(FindObjectOfType<LanguageManager>().languageNumber);
    }
    public virtual void OnEndDrag(PointerEventData eventData)
    {
        //print("OnEndDrag");
        _onDrag = false;
        if (FindObjectOfType<CameraManager>().CameraPosition() == 2)
        {
            bool posSelected = false;
            foreach (MapPosition pos in _table.playerPositions)
                if (Vector3.Distance(pos.cardPos.transform.position, transform.position) < 0.15f)
                {
                    Selected(pos);
                    posSelected = true;
                }
            if (!posSelected)
            {
                _draw.AdjustHand();
                FindObjectOfType<CameraManager>().HandCamera();
            }
        }
        else
        {
            _draw.AdjustHand();
            FindObjectOfType<CameraManager>().HandCamera();
        }
    }
    protected virtual void Selected(MapPosition pos)
    {

    }
    public virtual void OnDrop(PointerEventData eventData)
    {
        //print("OnDrop");
    }
    public virtual void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        if (!CanPlayCard(eventData))
            return;
        ZoomOut();
        transform.parent = null;
        if (!_onDrag)
            _onDrag = true;
        if (Input.mousePosition.y > Camera.main.pixelHeight / 2 && FindObjectOfType<CameraManager>().CameraPosition() != 2)
        {
            FindObjectOfType<CameraManager>().PlaceCardCamera();
            Quaternion newRot = Quaternion.Euler(90, 180, 0);
            transform.rotation = newRot;
            _mouseZCoord = 1.5f;
        }
        if (Input.mousePosition.y < Camera.main.pixelHeight / 16 && FindObjectOfType<CameraManager>().CameraPosition() != 1)
        {
            FindObjectOfType<CameraManager>().HandCamera();
            Quaternion newRot = Quaternion.Euler(0, 180, 0);
            transform.rotation = newRot;
            _mouseZCoord = 0.78f;
        }
    }
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (_onDrag)
            return;
        //Debug.Log("OnPointerEnter");
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
        if (!CanPlayCard(eventData))
            return;
        //_mouseZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        _mouseZCoord = 0.78f;
        //ZoomOut();
        //_cardManager.canZoom = false;
        //SelectCard();
        //_cardManager.PlaceCards(gameObject);
    }
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (!CanPlayCard(eventData))
            return;
        if (eventData.dragging)
            return;
        ZoomOut();
        _cardManager.canZoom = false;
        SelectCard();
        _cardManager.PlaceCards(gameObject);
    }
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z  = _mouseZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    private bool CanPlayCard(PointerEventData eventData)
    {
        if (_draw == null)
            return false;
        else if (eventData.button != PointerEventData.InputButton.Left && (currentPosition.cardPos != null || playerCard))
            return false;
        else if (!_turnManager.CanPlayCards())
            return false;
        else if (!_table.player.EnoughMana(card.manaCost) || !_table.player.EnoughHealth(card.healthCost))
            return false;
        else return true;
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
