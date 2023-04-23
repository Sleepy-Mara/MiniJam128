using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cemetery : MonoBehaviour
{
    [SerializeField]
    private GameObject cardCore;
    [SerializeField]
    private Transform cemetery;
    public bool player;
    private Camera mainCamera;
    [SerializeField] private int originalCameraHeight;
    [SerializeField] private int originalCameraWidth;
    private int actualCameraHeight;
    private int actualCameraWidth;
    [SerializeField]
    private Transform playerCemetery;
    [SerializeField]
    private Transform enemyCemetery;
    private void Awake()
    {
        mainCamera = FindObjectOfType<Camera>();
        var listCemetery = cemetery.GetComponentInChildren<GridLayoutGroup>();
        listCemetery.spacing = new Vector2(listCemetery.spacing.x * ((float)mainCamera.pixelWidth / (float)originalCameraWidth),
            listCemetery.spacing.y * ((float)mainCamera.pixelHeight / (float)originalCameraHeight));
        listCemetery.cellSize = new Vector2(listCemetery.cellSize.x * ((float)mainCamera.pixelWidth / (float)originalCameraWidth),
            listCemetery.cellSize.y * ((float)mainCamera.pixelHeight / (float)originalCameraHeight));
        actualCameraHeight = mainCamera.pixelHeight;
        actualCameraWidth = mainCamera.pixelWidth;
    }
    private void Update()
    {
        if (actualCameraHeight != mainCamera.pixelHeight || actualCameraWidth != mainCamera.pixelWidth)
        {
            var listCemetery = cemetery.GetComponent<GridLayoutGroup>();
            listCemetery.spacing = new Vector2(listCemetery.spacing.x * ((float)mainCamera.pixelWidth / (float)actualCameraWidth),
                listCemetery.spacing.y * ((float)mainCamera.pixelHeight / (float)actualCameraHeight));
            listCemetery.cellSize = new Vector2(listCemetery.cellSize.x * ((float)mainCamera.pixelWidth / (float)actualCameraWidth),
                listCemetery.cellSize.y * ((float)mainCamera.pixelHeight / (float)actualCameraHeight));
            actualCameraHeight = mainCamera.pixelHeight;
            actualCameraWidth = mainCamera.pixelWidth;
        }
    }
    public void AddCard(Cards cardToAdd)
    {
        Transform newCemetery;
        if (player)
            newCemetery = playerCemetery;
        else newCemetery = enemyCemetery;
        var newCard = Instantiate(cardCore, newCemetery).GetComponent<CardCore>();
        newCard.card = cardToAdd;
    }
    private void OnMouseDown()
    {
        if (player)
            cemetery.GetComponent<Animator>().SetBool("OpenPlayer", true);
        if (!player)
            cemetery.GetComponent<Animator>().SetBool("OpenEnemy", true);
    }
    public void CloseMenu()
    {
        cemetery.GetComponent<Animator>().SetBool("OpenPlayer", false);
        cemetery.GetComponent<Animator>().SetBool("OpenEnemy", false);
    }
}
