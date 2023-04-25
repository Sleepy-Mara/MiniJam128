using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardToCemeteryAnimation : MonoBehaviour
{
    [SerializeField]
    private GameObject cardAnim;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float spellTimeInScreen;
    [SerializeField]
    private Vector3 spellDistanceToScreen;
    [SerializeField]
    private List<Transform> playerBattleMapPos;
    [SerializeField]
    private List<Transform> enemyBattleMapPos;
    [SerializeField]
    private Transform playerCemetery;
    [SerializeField]
    private Transform enemyCemetery;
    public List<GameObject> cardFromPlayer = new List<GameObject>();
    public List<GameObject> cardFromEnemy = new List<GameObject>();
    void Update()
    {
        foreach (GameObject card in cardFromPlayer)
        {
            var step = speed * Time.deltaTime;
            var stepRotation = rotationSpeed * Time.deltaTime;
            card.transform.position = Vector3.MoveTowards(card.transform.position, playerCemetery.position, step);
            Vector3 toRotation = Vector3.RotateTowards(card.transform.position, playerCemetery.position, stepRotation, 0.0f);
            toRotation.y = 0;
            card.transform.rotation = Quaternion.LookRotation(toRotation);
            if (Vector3.Distance(card.transform.position, playerCemetery.position) < 0.001f)
            {
                foreach (Cemetery cemetery in FindObjectsOfType<Cemetery>())
                    if (cemetery.player)
                        cemetery.AddCard(card.GetComponentInChildren<CardCore>().card);
                cardFromPlayer.Remove(card);
                Destroy(card);
                break;
            }
        }
        foreach (GameObject card in cardFromEnemy)
        {
            var step = speed * Time.deltaTime;
            var stepRotation = rotationSpeed * Time.deltaTime;
            card.transform.position = Vector3.MoveTowards(card.transform.position, enemyCemetery.position, step);
            Vector3 toRotation = Vector3.RotateTowards(card.transform.position, enemyCemetery.position, stepRotation, 0.0f);
            toRotation.y = 0;
            card.transform.rotation = Quaternion.LookRotation(toRotation);
            if (Vector3.Distance(card.transform.position, enemyCemetery.position) < 0.001f)
            {
                foreach (Cemetery cemetery in FindObjectsOfType<Cemetery>())
                    if (!cemetery.player)
                        cemetery.AddCard(card.GetComponentInChildren<CardCore>().card);
                cardFromEnemy.Remove(card);
                Destroy(card);
                break;
            }
        }
    }
    public void AddCard(Cards card, MapPosition pos, bool player)
    {
        Transform actualPos = transform;
        if (player)
        {
            for (int i = 0; i < FindObjectOfType<Table>().playerPositions.Length; i++)
                if (FindObjectOfType<Table>().playerPositions[i] == pos)
                {
                    Debug.Log(i);
                    actualPos = playerBattleMapPos[i];
                }
        }
        else for (int i = 0; i < FindObjectOfType<Table>().enemyFront.Length; i++)
                        if (FindObjectOfType<Table>().enemyFront[i] == pos)
                {
                    Debug.Log(i);
                    actualPos = enemyBattleMapPos[i];
                }
        if (card.spell)
            FindObjectOfType<Camera>().transform.position += spellDistanceToScreen;
        GameObject newCard = Instantiate(cardAnim, actualPos.position, actualPos.rotation);
        newCard.GetComponentInChildren<CardCore>().card = card;
        newCard.GetComponentInChildren<CardCore>().SetData();
        newCard.GetComponentInChildren<Animator>().enabled = false;
        if (card.spell)
            StartCoroutine(Wait(newCard, player));
        else if (player)
        {
            newCard.transform.parent = null;
            newCard.transform.position = actualPos.position;
            cardFromPlayer.Add(newCard);
        }
        else
        {
            newCard.transform.parent = null;
            newCard.transform.position = actualPos.position;
            cardFromEnemy.Add(newCard);
        }
    }
    IEnumerator Wait(GameObject newCard, bool player)
    {
        yield return new WaitForSeconds(spellTimeInScreen);
        newCard.transform.parent = null;
        if (player)
            cardFromPlayer.Add(newCard);
        else cardFromEnemy.Add(newCard);
    }
}
