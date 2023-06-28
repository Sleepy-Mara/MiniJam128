using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] string gameId = "5303268";
    [SerializeField] string adId = "Rewarded_Android";
    [SerializeField] GameObject errorMessage;
    [SerializeField] GameObject skippedMessage;
    private GameObject adsButton;

    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId);
    }
    public void ShowAd(GameObject button)
    {
        if (!Advertisement.IsReady()) return;

        Advertisement.Show(adId);
        adsButton = button;
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
                adsButton.SetActive(false);
                FindObjectOfType<CurrencyManager>().Currency = FindObjectOfType<NextCombat>().EnemyReward;
            }
            else if (showResult == ShowResult.Skipped)
            {
                skippedMessage.SetActive(true);
            }
            else
            {
                errorMessage.SetActive(true);
            }
        }
    }
}
