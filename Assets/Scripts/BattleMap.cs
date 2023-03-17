using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMap : MonoBehaviour
{
    public MapPosition[] positions;

    private void Awake()
    {
        for (int i = 0; i < 5; i++)
        {
            positions[i].positionFacing = positions[i + 5];
        }
    }
    public void SetCard(GameObject card, int pos)
    {
        card.transform.position = positions[pos].transform.position;
        card.transform.rotation = positions[pos].transform.rotation;
        positions[pos].card = card.GetComponent<ThisCard>();
        positions[pos].card.actualPosition = positions[pos];
    }
}
