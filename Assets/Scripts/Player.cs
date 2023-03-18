using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth;
    private int actualHealth;
    public int manaLimit;
    private int maxMana;
    private int actualMana;

    private void Awake()
    {
        actualHealth = maxHealth;
    }
    public void ReceiveDamage(int damage)
    {
        //sonido / animacion
        if((actualHealth - damage) <= 0)
        {
            actualHealth = 0;
            Defeat();
        } else
            actualHealth -= damage;
    }
    public bool EnoughMana(int cost)
    {
        return actualMana >= cost;
    }
    public void SpendMana(int cost)
    {
        actualMana -= cost;
        //sonido / animacion
    }
    public void RestoreMana()
    {
        if (maxMana != manaLimit)
            maxMana++;
        actualMana = maxMana;
        //sonido / animacion
    }
    public void Defeat()
    {
        //lo que sea que pase cuando perdes
        Debug.Log("Te derrotaron wey");
    }

}
