using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public int actualHealth;
    [SerializeField] private Image healthFill;

    protected virtual void Awake()
    {
        actualHealth = maxHealth;
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
        if ((actualHealth - damage) <= 0)
        {
            actualHealth = 0;
            RefreshHealth();
            Defeat();
        }
        else
            actualHealth -= damage;
        RefreshHealth();
    }
    protected void RefreshHealth()
    {
        healthFill.fillAmount = (float)actualHealth / (float)maxHealth;
    }
    public virtual void Defeat()
    {

    }
}
