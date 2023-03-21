using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaFiller : MonoBehaviour
{
    public GameObject[] manaFillers;

    public void RefreshManas(int actualMana, int maxMana)
    {
        for (int i = maxMana - 1; i > actualMana - 1; i--)
        {
            manaFillers[i].SetActive(false);
        }
        for (int j = 0; j < actualMana; j++)
        {
            manaFillers[j].SetActive(true);
        }
    }
}
