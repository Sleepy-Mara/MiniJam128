using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

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
    [SerializeField] private TextMeshProUGUI effectText;
    [SerializeField] private Image image;
    //[SerializeField] private Image effectImage;
    [SerializeField] private Canvas canvas;
    [HideInInspector] public bool canPlay;
    private CardManager _cardManager;
    private TurnManager _turnManager;
    private Draw _draw;
    private Table _table;
    private bool _inmune = false;
    public RuntimeAnimatorController handAnimator;
    public RuntimeAnimatorController tableAnimator;
    public bool attack;
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
                Debug.Log("Yo " + card.name + " ataque a " + lastTarget.GetComponent<ThisCard>().card.name);
                lastTarget.GetComponent<ThisCard>().ReceiveDamage(actualAttack, this);
                //ejecutar audio y/o animacion
                if (actualPosition.positionFacing.card != null)
                    CheckEffect(4, actualPosition.positionFacing.card.gameObject);
                ReceiveDamage(lastTarget.GetComponent<ThisCard>().actualAttack, null);
            }
            if(lastTarget.GetComponent<Health>())
            {
                lastTarget.GetComponent<Health>().ReceiveDamage(card.attackToPlayer);
                CheckEffect(4, actualPosition.oponent.gameObject);
            }
            attack = false;
            lastTarget = null;
        }
    }
    public void SetData()
    {
        actualLife = card.life;
        actualAttack = card.attack;
        nameText.text = card.name;
        image.sprite = card.sprite;
        //image.SetNativeSize();
        attackText.text = card.attack.ToString();
        lifeText.text = actualLife.ToString();
        manaCostText.text = card.manaCost.ToString();
        healthCostText.text = card.healthCost.ToString();
        if (card.hasEffect)
        {
            effectText.text = card.effectDesc;
        }
        else
            effectText.enabled = false;
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
        if(this != null)
            GetComponent<Animator>().SetTrigger("Attack");
    }
    public void ReceiveDamage(int damage, ThisCard enemy)
    {
        if (enemy != null)
            CheckEffect(4, enemy.gameObject);
        if (!_inmune)
        {
            GetComponent<Animator>().SetTrigger("GetDamage");
            actualLife -= damage;
            lifeText.text = actualLife.ToString();
        }
        if (actualLife <= 0)
        {
            Death(enemy);
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
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if (_table.player.EnoughMana(card.manaCost) && _table.player.EnoughHealth(card.healthCost))
            {
                if (actualPosition.cardPos == null && _turnManager.CanPlayCards())
                {
                    _cardManager.PlaceCards(gameObject);
                }
            }
            else
            {
                Debug.Log("no tenes mana o vida");
            }
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
        if (actualPosition.cardPos != null)
            CheckEffect(1, null);
    }
    public void OnTurnStart()
    {
        if (actualPosition.cardPos != null)
            CheckEffect(0, null);
    }
    public void OnTurnEnd()
    {
        if (actualPosition.cardPos != null)
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
            Debug.LogError(doEffect +"&"+ x + "&" + effectToDo + card.name);
            if (doEffect && effectToDo != null)
                foreach (string effect in effectToDo)
                    Effect(effect, target);
        }
    }
    private void Effect(string effect, GameObject target)
    {
        var eventNumber = card.keywords.IndexOf(effect);
        if (eventNumber == 5)
            DrawEffect();
        if (eventNumber == 6)
            DealDamgeEffect(target);
        if (eventNumber == 7)
            BuffAlly();
        if (eventNumber == 8)
            GiveCard();
    }
    private void DrawEffect()
    {
        Debug.LogError("Tengo efecto");
        _draw.DrawACard();
    }
    private void DealDamgeEffect(GameObject target)
    {
        if (target.GetComponent<ThisCard>())
            target.GetComponent<ThisCard>().ReceiveDamage(actualAttack, this);
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
    private void GiveCard()
    {
        //if (card.effectDesc.Contains(card.keywords[9]))
        //{
        //    Cards newCard = null;
        //    if (card.effectDesc.Contains(card.keywords[11]))
        //        newCard = (Cards)AssetDatabase.LoadAssetAtPath("Assets/ScripObjects/Normal cards/" + card.keywords[11], typeof(ScriptableObject));
        //    if (card.effectDesc.Contains(card.keywords[12]))
        //        newCard = (Cards)AssetDatabase.LoadAssetAtPath("Assets/ScripObjects/Normal cards/" + card.keywords[12], typeof(ScriptableObject));
        //    _draw.AddACard(newCard);
        //}
        //if (card.effectDesc.Contains(card.keywords[10]))
        //{
        //    GameObject newCard = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Card", typeof(GameObject));
        //    if (card.effectDesc.Contains(card.keywords[13]))
        //        newCard.GetComponent<ThisCard>().card = (Cards)AssetDatabase.LoadAssetAtPath("Assets/ScripObjects/Normal cards" + card.keywords[13],
        //            typeof(ScriptableObject));
        //    if (card.effectDesc.Contains(card.keywords[14]))
        //        newCard.GetComponent<ThisCard>().card = (Cards)AssetDatabase.LoadAssetAtPath("Assets/ScripObjects/Normal cards" + card.keywords[14],
        //            typeof(ScriptableObject));
        //    Instantiate(newCard);
        //    _draw.AddCardToHand(newCard.GetComponent<ThisCard>());
        //}
    }
}