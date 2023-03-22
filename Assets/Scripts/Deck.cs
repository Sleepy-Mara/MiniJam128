using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    private Draw draw;
    private Animator animator;
    private void Start()
    {
        draw = FindObjectOfType<Draw>();
        animator = GetComponentInParent<Animator>();
    }
    private void OnMouseDown()
    {
        if (draw.canDraw)
            animator.SetTrigger("Activate");
    }
    public void DrawedCard()
    {
        draw.PlayerDraw();
    }
}
