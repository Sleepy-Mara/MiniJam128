using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public Transform showCard;
    [HideInInspector] public bool placeCards;
    [HideInInspector] public GameObject cardToPlace;
    private Draw draw;
    private BattleMap battleMap;
    void Start()
    {
        draw = FindObjectOfType<Draw>();
        battleMap = FindObjectOfType<BattleMap>();
    }

    // Update is called once per frame
    void Update()
    {
        if(placeCards)
        {
            if(Input.GetMouseButtonDown(1))
            {
                placeCards = false;
                return;
            }
        }
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
        battleMap.ChangeCamera();
        cardToPlace = card;
        placeCards = true;
    }
}
