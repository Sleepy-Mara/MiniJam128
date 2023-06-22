using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : Health
{
    public int manaLimit;
    public int maxMana;
    public int currentMana;
    [SerializeField] private ManaFiller manaFiller;
    public Animator notEnoughManaWindow;
    [SerializeField] private float shakeDuration;
    [SerializeField] private float shakeMagnitude;


    public bool EnoughMana(int cost)
    {
        if (currentMana < cost)
        {
            notEnoughManaWindow.SetTrigger("Activate");
        }
        return currentMana >= cost;
    }
    public void SpendMana(int cost)
    {
        currentMana -= cost;
        RefreshMana();
        //sonido / animacion
    }
    public bool EnoughHealth(int cost)
    {
        return currentHealth > cost;
    }
    public void SpendHealth(int cost)
    {
        currentHealth -= cost;
        RefreshHealth();
        //algun sonido animacion especial
    }
    public void RestoreMana()
    {
        if (maxMana != manaLimit)
            maxMana++;
        currentMana = maxMana;
        RefreshMana();
        //sonido / animacion
    }
    public void RestartPlayer()
    {
        currentMana = 0;
        maxMana = 0;
        currentHealth = maxHealth;
        RefreshHealth();
        RefreshMana();
    }
    public override void Defeat()
    {
        //lo que sea que pase cuando perdes
        Debug.Log("Te derrotaron wey");
        FindObjectOfType<NextCombat>().Defeat();
    }
    private void RefreshMana()
    {
        manaFiller.RefreshManas(currentMana, manaLimit);
    }
    public override void ReceiveDamage(int damage)
    {
        //StartCoroutine(Shake());
        base.ReceiveDamage(damage);
    }
    IEnumerator Shake()
    {
        Camera cam = FindObjectOfType<Camera>();
        Vector3 originalPos = cam.transform.localPosition;
        float elapsed = 0.0f;
        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, +1f) * shakeMagnitude;
            float y = Random.Range(-1f, +1f) * shakeMagnitude;
            cam.transform.localPosition = new Vector3(x, y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        cam.transform.localPosition = originalPos;
    }
}
