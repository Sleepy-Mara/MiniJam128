using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LanguageManager : MonoBehaviour
{
    public string language;
    public int languageNumber;
    static int savedLanguageNumber;
    static LanguageManager instance;
    [SerializeField]
    private List<TextLanguage> texts;
    private SaveWithJson json;
    private void Start()
    {
        json = FindObjectOfType<SaveWithJson>();
        language = json.SaveData.language;
        ChangeLanguage(language);
    }
    public void ChangeLanguage(string selectedLanguage)
    {
        language = selectedLanguage;
        if (language == "English" || language == "english")
            languageNumber = 0;
        if (language == "Spanish" || language == "spanish")
            languageNumber = 1;
        savedLanguageNumber = languageNumber;
        SaveData saveData = json.SaveData;
        saveData.language = language;
        json.SaveData = saveData;
        UpdateLanguage();
    }
    private void UpdateLanguage()
    {
        foreach (TextLanguage textLanguage in texts)
            textLanguage.text.text = textLanguage.languageText[languageNumber];
        foreach (CardCore cardCore in FindObjectsOfType<CardCore>())
            if (cardCore.card != null)
                cardCore.UpdateLanguage(languageNumber);
    }
}

[System.Serializable]
public class TextLanguage
{
    [SerializeField] private string name;
    [TextArea(1, 4)]
    public List<string> languageText;
    public TextMeshProUGUI text;
}
