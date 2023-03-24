using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField]
    private List<string> conditions = new List<string>()
    {
        "At the start of the turn",
        "When it's played",
        "When it attacks",
        "When it gets damaged",
        "When it's defeated",
        "At the end of the turn",
        "until your next turn",
        "until the end of the turn"
    };
    [SerializeField]
    private List<string> effects = new List<string>()
    {
        "draw",
        "deal",
        "heal",
        "add",
        "give",
        "immune",
        "summon"
    };
    [SerializeField]
    private List<string> targets = new List<string>()
    {
        "enemy creature",
        "ally creature",
        "random enemy",
        "random ally",
        "random enemy creature",
        "random ally creature",
        "random enemy creatures",
        "random ally creatures",
        "all enemy creatures",
        "all ally creatures",
        "itself",
        "hand",
        "deck",
        "life deck"
    };
    [SerializeField]
    private List<Cards> cards;
    [SerializeField]
    private Card newCard;
    private Table _table;
    private Draw _draw;
    public bool checking;

    private void Awake()
    {
        _table = FindObjectOfType<Table>();
        _draw = FindObjectOfType<Draw>();
    }
    public void CheckConditionStartOfTurn(Card card)
    {
        if (card.card.effectDesc.Contains(". "))
        {
            List<string> effectDescriptions = new List<string>();
            int lastCharacter = 0;
            for (int i = 0; i < card.card.effectDesc.Length; i++)
            {
                if (card.card.effectDesc[lastCharacter].ToString() + card.card.effectDesc[i].ToString() == ". ")
                {
                    for (int j = 0; j < i; j++)
                    {
                        string newEffect = card.card.effectDesc[j].ToString();
                        effectDescriptions.Add(newEffect);
                    }
                    for (int k = i; k < card.card.effectDesc.Length; k++)
                    {
                        string newEffect = card.card.effectDesc[k].ToString();
                        effectDescriptions.Add(newEffect);
                    }
                }
                lastCharacter = card.card.effectDesc[i];
            }
            foreach (string effectDescription in effectDescriptions)
                if (effectDescription.Contains(conditions[0]))
                    CheckEffect(card);
        }
        else if (card.card.effectDesc.Contains(conditions[0]))
            CheckEffect(card);
    }
    public void CheckConditionIsPlayed(Card card)
    {
        if (card.card.effectDesc.Contains(". "))
        {
            List<string> effectDescriptions = new List<string>();
            int lastCharacter = 0;
            for (int i = 0; i < card.card.effectDesc.Length; i++)
            {
                if (card.card.effectDesc[lastCharacter].ToString() + card.card.effectDesc[i].ToString() == ". ")
                {
                    for (int j = 0; j < i; j++)
                    {
                        string newEffect = card.card.effectDesc[j].ToString();
                        effectDescriptions.Add(newEffect);
                    }
                    for (int k = i; k < card.card.effectDesc.Length; k++)
                    {
                        string newEffect = card.card.effectDesc[k].ToString();
                        effectDescriptions.Add(newEffect);
                    }
                }
                lastCharacter = card.card.effectDesc[i];
            }
            foreach (string effectDescription in effectDescriptions)
                if (effectDescription.Contains(conditions[1]))
                    CheckEffect(card);
        }
        else if (card.card.effectDesc.Contains(conditions[1]))
            CheckEffect(card);
    }
    public void CheckConditionAttack(Card card)
    {
        if (card.card.effectDesc.Contains(". "))
        {
            List<string> effectDescriptions = new List<string>();
            int lastCharacter = 0;
            for (int i = 0; i < card.card.effectDesc.Length; i++)
            {
                if (card.card.effectDesc[lastCharacter].ToString() + card.card.effectDesc[i].ToString() == ". ")
                {
                    for (int j = 0; j < i; j++)
                    {
                        string newEffect = card.card.effectDesc[j].ToString();
                        effectDescriptions.Add(newEffect);
                    }
                    for (int k = i; k < card.card.effectDesc.Length; k++)
                    {
                        string newEffect = card.card.effectDesc[k].ToString();
                        effectDescriptions.Add(newEffect);
                    }
                }
                lastCharacter = card.card.effectDesc[i];
            }
            foreach (string effectDescription in effectDescriptions)
                if (effectDescription.Contains(conditions[2]))
                    CheckEffect(card);
        }
        else if (card.card.effectDesc.Contains(conditions[2]))
            CheckEffect(card);
    }
    public void CheckConditionGetDamaged(Card card)
    {
        if (card.card.effectDesc.Contains(". "))
        {
            List<string> effectDescriptions = new List<string>();
            int lastCharacter = 0;
            for (int i = 0; i < card.card.effectDesc.Length; i++)
            {
                if (card.card.effectDesc[lastCharacter].ToString() + card.card.effectDesc[i].ToString() == ". ")
                {
                    for (int j = 0; j < i; j++)
                    {
                        string newEffect = card.card.effectDesc[j].ToString();
                        effectDescriptions.Add(newEffect);
                    }
                    for (int k = i; k < card.card.effectDesc.Length; k++)
                    {
                        string newEffect = card.card.effectDesc[k].ToString();
                        effectDescriptions.Add(newEffect);
                    }
                }
                lastCharacter = card.card.effectDesc[i];
            }
            foreach (string effectDescription in effectDescriptions)
                if (effectDescription.Contains(conditions[3]))
                    CheckEffect(card);
        }
        else if (card.card.effectDesc.Contains(conditions[3]))
            CheckEffect(card);
    }
    public void CheckConditionDefeated(Card card)
    {
        if (card.card.effectDesc.Contains(". "))
        {
            List<string> effectDescriptions = new List<string>();
            int lastCharacter = 0;
            for (int i = 0; i < card.card.effectDesc.Length; i++)
            {
                if (card.card.effectDesc[lastCharacter].ToString() + card.card.effectDesc[i].ToString() == ". ")
                {
                    for (int j = 0; j < i; j++)
                    {
                        string newEffect = card.card.effectDesc[j].ToString();
                        effectDescriptions.Add(newEffect);
                    }
                    for (int k = i; k < card.card.effectDesc.Length; k++)
                    {
                        string newEffect = card.card.effectDesc[k].ToString();
                        effectDescriptions.Add(newEffect);
                    }
                }
                lastCharacter = card.card.effectDesc[i];
            }
            foreach (string effectDescription in effectDescriptions)
                if (effectDescription.Contains(conditions[4]))
                    CheckEffect(card);
        }
        else if (card.card.effectDesc.Contains(conditions[4]))
        {
            CheckEffect(card);
            Debug.Log("DefeatedCheckTrue");
        }
        else checking = false;
    }
    public void CheckConditionEndOfTurn(Card card)
    {
        if (card.card.effectDesc.Contains(". "))
        {
            List<string> effectDescriptions = new List<string>();
            int lastCharacter = 0;
            for (int i = 0; i < card.card.effectDesc.Length; i++)
            {
                if (card.card.effectDesc[lastCharacter].ToString() + card.card.effectDesc[i].ToString() == ". ")
                {
                    for (int j = 0; j < i; j++)
                    {
                        string newEffect = card.card.effectDesc[j].ToString();
                        effectDescriptions.Add(newEffect);
                    }
                    for (int k = i; k < card.card.effectDesc.Length; k++)
                    {
                        string newEffect = card.card.effectDesc[k].ToString();
                        effectDescriptions.Add(newEffect);
                    }
                }
                lastCharacter = card.card.effectDesc[i];
            }
            foreach (string effectDescription in effectDescriptions)
                if (effectDescription.Contains(conditions[5]))
                    CheckEffect(card);
        }
        else if (card.card.effectDesc.Contains(conditions[5]))
        {
            CheckEffect(card);
            Debug.Log("EndTurnCheckTrue");
        }
    }
    public void CheckConditionUntilYourNextTurn(Card card)
    {
        if (card.card.effectDesc.Contains(". "))
        {
            List<string> effectDescriptions = new List<string>();
            int lastCharacter = 0;
            for (int i = 0; i < card.card.effectDesc.Length; i++)
            {
                if (card.card.effectDesc[lastCharacter].ToString() + card.card.effectDesc[i].ToString() == ". ")
                {
                    for (int j = 0; j < i; j++)
                    {
                        string newEffect = card.card.effectDesc[j].ToString();
                        effectDescriptions.Add(newEffect);
                    }
                    for (int k = i; k < card.card.effectDesc.Length; k++)
                    {
                        string newEffect = card.card.effectDesc[k].ToString();
                        effectDescriptions.Add(newEffect);
                    }
                }
                lastCharacter = card.card.effectDesc[i];
            }
            foreach (string effectDescription in effectDescriptions)
                if (effectDescription.Contains(conditions[6]))
                    CheckEffect(card);
        }
        else if (card.card.effectDesc.Contains(conditions[6]))
            CheckEffect(card);
    }
    public void CheckConditionUntilTheEndOfTheTurn(Card card)
    {
        if (card.card.effectDesc.Contains(". "))
        {
            List<string> effectDescriptions = new List<string>();
            int lastCharacter = 0;
            for (int i = 0; i < card.card.effectDesc.Length; i++)
            {
                if (card.card.effectDesc[lastCharacter].ToString() + card.card.effectDesc[i].ToString() == ". ")
                {
                    for (int j = 0; j < i; j++)
                    {
                        string newEffect = card.card.effectDesc[j].ToString();
                        effectDescriptions.Add(newEffect);
                    }
                    for (int k = i; k < card.card.effectDesc.Length; k++)
                    {
                        string newEffect = card.card.effectDesc[k].ToString();
                        effectDescriptions.Add(newEffect);
                    }
                }
                lastCharacter = card.card.effectDesc[i];
            }
            foreach (string effectDescription in effectDescriptions)
                if (effectDescription.Contains(conditions[7]))
                    CheckEffect(card);
        }
        else if (card.card.effectDesc.Contains(conditions[7]))
            CheckEffect(card);
    }
    private void CheckEffect(Card card)
    {
        if (card.card.effectDesc.Contains("and"))
        {
            List<string> effectDescriptions = new List<string>();
            int lastFirstCharacter = 0;
            int lastSecondCharacter = 0;
            for (int i = 0; i < card.card.effectDesc.Length; i++)
            {
                if (card.card.effectDesc[lastSecondCharacter].ToString() + card.card.effectDesc[lastFirstCharacter].ToString() 
                    + card.card.effectDesc[i].ToString() == "and")
                {
                    string newEffect = "";
                    for (int j = 0; j < i; j++)
                    {
                        newEffect = newEffect + card.card.effectDesc[j].ToString();
                    }
                    effectDescriptions.Add(newEffect);
                    newEffect = "";
                    for (int k = i; k < card.card.effectDesc.Length; k++)
                    {
                        newEffect = newEffect + card.card.effectDesc[k].ToString();
                    }
                    effectDescriptions.Add(newEffect);
                }
                lastSecondCharacter = lastFirstCharacter;
                lastFirstCharacter = i;
            }
            foreach (string effectDescription in effectDescriptions)
            {
                Debug.LogError(effectDescription);
                foreach (string effect in effects)
                    if (effectDescription.Contains(effect))
                    {
                        if (effect == effects[0])
                            DrawEffect(card);
                        if (effect == effects[1])
                            DealDamageEffect(card);
                        if (effect == effects[2])
                            HealEffect(card);
                        if (effect == effects[3])
                            AddEffect(card);
                        if (effect == effects[4])
                            GiveEffect(card);
                        if (effect == effects[5])
                            ImmuneEffect(card);
                        if (effect == effects[6])
                            SumonEffect(card);
                        continue;
                    }
            }
                
        }
        else
            foreach (string effect in effects)
                if (card.card.effectDesc.Contains(effect))
                {
                    if (effect == effects[0])
                        DrawEffect(card);
                    if (effect == effects[1])
                        DealDamageEffect(card);
                    if (effect == effects[2])
                        HealEffect(card);
                    if (effect == effects[3])
                        AddEffect(card);
                    if (effect == effects[4])
                        GiveEffect(card);
                    if (effect == effects[5])
                        ImmuneEffect(card);
                    if (effect == effects[6])
                        SumonEffect(card);
                    return;
                }
        checking = false;
    }
    private void DrawEffect(Card card)
    {
        Debug.Log("Im going to draw maybe");
        #region Draw
        foreach (string target in targets)
            if (card.card.effectDesc.Contains(target))
                for (int i = 0; i < 50; i++)
                    if (card.card.effectDesc.Contains(" " + i.ToString() + " "))
                        for (int j = 0; j < i; j++)
                        {
                            if (target == targets[12])
                                _draw.DrawACard();
                            else if (target == targets[12])
                                Debug.Log("Draw from life deck");
                        }
        checking = false;
        #endregion
    }
    private void DealDamageEffect(Card card)
    {
        Debug.Log("Im going to do damage maybe");
        #region DealDamage
        foreach (string target in targets)
            if (card.card.effectDesc.Contains(target))
            {
                Debug.Log("Im going to do damage");
                for (int i = 0; i < 50; i++)
                    if (card.card.effectDesc.Contains(i.ToString() + " damage"))
                    {
                        if (target == targets[0])
                        {
                            Debug.Log("Elige Enemigo");
                        }
                        else if (target == targets[1])
                        {
                            Debug.Log("Elige Aliado");
                        }
                        else if (target == targets[2])
                        {
                            var enemysAlive = new List<MapPosition>();
                            foreach (MapPosition enemy in _table.enemyFront)
                                if (enemy.card != null)
                                    if (enemy.card.ActualLife > 0)
                                        enemysAlive.Add(enemy);
                            var selected = Random.Range(0, enemysAlive.Count + 1);
                            if (selected == _table.enemyFront.Length)
                                FindObjectOfType<Enemy>().ReceiveDamage(i);
                            else if (_table.enemyFront[selected].card != null)
                                _table.enemyFront[selected].card.ReceiveDamagePublic(i, null);
                        }
                        else if (target == targets[3])
                        {
                            var enemysAlive = new List<MapPosition>();
                            foreach (MapPosition enemy in _table.playerPositions)
                                if (enemy.card != null)
                                    if (enemy.card.ActualLife > 0)
                                        enemysAlive.Add(enemy);
                            var selected = Random.Range(0, enemysAlive.Count + 1);
                            if (selected == _table.playerPositions.Length)
                                FindObjectOfType<Player>().ReceiveDamage(i);
                            else if (_table.playerPositions[selected].card != null)
                                _table.playerPositions[selected].card.ReceiveDamagePublic(i, null);
                        }
                        else if (target == targets[4])
                        {
                            var enemysAlive = new List<MapPosition>();
                            foreach (MapPosition enemy in _table.enemyFront)
                                if (enemy.card != null)
                                    if (enemy.card.ActualLife > 0)
                                        enemysAlive.Add(enemy);
                            var selected = Random.Range(0, enemysAlive.Count);
                            if (_table.enemyFront[selected].card != null)
                                _table.enemyFront[selected].card.ReceiveDamagePublic(i, null);
                        }
                        else if (target == targets[5])
                        {
                            var enemysAlive = new List<MapPosition>();
                            foreach (MapPosition enemy in _table.playerPositions)
                                if (enemy.card != null)
                                    if (enemy.card.ActualLife > 0)
                                        enemysAlive.Add(enemy);
                            var selected = Random.Range(0, enemysAlive.Count);
                            if (_table.playerPositions[selected].card != null)
                                _table.playerPositions[selected].card.ReceiveDamagePublic(i, null);
                        }
                        else if (target == targets[6])
                        {
                            for (int j = 0; j < 3; j++)
                                if (card.card.effectDesc.Contains(j.ToString()))
                                    for (int k = 0; k < j; k++)
                                    {
                                        var enemysAlive = new List<MapPosition>();
                                        foreach (MapPosition enemy in _table.enemyFront)
                                            if (enemy.card != null)
                                                if (enemy.card.ActualLife > 0)
                                                    enemysAlive.Add(enemy);
                                        var selected = Random.Range(0, enemysAlive.Count);
                                        if (_table.enemyFront[selected].card != null)
                                            _table.enemyFront[selected].card.ReceiveDamagePublic(i, null);
                                    }
                        }
                        else if (target == targets[7])
                        {
                            for (int j = 0; j < 3; j++)
                                if (card.card.effectDesc.Contains(j.ToString()))
                                    for (int k = 0; k < j; k++)
                                    {
                                        var enemysAlive = new List<MapPosition>();
                                        foreach (MapPosition enemy in _table.playerPositions)
                                            if (enemy.card != null)
                                                if (enemy.card.ActualLife > 0)
                                                    enemysAlive.Add(enemy);
                                        var selected = Random.Range(0, enemysAlive.Count);
                                        if (_table.playerPositions[selected].card != null)
                                            _table.playerPositions[selected].card.ReceiveDamagePublic(i, null);
                                    }
                        }
                        else if (target == targets[8])
                        {
                            foreach (MapPosition selected in _table.enemyFront)
                                if (selected.card)
                                    selected.card.ReceiveDamagePublic(i, null);
                        }
                        else if (target == targets[9])
                        {
                            foreach (MapPosition selected in _table.playerPositions)
                                if (selected.card != null)
                                    selected.card.ReceiveDamagePublic(i, null);
                        }
                        else if (target == targets[10])
                            card.ReceiveDamagePublic(i, null);
                    }
            }
        Debug.Log("I did damage");
        checking = false;
        #endregion
    }
    private void HealEffect(Card card)
    {
        Debug.Log("Im going to heal maybe");
        #region Heal
        foreach (string target in targets)
            if (card.card.effectDesc.Contains(target))
                for (int i = 0; i < 50; i++)
                    if (card.card.effectDesc.Contains("heal " + i.ToString()))
                    {
                        Debug.Log("Im going to heal");
                        if (target == targets[0])
                        {
                            Debug.Log("Elige Enemigo");
                        }
                        else if (target == targets[1])
                        {
                            Debug.Log("Elige Aliado");
                        }
                        else if (target == targets[2])
                        {
                            var allysToHeal = new List<MapPosition>();
                            foreach (MapPosition ally in _table.enemyFront)
                                if (ally.card != null)
                                    if (ally.card.ActualLife > 0)
                                        allysToHeal.Add(ally);
                            var selected = Random.Range(0, allysToHeal.Count + 1);
                            if (selected == _table.enemyFront.Length)
                                FindObjectOfType<Enemy>().RestoreHealth(i);
                            else if (_table.enemyFront[selected].card != null)
                                _table.enemyFront[selected].card.Heal(i);
                        }
                        else if (target == targets[3])
                        {
                            var allysToHeal = new List<MapPosition>();
                            foreach (MapPosition ally in _table.playerPositions)
                                if (ally.card != null)
                                    if (ally.card.ActualLife > 0)
                                        allysToHeal.Add(ally);
                            var selected = Random.Range(0, allysToHeal.Count + 1);
                            if (selected == _table.playerPositions.Length)
                                FindObjectOfType<Player>().RestoreHealth(i);
                            else if (_table.playerPositions[selected].card != null)
                                _table.playerPositions[selected].card.Heal(i);
                        }
                        else if (target == targets[4])
                        {
                            var allysToHeal = new List<MapPosition>();
                            foreach (MapPosition ally in _table.enemyFront)
                                if (ally.card != null)
                                    if (ally.card.ActualLife > 0)
                                        allysToHeal.Add(ally);
                            var selected = Random.Range(0, allysToHeal.Count);
                            Debug.Log("Im going to heal you " + allysToHeal[selected].card.card.name);
                            if (allysToHeal[selected].card != null)
                                allysToHeal[selected].card.Heal(i);
                        }
                        else if (target == targets[5])
                        {
                            var allysToHeal = new List<MapPosition>();
                            foreach (MapPosition ally in _table.playerPositions)
                                if (ally.card != null)
                                    if (ally.card.ActualLife > 0)
                                        allysToHeal.Add(ally);
                            var selected = Random.Range(0, allysToHeal.Count);
                            Debug.Log("Im going to heal you " + allysToHeal[selected].card.card.name);
                            if (allysToHeal[selected].card != null)
                                allysToHeal[selected].card.Heal(i);
                        }
                        else if (target == targets[6])
                        {
                            for (int j = 0; j < 3; j++)
                                if (card.card.effectDesc.Contains(j.ToString()))
                                    for (int k = 0; k < j; k++)
                                    {
                                        var allysToHeal = new List<MapPosition>();
                                        foreach (MapPosition ally in _table.enemyFront)
                                            if (ally.card != null)
                                                if (ally.card.ActualLife > 0)
                                                    allysToHeal.Add(ally);
                                        var selected = Random.Range(0, allysToHeal.Count);
                                        if (_table.enemyFront[selected].card != null)
                                            _table.enemyFront[selected].card.Heal(i);
                                    }
                        }
                        else if (target == targets[7])
                        {
                            for (int j = 0; j < 3; j++)
                                if (card.card.effectDesc.Contains(j.ToString()))
                                    for (int k = 0; k < j; k++)
                                    {
                                        var allysToHeal = new List<MapPosition>();
                                        foreach (MapPosition ally in _table.playerPositions)
                                            if (ally.card != null)
                                                if (ally.card.ActualLife > 0)
                                                    allysToHeal.Add(ally);
                                        var selected = Random.Range(0, allysToHeal.Count);
                                        if (_table.playerPositions[selected].card != null)
                                            _table.playerPositions[selected].card.Heal(i);
                                    }
                        }
                        else if (target == targets[8])
                        {
                            foreach (MapPosition selected in _table.enemyFront)
                                if (selected.card)
                                    selected.card.Heal(i);
                        }
                        else if (target == targets[9])
                        {
                            foreach (MapPosition selected in _table.playerPositions)
                                if (selected.card != null)
                                    selected.card.Heal(i);
                        }
                        else if (target == targets[10])
                            card.Heal(i);
                    }
        checking = false;
        #endregion
    }
    private void AddEffect(Card card)
    {
        Debug.Log("Im going to add a card maybe");
        #region Add
        bool added = false;
        if (card.card.effectDesc.Contains(targets[11]))
            foreach (Cards cards in cards)
                if (card.card.effectDesc.Contains(cards.name))
                {
                    for (int i = 0; i < 50; i++)
                        if (card.card.effectDesc.Contains(" " + i.ToString() + " "))
                        {
                            for (int j = 0; j < i; j++)
                            {
                                var addCard = Instantiate(newCard, _draw.transform);
                                addCard.card = cards;
                                addCard.GetComponent<Card>().SetData();
                                _draw.AddCardToHand(addCard);
                            }
                            added = true;
                        }
                    if (!added)
                    {
                        var addCard = Instantiate(newCard, _draw.transform);
                        addCard.card = cards;
                        addCard.GetComponent<Card>().SetData();
                        _draw.AddCardToHand(addCard);
                    }
                }
        if (card.card.effectDesc.Contains(targets[12]))
            foreach (Cards cards in cards)
                if (card.card.effectDesc.Contains(cards.name))
                {
                    for (int i = 0; i < 50; i++)
                        if (card.card.effectDesc.Contains(" " + i.ToString() + " "))
                        {
                            for (int j = 0; j < i; j++)
                                _draw.AddATempCard(cards);
                            added = true;
                        }
                    if (!added)
                        _draw.AddATempCard(cards);
                }
        if (card.card.effectDesc.Contains(targets[13]))
            foreach (Cards cards in cards)
                if (card.card.effectDesc.Contains(cards.name))
                {
                    for (int i = 0; i < 50; i++)
                        if (card.card.effectDesc.Contains(" " + i.ToString() + " "))
                        {
                            for (int j = 0; j < i; j++)
                                Debug.Log("Add to life deck");
                            added = true;
                        }
                    if (!added)
                        Debug.Log("Add to life deck");
                }
        checking = false;
        #endregion
    }
    private void GiveEffect(Card card)
    {
        Debug.Log("Im going to buff maybe");
        #region Give
        foreach (string target in targets)
            if (card.card.effectDesc.Contains(target))
                for (int i = 0; i < 50; i++)
                    for(int j = 0; j < 50; j++)
                    {
                        if (card.card.effectDesc.Contains("+" + i.ToString() + "/+" + j.ToString()))
                        {
                            i *= 1;
                            j *= 1;
                        }
                        else if (card.card.effectDesc.Contains("-" + i.ToString() + "/-" + j.ToString()))
                        {
                            i *= -1;
                            j *= -1;
                        }
                        else if (card.card.effectDesc.Contains("+" + i.ToString() + "/-" + j.ToString()))
                        {
                            i *= 1;
                            j *= -1;
                        }
                        else if (card.card.effectDesc.Contains("-" + i.ToString() + "/+" + j.ToString()))
                        {
                            i *= -1;
                            j *= 1;
                        }
                        else continue;
                        Debug.LogError("Im going to buff");
                        if (target == targets[0])
                        {
                            Debug.Log("Elige Enemigo");
                        }
                        else if (target == targets[1])
                        {
                            Debug.Log("Elige Aliado");
                        }
                        else if (target == targets[4])
                        {
                            var creatureToBuff = new List<MapPosition>();
                            foreach (MapPosition creature in _table.enemyFront)
                                if (creature.card != null)
                                    if (creature.card.ActualLife > 0)
                                        creatureToBuff.Add(creature);
                            var selected = Random.Range(0, creatureToBuff.Count);
                            if (creatureToBuff[selected].card != null)
                                creatureToBuff[selected].card.Buff(i, j);
                        }
                        else if (target == targets[5])
                        {
                            Debug.LogError("Im going to buff");
                            var creatureToBuff = new List<MapPosition>();
                            foreach (MapPosition creature in _table.playerPositions)
                                if (creature.card != null)
                                    if (creature.card.ActualLife > 0)
                                        creatureToBuff.Add(creature);
                            var selected = Random.Range(0, creatureToBuff.Count);
                            if (creatureToBuff[selected].card != null)
                                creatureToBuff[selected].card.Buff(i, j);
                        }
                        else if (target == targets[6])
                        {
                            for (int l = 0; l < 3; l++)
                                if (card.card.effectDesc.Contains(j.ToString()))
                                    for (int k = 0; k < l; k++)
                                    {
                                        var creatureToBuff = new List<MapPosition>();
                                        foreach (MapPosition creature in _table.enemyFront)
                                            if (creature.card != null)
                                                if (creature.card.ActualLife > 0)
                                                    creatureToBuff.Add(creature);
                                        var selected = Random.Range(0, creatureToBuff.Count);
                                        if (creatureToBuff[selected].card != null)
                                            creatureToBuff[selected].card.Buff(i, j);
                                    }
                        }
                        else if (target == targets[7])
                        {
                            for (int l = 0; l < 3; l++)
                                if (card.card.effectDesc.Contains(j.ToString()))
                                    for (int k = 0; k < l; k++)
                                    {
                                        var creatureToBuff = new List<MapPosition>();
                                        foreach (MapPosition creature in _table.playerPositions)
                                            if (creature.card != null)
                                                if (creature.card.ActualLife > 0)
                                                    creatureToBuff.Add(creature);
                                        var selected = Random.Range(0, creatureToBuff.Count);
                                        if (creatureToBuff[selected].card != null)
                                            creatureToBuff[selected].card.Buff(i, j);
                                    }
                        }
                        else if (target == targets[8])
                        {
                            foreach (MapPosition selected in _table.enemyFront)
                                if (selected.card != null)
                                    selected.card.Buff(i, j);
                        }
                        else if (target == targets[9])
                        {
                            foreach (MapPosition selected in _table.playerPositions)
                                if (selected.card != null)
                                    selected.card.Buff(i, j);
                        }
                        else if (target == targets[10])
                            card.Buff(i, j);
                    }
        checking = false;
        #endregion
    }
    private void ImmuneEffect(Card card)
    {
        Debug.Log("Im immune maybe");
        #region Inmune
        foreach (string target in targets)
            if (card.card.effectDesc.Contains(target))
            {
                if (target == targets[0])
                {
                    Debug.Log("Elige Enemigo");
                }
                else if (target == targets[1])
                {
                    Debug.Log("Elige Aliado");
                }
                else if (target == targets[4])
                {
                    var selected = Random.Range(0, _table.enemyFront.Length - 1);
                    if (_table.enemyFront[selected].card != null)
                        _table.enemyFront[selected].card.immune = true;
                }
                else if (target == targets[5])
                {
                    var selected = Random.Range(0, _table.playerPositions.Length - 1);
                    if (_table.playerPositions[selected].card != null)
                        _table.playerPositions[selected].card.immune = true;
                }
                else if (target == targets[6])
                {
                    for (int j = 0; j < 3; j++)
                        if (card.card.effectDesc.Contains(j.ToString()))
                            for (int k = 0; k < j; k++)
                            {
                                var selected = Random.Range(0, _table.enemyFront.Length - 1);
                                if (_table.enemyFront[selected].card != null)
                                    _table.enemyFront[selected].card.immune = true;
                            }
                }
                else if (target == targets[7])
                {
                    for (int j = 0; j < 3; j++)
                        if (card.card.effectDesc.Contains(j.ToString()))
                            for (int k = 0; k < j; k++)
                            {
                                var selected = Random.Range(0, _table.playerPositions.Length - 1);
                                if (_table.playerPositions[selected].card != null)
                                    _table.playerPositions[selected].card.immune = true;
                            }
                }
                else if (target == targets[8])
                {
                    foreach (MapPosition selected in _table.enemyFront)
                        if (selected.card != null)
                            selected.card.immune = true;
                }
                else if (target == targets[9])
                {
                    foreach (MapPosition selected in _table.playerPositions)
                        if (selected.card != null)
                            selected.card.immune = true;
                }
                else if (target == targets[10])
                    card.immune = true;
            }
        checking = false;
        #endregion
    }
    private void SumonEffect(Card card)
    {
        Debug.Log("Im going to sumon maybe");
        #region Sumon
        bool sumoned = false;
        Cards selectedCard = null;
        foreach (Cards cards in cards)
            if (card.card.effectDesc.Contains(cards.name))
                selectedCard = cards;
        if (selectedCard != null)
        {
            if (card.actualPosition.oponent.GetComponent<Enemy>())
            {
                for (int i = 0; i < _table.enemyFront.Length; i++)
                    if (card.card.effectDesc.Contains(i.ToString()))
                    {
                        for (int j = 0; j < i; j++)
                        {
                            var pos = Random.Range(0, _table.playerPositions.Length - 1);
                            GameObject sumonCard = Instantiate(newCard).gameObject;
                            sumonCard.GetComponent<Card>().card = selectedCard;
                            _table.SetCard(sumonCard, pos);
                            sumoned = true;
                        }
                    }
            }
            else if (card.actualPosition.oponent.GetComponent<Player>())
            {
                for (int i = 0; i < _table.enemyFront.Length; i++)
                    if (card.card.effectDesc.Contains(i.ToString()))
                    {
                        for (int j = 0; j < i; j++)
                        {
                            var pos = Random.Range(0, _table.enemyFront.Length - 1);
                            GameObject sumonCard = Instantiate(newCard).gameObject;
                            sumonCard.GetComponent<Card>().card = selectedCard;
                            _table.EnemySpawnCard(pos , sumonCard);
                            sumoned = true;
                        }
                    }
            }
        }
        checking = false;
        #endregion
    }
}
