using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using static UnityEditor.PlayerSettings;

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

    private float _mouseZCoord;
    private Vector3 _mouseOffset;
    private bool _firstTime;
    private float _speed = 5;
    private bool _onDrag;

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
                direction = direction * Time.deltaTime * _speed;
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
                    Debug.Log("SIIIIIII " + pos.cardPos.name);
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
        if (_draw == null)
            return;
        if (eventData.button != PointerEventData.InputButton.Left && (currentPosition.cardPos != null || playerCard))
            return;
        if (!_turnManager.CanPlayCards())
            return;
        if (!_table.player.EnoughMana(card.manaCost) || !_table.player.EnoughHealth(card.healthCost))
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
        }
        if (FindObjectOfType<CameraManager>().CameraPosition() == 2)
        {
            if (_firstTime)
            {
                _mouseZCoord = 1.5f;
                _firstTime = false;
            }
            foreach (MapPosition x in _table.playerPositions)
                Debug.Log(GetMouseWorldPos() + " " + x.cardPos.transform.position);
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
        if (_draw == null)
            return;
        if (eventData.button != PointerEventData.InputButton.Left && (currentPosition.cardPos != null || playerCard))
            return;
        if (!_turnManager.CanPlayCards())
            return;
        if (!_table.player.EnoughMana(card.manaCost) && !_table.player.EnoughHealth(card.healthCost))
            return;
        _mouseZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        _mouseOffset = gameObject.transform.position - GetMouseWorldPos();
        _firstTime = true;
        //ZoomOut();
        //_cardManager.canZoom = false;
        //SelectCard();
        //_cardManager.PlaceCards(gameObject);
    }
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z  = _mouseZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
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
