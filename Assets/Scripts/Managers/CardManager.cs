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
    public bool canZoom = true;
    public CardMagic previewMagic;
    public Card previewCard;
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

    public void PlaceCards(GameObject card)
    {
        cardToPlace = null;
        //foreach (GameObject things in draw.drawThings)
        //    things.SetActive(false);
        CardCore core = card.GetComponent<CardCore>();
        if (core.Equals(typeof(Card)))
        {
            previewCard.card = core.card;
        } else if (core.Equals(typeof(CardMagic)))
        {
            previewMagic.card = core.card;
        }
        _camera.PlaceCardCamera();
        cardToPlace = card;
        placeCards = true;
    }
    public void CancelPlacing()
    {
        canZoom = true;
        placeCards = false;
        cardToPlace = null;
        _camera.HandCamera();
    }
}
