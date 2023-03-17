using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Test2")]
public class TestEffect2 : Cards
{
    public override void PlayEffect()
    {
        Debug.Log("La carta " + cardName + " juega el efecto " + effectDesc);
    }
}
