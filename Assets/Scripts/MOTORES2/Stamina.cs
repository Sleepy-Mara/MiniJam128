using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stamina : MonoBehaviour
{
    private int currentStamina;
    public int CurrentStamina
    {
        get { return currentStamina; }
        set
        {
            int posOrNeg = 1;
            if (value < 0 )
                posOrNeg = -1;
            for(int i = 0; i < value * posOrNeg; i++)
            {
                currentStamina += i * posOrNeg;
                if (currentStamina >= maxStamina || currentStamina <= 0)
                    continue;
                textStamina.text = currentStamina.ToString();
            }
            if (currentStamina > maxStamina)
                currentStamina = maxStamina;
            if (currentStamina < 0)
                currentStamina = 0;
            SaveData saveData = json.SaveData;
            saveData.currentStamina = currentStamina;
            json.SaveData = saveData;
        }
    }
    [SerializeField] private int maxStamina;
    [SerializeField] private float timeToRecharge;
    [SerializeField] private TextMeshProUGUI textStamina;
    private float currentTimeToRecharge;
    private SaveWithJson json;
    private void Awake()
    {
        json = FindObjectOfType<SaveWithJson>();
        currentStamina = json.SaveData.currentStamina;
        textStamina.text = currentStamina.ToString();
    }
    void Update()
    {
        if (currentStamina >= maxStamina)
            return;
        if (currentTimeToRecharge >= timeToRecharge)
        {
            currentTimeToRecharge = 0;
            CurrentStamina = 1;
            return;
        }
        currentTimeToRecharge += Time.deltaTime;
    }
}
