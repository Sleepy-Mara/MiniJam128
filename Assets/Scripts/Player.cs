using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int maxHealth;
    public int actualHealth;
    public int manaLimit;
    public int maxMana;
    public int actualMana;
    [SerializeField] private Image manaFill;
    [SerializeField] private Image healthFill;

    private void Awake()
    {
        actualHealth = maxHealth;
        RefreshMana();
        RefreshHealth();
    }
    public void RestoreHealth(int heal)
    {
        actualHealth = Mathf.Clamp(actualHealth + heal, 0, maxHealth);
        RefreshHealth();
        //audio / algo
    }
    public void ReceiveDamage(int damage)
    {
        //sonido / animacion
        if((actualHealth - damage) <= 0)
        {
            actualHealth = 0;
            RefreshHealth();
            Defeat();
        } else
            actualHealth -= damage;
        RefreshHealth();
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
    public void RestoreMana()
    {
        if (maxMana != manaLimit)
            maxMana++;
        actualMana = maxMana;
        RefreshMana();
        //sonido / animacion
    }
    public void Defeat()
    {
        //lo que sea que pase cuando perdes
        Debug.Log("Te derrotaron wey");
    }
    private void RefreshHealth()
    {
        healthFill.fillAmount = (float)actualHealth / (float)maxHealth;
    }
    private void RefreshMana()
    {
        manaFill.fillAmount = (float)actualMana / (float)manaLimit;
    }
}
