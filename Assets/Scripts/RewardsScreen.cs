using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RewardsScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI rewardText;
    [SerializeField] TextMeshProUGUI rewardAdText;

    public void Open()
    {
        rewardText.text = FindObjectOfType<NextCombat>().EnemyReward.ToString();
    }
    public void Close()
    {
        rewardText.text = "0";
        rewardAdText.text = "0";
    }
    public void AdReward(int reward)
    {
        rewardAdText.text = reward.ToString();
    }
}
