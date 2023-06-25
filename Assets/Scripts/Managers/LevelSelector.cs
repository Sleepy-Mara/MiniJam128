using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    private SaveWithJson json;
    private int unlockedLevels;
    [SerializeField] private List<GameObject> unlockedLevel;
    [SerializeField] private string sceneToGo;
    private void Awake()
    {
        json = FindObjectOfType<SaveWithJson>();
        unlockedLevels = json.SaveData.currentUnlockedLevels;
        if (unlockedLevels >= unlockedLevel.Count)
            return;
        for(int i = 0; i < unlockedLevels; i++)
            unlockedLevel[i].SetActive(false);
    }
    public void SelectedLevel(int selectedLevel)
    {
        FindObjectOfType<LevelSelected>().level = selectedLevel;
        FindObjectOfType<ChangeSceneManager>().ChangeScene(sceneToGo);
    }
}
