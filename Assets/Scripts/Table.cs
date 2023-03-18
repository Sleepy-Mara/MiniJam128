using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    public List<GameObject> cameras;
    private bool _inTable;
    private Draw _draw;
    public List<MapPosition> mapPositions;

    [HideInInspector] public List<ThisCard> myCards = new List<ThisCard>();

    void Start()
    {
        _draw = FindObjectOfType<Draw>();
        foreach (MapPosition position in mapPositions)
            foreach (MapPosition mapPosition in mapPositions)
                if (position.transform == mapPosition.transform)
                    position.positionFacing = mapPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
            ChangeCamera();
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
            ChangeCamera();
    }

    public void SetCard(GameObject card, Transform position)
    {
        card.transform.position = position.position + new Vector3(0, 0.01f, 0);
        card.transform.rotation = Quaternion.Euler(90, 0, 0);
        foreach (MapPosition positions in mapPositions)
            if (positions.transform == position)
                positions.card = card.GetComponent<ThisCard>();
        myCards.Add(card.GetComponent<ThisCard>());
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
