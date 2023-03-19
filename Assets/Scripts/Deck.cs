using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    private Draw draw;
    private void Start()
    {
        draw = FindObjectOfType<Draw>();
    }
    private void OnMouseDown()
    {
        draw.PlayerDraw();
    }
}
