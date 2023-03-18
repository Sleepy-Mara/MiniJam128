using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ThisCard : MonoBehaviour
{
    public Cards card;
    private Player player;
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
    private void Awake()
    {
        player = FindObjectOfType<Player>();
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
            player.ReceiveDamage(card.attackToPlayer);
        }
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
        //animacion / audio
        Debug.Log("La carta " + card.cardName + " se murio :c");
        actualPosition.card = null;
        GameObject.Destroy(this);
    }
    public void OnPlayEffect()
    {
        card.PlayEffect();
    }
    public void OnTurnStart()
    {
        card.TurnStart();
    }
    public void OnTurnEnd()
    {
        card.TurnEnd();
    }
}
