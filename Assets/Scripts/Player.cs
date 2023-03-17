using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth;
    private int actualHealth;
    public int maxMana;
    public int actualMana;

    private void Awake()
    {
        actualHealth = maxHealth;
    }
    public void ReceiveDamage(int damage)
    {
        if((actualHealth - damage) <= 0)
        {
            actualHealth = 0;
            Defeat();
        }
        actualHealth -= damage;
    }
    public void Defeat()
    {

    }
}
