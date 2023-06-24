using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private List<PackInShop> packInShops;
    [SerializeField] private List<CardPacks> myCardPacks;
    [SerializeField] private GameObject shop;
    [SerializeField] private GameObject openPacks;
    [SerializeField] private GridLayoutGroup shopPacks;
    [SerializeField] private GridLayoutGroup myPacks;
    public void Buy(CardPacks pack)
    {
        foreach (PackInShop shop in packInShops)
            if (shop.pack == pack)
                if (FindObjectOfType<CurrencyManager>().Currency >= shop.cost)
                {
                    myCardPacks.Add(pack);
                    FindObjectOfType<CurrencyManager>().Currency = -shop.cost;
                }
    }
    public void OpenShop()
    {
        shop.SetActive(true);
    }
    public void CloseShop()
    {
        shop.SetActive(false);
    }
    public void OpenMyPacks()
    {
        openPacks.SetActive(true);
    }
    public void CloseMyPacks()
    {
        openPacks.SetActive(false);
    }
}
[System.Serializable]
public class PackInShop
{
    public int cost;
    public CardPacks pack;
}
