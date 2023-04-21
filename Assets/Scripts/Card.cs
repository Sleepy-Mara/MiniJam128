using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : CardCore, IPointerDownHandler
{
    public bool inAttackAnim;
    public bool inDamageAnim;
    [HideInInspector]
    public bool immune;
    public RuntimeAnimatorController handAnimator;
    public RuntimeAnimatorController tableAnimator;
    [HideInInspector]
    public bool played = false;
    private void Start()
    {
        _effectManager = FindObjectOfType<EffectManager>();
        _cardManager = FindObjectOfType<CardManager>();
        _turnManager = FindObjectOfType<TurnManager>();
        immune = false;
    }
    public int ActualLife
    {
        get { return actualLife; }
        set { actualLife += value;
            if (actualLife < card.life)
                lifeText.color = Color.red;
            else if (actualLife > card.life)
                lifeText.color = Color.green;
            else lifeText.color = Color.black;
            lifeText.text = actualLife.ToString();
        }
    }
    public int ActualAttack
    {
        get { return actualAttack; }
        set { actualAttack += value;
            if (actualAttack < card.attack)
                attackText.color = Color.red;
            else if (actualAttack > card.attack)
                attackText.color = Color.green;
            else attackText.color = Color.black;
            attackText.text = actualAttack.ToString();
        }
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
                Debug.Log(card.name + " ataco");
            actualPosition.positionFacing.card.GetComponent<Card>().ReceiveDamagePublic(ActualAttack, this);
            //ejecutar audio y/o animacion
            checkingEffect = true;
            _effectManager.CheckConditionAttack(this);
            ReceiveDamagePublic(actualPosition.positionFacing.card.GetComponent<Card>().ActualAttack, null);
        }
        else
        {
            actualPosition.oponent.GetComponent<Health>().ReceiveDamage(card.attackToPlayer);
            checkingEffect = true;
            _effectManager.CheckConditionAttack(this);
        }
    }
    public void ReceiveDamagePublic(int damage, Card attacker)
    {
        StartCoroutine(ReceiveDamage(damage, attacker));
    }
    IEnumerator ReceiveDamage(int damage, Card attacker)
    {
        yield return new WaitUntil(() => checkingEffect == false);
        yield return new WaitForSeconds(1);
        if (attacker != null)
            yield return new WaitUntil(() => inAttackAnim == false && attacker.inAttackAnim == false);
        else yield return new WaitUntil(() => inAttackAnim == false);
        checkingEffect = true;
        _effectManager.CheckConditionGetDamaged(this);
        if (!immune)
        {
            if (damage > 0)
                GetComponent<Animator>().SetTrigger("GetDamage");
            ActualLife = -damage;
        }
        else
            immune = false;
        yield return new WaitUntil(() => checkingEffect == false);
        if (ActualLife <= 0)
        {
            StartCoroutine(Defeated(attacker));
        }
    }
    IEnumerator Defeated(Card attacker)
    {
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => inDamageAnim == false);
        if (attacker != null)
        {
            checkingEffect = true;
            _effectManager.CheckConditionDefeatsAnEnemy(attacker);
        }
        if (actualPosition.oponent == FindObjectOfType<Enemy>())
        {
            foreach (MapPosition mapPosition in _table.playerPositions)
                if (mapPosition.card != null)
                {
                    checkingEffect = true;
                    _effectManager.CheckConditionAllyIsDefeated(mapPosition.card);
                }
        }
        else
            foreach (MapPosition mapPosition in _table.enemyFront)
                if (mapPosition.card != null)
                {
                    checkingEffect = true;
                    _effectManager.CheckConditionAllyIsDefeated(mapPosition.card);
                }
        checkingEffect = true;
        _effectManager.CheckConditionDefeated(this);
        yield return new WaitUntil(() => checkingEffect == false);
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
        foreach (Cemetery cemetery in FindObjectsOfType<Cemetery>())
        {
            if (cemetery.player && actualPosition.oponent.GetComponent<Enemy>())
                cemetery.AddCard(card);
            if (!cemetery.player && actualPosition.oponent.GetComponent<Player>())
                cemetery.AddCard(card);
        }
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
        {
            posOrNegLife = -1;
            if (ActualLife <= 1)
                life = 0;
        }
        if (attack < 0)
        {
            posOrNegAttack = -1;
            if (ActualAttack <= 0)
                attack = 0;
        }
        Debug.LogError(card.cardName + " get buffed by " + attack * posOrNegAttack + "/" + life * posOrNegLife);
        for (int i = 0; i < life; i++)
            ActualLife = posOrNegLife;
        for (int i = 0; i < attack; i++)
            ActualAttack = posOrNegAttack;
        checkingEffect = true;
        _effectManager.CheckConditionGetBuffed(this);
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
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (!_draw.zoomingCard)
        {
            GetComponent<Animator>().SetBool("Zoomed", true);
            canvas.overrideSorting = true;
            canvas.sortingOrder = 5;
            _draw.zoomingCard = true;
        }
    }
    public override void OnPointerExit(PointerEventData eventData)
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
