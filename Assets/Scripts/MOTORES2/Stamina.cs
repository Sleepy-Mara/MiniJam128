using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Stamina : MonoBehaviour
{
    [SerializeField] int maxStamina;
    [SerializeField] float timeToRecharge;
    int currentStamina;
    [SerializeField] List<TextMeshProUGUI> staminaText;
    [SerializeField] List<TextMeshProUGUI> timerText;
    bool recharging;
    DateTime nextStaminaTime;
    DateTime lastStaminaTime;
    [SerializeField] string notifTitle = "Full Stamina";
    [SerializeField] string notifText = "Tenes la stamina al borde de colapzar, volve al juego";
    int id;
    TimeSpan timer;
    private SaveWithJson json;
    private void Awake()
    {
        json = FindObjectOfType<SaveWithJson>();
    }

    void Start()
    {
        Load();
        StartCoroutine(RechargeStamina());
        if (currentStamina < maxStamina)
        {
            timer = nextStaminaTime - DateTime.Now;
            //id = NotificationManager.Instance.DisplayNotification(notifTitle, notifText,
            //    AddDuration(DateTime.Now, ((maxStamina - (currentStamina) + 1) * timeToRecharge) + 1 + (float)timer.TotalMinutes));
        }
    }
    public bool HasEnoughStamina(int stamina) => currentStamina - stamina >= 0;
    IEnumerator RechargeStamina()
    {
        UpdateTimer();
        recharging = true;
        while (currentStamina < maxStamina)
        {
            DateTime currentTime = DateTime.Now;
            DateTime nextTime = nextStaminaTime;
            bool staminaAdd = false;
            while (currentTime > nextTime)
            {
                if (currentStamina >= maxStamina) 
                    break;
                ChargeStamina(1);
                staminaAdd = true;
                UpdateStamina();
                DateTime timeToAdd = nextTime;
                if (lastStaminaTime > nextTime)
                    timeToAdd = lastStaminaTime;
                nextTime = AddDuration(timeToAdd, timeToRecharge);

            }
            if (staminaAdd)
            {
                nextStaminaTime = nextTime;
                lastStaminaTime = DateTime.Now;
            }
            UpdateTimer();
            UpdateStamina();
            Save();
            yield return new WaitForEndOfFrame();
        }
        //NotificationManager.Instance.CancelNotification(id);
        recharging = false;
    }
    public void ChargeStamina(int stamina)
    {
        currentStamina += stamina;
        UpdateTimer();
        UpdateStamina();
    }
    DateTime AddDuration(DateTime date, float durationMinutes)
    {
        return date.AddMinutes(durationMinutes);
    }
    public bool UseStamina(int staminaToUse)
    {
        if (currentStamina - staminaToUse >= 0)
        {
            currentStamina -= staminaToUse;
            UpdateStamina();
            //NotificationManager.Instance.CancelNotification(id);
            //id = NotificationManager.Instance.DisplayNotification(notifTitle, notifText,
                //AddDuration(DateTime.Now, ((maxStamina - (currentStamina) + 1) * timeToRecharge) + 1 + (float)timer.TotalMinutes));
            if (!recharging)
            {
                nextStaminaTime = AddDuration(DateTime.Now, timeToRecharge);
                StartCoroutine(RechargeStamina());
            }
            return true;
        }
        else
            return false;
    }
    void UpdateTimer()
    {
        if (currentStamina >= maxStamina)
        {
            foreach (TextMeshProUGUI timerText in timerText)
                timerText.text = "00:00";
            return;
        }
        timer = nextStaminaTime - DateTime.Now;
        foreach (TextMeshProUGUI timerText in timerText)
            timerText.text = timer.Minutes.ToString("00") + ":" + timer.Seconds.ToString("00");
    }
    void UpdateStamina()
    {
        foreach (TextMeshProUGUI staminaText in staminaText)
            staminaText.text = currentStamina.ToString() + " / " + maxStamina.ToString();
    }
    void Save()
    {
        SaveData saveData = json.SaveData;
        saveData.currentStamina = currentStamina;
        saveData.nextStaminaTime = nextStaminaTime.ToString();
        saveData.lastStaminaTime = lastStaminaTime.ToString();
        json.SaveData = saveData;
    }
    void Load()
    {
        currentStamina = json.SaveData.currentStamina;
        nextStaminaTime = StringToDateTime(json.SaveData.nextStaminaTime);
        lastStaminaTime = StringToDateTime(json.SaveData.lastStaminaTime);
    }
    DateTime StringToDateTime(string date)
    {
        if (string.IsNullOrEmpty(date))
            return DateTime.Now;
        else
            return DateTime.Parse(date);
    }
    private void OnApplicationQuit()
    {
        Save();
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause) Save();
    }
}
