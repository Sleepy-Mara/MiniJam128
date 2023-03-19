using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatMenu : MonoBehaviour
{
    public GameObject defeatMenu;
    
    public void Defeat()
    {
        defeatMenu.SetActive(true);
    }
}
