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
    private bool _inmune;
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
            actualPosition.positionFacing.card.ReceiveDamage(actualAttack, this, true);
            //ejecutar audio y/o animacion
            CheckEffect(4, actualPosition.positionFacing.card.gameObject);
        }
        else
        {
            actualPosition.oponent.ReceiveDamage(card.attackToPlayer);
            CheckEffect(4, actualPosition.oponent.gameObject);
        }
    }
    public void ReceiveDamage(int damage, ThisCard enemy, bool vengance)
    {
        CheckEffect(4, enemy.gameObject);
        actualLife -= damage;
        lifeText.text = actualLife.ToString();
        if (!_inmune && enemy != null && vengance == true)
            enemy.ReceiveDamage(damage, this, false);
        if (actualLife <= 0)
        {
            Death(enemy);
        }
        //ejecutar audio y/o animacion
    }
    public void Death(ThisCard enemy)
    {
        CheckEffect(3, enemy.gameObject);
        //animacion / audio
        Debug.Log("La carta " + card.cardName + " se murio :c");
        actualPosition.card = null;
        GameObject.Destroy(this);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (actualPosition.cardPos == null && _turnManager.canPlayCards)
            _cardManager.PlaceCards(gameObject);
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
        CheckEffect(1, null);
    }
    public void OnTurnStart()
    {
        CheckEffect(0, null);
    }
    public void OnTurnEnd()
    {
        CheckEffect(2, null);
    }
    private void CheckEffect(int x, GameObject target)
    {
        if (card.hasEffect)
        {
            bool doEffect = false;
            List<string> effectToDo = null;
            foreach (string effect in card.keywords)
                if (card.effectDesc.Contains(effect))
                {
                    if (effect == card.keywords[x])
                        doEffect = true;
                    if (card.keywords.IndexOf(effect) > 4)
                    {
                        if (effectToDo == null)
                            effectToDo = new List<string>();
                        effectToDo.Add(effect);
                    }
                }
            if (doEffect && effectToDo != null)
                foreach (string effect in effectToDo)
                    Effect(effect, target);
        }
    }
    private void Effect(string effect, GameObject target)
    {
        var eventNumber = card.keywords.IndexOf(effect) - 4;
        if (eventNumber == 0)
            DrawEffect();
        if (eventNumber == 1)
            DealDamgeEffect(target);
    }
    private void DrawEffect()
    {
        _draw.DrawACard();
    }
    private void DealDamgeEffect(GameObject target)
    {
        if (target.GetComponent<ThisCard>())
            target.GetComponent<ThisCard>().ReceiveDamage(actualAttack, this, false);
        if (target.GetComponent<Health>())
            target.GetComponent<Health>().ReceiveDamage(card.attackToPlayer);
    }
}
