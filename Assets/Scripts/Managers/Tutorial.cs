using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private List<TutotialPhases> phases;
    private bool tutorial = true;

    public void StartTutorial()
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
            case tipePhase.PlayCard:
                StartCoroutine(WaitToPlay(actualPhase));
                break;
            case tipePhase.Button:
                StartCoroutine(WaitToPress(actualPhase));
                break;
        }
    }
    private void EndPhase(int actualPhase)
    {
        if (!FindObjectOfType<TurnManager>().canPlayCards && !FindObjectOfType<Draw>().canDraw)
        {
            StartCoroutine(WaitToPlayerTurn(actualPhase));
            return;
        }
        actualPhase++;
        StopAllCoroutines();
        if (actualPhase >= phases.Count)
        {
            tutorial = false;
            return;
        }
        NextPhase(actualPhase);
    }
    IEnumerator WaitToPlay(int actualPhase)
    {
        MapPosition posToWait = null;
        foreach (MapPosition pos in FindObjectOfType<Table>().playerPositions)
            if (pos.cardPos.gameObject == phases[actualPhase].toDo)
                posToWait = pos;
            else pos.cardPos.isPlayable = false;
        if (posToWait != null)
        {
            yield return new WaitUntil(() => posToWait.card != null);
        }
        foreach (MapPosition pos in FindObjectOfType<Table>().playerPositions)
            pos.cardPos.isPlayable = true;
        foreach (var things in phases[actualPhase].thingsInPhase)
            things.SetActive(!things.activeSelf);
        EndPhase(actualPhase);
    }
    IEnumerator WaitToPress(int actualPhase)
    {
        bool clicked = false;
        phases[actualPhase].toDo.GetComponent<Button>().onClick.AddListener(() => clicked = true);
        yield return new WaitUntil(() => clicked);
        foreach (var things in phases[actualPhase].thingsInPhase)
            things.SetActive(!things.activeSelf);
        EndPhase(actualPhase);
    }
    IEnumerator WaitToDraw(int actualPhase)
    {
        int cards = phases[actualPhase].toDo.GetComponent<Draw>()._cardsInHand.Count;
        yield return new WaitUntil(() => cards < phases[actualPhase].toDo.GetComponent<Draw>()._cardsInHand.Count);
        foreach (var things in phases[actualPhase].thingsInPhase)
            things.SetActive(!things.activeSelf);
        EndPhase(actualPhase);
    }
    IEnumerator WaitToPlayerTurn(int actualPhase)
    {
        yield return new WaitUntil(() => FindObjectOfType<Draw>().canDraw);
        EndPhase(actualPhase);
    }
}
[System.Serializable]
public class TutotialPhases
{
    [SerializeField] private string name;
    public tipePhase tipePhase;
    public GameObject toDo;
    public List<GameObject> thingsInPhase;
}
public enum tipePhase
{
    Draw,
    PlayCard,
    Button,
}
