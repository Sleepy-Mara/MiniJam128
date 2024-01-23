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
    public string effect;

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
        RefreshHealth();
    }
    public void RestoreHealth(int heal)
    {
        currentHealth = Mathf.Clamp(currentHealth + heal, 0, maxHealth);
        RefreshHealth();
        //audio / algo
    }
    virtual public void ReceiveDamage(int damage)
    {
        //sonido / animacion
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
}
