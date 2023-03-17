using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public Transform showCard;
    void Start()
    {
        
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
        Debug.Log("aaaaa");
        Destroy(card);
    }
}
