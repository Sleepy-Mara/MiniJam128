using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textCurrency;
    [SerializeField] private TextMeshProUGUI moneyEarned;
    [SerializeField] private Animator animator;
    [SerializeField] private bool enabled;
    private bool disabled;
    private static CurrencyManager instance;
    private static int savedCurrency;
    private SaveWithJson json;
    private int currency;
    public int Currency
    {
        get { return currency; }
        set {
            string posOrNeg = "+";
            if(value < 0)
                posOrNeg = "-";
            moneyEarned.text = posOrNeg + value.ToString();
            SaveData saveData = json.SaveData;
            saveData.currentCurrency = currency;
            json.SaveData = saveData;
            animator.SetTrigger("EarnMoney");
            StartCoroutine(EarnMoney(value));
        }
    }
    private void Awake()
    {
        json = FindObjectOfType<SaveWithJson>();
        if (instance == null)
        {
            instance = this;
            savedCurrency = currency;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
        currency = savedCurrency;
        moneyEarned.text = currency.ToString();
        if (enabled)
            disabled = false;
        else disabled = true;
        animator.SetBool("Disabled", disabled);
        animator.SetBool("Enabled", enabled);
    }
    IEnumerator EarnMoney(int value)
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("EarnMoney"));
        currency += value;
        if (currency < 0)
            currency = 0;
        textCurrency.text = currency.ToString();
    }
    public void ChangeDisabledAndEnabled()
    {
        if(enabled)
        {
            enabled = false;
            disabled = true;
        }
        else
        {
            disabled = false;
            enabled = true;
        }
        animator.SetBool("Disabled", disabled);
        animator.SetBool("Enabled", enabled);
    }
}
