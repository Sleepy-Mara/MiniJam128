using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UIElements;

public class GenerateCard : MonoBehaviour
{
    public List<string> cardName = new List<string> {"English name", "Spanish name"};
    public int attack;
    public int life;
    public int manaCost;
    public int healthCost;
    public Sprite sprite;
    public bool hasEffect = false;
    public int hasEffectInt = 1;
    public string effect;
    public List<string> effectDescription = new List<string> { "English effect text", "Spanish effect text" };
    public Effects effects;
    public bool clearEffexts = true;
    public bool spell = false;
    public int spellInt = 1;
    public void CreateCard()
    {
        var card = ScriptableObject.CreateInstance<Cards>();
        string direction = "";
        if (spell)
            direction = "SpellCards";
        else if (hasEffect)
            direction = "EffectCards";
        else direction = "NormalCards";
        AssetDatabase.CreateAsset(card, "Assets/ScriptableObjects/" + direction + "/" + cardName[0] + ".asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();

        card.cardName = cardName;
        card.attack = attack;
        card.life = life;
        card.manaCost = manaCost;
        card.healthCost = healthCost;
        card.sprite = sprite;
        card.hasEffect = hasEffect;
        card.spell = spell;
        card.effects = effects;
        card.effectDesc = effectDescription;
        if (hasEffect)
        {
            card.hasEffect = hasEffect;
            card.effect = effect;
            card.effectDesc = effectDescription;
        }
    }
    public void UpdateCard(string name)
    {
        foreach (var asset in AssetDatabase.FindAssets(name))
        {
            var path = AssetDatabase.GUIDToAssetPath(asset);
            AssetDatabase.DeleteAsset(path);
        }
        CreateCard();
    }
    public void ClearInfo()
    {
        cardName = new List<string> { "English name", "Spanish name" };
        attack = 0;
        life = 0;
        manaCost = 0;
        healthCost = 0;
        sprite = null;
        hasEffect = false;
        hasEffectInt = 1;
        effect = null;
        effectDescription = new List<string> { "English effect text", "Spanish effect text" };
        effects = new Effects();
        clearEffexts = true;
        spell = false;
        spellInt = 0;
    }
}

[CustomEditor(typeof(GenerateCard))]
public class GenerateCardEditor : Editor
{
    private static GUIStyle myStyle = new GUIStyle();
    int indexCondition = 0;
    int indexExtraCondition = 0;
    int indexEffect = 0;
    int indexTarget = 0;
    int effextX = 0;
    int effextY = 0;
    int creaturesToAffect = 0;
    List<string> cards = new List<string>();
    bool updateCard = false;
    string cardName = "";
    public override void OnInspectorGUI()
    {
        GenerateCard card = (GenerateCard)target;
        if (card.clearEffexts)
        {
            card.effects.conditions = new List<string>();
            card.effects.extraConditions = new List<string>();
            card.effects.effects = new List<string>();
            card.effects.targets = new List<string>();
            card.clearEffexts = false;
        }
        var width = EditorGUIUtility.currentViewWidth;
        var height = Screen.height * (width / Screen.width) - 160;

        card.spellInt = GUILayout.Toolbar(card.spellInt, new string[] { "Creature card", "Spell card" });
        switch (card.spellInt)
        {
            case 0:
                card.spell = false;
                break;
            case 1:
                card.spell = true;
                break;
        }
        GUILayout.Space(20);
        GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
                EditorGUILayout.LabelField("English name", myStyle);
                card.cardName[0] = EditorGUILayout.TextField(card.cardName[0]);
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
                EditorGUILayout.LabelField("Spanish name", myStyle);
                card.cardName[1] = EditorGUILayout.TextField(card.cardName[1]);
            GUILayout.EndVertical();
        GUILayout.EndHorizontal();
        GUILayout.Space(20);
        GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
                EditorGUILayout.LabelField("Mana cost");
                card.manaCost = EditorGUILayout.IntField(card.manaCost, GUILayout.MaxWidth(50));
            GUILayout.EndVertical();
            GUILayout.Space(50);
            GUILayout.BeginVertical();
                EditorGUILayout.LabelField("Health cost");
                card.healthCost = EditorGUILayout.IntField(card.healthCost, GUILayout.MaxWidth(50));
            GUILayout.EndVertical();
        GUILayout.EndHorizontal();
        GUILayout.Space(20);
        GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();
                EditorGUILayout.LabelField("Sprite", myStyle);
                card.sprite = (Sprite)EditorGUILayout.ObjectField(card.sprite, typeof(Sprite), true,
                    GUILayout.MinWidth(50), GUILayout.MinHeight(75),
                    GUILayout.MaxWidth(200), GUILayout.MaxHeight(300));
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.Space(20);
        if (!card.spell)
        {
            GUILayout.BeginHorizontal();
                GUILayout.BeginVertical();
                    EditorGUILayout.LabelField("Attack");
                    card.attack = EditorGUILayout.IntField(card.attack, GUILayout.MaxWidth(50));
                GUILayout.EndVertical();
                GUILayout.Space(50);
                GUILayout.BeginVertical();
                    EditorGUILayout.LabelField("Life");
                    card.life = EditorGUILayout.IntField(card.life, GUILayout.MaxWidth(50));
                GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUILayout.Space(20);
            EditorGUILayout.LabelField("Has effect?", myStyle);
            card.hasEffectInt = GUILayout.Toolbar(card.hasEffectInt, new string[] { "Yes", "No" });
            switch (card.hasEffectInt)
            {
                case 0:
                    card.hasEffect = true;
                    break;
                case 1:
                    card.hasEffect = false;
                    break;
            }
            GUILayout.Space(10);
        }
        else card.hasEffect = true;
        switch (card.hasEffect)
        {
            case true:
                EditorGUILayout.LabelField("English effect text:", myStyle);
                card.effectDescription[0] = EditorGUILayout.TextArea(card.effectDescription[0], GUILayout.MaxHeight(50));
                GUILayout.Space(10);
                EditorGUILayout.LabelField("Spanish effect text:", myStyle);
                card.effectDescription[1] = EditorGUILayout.TextArea(card.effectDescription[1], GUILayout.MaxHeight(50));
                GUILayout.Space(10);
                GUILayout.BeginHorizontal();
                    GUILayout.BeginVertical();
                        EditorGUILayout.LabelField("Conditions:", myStyle);
                        indexCondition = EditorGUILayout.Popup(indexCondition, new Effects().conditions.ToArray());
                        var condition = new Effects().conditions[indexCondition];
                    GUILayout.EndVertical();
                    GUILayout.BeginVertical();
                        EditorGUILayout.LabelField("Conditions:", myStyle);
                        indexExtraCondition = EditorGUILayout.Popup(indexExtraCondition, new Effects().extraConditions.ToArray());
                        var extraCondition = new Effects().extraConditions[indexExtraCondition];
                    GUILayout.EndVertical();
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                    GUILayout.BeginVertical();
                        EditorGUILayout.LabelField("Effects:", myStyle);
                        indexEffect = EditorGUILayout.Popup(indexEffect, new Effects().effects.ToArray());
                        var effect = new Effects().effects[indexEffect];
                        switch (indexEffect)
                        {
                            case 1:
                                EditorGUILayout.LabelField("Draw cards");
                                effextX = EditorGUILayout.IntField(effextX, GUILayout.MaxWidth(50));
                                break;
                            case 2:
                                EditorGUILayout.LabelField("Damage");
                                effextX = EditorGUILayout.IntField(effextX, GUILayout.MaxWidth(50));
                                break;
                            case 3:
                                EditorGUILayout.LabelField("Health");
                                effextX = EditorGUILayout.IntField(effextX, GUILayout.MaxWidth(50));
                                break;
                            case 4:
                                EditorGUILayout.LabelField("Add cards");
                                effextX = EditorGUILayout.IntField(effextX, GUILayout.MaxWidth(50));
                                for (int i = 0; i < effextX; i++)
                                {
                                    if (cards.Count <= i)
                                        cards.Add("");
                                    cards[i] = EditorGUILayout.TextField(cards[i]);
                                }
                                break;
                            case 5:
                                EditorGUILayout.LabelField("Attack");
                                effextX = EditorGUILayout.IntField(effextX, GUILayout.MaxWidth(50));
                                EditorGUILayout.LabelField("Health");
                                effextY = EditorGUILayout.IntField(effextY, GUILayout.MaxWidth(50));
                                break;
                            case 7:
                                EditorGUILayout.LabelField("Summon cards");
                                effextX = EditorGUILayout.IntField(effextX, GUILayout.MaxWidth(50));
                                for (int i = 0; i < effextX; i++)
                                {
                                    if (cards.Count <= i)
                                        cards.Add("");
                                    cards[i] = EditorGUILayout.TextField(cards[i]);
                                }
                                break;
                        }
                    GUILayout.EndVertical();
                    GUILayout.BeginVertical();
                        EditorGUILayout.LabelField("Target:", myStyle);
                        indexTarget = EditorGUILayout.Popup(indexTarget, new Effects().targets.ToArray());
                        var target = new Effects().targets[indexTarget];
                        switch (indexTarget)
                        {
                            case 7:
                                EditorGUILayout.LabelField("Creatures");
                                creaturesToAffect = EditorGUILayout.IntField(creaturesToAffect, GUILayout.MaxWidth(50));
                                break;
                            case 8:
                                EditorGUILayout.LabelField("Creatures");
                                creaturesToAffect = EditorGUILayout.IntField(creaturesToAffect, GUILayout.MaxWidth(50));
                                break;
                            case 9:
                                EditorGUILayout.LabelField("Creatures");
                                creaturesToAffect = EditorGUILayout.IntField(creaturesToAffect, GUILayout.MaxWidth(50));
                                break;
                            case 10:
                                EditorGUILayout.LabelField("Creatures");
                                creaturesToAffect = EditorGUILayout.IntField(creaturesToAffect, GUILayout.MaxWidth(50));
                                break;
                            case 21:
                                EditorGUILayout.LabelField("Creatures");
                                creaturesToAffect = EditorGUILayout.IntField(creaturesToAffect, GUILayout.MaxWidth(50));
                                break;
                            case 22:
                                EditorGUILayout.LabelField("Creatures");
                                creaturesToAffect = EditorGUILayout.IntField(creaturesToAffect, GUILayout.MaxWidth(50));
                                break;
                        }
                    GUILayout.EndVertical();
                GUILayout.EndHorizontal();
                GUILayout.Space(10);
                if (GUILayout.Button("Add effect"))
                {
                    card.effects.conditions.Add(condition);
                    card.effects.extraConditions.Add(extraCondition);
                    card.effects.effects.Add(effect);
                    card.effects.targets.Add(target);
                    card.effects.x.Add(effextX);
                    card.effects.y.Add(effextY);
                    card.effects.numberOfTargets.Add(creaturesToAffect);
                    card.effects.cards.Add(cards);
                    indexCondition = 0;
                    indexExtraCondition = 0;
                    indexEffect = 0;
                    indexTarget = 0;
                    effextX = 0;
                    effextY = 0;
                    creaturesToAffect = 0;
                    cards = new List<string>();
                }
                for (int i = 0; i < card.effects.conditions.Count; i++)
                {
                    GUILayout.Space(10);
                    GUILayout.TextArea("Condition: " + card.effects.conditions[i] + " ExtraConditions: " + card.effects.extraConditions[i] 
                        + " Effect: " + card.effects.effects[i] + " " + card.effects.x[i] + " " + card.effects.y[i]
                        + " Targets: " + card.effects.targets[i] + " " + card.effects.numberOfTargets[i]);
                    if (GUILayout.Button("Remove effect"))
                    {
                        card.effects.conditions.RemoveAt(i);
                        card.effects.extraConditions.RemoveAt(i);
                        card.effects.effects.RemoveAt(i);
                        card.effects.targets.RemoveAt(i);
                        card.effects.x.RemoveAt(i);
                        card.effects.y.RemoveAt(i);
                        card.effects.numberOfTargets.RemoveAt(i);
                        card.effects.cards.RemoveAt(i);
                    }
                }
                break;
            case false:
                break;
        }
        GUILayout.Space(20);
        GUILayout.BeginHorizontal();
            if (GUILayout.Button("Create card"))
            {
                card.CreateCard();
                ClearInfo();
            }
            if (GUILayout.Button("Update card"))
            {
                updateCard = true;
            }
            if (GUILayout.Button("Clear info"))
            {
                card.ClearInfo();
                ClearInfo();
            }
        GUILayout.EndHorizontal();
        if (updateCard)
        {
            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Original english card name", myStyle);
                cardName = EditorGUILayout.TextField(cardName);
            GUILayout.EndHorizontal();
            GUILayout.Space(5);
            if (GUILayout.Button("Update card"))
            {
                updateCard = true;
                card.UpdateCard(cardName);
                ClearInfo();
            }
        }
    }
    private void ClearInfo()
    {
        indexCondition = 0;
        indexExtraCondition = 0;
        indexEffect = 0;
        indexTarget = 0;
        effextX = 0;
        effextY = 0;
        creaturesToAffect = 0;
        updateCard = false;
    }
    void OnEnable()
    {
        myStyle.alignment = TextAnchor.MiddleCenter;
    }
}
