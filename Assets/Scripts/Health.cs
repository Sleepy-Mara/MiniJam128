using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    [SerializeField] private Image healthFill;
    [SerializeField] private TextMeshProUGUI healthText;
    [HideInInspector] public int currentMana;
    [HideInInspector] public List<PlayersTempEffect> tempEffect = new List<PlayersTempEffect>();
    public string effect;

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
        RefreshHealth();
    }
    public void RestoreHealth(int heal, bool startTurn, bool endTurn)
    {
        if (startTurn)
            tempEffect.Add(new PlayersTempEffect { startTurn = true, endTurn = false, heal = heal });
        if (endTurn)
            tempEffect.Add(new PlayersTempEffect { startTurn = false, endTurn = true, heal = heal });
        currentHealth = Mathf.Clamp(currentHealth + heal, 0, maxHealth);
        RefreshHealth();
        //audio / algo
    }
    virtual public void ReceiveDamage(int damage, bool startTurn, bool endTurn)
    {
        //sonido / animacion
        if (startTurn)
            tempEffect.Add(new PlayersTempEffect { startTurn = true, endTurn = false, damage = damage });
        if (endTurn)
            tempEffect.Add(new PlayersTempEffect { startTurn = false, endTurn = true, damage = damage });
        FindObjectOfType<CameraManager>().Shake();
        if ((currentHealth - damage) <= 0)
        {
            currentHealth = 0;
            RefreshHealth();
            Defeat();
        }
        else
            currentHealth -= damage;
        RefreshHealth();
    }
    protected void RefreshHealth()
    {
        healthFill.fillAmount = (float)currentHealth / (float)maxHealth;
        healthText.text = currentHealth.ToString();
    }
    public virtual void Defeat()
    {

    }
    public void ManaEffect(int mana, bool startTurn, bool endTurn)
    {
        if (startTurn)
            tempEffect.Add(new PlayersTempEffect { startTurn = true, endTurn = false, mana = mana });
        if (endTurn)
            tempEffect.Add(new PlayersTempEffect { startTurn = false, endTurn = true, mana = mana });
        currentMana += mana;
        if (currentMana < 0)
            currentMana = 0;
    }
    public void StartTurn()
    {
        foreach (PlayersTempEffect effect in tempEffect)
        {
            if (effect.endTurn || !effect.startTurn)
                continue;
            if (effect.mana > 0)
                currentMana -= effect.mana;
            if (effect.heal > 0)
                ReceiveDamage(effect.heal, false, false);
            if (effect.damage > 0)
                RestoreHealth(effect.damage, false, false);
            effect.startTurn = false;
        }
    }
    public void EndTurn()
    {
        foreach (PlayersTempEffect effect in tempEffect)
        {
            if (effect.startTurn || !effect.endTurn)
                continue;
            if (effect.mana != 0)
                ManaEffect(-effect.mana, false, false);
            if (effect.heal != 0)
                ReceiveDamage(effect.heal, false, false);
            if (effect.damage != 0)
                RestoreHealth(effect.damage, false, false);
            effect.endTurn = false;
        }
    }
}
[System.Serializable]
public class PlayersTempEffect
{
    public bool startTurn;
    public bool endTurn;
    public int mana;
    public int heal;
    public int damage;
}
