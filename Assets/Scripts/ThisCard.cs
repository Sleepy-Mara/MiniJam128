using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
using UnityEditor.Animations;

public class ThisCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public Cards card;
    public MapPosition actualPosition;
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
    private Table _table;
    private bool _inmune = false;
    public AnimatorController handAnimator;
    public AnimatorController tableAnimator;
    public bool attack;
    public bool getAttacked;
    private int lastDamage;
    private ThisCard lastEnemy;
    private GameObject lastTarget;
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
        _table = FindObjectOfType<Table>();
    }
    private void FixedUpdate()
    {
        if(attack)
        {
            if (lastTarget.GetComponent<ThisCard>())
            {
                lastTarget.GetComponent<ThisCard>().ReceiveDamage(actualAttack, this, true);
                //ejecutar audio y/o animacion
                if (actualPosition.positionFacing.card != null)
                    CheckEffect(4, actualPosition.positionFacing.card.gameObject);
            }
            if(lastTarget.GetComponent<Health>())
            {
                lastTarget.GetComponent<Health>().ReceiveDamage(card.attackToPlayer);
                CheckEffect(4, actualPosition.oponent.gameObject);
            }
            attack = false;
            lastTarget = null;
        }
        if (getAttacked)
        {
            lastEnemy.ReceiveDamage(lastDamage, this, false);
            lastDamage = 0;
            lastEnemy = null;
            getAttacked = false;
        }
        if (actualLife <= 0)
        {
            Death(lastEnemy);
        }
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
        if (actualPosition.positionFacing.card != null)
        {
            lastTarget = actualPosition.positionFacing.card.gameObject;
            //ejecutar audio y/o animacion
        }
        else
        {
            lastTarget = actualPosition.oponent.gameObject;
        }
        //buscar al wachin que este al frente y hacerlo poronga
        GetComponent<Animator>().SetTrigger("Attack");
    }
    public void ReceiveDamage(int damage, ThisCard enemy, bool vengance)
    {
        if (enemy != null)
            CheckEffect(4, enemy.gameObject);
        actualLife -= damage;
        lifeText.text = actualLife.ToString();
        if (!_inmune && enemy != null && vengance == true)
        {
            lastDamage = damage;
            lastEnemy = enemy;
            GetComponent<Animator>().SetTrigger("GetDamage");
        }
        //ejecutar audio y/o animacion
    }
    public void Death(ThisCard enemy)
    {
        if (enemy != null)
            CheckEffect(3, enemy.gameObject);
        //animacion / audio
        Debug.Log("La carta " + card.cardName + " se murio :c");
        actualPosition.card = null;
        _table.myCards.Remove(this);
        Destroy(gameObject);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (_table.player.EnoughMana(card.manaCost) && _table.player.EnoughHealth(card.healthCost))
        {
            if (actualPosition.cardPos == null && _turnManager.canPlayCards)
            {
                _cardManager.PlaceCards(gameObject);
                _table.player.SpendMana(card.manaCost);
            }
        }
        else
        {
            Debug.Log("no tenes mana");
        }
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
        if (eventNumber == 2)
            BuffAlly();
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
    private void BuffAlly()
    {
        //if(card.effectDesc.Contains("select"))
        //    target = 
        if (card.effectDesc.Contains("random"))
            Buff(_table.myCards[Random.Range(0, _table.myCards.Count)].gameObject);
    }
    private void Buff(GameObject target)
    {
        for (int i = 0; i > 50; i++)
            for (int j = 0; j < 50; j++)
                if (card.effectDesc.Contains(j.ToString() + "/" + i.ToString()))
                {
                    target.GetComponent<ThisCard>().actualAttack = j;
                    target.GetComponent<ThisCard>().actualLife = i;
                }
    }
}
