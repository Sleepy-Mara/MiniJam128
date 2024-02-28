using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPoint : MonoBehaviour
{
    public GameObject canvas;
    public bool isArrow;
    void Start()
    {
        canvas = GameObject.Find("MainCanvas");
        transform.SetParent(canvas.transform);
        transform.localScale = Vector3.one;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
