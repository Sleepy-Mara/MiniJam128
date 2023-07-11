using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField]
    private GameObject loadingScreen;
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private float timeBetweenDots;
    [SerializeField]
    private Animator fade;
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
        fade.SetTrigger("Fade");
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => fade.GetCurrentAnimatorStateInfo(0).IsName("Fade"));
        loadingScreen.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        while (!operation.isDone)
        {
            string currentText = text.text;
            for (int i = 0; i < 3; i++)
            {
                text.text += ".";
                yield return new WaitForSeconds(timeBetweenDots);
            }
            text.text = currentText;
            yield return null;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
