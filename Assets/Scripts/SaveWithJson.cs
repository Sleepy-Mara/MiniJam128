using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveWithJson : MonoBehaviour
{
    [SerializeField] SaveData saveData = new SaveData();
    [HideInInspector] public SaveData SaveData
    {
        get { return saveData; }
        set { 
            saveData = value;
            string json = JsonUtility.ToJson(saveData);
            File.WriteAllText(path, json);
        }
    }
    [SerializeField] SaveData defaultSaveData;
    string path;
    // Start is called before the first frame update
    void Awake()
    {
        path = Application.persistentDataPath + "/data.jsan";
        if(File.ReadAllText(path)!= null)
        {
            string data = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(data, saveData);
            return;
        }
        string json = JsonUtility.ToJson(defaultSaveData);
        File.WriteAllText(path, json);
    }
}
