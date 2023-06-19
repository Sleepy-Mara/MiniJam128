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
    [SerializeField] private int originalCameraHeight;
    [SerializeField] private int originalCameraWidth;
    private int actualCameraHeight;
    private int actualCameraWidth;
    private Camera mainCamera;
    [SerializeField] private GridLayoutGroup shopPacks;
    [SerializeField] private GridLayoutGroup myPacks;
    private void Awake()
    {
        mainCamera = FindObjectOfType<Camera>();
        ChangeCameraSize();
    }
    private void Update()
    {
        if (actualCameraHeight != mainCamera.pixelHeight || actualCameraWidth != mainCamera.pixelWidth)
        {
            ChangeCameraSize();
        }
    }
    private void ChangeCameraSize()
    {
        shopPacks.spacing = new Vector2(shopPacks.spacing.x * ((float)mainCamera.pixelWidth / (float)originalCameraWidth),
            shopPacks.spacing.y * ((float)mainCamera.pixelHeight / (float)originalCameraHeight));
        shopPacks.cellSize = new Vector2(shopPacks.cellSize.x * ((float)mainCamera.pixelWidth / (float)originalCameraWidth),
            shopPacks.cellSize.y * ((float)mainCamera.pixelHeight / (float)originalCameraHeight));
        myPacks.spacing = new Vector2(myPacks.spacing.x * ((float)mainCamera.pixelWidth / (float)actualCameraWidth),
            myPacks.spacing.y * ((float)mainCamera.pixelHeight / (float)actualCameraHeight));
        myPacks.cellSize = new Vector2(myPacks.cellSize.x * ((float)mainCamera.pixelWidth / (float)actualCameraWidth),
            myPacks.cellSize.y * ((float)mainCamera.pixelHeight / (float)actualCameraHeight));
        actualCameraHeight = mainCamera.pixelHeight;
        actualCameraWidth = mainCamera.pixelWidth;
    }
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
