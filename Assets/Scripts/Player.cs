using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Health
{
    public int manaLimit;
    public int maxMana;
    public int actualMana;
    [SerializeField] private Image manaFill;

    private new void Awake()
    {
        base.Awake();
        RefreshMana();
    }
    
    public bool EnoughMana(int cost)
    {
        return actualMana >= cost;
    }
    public void SpendMana(int cost)
    {
        actualMana -= cost;
        RefreshMana();
        //sonido / animacion
    }
    public bool EnoughHealth(int cost)
    {
        return actualHealth > cost;
    }
    public void SpendHealth(int cost)
    {
        actualHealth -= cost;
        RefreshHealth();
        //algun sonido animacion especial
    }
    public void RestoreMana()
    {
        if (maxMana != manaLimit)
            maxMana++;
        actualMana = maxMana;
        RefreshMana();
        //sonido / animacion
    }
    public void RestartPlayer()
    {
        actualMana = 0;
        maxMana = 0;
        actualHealth = maxHealth;
        RefreshHealth();
        RefreshMana();
    }
    public override void Defeat()
    {
        //lo que sea que pase cuando perdes
        Debug.Log("Te derrotaron wey");
        FindObjectOfType<DefeatMenu>().Defeat();
    }
    private void RefreshMana()
    {
        manaFill.fillAmount = (float)actualMana / (float)manaLimit;
    }
}
