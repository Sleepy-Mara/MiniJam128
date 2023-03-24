using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardCore : MonoBehaviour
{
    public Cards card;
    public MapPosition actualPosition;
    protected int actualLife;
    protected int actualAttack;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] protected TextMeshProUGUI attackText;
    [SerializeField] protected TextMeshProUGUI lifeText;
    [SerializeField] private TextMeshProUGUI manaCostText;
    [SerializeField] private TextMeshProUGUI healthCostText;
    [SerializeField] private TextMeshProUGUI effectText;
    [SerializeField] private Image image;

    private void Awake()
    {
        if (card != null)
            SetData();
    }

    public void SetData()
    {
        actualLife = card.life;
        actualAttack = card.attack;
        nameText.text = card.name;
        image.sprite = card.sprite;
        //float aspectRatio = card.sprite.rect.width / card.sprite.rect.height;
        //image.GetComponent<AspectRatioFitter>().aspectRatio = aspectRatio;
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
}
