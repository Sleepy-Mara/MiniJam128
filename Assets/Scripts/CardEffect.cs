using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffect : ScriptableObject
{
    public string effect;
    public Sprite sprite;

    public void PlayEffect()
    {
        Debug.Log("se jugo " + effect);
    }
}
