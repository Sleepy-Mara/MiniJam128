using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

//#if UNITY_WEBGL && !UNITY_EDITOR
//        [DllImport("__Internal")]
//        private static extern void SyncDB();
//#endif
public class SaveWithJson : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void SyncDB();
    [SerializeField] SaveData saveData = new SaveData();
    [HideInInspector] public SaveData SaveData
    {
        get { return saveData; }
        set {
            saveData = value;
            string json = JsonUtility.ToJson(saveData, true);
            File.WriteAllText(path, json);
            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                SyncDB();
            }
        }
    }
    [SerializeField] SaveData defaultSaveData;
    string path;
    // Start is called before the first frame update
    void Awake()
    {
        path = Application.persistentDataPath + "/data.json";
        if(File.Exists(path))
        {
            string data = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(data, saveData);
            return;
        }
        saveData = defaultSaveData;
        string json = JsonUtility.ToJson(defaultSaveData, true);
        File.WriteAllText(path, json);
    }
    public void ResetSave()
    {
        SaveData = defaultSaveData;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
