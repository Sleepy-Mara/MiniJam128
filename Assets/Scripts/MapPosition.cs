using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MapPosition 
{
    public Card card;
    public CardPos cardPos;
    public int positionNum;
    public MapPosition positionFacing;
    public MapPosition nextPosition;
    public Health oponent;
}
