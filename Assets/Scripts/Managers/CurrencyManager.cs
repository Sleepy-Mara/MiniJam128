using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private TextMeshPro textCurrency;
    private int currency;
    public int Currency
    {
        get { return currency; }
        set {
            currency += value;
            textCurrency.text = currency.ToString();
        }
    }
}
