using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public Transform showCard;
    [HideInInspector] public bool placeCards;
    [HideInInspector] public GameObject cardToPlace;
    [HideInInspector] public Draw draw;
    private Table _table;
    void Start()
    {
        draw = FindObjectOfType<Draw>();
        Debug.Log(draw.gameObject.name);
        _table = FindObjectOfType<Table>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowCard(GameObject card)
    {
        card.GetComponentInChildren<Card>().enabled = false;
        card.transform.position = showCard.position;
        card.transform.rotation = showCard.rotation;
        card.transform.localScale = card.transform.localScale * 2;
    }

    public void HideCard(GameObject card)
    {
        Destroy(card);
    }

    public void PlaceCards(GameObject card)
    {
        foreach (GameObject things in draw.drawThings)
            things.SetActive(false);
        _table.ChangeCamera();
        cardToPlace = card;
        placeCards = true;
    }
}
