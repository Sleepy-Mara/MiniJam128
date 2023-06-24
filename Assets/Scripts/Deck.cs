using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public Draw.DeckType type;
    private Draw draw;
    private Animator animator;
    private void Start()
    {
        draw = FindObjectOfType<Draw>();
        if (type == Draw.DeckType.Mana)
            draw.manaDeckObject = gameObject;
        else
            draw.bloodDeckObject = gameObject;
        animator = GetComponentInParent<Animator>();
    }
    private void OnMouseDown()
    {
        if (draw.canDraw)
        {
            animator.SetTrigger("Activate");
            GetComponent<AudioSource>().Play();
            draw.canDraw = false;
        }
    }
    public void DrawedCard()
    {
        draw.PlayerDraw(type);
    }
}
