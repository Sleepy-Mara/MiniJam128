using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Strategy")]
public class Strategy : ScriptableObject
{
    public CardPlaceing[] turns;
}
