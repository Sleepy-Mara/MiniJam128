using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LanguageManager)), CanEditMultipleObjects]
public class LanguageManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        LanguageManager languageManager = (LanguageManager)target;
        if (GUILayout.Button("Change Language"))
            languageManager.ChangeLanguage(languageManager.language);
    }
}
