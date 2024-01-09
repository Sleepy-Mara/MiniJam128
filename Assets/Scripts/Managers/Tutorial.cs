using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private List<TutotialPhases> phases;
    private bool tutorial;

    private void Start()
    {
        if (!tutorial || FindObjectOfType<NextCombat>().enemyNum > 0)
            return;
        NextPhase(0);
    }

    private void NextPhase(int actualPhase)
    {
        if (phases[actualPhase] == null)
            return;
        foreach(var things in phases[actualPhase].thingsInPhase)
            things.SetActive(!things.activeSelf);
        switch(phases[actualPhase].tipePhase)
        {
            case tipePhase.Draw:
                StartCoroutine(WaitToDraw(actualPhase));
                break;
            case tipePhase.PutCard:
                StartCoroutine(WaitToPlay(actualPhase));
                break;
            case tipePhase.EndTurn:
                StartCoroutine(WaitToPress(actualPhase));
                break;
        }
        if (phases[actualPhase].toDo.GetComponent<CardPos>())
        {
            StartCoroutine(WaitToPlay(actualPhase));
            return;
        }
        if (phases[actualPhase].toDo.GetComponent<Button>())
        {
            StartCoroutine(WaitToPress(actualPhase));
            return;
        }
    }
    private void EndPhase(int actualPhase)
    {
        foreach (var things in phases[actualPhase].thingsInPhase)
            things.SetActive(!things.activeSelf);
        if (!FindObjectOfType<TurnManager>().canPlayCards)
        {
            StartCoroutine(WaitToPlayerTurn(actualPhase));
            return;
        }
        NextPhase(actualPhase++);
    }
    IEnumerator WaitToPlay(int actualPhase)
    {
        MapPosition posToWait = null;
        foreach (MapPosition pos in FindObjectOfType<Table>().playerPositions)
            if (pos.cardPos.gameObject == phases[actualPhase].toDo)
                posToWait = pos;
            else pos.cardPos.gameObject.SetActive(false);
        if (posToWait != null)
        {
            yield return new WaitUntil(() => posToWait.card != null);
        }
        foreach (MapPosition pos in FindObjectOfType<Table>().playerPositions)
            pos.cardPos.gameObject.SetActive(true);
        EndPhase(actualPhase);
    }
    IEnumerator WaitToPress(int actualPhase)
    {
        bool clicked = false;
        phases[actualPhase].toDo.GetComponent<Button>().onClick.AddListener(() => clicked = true);
        yield return new WaitUntil(() => clicked);
        EndPhase(actualPhase);
    }
    IEnumerator WaitToDraw(int actualPhase)
    {
        int cards = phases[actualPhase].toDo.GetComponent<Draw>()._cardsInHand.Count;
        yield return new WaitUntil(() => cards < phases[actualPhase].toDo.GetComponent<Draw>()._cardsInHand.Count);
        EndPhase(actualPhase);
    }
    IEnumerator WaitToPlayerTurn(int actualPhase)
    {
        yield return new WaitUntil(() => FindObjectOfType<TurnManager>().canPlayCards);
        EndPhase(actualPhase);
    }
}
[System.Serializable]
public class TutotialPhases
{
    public tipePhase tipePhase;
    public GameObject toDo;
    public Cards card;
    public List<GameObject> thingsInPhase;
}
public enum tipePhase
{
    Draw,
    PutCard,
    EndTurn,
}
