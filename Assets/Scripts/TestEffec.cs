using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Test1")]
public class TestEffec : Cards
{

    public override void PlayEffect()
    {
        //base.PlayEffect();
        Debug.Log("La carta " + cardName + " activa el efecto " + effectDesc);
    }
}
