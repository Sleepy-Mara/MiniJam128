using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private List<GameObject> lockLevel;
    [SerializeField] private string sceneToGo;
    private void Start()
    {
        SaveWithJson json = FindObjectOfType<SaveWithJson>();
        int unlockedLevels = json.SaveData.currentUnlockedLevels;
        for (int i = 0; i <= unlockedLevels; i++)
            lockLevel[i].SetActive(true);
    }
    public void SelectedLevel(int selectedLevel)
    {
        if (!FindObjectOfType<Stamina>().UseStamina(1))
            return;
        FindObjectOfType<LevelSelected>().Level = selectedLevel;
        FindObjectOfType<ChangeSceneManager>().ChangeScene(sceneToGo);
    }
    public void TestStamina()
    {
        if (!FindObjectOfType<Stamina>().UseStamina(1))
            return;
    }
}
