using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ThisCard : MonoBehaviour
{
    public Cards card;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private TextMeshProUGUI lifeText;
    [SerializeField] private TextMeshProUGUI manaCostText;
    [SerializeField] private TextMeshProUGUI healthCostText;
    [SerializeField] private Image image;
    [SerializeField] private Image effectImage;
    private void Awake()
    {
        nameText.text = card.name;
        image.sprite = card.sprite;
        attackText.text = card.attack.ToString();
        lifeText.text = card.life.ToString();
        manaCostText.text = card.manaCost.ToString();
        healthCostText.text = card.healthCost.ToString();
        if (card.hasEffect)
        {
            effectImage.sprite = card.effectSprite;
        }
        else 
            effectImage.enabled = false;
        card.PlayEffect();
    }
}
