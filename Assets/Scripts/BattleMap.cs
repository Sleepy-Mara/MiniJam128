using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMap : MonoBehaviour
{
    public MapPosition[] playerPositions;
    public MapPosition[] enemyPositions;
    public List<GameObject> cameras;

    private void Awake()
    {
        for (int i = 0; i < playerPositions.Length; i++)
        {
            playerPositions[i].positionFacing = enemyPositions[i];
            playerPositions[i].positionNum = i;
            enemyPositions[i].positionFacing = playerPositions[i];
            enemyPositions[i].positionNum = i;
        }
    }
    public void SetCard(GameObject card, int pos)
    {
        card.transform.SetPositionAndRotation(playerPositions[pos].cardPos.transform.position, playerPositions[pos].cardPos.transform.rotation);
        playerPositions[pos].card = card.GetComponent<ThisCard>();
        playerPositions[pos].card.actualPosition = playerPositions[pos];
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
