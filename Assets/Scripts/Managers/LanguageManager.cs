using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LanguageManager : MonoBehaviour
{
    private string language;
    public int languageNumber;
    static int savedLanguageNumber;
    static LanguageManager instance;
    [SerializeField]
    private List<TextLanguage> texts;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            savedLanguageNumber = languageNumber;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
        languageNumber = savedLanguageNumber;
        UpdateLanguage();
    }
    public void ChangeLanguage(string selectedLanguage)
    {
        language = selectedLanguage;
        if (language == "English" || language == "english")
            languageNumber = 0;
        if (language == "Spanish" || language == "spanish")
            languageNumber = 1;
        UpdateLanguage();
    }
    private void UpdateLanguage()
    {
        foreach (TextLanguage textLanguage in texts)
            textLanguage.text.text = textLanguage.languageText[languageNumber];
        foreach (CardCore cardCore in FindObjectsOfType<CardCore>())
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
