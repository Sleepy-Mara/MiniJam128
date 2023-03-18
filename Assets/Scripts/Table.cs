using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    public List<GameObject> cameras;
    private bool _inTable;
    private Draw _draw;

    void Start()
    {
        _draw = FindObjectOfType<Draw>();
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
