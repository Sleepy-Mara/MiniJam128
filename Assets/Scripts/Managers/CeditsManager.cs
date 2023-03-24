using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeditsManager : MonoBehaviour
{
    [HideInInspector]
    public bool changeScene;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (changeScene)
            FindObjectOfType<ChangeSceneManager>().ChangeScene("MainMenu");
    }
}
