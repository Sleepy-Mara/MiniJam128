using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneManager : MonoBehaviour
{
    [HideInInspector]
    private static bool win;
    public bool Win;
    private static ChangeSceneManager instance;
    [SerializeField]
    private GameObject buttonMonkey;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
        if (win && buttonMonkey != null)
            buttonMonkey.SetActive(true);
    }
    public void ChangeScene(string scene)
    {
        win = Win;
        Time.timeScale = 1;
        SceneManager.LoadScene(scene);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
