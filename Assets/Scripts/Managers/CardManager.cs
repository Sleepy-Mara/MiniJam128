using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public Transform showCard;
    [HideInInspector] public bool placeCards;
    [HideInInspector] public GameObject cardToPlace;
    [HideInInspector] public Draw draw;
    public CameraManager _camera;
    void Start()
    {
        draw = FindObjectOfType<Draw>();
        //Debug.Log(draw.gameObject.name);
        _camera = FindObjectOfType<CameraManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (placeCards && Input.GetButtonDown("Fire2"))
        {
            CancelPlacing();
        }
    }

    public void ShowCard(GameObject card)
    {
        card.GetComponentInChildren<Card>().enabled = false;
        card.transform.position = showCard.position;
        card.transform.rotation = showCard.rotation;
        card.transform.localScale = card.transform.localScale * 1.5f;
    }

    public void HideCard(GameObject card)
    {
        Destroy(card);
    }

    public void PlaceCards(GameObject card)
    {
        cardToPlace = null;
        foreach (GameObject things in draw.drawThings)
            things.SetActive(false);
        _camera.PlaceCardCamera();
        cardToPlace = card;
        placeCards = true;
    }
    public void CancelPlacing()
    {
        placeCards = false;
        cardToPlace = null;
        _camera.HandCamera();
    }
}
