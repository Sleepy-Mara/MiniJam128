using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    private int currentStamina;
    public int CurrentStamina
    {
        get { return currentStamina; }
        set
        {
            int posOrNeg = 1;
            int fillAmount = 100;
            if (value < 0 )
            {
                posOrNeg = -1;
                fillAmount = 0;
            }
            for(int i = 0; i < value * posOrNeg; i++)
            {
                currentStamina += i * posOrNeg;
                if (currentStamina >= stamina.Count || currentStamina <= 0)
                    continue;
                stamina[currentStamina].fillAmount = fillAmount;
            }
            if (currentStamina > stamina.Count)
                currentStamina = stamina.Count;
            if (currentStamina < 0)
                currentStamina = 0;
        }
    }
    [SerializeField] private List<Image> stamina;
    [SerializeField] private float timeToRecharge;
    private float currentTimeToRecharge;
    void Update()
    {
        if (currentStamina >= stamina.Count)
            return;
        if (currentTimeToRecharge >= timeToRecharge)
        {
            currentTimeToRecharge = 0;
            CurrentStamina = 1;
            return;
        }
        currentTimeToRecharge += Time.deltaTime;
        stamina[currentStamina].fillAmount = currentTimeToRecharge;
    }
}
