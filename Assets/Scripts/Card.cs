using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : CardCore, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public bool inAttackAnim;
    public bool inDamageAnim;
    [SerializeField] 
    private Canvas canvas;
    private EffectManager _effectManager;
    private CardManager _cardManager;
    private TurnManager _turnManager;
    private Table _table;
    private Draw _draw;
    [HideInInspector]
    public bool immune;
    public RuntimeAnimatorController handAnimator;
    public RuntimeAnimatorController tableAnimator;
    [HideInInspector]
    public bool playerCard;
    private void Start()
    {
        _effectManager = FindObjectOfType<EffectManager>();
        _cardManager = FindObjectOfType<CardManager>();
        _turnManager = FindObjectOfType<TurnManager>();
        _table = FindObjectOfType<Table>();
        _draw = FindObjectOfType<Draw>();
        immune = false;
    }
    public int ActualLife
    {
        get { return actualLife; }
        set { actualLife += value;
            if (actualLife < card.life)
                lifeText.color = Color.red;
            else lifeText.color = Color.black;
            lifeText.text = actualLife.ToString();
        }
    }
    public int ActualAttack
    {
        get { return actualAttack; }
        set { actualAttack += value; }
    }
    public void Attack()
    {
        if (ActualAttack <= 0)
            return;
        if (this != null)
            if (actualPosition.oponent.GetComponent<Enemy>())
                GetComponent<Animator>().SetTrigger("AttackPlayer");
            else GetComponent<Animator>().SetTrigger("AttackEnemy");
        if (actualPosition.positionFacing.card != null)
        {
            actualPosition.positionFacing.card.GetComponent<Card>().ReceiveDamagePublic(ActualAttack, actualPosition.positionFacing.card.GetComponent<Card>());
            //ejecutar audio y/o animacion
            _effectManager.CheckConditionAttack(this);
            ReceiveDamagePublic(actualPosition.positionFacing.card.GetComponent<Card>().ActualAttack, null);
        }
        else
        {
            actualPosition.oponent.GetComponent<Health>().ReceiveDamage(card.attackToPlayer);
            _effectManager.CheckConditionAttack(this);
        }
    }
    public void ReceiveDamagePublic(int damage, Card attacker)
    {
        StartCoroutine(ReceiveDamage(damage, attacker));
    }
    IEnumerator ReceiveDamage(int damage, Card attacker)
    {
        yield return new WaitForSeconds(1);
        if (attacker != null)
            yield return new WaitUntil(() => inAttackAnim == false && attacker.inAttackAnim == false);
        else yield return new WaitUntil(() => inAttackAnim == false);
        _effectManager.CheckConditionGetDamaged(this);
        if (!immune)
        {
            if (damage > 0)
                GetComponent<Animator>().SetTrigger("GetDamage");
            ActualLife = -damage;
        }
        if (ActualLife <= 0)
        {
            StartCoroutine(Defeated(attacker));
        }
    }
    IEnumerator Defeated(Card attacker)
    {
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => inDamageAnim == false);
        _effectManager.checking = true;
        _effectManager.CheckConditionDefeated(this);
        yield return new WaitUntil(() => _effectManager.checking == false);
        //animacion / audio
        actualPosition.card = null;
        if (actualPosition.oponent.GetComponent<Player>())
        {
            for (int j = 0; j < _table.enemyFront.Length; j++)
                if (_table.enemyFront[j].card != null)
                    if (_table.enemyFront[j].card == this)
                        _table.enemyFront[j].card = null;
        }
        else
            for (int j = 0; j < _table.playerPositions.Length; j++)
                if (_table.playerPositions[j].card != null)
                    if(_table.playerPositions[j].card == this)
                        _table.playerPositions[j].card = null;
        Destroy(gameObject);
    }
    public void Heal(int heal)
    {
        for (int i = 0; i < heal; i++)
            if (ActualLife < card.life)
                ActualLife = 1;
        Debug.Log("Heal me " + heal);
    }
    public void Buff(int attack, int life)
    {
        int posOrNegLife = 1;
        int posOrNegAttack = 1;
        if (life < 0)
            posOrNegLife = -1;
        if (attack < 0)
            posOrNegAttack = -1;
        for (int i = 0; i < life; i++)
            if (ActualLife < card.life)
                ActualLife = posOrNegLife;
        for (int i = 0; i < attack; i++)
            if (ActualAttack < card.attack)
                ActualAttack = posOrNegAttack;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && (actualPosition.cardPos == null || playerCard))
        {
            if (_turnManager.CanPlayCards())
            {
                if (_table.player.EnoughMana(card.manaCost) && _table.player.EnoughHealth(card.healthCost))
                {
                    _cardManager.PlaceCards(gameObject);
                }
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_draw.zoomingCard)
        {
            GetComponent<Animator>().SetBool("Zoomed", true);
            canvas.overrideSorting = true;
            canvas.sortingOrder = 5;
            _draw.zoomingCard = true;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.fullyExited)
        {
            GetComponent<Animator>().SetBool("Zoomed", false);
            _draw.zoomingCard = false;
            canvas.sortingOrder = 0;
            canvas.overrideSorting = false;
        }
    }
}
