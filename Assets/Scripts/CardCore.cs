using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardCore : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
    [SerializeField] protected Canvas canvas;
    protected Table _table;
    protected Draw _draw;

    private void Awake()
    {
        if (card != null)
            SetData();
        _table = FindObjectOfType<Table>();
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
            UpdateLanguage(FindObjectOfType<LanguageManager>().languageNumber);
        }
        else
            effectText.enabled = false;
    }
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
            GetComponent<Animator>().SetBool("Zoomed", true);
            canvas.overrideSorting = true;
            canvas.sortingOrder = 5;
        //if (!_draw.zoomingCard)
        //{
        //    //_draw.zoomingCard = true;
        //}
    }
    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.fullyExited)
        {
            GetComponent<Animator>().SetBool("Zoomed", false);
            //_draw.zoomingCard = false;
            canvas.sortingOrder = 0;
            canvas.overrideSorting = false;
        }
    }
    public void UpdateLanguage(int languageNumber)
    {
        if(card.hasEffect)
            effectText.text = card.effectDesc[languageNumber];
    }
}
