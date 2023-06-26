using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelected : MonoBehaviour
{
    private static LevelSelected instance;
    private static int level;
    public int Level
    {
        get { return level; }
        set { level = value; }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            level = 0;
        }
        else Destroy(gameObject);
        DontDestroyOnLoad(this);
    }
}
