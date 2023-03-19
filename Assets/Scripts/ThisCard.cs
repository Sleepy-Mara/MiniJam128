using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ThisCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public Cards card;
    [HideInInspector] public MapPosition actualPosition;
    private int actualLife;
    private int actualAttack;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private TextMeshProUGUI lifeText;
    [SerializeField] private TextMeshProUGUI manaCostText;
    [SerializeField] private TextMeshProUGUI healthCostText;
    [SerializeField] private Image image;
    [SerializeField] private Image effectImage;
    [SerializeField] private Canvas canvas;
    [HideInInspector] public bool canPlay;
    private CardManager _cardManager;
    private TurnManager _turnManager;
    private Draw _draw;
    private void Awake()
    {
        if (card != null)
            SetData();
    }
    private void Start()
    {
        _cardManager = FindObjectOfType<CardManager>();
        _turnManager = FindObjectOfType<TurnManager>();
        _draw = FindObjectOfType<Draw>();
    }
    public void SetData()
    {
        actualLife = card.life;
        actualAttack = card.attack;
        nameText.text = card.name;
        image.sprite = card.sprite;
        attackText.text = card.attack.ToString();
        lifeText.text = actualLife.ToString();
        manaCostText.text = card.manaCost.ToString();
        healthCostText.text = card.healthCost.ToString();
        if (card.hasEffect)
        {
            effectImage.sprite = card.effectSprite;
        }
        else
            effectImage.enabled = false;
    }

    public void Attack()
    {
        //buscar al wachin que este al frente y hacerlo poronga
        if (actualPosition.positionFacing.card != null)
        {
            actualPosition.positionFacing.card.ReceiveDamage(actualAttack);
            //ejecutar audio y/o animacion
        }
        else
        {
            actualPosition.oponent.ReceiveDamage(card.attackToPlayer);
        }
        CheckEffect(4);
    }
    public void ReceiveDamage(int damage)
    {
        actualLife -= damage;
        lifeText.text = actualLife.ToString();
        if (actualLife <= 0)
        {
            Death();
        }
        //ejecutar audio y/o animacion
    }
    public void Death()
    {
        CheckEffect(3);
        //animacion / audio
        Debug.Log("La carta " + card.cardName + " se murio :c");
        actualPosition.card = null;
        GameObject.Destroy(this);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (actualPosition == null && _turnManager.canPlayCards)
            _cardManager.PlaceCards(gameObject.transform.parent.gameObject);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //animacion
        GetComponent<Animator>().SetBool("Zoomed", true);
        canvas.overrideSorting = true;
        canvas.sortingOrder = 1;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //animacion
        GetComponent<Animator>().SetBool("Zoomed", false);
        canvas.sortingOrder = 0;
        canvas.overrideSorting = false;
    }
    public void OnPlayEffect()
    {
        CheckEffect(1);
    }
    public void OnTurnStart()
    {
        CheckEffect(0);
    }
    public void OnTurnEnd()
    {
        CheckEffect(2);
    }
    private void CheckEffect(int x)
    {
        if (card.hasEffect)
        {
            bool doEffect = false;
            string effectToDo = null;
            foreach (string effect in card.keywords)
                if (card.effectDesc.Contains(effect))
                {
                    if (effect == card.keywords[x])
                        doEffect = true;
                    if (card.keywords.IndexOf(effect) > 4)
                        effectToDo = effect;
                    if (doEffect && effectToDo != null)
                    {
                        Effect(effectToDo);
                        effectToDo = null;
                    }
                }
        }
    }
    private void Effect(string effect)
    {
        var eventNumber = card.keywords.IndexOf(effect) - 4;
        if (eventNumber == 0)
            DrawEffect();
        if (eventNumber == 1)
            DealDamgeEffect();
    }
    private void DrawEffect()
    {
        _draw.DrawACard();
    }
    private void DealDamgeEffect()
    {

    }
}
