using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] string gameId;
    [SerializeField] string adId;
    [SerializeField] GameObject errorMessage;
    [SerializeField] GameObject skippedMessage;
    private GameObject adsButton;
    private int currencyReward;
    private int staminaReward;

    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId);
    }
    public void ShowAdAfterWin(GameObject button)
    {
        if (!Advertisement.IsReady()) return;
        currencyReward = FindObjectOfType<NextCombat>().EnemyReward;
        Advertisement.Show(adId);
        adsButton = button;
    }
    public void ShowAdCurrency(int currency)
    {
        if (!Advertisement.IsReady()) return;
        currencyReward = currency;
        Advertisement.Show(adId);
    }
    public void ShowAdStamina(int stamina)
    {
        if (!Advertisement.IsReady()) return;
        staminaReward = stamina;
        Advertisement.Show(adId);
    }
    public void OnUnityAdsReady(string placementId)
    {
        if (placementId == adId) Debug.Log("Is ready!");
    }

    public void OnUnityAdsDidError(string message)
    {
    }

    public void OnUnityAdsDidStart(string placementId)
    {
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == adId)
        {
            if (showResult == ShowResult.Finished) 
            {
                if (adsButton != null)
                    adsButton.SetActive(false);
                FindObjectOfType<CurrencyManager>().Currency = currencyReward;
                FindObjectOfType<Stamina>()?.ChargeStamina(staminaReward);
                FindObjectOfType<RewardsScreen>()?.AdReward(FindObjectOfType<NextCombat>().EnemyReward);
            }
            else if (showResult == ShowResult.Skipped)
            {
                if (skippedMessage != null)
                    skippedMessage.SetActive(true);
            }
            else
            {
                if (errorMessage != null)
                    errorMessage.SetActive(true);
            }
            currencyReward = 0;
            staminaReward = 0;
            adsButton = null;
        }
    }
}
