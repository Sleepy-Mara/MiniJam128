using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : CardCore
{
    public bool inAttackAnim;
    public bool inDamageAnim;
    public bool immune;
    public RuntimeAnimatorController handAnimator;
    public RuntimeAnimatorController tableAnimator;
    [HideInInspector]
    public bool played = false;
    public bool inAnimation;
    private List<CardTempEffect> cardTempEffects = new List<CardTempEffect>();
    private int attackToPlayer;
    [SerializeField]
    private string enemyAttackTrigger;
    [SerializeField]
    private string enemyDamageTrigger;
    [SerializeField]
    private string playerAttackTrigger;
    [SerializeField]
    private string playerDamageTrigger;
    [SerializeField]
    private string enemyAttackAnim;
    [SerializeField]
    private string enemyDamageAnim;
    [SerializeField]
    private string playerAttackAnim;
    [SerializeField]
    private string playerDamageAnim;
    private string attackTrigger;
    private string damageTrigger;
    private string oponentTrigger;
    [HideInInspector]
    public string attackAnim;
    private string damageAnim;
    private string oponentAnim;
    [HideInInspector]
    public bool waitForSelect;
    private void Start()
    {
        _effectManager = FindObjectOfType<EffectManager>();
        _cardManager = FindObjectOfType<CardManager>();
        _turnManager = FindObjectOfType<TurnManager>();
    }
    public int ActualLife
    {
        get { return currentLife; }
        set { currentLife += value;
            if (currentLife < card.life)
                lifeText.color = Color.red;
            else if (currentLife > card.life)
                lifeText.color = Color.green;
            else lifeText.color = Color.black;
            lifeText.text = currentLife.ToString();
        }
    }
    public int ActualAttack
    {
        get { return currentAttack; }
        set { currentAttack += value;
            if (currentAttack < card.attack)
                attackText.color = Color.red;
            else if (currentAttack > card.attack)
                attackText.color = Color.green;
            else attackText.color = Color.black;
            attackText.text = currentAttack.ToString();
        }
    }
    public void Attack()
    {
        if (ActualAttack <= 0)
        {
            inAnimation = false;
            return;
        }
        //if (currentPosition.oponent.GetComponent<Enemy>())
        //    GetComponent<Animator>().SetTrigger("AttackPlayer");
        //else GetComponent<Animator>().SetTrigger("AttackEnemy");
        GetComponent<Animator>().SetTrigger(attackTrigger);
        //print(card.name + " se enfrenta a " + currentPosition.positionFacing.card);
        if (currentPosition.positionFacing.card != null)
        {
            currentPosition.positionFacing.card.GetComponent<Card>().ReceiveDamagePublic(ActualAttack, this);
            //ejecutar audio y/o animacion
            checkingEffect = true;
            _effectManager.CheckConditionAttack(this);
            //ReceiveDamagePublic(actualPosition.positionFacing.card.GetComponent<Card>().ActualAttack, null);
        }
        else
        {
            if (ActualAttack >= 10)
                attackToPlayer = 3;
            else if (ActualAttack >= 5)
                attackToPlayer = 2;
            else attackToPlayer = 1;
            StartCoroutine(AttackCharacter());
        }
    }
    IEnumerator AttackCharacter()
    {
        //string myAttackAnim = "";
        //if (currentPosition.oponent.GetComponent<Enemy>())
        //    myAttackAnim = "AttackPlayer";
        //else
        //    myAttackAnim = "AttackEnemy";
        yield return new WaitUntil(() => !inAnimation);
        currentPosition.oponent.GetComponent<Health>().ReceiveDamage(attackToPlayer, false, false);
        checkingEffect = true;
        _effectManager.CheckConditionAttack(this);
    }
    public void ReceiveDamagePublic(int damage, Card attacker)
    {
        inAnimation = true;
        StartCoroutine(ReceiveDamage(damage, attacker));
    }
    IEnumerator ReceiveDamage(int damage, Card attacker)
    {
        yield return new WaitUntil(() => checkingEffect == false);
        //string myAttackAnim = "";
        //string attackerAttackAnim = "";
        //if (currentPosition.oponent.GetComponent<Enemy>())
        //{
        //    attackerAttackAnim = "AttackEnemy";
        //    myAttackAnim = "AttackPlayer";
        //}
        //else
        //{
        //    attackerAttackAnim = "AttackPlayer";
        //    myAttackAnim = "AttackEnemy";
        //}
        //if (attacker != null)
        //    yield return new WaitUntil(() => GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(myAttackAnim) && attacker.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("AttackEnemy"));
        //else yield return new WaitUntil(() => GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("AttackEnemy"));
        
        //if (attacker != null)
        //    yield return new WaitUntil(() => !attacker.inAnimation);
        checkingEffect = true;
        _effectManager.CheckConditionGetDamaged(this);
        bool damaged = false;
        if (!immune)
        {
            if (damage > 0)
                GetComponent<Animator>().SetTrigger(damageTrigger);
            ActualLife = -damage;
            damaged = true;
        } else inAnimation = false;
        yield return new WaitUntil(() => checkingEffect == false);
        if (damaged)
            yield return new WaitUntil(() => !inAnimation);
        if (ActualLife <= 0)
        {
            StartCoroutine(Defeated(attacker));
        }
    }
    IEnumerator Defeated(Card attacker)
    {
        if (attacker != null)
        {
            checkingEffect = true;
            _effectManager.CheckConditionDefeatsAnEnemy(attacker);
        }
        if (currentPosition.oponent == FindObjectOfType<Enemy>())
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
        currentPosition.card = null;
        if (currentPosition.oponent.GetComponent<Player>())
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
        //foreach (Cemetery cemetery in FindObjectsOfType<Cemetery>())
        //{
        //    if (cemetery.player && actualPosition.oponent.GetComponent<Enemy>())
        //        cemetery.AddCard(card);
        //    if (!cemetery.player && actualPosition.oponent.GetComponent<Player>())
        //        cemetery.AddCard(card);
        //}
        //!!!!!!!---REACER---
        //if (currentPosition.oponent.GetComponent<Enemy>())
        //    FindObjectOfType<CardToCemeteryAnimation>().AddCard(card, currentPosition, true);
        //if (currentPosition.oponent.GetComponent<Player>())
        //    FindObjectOfType<CardToCemeteryAnimation>().AddCard(card, currentPosition, false);
        //!!!!!!!--- ---41
        Destroy(gameObject);
    }
    public void ReceiveDamageEffect(int damage, Card attacker, bool startTurn, bool endTurn)
    {
        if (startTurn)
            cardTempEffects.Add(new CardTempEffect { startTurn = true, endTurn = false, damage = damage });
        if (endTurn)
            cardTempEffects.Add(new CardTempEffect { startTurn = false, endTurn = true, damage = damage });
        StartCoroutine(ReceiveDamage(damage, attacker));
    }
    public void HealEffect(int heal, bool startTurn, bool endTurn)
    {
        if (startTurn)
            cardTempEffects.Add(new CardTempEffect { startTurn = true, endTurn = false, heal = heal });
        if (endTurn)
            cardTempEffects.Add(new CardTempEffect { startTurn = false, endTurn = true, heal = heal });
        Heal(heal);
    }
    public void Heal(int heal)
    {
        for (int i = 0; i < heal; i++)
            if (ActualLife < card.life)
                ActualLife = 1;
    }
    public void BuffEffect(int attack, int life, bool startTurn, bool endTurn)
    {
        if (startTurn)
            cardTempEffects.Add(new CardTempEffect { startTurn = true, endTurn = false, attack = attack, life = life });
        if (endTurn)
            cardTempEffects.Add(new CardTempEffect { startTurn = false, endTurn = true, attack = attack, life = life });
        Buff(attack, life);
    }
    public void Buff(int attack, int life)
    {
        int posOrNegLife = 1;
        int posOrNegAttack = 1;
        if (life > 0 || attack > 0)
            _effectManager.CheckConditionGetBuffed(this);
        if (life < 0)
        {
            posOrNegLife = -1;
        }
        if (attack < 0)
        {
            posOrNegAttack = -1;
        }
        for (int i = 0; i < life; i++)
        {
            if (ActualLife <= 1 && posOrNegLife < 0)
                break;
            ActualLife = posOrNegLife;
        }
        for (int i = 0; i < attack; i++)
        {
            if (ActualAttack <= 0 && posOrNegAttack < 0)
                break;
            ActualAttack = posOrNegAttack;
        }
    }
    public void ImmuneEffect(bool startTurn, bool endTurn)
    {
        if (startTurn)
            cardTempEffects.Add(new CardTempEffect { startTurn = true, endTurn = false, immune = true });
        if (endTurn)
            cardTempEffects.Add(new CardTempEffect { startTurn = false, endTurn = true, immune = true });
        Immune();
    }
    public void Immune()
    {
        immune = true;
    }
    public void StartTurn()
    {
        foreach (CardTempEffect effect in cardTempEffects)
        {
            if (effect.endTurn || !effect.startTurn)
                continue;
            if (effect.immune)
                immune = false;
            if (effect.destroy)
                StartCoroutine(Defeated(null));
            if (effect.damage > 0)
                Heal(effect.damage);
            if (effect.heal > 0)
                ReceiveDamagePublic(effect.heal, null);
            if (effect.attack != 0 || effect.life != 0)
                Buff(effect.attack * -1, effect.life * -1);
            effect.startTurn = false;
        }
    }
    public void EndTurn()
    {
        foreach (CardTempEffect effect in cardTempEffects)
        {
            if (effect.startTurn || !effect.endTurn)
                continue;
            if (effect.immune)
                immune = false;
            if (effect.destroy)
                StartCoroutine(Defeated(null));
            if (effect.damage != 0)
                Heal(effect.damage);
            if (effect.heal != 0)
                ReceiveDamagePublic(effect.heal, null);
            if (effect.attack != 0 || effect.life != 0)
                Buff(effect.attack * -1, effect.life * -1);
            effect.endTurn = false;
        }
    }
    protected override void Selected(MapPosition pos)
    {
        if (pos.card != null)
        {
            _draw.AdjustHand();
            FindObjectOfType<CameraManager>().HandCamera();
            return;
        }
        pos.cardPos.SelectThisPosition(gameObject);
    }
    protected override void SelectCard()
    {
        _cardManager.PlaceCards(gameObject);
    }
    public void PlayerCard()
    {
        attackTrigger = playerAttackTrigger;
        damageTrigger = playerDamageTrigger;
        oponentTrigger = enemyAttackTrigger;
        attackAnim = playerAttackAnim;
        damageAnim = playerDamageAnim;
        oponentAnim = enemyAttackAnim;
    }
    public void EnemyCard()
    {
        attackTrigger = enemyAttackTrigger;
        damageTrigger = enemyDamageTrigger;
        oponentTrigger = playerAttackTrigger;
        attackAnim = enemyAttackAnim;
        damageAnim = enemyDamageAnim;
        oponentAnim = playerAttackAnim;
    }
    public void EndAnim()
    {
        inAnimation = false;
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (waitForSelect)
        {
            _effectManager.selectedCard = this;
            waitForSelect = false;
            return;
        }
        base.OnPointerClick(eventData);
    }
    public IEnumerator CardSelected()
    {
        yield return new WaitUntil(() => _effectManager.waitForSelect == false);
        waitForSelect = false;
    }
}
[System.Serializable]
public class CardTempEffect
{
    public bool startTurn;
    public bool endTurn;
    public bool destroy;
    public int attack;
    public int life;
    public int heal;
    public int damage;
    public bool immune;
}
