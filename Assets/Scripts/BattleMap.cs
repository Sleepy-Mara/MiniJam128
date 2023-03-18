using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMap : MonoBehaviour
{
    public MapPosition[] positions;
    public List<GameObject> cameras;

    private void Awake()
    {
        for (int i = 0; i < positions.Length / 2; i++)
        {
            positions[i].positionFacing = positions[i + (positions.Length / 2)];
        }
    }
    public void SetCard(GameObject card, int pos)
    {
        card.transform.position = positions[pos].transform.position;
        card.transform.rotation = positions[pos].transform.rotation;
        positions[pos].card = card.GetComponent<ThisCard>();
        positions[pos].card.actualPosition = positions[pos];
    }

    public void ChangeCamera()
    {
        if (cameras[0].activeSelf)
        {
            cameras[1].SetActive(true);
            cameras[0].SetActive(false);
        }
        else
        {
            cameras[0].SetActive(true);
            cameras[1].SetActive(false);
        }
    }
}
