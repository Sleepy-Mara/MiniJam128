using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeSceneManager : MonoBehaviour
{
    [HideInInspector]
    private static bool win;
    public bool Win;
    private static ChangeSceneManager instance;
    [SerializeField]
    private GameObject buttonMonkey;
    [SerializeField]
    private GameObject loadingScreen;
    [SerializeField]
    private Image loadingBarFill;
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
        //SceneManager.LoadScene(scene);
        StartCoroutine(LoadSceneAsync(scene));
    }
    IEnumerator LoadSceneAsync(string scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress/ 0.9f);
            loadingBarFill.fillAmount = progressValue;
            yield return null;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
