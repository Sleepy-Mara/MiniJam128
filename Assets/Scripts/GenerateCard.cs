using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEditor.UIElements;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

public class GenerateCard : MonoBehaviour
{
    #region private variables
    private List<string> cardName = new List<string> { "English name", "Spanish name" };
    private int attack;
    private int life;
    private int manaCost;
    private int healthCost;
    private Sprite sprite;
    private List<string> effectDescription = new List<string> { "English effect text", "Spanish effect text" };
    #endregion
    public string scriptableName = "Scriptable name";
    public List<string> CardName
    {
        get { return cardName; }
        set 
        {
            cardName = value;
            VisualCard();
        }
    }
    public int Attack
    {
        get { return attack; }
        set
        {
            attack = value;
            VisualCard();
        }
    }
    public int Life
    {
        get { return life; }
        set
        {
            life = value;
            VisualCard();
        }
    }
    public int ManaCost
    {
        get { return manaCost; }
        set
        {
            manaCost = value;
            VisualCard();
        }
    }
    public int HealthCost
    {
        get { return healthCost; }
        set
        {
            healthCost = value;
            VisualCard();
        }
    }
    public Sprite Sprite
    {
        get { return sprite; }
        set
        {
            sprite = value;
            VisualCard();
        }
    }
    public List<string> EffectDescription
    {
        get { return effectDescription; }
        set
        {
            effectDescription = value;
            VisualCard();
        }
    }
    public bool hasEffect = false;
    public int hasEffectInt = 1;
    public string effect;
    public Effects effects;
    public bool clearEffexts = true;
    public bool spell = false;
    public int spellInt = 0;
    public bool token = false;
    public string id = "0000";
    public CardCore card;
    public Cards CreateCard()
    {
        Cards card = ScriptableObject.CreateInstance<Cards>();
        string direction = "";
        string suffix = "_";
        if (token)
            suffix += "T";
        if (healthCost > 0)
            suffix += "B";
        if (spell)
        {
            direction = "SpellCards";
            suffix += "M";
        }
        else if (hasEffect)
        {
            direction = "EffectCards";
            suffix += "E";
        }
        else
        {
            direction = "NormalCards";
            suffix += "N";
        }
        if (token)
            direction += "/Tokens";
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
        UnityEditor.EditorUtility.SetDirty(card);
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
        token = false;
        VisualCard();
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
    public void VisualCard()
    {
        if (card == null)
        {
            if (!FindObjectOfType<CardCore>())
                return;
            card = FindObjectOfType<CardCore>();
        }
        if (card.card == null)
            card.card = new Cards();
        card.card.cardName = cardName;
        card.card.attack = attack;
        card.card.life = life;
        card.ManaCost = manaCost;
        card.HealthCost = healthCost;
        card.card.sprite = sprite;
        card.card.hasEffect = hasEffect;
        card.card.effectDesc = effectDescription;
        card.SetData();
    }
}

[CustomEditor(typeof(GenerateCard))]
public class GenerateCardEditor : Editor
{
    private static GUIStyle myStyle = new GUIStyle();
    Effects effects = new Effects();
    int indexCondition = 0;
    int indexTempEffect = 0;
    int indexEffect = 0;
    List<int> indexExtraCondition = new List<int>();
    int hasExtraConditions = 1;
    ExtraConditions extraConditions = new ExtraConditions();
    int indexTargetCreature = 0;
    int indexTargetDeck = 0;
    int indexTargetCard = 0;
    int indexTargetPlayers = 0;
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
            card.effects.tempEffect = new List<string>();
            card.effects.effects = new List<string>();
            card.effects.extraConditions = new List<ExtraConditions>();
            card.effects.targetsCreatures = new List<string>();
            card.effects.targetsDecks = new List<string>();
            card.effects.targetsCards = new List<string>();
            card.effects.targetsPlayers = new List<string>();
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
                card.CardName[0] = EditorGUILayout.TextField(card.CardName[0]);
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
                EditorGUILayout.LabelField("Spanish name", myStyle);
                card.CardName[1] = EditorGUILayout.TextField(card.CardName[1]);
            GUILayout.EndVertical();
        GUILayout.EndHorizontal();
        GUILayout.Space(20);
        GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
                EditorGUILayout.LabelField("Mana cost");
                card.ManaCost = EditorGUILayout.IntField(card.ManaCost, GUILayout.MaxWidth(50));
            GUILayout.EndVertical();
            GUILayout.Space(50);
            GUILayout.BeginVertical();
                EditorGUILayout.LabelField("Health cost");
                card.HealthCost = EditorGUILayout.IntField(card.HealthCost, GUILayout.MaxWidth(50));
            GUILayout.EndVertical();
        GUILayout.EndHorizontal();
        GUILayout.Space(20);
        GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();
                EditorGUILayout.LabelField("Sprite", myStyle);
                card.Sprite = (Sprite)EditorGUILayout.ObjectField(card.Sprite, typeof(Sprite), true,
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
                    card.Attack = EditorGUILayout.IntField(card.Attack, GUILayout.MaxWidth(50));
                GUILayout.EndVertical();
                GUILayout.Space(50);
                GUILayout.BeginVertical();
                    EditorGUILayout.LabelField("Life");
                    card.Life = EditorGUILayout.IntField(card.Life, GUILayout.MaxWidth(50));
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
                card.EffectDescription[0] = EditorGUILayout.TextArea(card.EffectDescription[0], GUILayout.MaxHeight(50));
                GUILayout.Space(10);
                EditorGUILayout.LabelField("Spanish effect text:", myStyle);
                card.EffectDescription[1] = EditorGUILayout.TextArea(card.EffectDescription[1], GUILayout.MaxHeight(50));
                GUILayout.Space(10);
                GUILayout.BeginHorizontal();
                    GUILayout.BeginVertical();
                        EditorGUILayout.LabelField("Conditions:", myStyle);
                        indexCondition = EditorGUILayout.Popup(indexCondition, new Effects().conditions.ToArray());
                        var condition = new Effects().conditions[indexCondition];
                    GUILayout.EndVertical();
                    GUILayout.BeginVertical();
                        EditorGUILayout.LabelField("TempEffects:", myStyle);
                        indexTempEffect = EditorGUILayout.Popup(indexTempEffect, new Effects().tempEffect.ToArray());
                        var tempEffect = new Effects().tempEffect[indexTempEffect];
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
                            case 8:
                                EditorGUILayout.LabelField("Mana");
                                effextX = EditorGUILayout.IntField(effextX, GUILayout.MaxWidth(50));
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
                            indexTargetPlayers = 0;
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
                            indexTargetPlayers = 0;
                        }
                        else if (effect != new Effects().effects[7] && effect != new Effects().effects[8])
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
                                indexTargetPlayers = 0;
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
                                indexTargetPlayers = 0;
                            }
                        }
                        else
                        {
                            indexTargetPlayers = EditorGUILayout.Popup(indexTargetPlayers, new Effects().targetsPlayers.ToArray());
                            indexTargetDeck = 0;
                            indexTargetCard = 0;
                            indexTargetCreature = 0;
                        }
                        var targetDeck = new Effects().targetsDecks[new Effects().targetsDecks.IndexOf(effects.targetsDecks[indexTargetDeck])];
                        var targetCard = new Effects().targetsCards[new Effects().targetsCards.IndexOf(effects.targetsCards[indexTargetCard])];
                        var targetCreature = new Effects().targetsCreatures[new Effects().targetsCreatures.IndexOf
                            (effects.targetsCreatures[indexTargetCreature])];
                        var targetSummon = new Effects().targetsPlayers[indexTargetPlayers];
                    GUILayout.EndVertical();
                GUILayout.EndHorizontal();
                EditorGUILayout.LabelField("Has extra conditions?", myStyle);
                hasExtraConditions = GUILayout.Toolbar(hasExtraConditions, new string[] { "Yes", "No" });
                GUILayout.Space(5);
                switch (hasExtraConditions)
                {
                    case 0:
                        if (extraConditions == new ExtraConditions())
                        {
                            extraConditions.extraConditions.Clear();
                            extraConditions.extraConditionsInt.Clear();
                            extraConditions.cards.Clear();
                        }
                        if (GUILayout.Button("Add extra effect"))
                        {
                            extraConditions.extraConditions.Add("");
                            extraConditions.extraConditionsInt.Add(0);
                            extraConditions.cards.Add(new List<string>());
                            indexExtraCondition.Add(0);
                        }
                        for (int i = 0; i < indexExtraCondition.Count; i++)
                        {
                            GUILayout.Space(5);
                            GUILayout.BeginHorizontal();
                                GUILayout.BeginVertical();
                                    indexExtraCondition[i] = EditorGUILayout.Popup(indexExtraCondition[i], new ExtraConditions().extraConditions.ToArray());
                                    if (indexExtraCondition[i] == 5 || indexExtraCondition[i] == 6 || indexExtraCondition[i] == 11 || indexExtraCondition[i] == 12 ||
                                        indexExtraCondition[i] == 17 || indexExtraCondition[i] == 18 || indexExtraCondition[i] == 21 || indexExtraCondition[i] == 22 || 
                                        indexExtraCondition[i] == 25 || indexExtraCondition[i] == 26)
                                    {
                                        EditorGUILayout.LabelField("!!!! In AT LEAST this: '-' is equal to this: '<=' !!!!");
                                        EditorGUILayout.LabelField("!!!! If the number is '+' is equal to this: '>=' !!!!");
                                    }
                                    extraConditions.extraConditionsInt[i] = EditorGUILayout.IntField(extraConditions.extraConditionsInt[i], GUILayout.MaxWidth(50));
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                    switch (indexExtraCondition[i])
                                    {
                                        case 1:
                                            for (int j = 0; j < extraConditions.extraConditionsInt[i]; j++)
                                            {
                                                if (extraConditions.cards.Count <= i)
                                                    extraConditions.cards.Add(new List<string>());
                                                if (extraConditions.cards[i].Count <= j)
                                                    extraConditions.cards[i].Add("");
                                                extraConditions.cards[i][j] = EditorGUILayout.TextField(extraConditions.cards[i][j]);
                                            }
                                            break;
                                        case 2:
                                            for (int j = 0; j < extraConditions.extraConditionsInt[i]; j++)
                                            {
                                                if (extraConditions.cards.Count <= i)
                                                    extraConditions.cards.Add(new List<string>());
                                                if (extraConditions.cards[i].Count <= j)
                                                    extraConditions.cards[i].Add("");
                                                extraConditions.cards[i][j] = EditorGUILayout.TextField(extraConditions.cards[i][j]);
                                            }
                                            break;
                                        case 7:
                                            for (int j = 0; j < extraConditions.extraConditionsInt[i]; j++)
                                            {
                                                if (extraConditions.cards.Count <= i)
                                                    extraConditions.cards.Add(new List<string>());
                                                if (extraConditions.cards[i].Count <= j)
                                                    extraConditions.cards[i].Add("");
                                                extraConditions.cards[i][j] = EditorGUILayout.TextField(extraConditions.cards[i][j]);
                                            }
                                            break;
                                        case 8:
                                            for (int j = 0; j < extraConditions.extraConditionsInt[i]; j++)
                                            {
                                                if (extraConditions.cards.Count <= i)
                                                    extraConditions.cards.Add(new List<string>());
                                                if (extraConditions.cards[i].Count <= j)
                                                    extraConditions.cards[i].Add("");
                                                extraConditions.cards[i][j] = EditorGUILayout.TextField(extraConditions.cards[i][j]);
                                            }
                                            break;
                                        case 13:
                                            for (int j = 0; j < extraConditions.extraConditionsInt[i]; j++)
                                            {
                                                if (extraConditions.cards.Count <= i)
                                                    extraConditions.cards.Add(new List<string>());
                                                if (extraConditions.cards[i].Count <= j)
                                                    extraConditions.cards[i].Add("");
                                                extraConditions.cards[i][j] = EditorGUILayout.TextField(extraConditions.cards[i][j]);
                                            }
                                            break;
                                        case 14:
                                            for (int j = 0; j < extraConditions.extraConditionsInt[i]; j++)
                                            {
                                                if (extraConditions.cards.Count <= i)
                                                    extraConditions.cards.Add(new List<string>());
                                                if (extraConditions.cards[i].Count <= j)
                                                    extraConditions.cards[i].Add("");
                                                extraConditions.cards[i][j] = EditorGUILayout.TextField(extraConditions.cards[i][j]);
                                            }
                                            break;
                                    }
                                GUILayout.EndVertical();
                            GUILayout.EndHorizontal();
                        }
                        break;
                    case 1:
                        extraConditions = new ExtraConditions();
                        indexExtraCondition = new List<int>();
                        break;
                }
                GUILayout.Space(20);
                if (GUILayout.Button("Add effect"))
                {
                    card.effects.conditions.Add(condition);
                    card.effects.extraConditions.Add(extraConditions);
                    card.effects.tempEffect.Add(tempEffect);
                    card.effects.effects.Add(effect);
                    card.effects.targetsDecks.Add(targetDeck);
                    card.effects.targetsCards.Add(targetCard);
                    card.effects.targetsCreatures.Add(targetCreature);
                    card.effects.targetsPlayers.Add(targetSummon);
                    card.effects.x.Add(effextX);
                    card.effects.y.Add(effextY);
                    card.effects.numberOfTargets.Add(creaturesToAffect);
                    card.effects.cards.Add(cards);
                    indexCondition = 0;
                    extraConditions = new ExtraConditions();
                    indexExtraCondition = new List<int>();
                    hasExtraConditions = 1;
                    indexTempEffect = 0;
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
                    GUILayout.TextArea("Condition: " + card.effects.conditions[i] + " ExtraConditions: " + card.effects.tempEffect[i] 
                        + " Effect: " + card.effects.effects[i] + " " + card.effects.x[i] + " " + card.effects.y[i]
                        + " Targets: " + card.effects.targetsCreatures[i] + " " + card.effects.numberOfTargets[i]);
                    if (GUILayout.Button("Remove effect"))
                    {
                        card.effects.conditions.RemoveAt(i);
                        card.effects.tempEffect.RemoveAt(i);
                        card.effects.effects.RemoveAt(i);
                        card.effects.targetsCreatures.RemoveAt(i);
                        card.effects.x.RemoveAt(i);
                        card.effects.y.RemoveAt(i);
                        card.effects.numberOfTargets.RemoveAt(i);
                        card.effects.cards.RemoveAt(i);
                        card.effects.extraConditions.RemoveAt(i);
                    }
                }
                break;
            case false:
                break;
        }
        GUILayout.Space(20);
        GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Is a token:", myStyle);
            card.token = EditorGUILayout.Toggle(card.token);
        GUILayout.EndHorizontal();
        GUILayout.Space(20);
        GUILayout.BeginHorizontal();
            if (!updateCard)
            {
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
                    card.ClearInfo();
                    firstTimeId = true;
                    ClearInfo();
                    updateCard = true;
                    foreach (Cards cards in ReloadList(card))
                    {
                        if (cards.id == loadId)
                        {
                            cardName = cards.name;
                            card.scriptableName = cards.scriptableName;
                            card.CardName = cards.cardName;
                            card.Attack = cards.attack;
                            card.Life = cards.life;
                            card.ManaCost = cards.manaCost;
                            card.HealthCost = cards.healthCost;
                            card.Sprite = cards.sprite;
                            card.clearEffexts = false;
                            card.hasEffect = cards.hasEffect;
                            if (cards.hasEffect)
                                card.hasEffectInt = 0;
                            else card.hasEffectInt = 1;
                            card.EffectDescription = cards.effectDesc;
                            card.effects = cards.effects;
                            card.spell = cards.spell;
                            if (cards.spell)
                                card.spellInt = 1;
                            else card.spellInt = 0;
                            if (AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets(cards.name)[0]).Contains("/Tokens"))
                                card.token = true;
                            else card.token = false;
                            card.id = loadId;
                            showLastUpdate = true;
                        }
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
        indexExtraCondition = new List<int>();
        hasExtraConditions = 1;
        extraConditions = new ExtraConditions();
        indexTempEffect = 0;
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
