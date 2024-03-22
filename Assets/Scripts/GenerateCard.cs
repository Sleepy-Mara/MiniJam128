using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UIElements;

public class GenerateCard : MonoBehaviour
{
    public string scriptableName = "Scriptable name";
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
    public int spellInt = 0;
    public string id = "0000";
    public Cards CreateCard()
    {
        Cards card = ScriptableObject.CreateInstance<Cards>();
        string direction = "";
        string suffix = "";
        if (spell)
        {
            direction = "SpellCards";
            suffix = "_M";
        }
        else if (hasEffect)
        {
            direction = "EffectCards";
            suffix = "_E";
        }
        else
        {
            direction = "NormalCards";
            suffix = "_N";
        }
        AssetDatabase.CreateAsset(card, "Assets/ScriptableObjects/" + direction + "/SOC_" + id + "_" + scriptableName + suffix + ".asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        card.scriptableName = scriptableName;
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
        card.id = id;
        if (hasEffect)
        {
            card.hasEffect = hasEffect;
            card.effect = effect;
            card.effectDesc = effectDescription;
        }
        ClearInfo();
        return card;
    }
    public void UpdateCard(string cardId)
    {
        foreach (var asset in AssetDatabase.FindAssets("SOC_"))
        {
            var path = AssetDatabase.GUIDToAssetPath(asset);
            var objects = AssetDatabase.LoadAssetAtPath(path, typeof(Cards));
            if (objects.ConvertTo<Cards>().id == cardId)
                AssetDatabase.DeleteAsset(path);
        }
        CreateCard();
    }
    public void ClearInfo()
    {
        scriptableName = "Scriptable name";
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
    public string GetID(int id)
    {
        if (id < 10)
            return "000" + id;
        else if (id < 100)
            return "00" + id;
        else if (id < 1000)
            return "0" + id;
        else return id.ToString();
    }
}

[CustomEditor(typeof(GenerateCard))]
public class GenerateCardEditor : Editor
{
    private static GUIStyle myStyle = new GUIStyle();
    Effects effects = new Effects();
    int indexCondition = 0;
    int indexExtraCondition = 0;
    int indexEffect = 0;
    int indexTargetCreature = 0;
    int indexTargetDeck = 0;
    int indexTargetCard = 0;
    int indexTargetSummon = 0;
    int effextX = 0;
    int effextY = 0;
    int creaturesToAffect = 0;
    List<string> cards = new List<string>();
    bool updateCard = false;
    bool showLastUpdate = false;
    string cardName = "";
    bool firstTimeId = true;
    string loadId = "0000";
    public override void OnInspectorGUI()
    {
        GenerateCard card = (GenerateCard)target;
        if (card.clearEffexts)
        {
            card.effects.conditions = new List<string>();
            card.effects.extraConditions = new List<string>();
            card.effects.effects = new List<string>();
            card.effects.targetsCreatures = new List<string>();
            card.effects.targetsDecks = new List<string>();
            card.effects.targetsCards = new List<string>();
            card.effects.targetsSummon = new List<string>();
            card.clearEffexts = false;
        }
        var width = EditorGUIUtility.currentViewWidth;
        var height = Screen.height * (width / Screen.width) - 160;

        GUILayout.Space(10);
        if (GUILayout.Button("Reload card list"))
        {
            ReloadList(card);
        }
        GUILayout.Space(20);
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
            EditorGUILayout.LabelField("ID:", myStyle);
            if (ReloadList(card).Count != 0 && firstTimeId)
            {
                card.id = card.GetID(ReloadList(card).Count);
                loadId = card.id;
                firstTimeId = false;
            }
            EditorGUILayout.LabelField(card.id);
        GUILayout.EndHorizontal();
        GUILayout.Space(20);
        EditorGUILayout.LabelField("Name", myStyle);
        card.scriptableName = EditorGUILayout.TextField(card.scriptableName);
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
                        EditorGUILayout.LabelField("Extra conditions:", myStyle);
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
                        if (effect == new Effects().effects[1])
                        {
                            if (effects.targetsDecks[1] == new Effects().targetsDecks[1])
                                effects.targetsDecks.RemoveAt(1);
                            indexTargetCard = EditorGUILayout.Popup(indexTargetCard, new Effects().targetsCards.ToArray());
                            indexTargetDeck = EditorGUILayout.Popup(indexTargetDeck, effects.targetsDecks.ToArray());
                            indexTargetCreature = 0;
                            indexTargetSummon = 0;
                        }
                        else if (effect == new Effects().effects[4])
                        {
                            if (effects != new Effects())
                                effects = new Effects();
                            indexTargetDeck = EditorGUILayout.Popup(indexTargetDeck, new Effects().targetsDecks.ToArray());
                            indexTargetCard = EditorGUILayout.Popup(indexTargetCard, new Effects().targetsCards.ToArray());
                            if (indexTargetCard == 4)
                            {
                                for (int i = 0; i < effextX; i++)
                                {
                                    if (cards.Count <= i)
                                        cards.Add("");
                                    cards[i] = EditorGUILayout.TextField(cards[i]);
                                }
                            }
                            indexTargetCreature = 0;
                            indexTargetSummon = 0;
                        }
                        else if (effect != new Effects().effects[7])
                        {
                            if (effect == new Effects().effects[5])
                            {
                                if (effects.targetsCreatures[3] == new Effects().targetsCreatures[3])
                                {
                                    effects.targetsCreatures.RemoveAt(13);
                                    effects.targetsCreatures.RemoveAt(12);
                                    effects.targetsCreatures.RemoveAt(4);
                                    effects.targetsCreatures.RemoveAt(3);
                                }
                            }
                            else
                            {
                                if (effects != new Effects())
                                    effects = new Effects();
                            }
                            indexTargetCreature = EditorGUILayout.Popup(indexTargetCreature, effects.targetsCreatures.ToArray());
                            if (effect == new Effects().effects[5])
                            {
                                switch (indexTargetCreature)
                                {
                                    case 5:
                                        EditorGUILayout.LabelField("Creatures");
                                        creaturesToAffect = EditorGUILayout.IntField(creaturesToAffect, GUILayout.MaxWidth(50));
                                        break;
                                    case 6:
                                        EditorGUILayout.LabelField("Creatures");
                                        creaturesToAffect = EditorGUILayout.IntField(creaturesToAffect, GUILayout.MaxWidth(50));
                                        break;
                                    case 11:
                                        EditorGUILayout.LabelField("Creatures");
                                        creaturesToAffect = EditorGUILayout.IntField(creaturesToAffect, GUILayout.MaxWidth(50));
                                        break;
                                    case 12:
                                        EditorGUILayout.LabelField("Creatures");
                                        creaturesToAffect = EditorGUILayout.IntField(creaturesToAffect, GUILayout.MaxWidth(50));
                                        break;
                                }
                                indexTargetDeck = 0;
                                indexTargetCard = 0;
                                indexTargetSummon = 0;
                            }
                            else
                            {
                                switch (indexTargetCreature)
                                {
                                    case 7:
                                        EditorGUILayout.LabelField("Creatures");
                                        creaturesToAffect = EditorGUILayout.IntField(creaturesToAffect, GUILayout.MaxWidth(50));
                                        break;
                                    case 8:
                                        EditorGUILayout.LabelField("Creatures");
                                        creaturesToAffect = EditorGUILayout.IntField(creaturesToAffect, GUILayout.MaxWidth(50));
                                        break;
                                    case 15:
                                        EditorGUILayout.LabelField("Creatures");
                                        creaturesToAffect = EditorGUILayout.IntField(creaturesToAffect, GUILayout.MaxWidth(50));
                                        break;
                                    case 16:
                                        EditorGUILayout.LabelField("Creatures");
                                        creaturesToAffect = EditorGUILayout.IntField(creaturesToAffect, GUILayout.MaxWidth(50));
                                        break;
                                }
                                indexTargetDeck = 0;
                                indexTargetCard = 0;
                                indexTargetSummon = 0;
                            }
                        }
                        else
                        {
                            indexTargetSummon = EditorGUILayout.Popup(indexTargetSummon, new Effects().targetsSummon.ToArray());
                            indexTargetDeck = 0;
                            indexTargetCard = 0;
                            indexTargetCreature = 0;
                        }
                        var targetDeck = new Effects().targetsDecks[new Effects().targetsDecks.IndexOf(effects.targetsDecks[indexTargetDeck])];
                        var targetCard = new Effects().targetsCards[new Effects().targetsCards.IndexOf(effects.targetsCards[indexTargetCard])];
                        var targetCreature = new Effects().targetsCreatures[new Effects().targetsCreatures.IndexOf
                            (effects.targetsCreatures[indexTargetCreature])];
                        var targetSummon = new Effects().targetsSummon[indexTargetSummon];
                    GUILayout.EndVertical();
                GUILayout.EndHorizontal();
                GUILayout.Space(10);
                if (GUILayout.Button("Add effect"))
                {
                    card.effects.conditions.Add(condition);
                    card.effects.extraConditions.Add(extraCondition);
                    card.effects.effects.Add(effect);
                    card.effects.targetsDecks.Add(targetDeck);
                    card.effects.targetsCards.Add(targetCard);
                    card.effects.targetsCreatures.Add(targetCreature);
                    card.effects.targetsSummon.Add(targetSummon);
                    card.effects.x.Add(effextX);
                    card.effects.y.Add(effextY);
                    card.effects.numberOfTargets.Add(creaturesToAffect);
                    card.effects.cards.Add(cards);
                    indexCondition = 0;
                    indexExtraCondition = 0;
                    indexEffect = 0;
                    indexTargetCreature = 0;
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
                        + " Targets: " + card.effects.targetsCreatures[i] + " " + card.effects.numberOfTargets[i]);
                    if (GUILayout.Button("Remove effect"))
                    {
                        card.effects.conditions.RemoveAt(i);
                        card.effects.extraConditions.RemoveAt(i);
                        card.effects.effects.RemoveAt(i);
                        card.effects.targetsCreatures.RemoveAt(i);
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
                foreach (Cards cards in ReloadList(card))
                    if (cards.id == card.id)
                    {
                        Debug.Log("ID in use");
                        return;
                    }
                firstTimeId = true;
                ClearInfo();
            }
            if (GUILayout.Button("Update card"))
            {
                updateCard = true;
            }
            if (GUILayout.Button("Clear info"))
            {
                card.ClearInfo();
                firstTimeId = true;
                ClearInfo();
            }
        GUILayout.EndHorizontal();
        if (updateCard)
        {
            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Original ID", myStyle);
                loadId = EditorGUILayout.TextField(loadId);
                if (GUILayout.Button("Load card"))
                {
                    foreach (Cards cards in ReloadList(card))
                {
                    if (cards.id == loadId)
                    {
                        cardName = cards.name;
                        card.scriptableName = cards.scriptableName;
                        card.cardName = cards.cardName;
                        card.attack = cards.attack;
                        card.life = cards.life;
                        card.manaCost = cards.manaCost;
                        card.healthCost = cards.healthCost;
                        card.sprite = cards.sprite;
                        card.clearEffexts = false;
                        card.hasEffect = cards.hasEffect;
                        if (cards.hasEffect)
                            card.hasEffectInt = 0;
                        else card.hasEffectInt = 1;
                        card.effectDescription = cards.effectDesc;
                        card.effects = cards.effects;
                        card.spell = cards.spell;
                        if (cards.spell)
                            card.spellInt = 1;
                        else card.spellInt = 0;
                        card.id = loadId;
                        showLastUpdate = true;
                    }
                    Debug.Log(cards.id + " " + loadId);
                }
                }
            GUILayout.EndHorizontal();
            GUILayout.Space(5);
            if (showLastUpdate)
                if (GUILayout.Button("Update card"))
                {
                    showLastUpdate = false;
                    updateCard = false;
                    card.id = loadId;
                    card.UpdateCard(loadId);
                    firstTimeId = true;
                    ReloadList(card);
                    ClearInfo();
                }
        }
    }
    private void ClearInfo()
    {
        effects = new Effects();
        indexCondition = 0;
        indexExtraCondition = 0;
        indexEffect = 0;
        indexTargetCreature = 0;
        effextX = 0;
        effextY = 0;
        creaturesToAffect = 0;
        updateCard = false;
    }
    private List<Cards> ReloadList(GenerateCard card)
    {
        List<Cards> retirnGeneratedCards = new List<Cards>();
        var tempCards = new List<Cards>();
        foreach (var asset in AssetDatabase.FindAssets("SOC_"))
        {
            var path = AssetDatabase.GUIDToAssetPath(asset);
            var objects = AssetDatabase.LoadAssetAtPath(path, typeof(Cards));
            tempCards.Add(objects.ConvertTo<Cards>());
        }
        int j = tempCards.Count;
        int lastId = -1;
        for (int i = 0; i < j; i++)
        {
            int acrualId = tempCards.Count;
            Cards cardToRemove = null;
            foreach (Cards cards in tempCards)
            {
                int.TryParse(cards.id, out int x);
                if (x <= acrualId && x > lastId)
                {
                    acrualId = x;
                    cardToRemove = cards;
                }
            }
            if (cardToRemove == null)
                break;
            retirnGeneratedCards.Add(cardToRemove);
            lastId = acrualId;
        }
        card.id = card.GetID(retirnGeneratedCards.Count);
        return retirnGeneratedCards;
    }
    void OnEnable()
    {
        myStyle.alignment = TextAnchor.MiddleCenter;
    }
}
