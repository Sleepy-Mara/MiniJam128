using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelected : MonoBehaviour
{
    public static LevelSelected instance;
    public int level;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(this);
    }
}
