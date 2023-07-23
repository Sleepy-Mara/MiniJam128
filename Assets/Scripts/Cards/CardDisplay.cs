using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Cards card;
    [SerializeField] protected TextMeshProUGUI nameText;
    [SerializeField] protected TextMeshProUGUI attackText;
    [SerializeField] protected TextMeshProUGUI lifeText;
    [SerializeField] protected TextMeshProUGUI manaCostText;
    [SerializeField] protected TextMeshProUGUI healthCostText;
    [SerializeField] protected TextMeshProUGUI effectText;
    [SerializeField] protected Image image;
    [SerializeField] protected Canvas canvas;

    private void Awake()
    {
        if (card != null)
            SetData();
        canvas = FindObjectOfType<Canvas>();
    }

    public virtual void SetData()
    {
        image.sprite = card.sprite;
        attackText.text = card.attack.ToString();
        lifeText.text = card.life.ToString();
        manaCostText.text = card.manaCost.ToString();
        healthCostText.text = card.healthCost.ToString();
        UpdateLanguage(FindObjectOfType<LanguageManager>().languageNumber);
    }
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        ZoomIn();
    }
    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.fullyExited)
        {
            ZoomOut();
        }
    }
    #region Zoom
    private void ZoomIn()
    {
        GetComponent<Animator>().SetBool("Zoomed", true);
        canvas.overrideSorting = true;
        canvas.sortingOrder = 5;
    }
    private void ZoomOut()
    {
        GetComponent<Animator>().SetBool("Zoomed", false);
        canvas.sortingOrder = 0;
        canvas.overrideSorting = false;
    }
    #endregion
    public virtual void UpdateLanguage(int languageNumber)
    {
        nameText.text = card.cardName[languageNumber];
        if (card.hasEffect)
        {
            effectText.enabled = true;
            effectText.text = card.effectDesc[languageNumber];
        }
        else
            effectText.enabled = false;
    }
}
