using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject menu;
    
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            if (menu.activeSelf)
                SetActiveMenu(false);
            else SetActiveMenu(true);
    }

    public void SetActiveMenu(bool x)
    {
        if (x)
            Time.timeScale = 0;
        else Time.timeScale = 1;
        menu.SetActive(x);
    }
}
