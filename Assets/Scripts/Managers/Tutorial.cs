using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private List<TutotialPhases> gamePhases;
    [SerializeField]
    private List<TutotialPhases> menuPhases;
    private bool gameTutorial = true;
    private bool menuTutorial = true;
    [SerializeField]
    private List<Cards> deck;
    [SerializeField]
    private List<Cards> bloodDeck;
    private SaveWithJson json;
    [SerializeField] private Enemy enemy;
    private void Start()
    {
        json = FindObjectOfType<SaveWithJson>();
        gameTutorial = json.SaveData.gameTutorial;
        menuTutorial = json.SaveData.menuTutorial;
        if (enemy != null && gameTutorial)
        {
            enemy.enabled = true;
            FindObjectOfType<EnemyAI>().enabled = false;
            FindObjectOfType<NextCombat>().enemy = enemy;
        }
        if (menuTutorial && !gameTutorial && !FindObjectOfType<NextCombat>())
            NextPhase(0, menuPhases);
    }
    public void StartTutorial()
    {
        if (!FindObjectOfType<NextCombat>())
            return;
        if (!gameTutorial || FindObjectOfType<NextCombat>().enemyNum > 0)
            return;
        FindObjectOfType<Draw>().CurrentDeck = deck;
        FindObjectOfType<Draw>().CurrentBloodDeck = bloodDeck;
        NextPhase(0, gamePhases);
    }

    private void NextPhase(int actualPhase, List<TutotialPhases> phases)
    {
        if (phases[actualPhase] == null)
            return;
        foreach(var things in phases[actualPhase].thingsInPhase)
            things.SetActive(!things.activeSelf);
        switch(phases[actualPhase].tipePhase)
        {
            case tipePhase.Draw:
                StartCoroutine(WaitToDraw(actualPhase, phases));
                break;
            case tipePhase.PlayCard:
                StartCoroutine(WaitToPlay(actualPhase, phases));
                break;
            case tipePhase.Button:
                StartCoroutine(WaitToPress(actualPhase, phases));
                break;
        }
    }
    private void EndPhase(int actualPhase, List<TutotialPhases> phases)
    {
        if (FindObjectOfType<TurnManager>())
            if (!FindObjectOfType<TurnManager>().canPlayCards && !FindObjectOfType<Draw>().canDraw)
            {
                StartCoroutine(WaitToPlayerTurn(actualPhase, phases));
                return;
            }
        actualPhase++;
        StopAllCoroutines();
        if (actualPhase >= phases.Count)
        {
            SaveData saveData = json.SaveData;
            if (!FindObjectOfType<NextCombat>())
            {
                menuTutorial = false;
                saveData.menuTutorial = menuTutorial;
            } else
            {
                gameTutorial = false;
                saveData.gameTutorial = gameTutorial;
                enemy.enabled = false;
                FindObjectOfType<EnemyAI>().enabled = true;
            }
            json.SaveData = saveData;
            return;
        }
        NextPhase(actualPhase, phases);
    }
    IEnumerator WaitToPlay(int actualPhase, List<TutotialPhases> phases)
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
        EndPhase(actualPhase, phases);
    }
    IEnumerator WaitToPress(int actualPhase, List<TutotialPhases> phases)
    {
        bool clicked = false;
        phases[actualPhase].toDo.GetComponent<Button>().onClick.AddListener(() => clicked = true);
        yield return new WaitUntil(() => clicked);
        foreach (var things in phases[actualPhase].thingsInPhase)
            things.SetActive(!things.activeSelf);
        EndPhase(actualPhase, phases);
    }
    IEnumerator WaitToDraw(int actualPhase, List<TutotialPhases> phases)
    {
        yield return new WaitForEndOfFrame();
        int cards = phases[actualPhase].toDo.GetComponent<Draw>()._cardsInHand.Count;
        yield return new WaitUntil(() => cards < phases[actualPhase].toDo.GetComponent<Draw>()._cardsInHand.Count);
        foreach (var things in phases[actualPhase].thingsInPhase)
            things.SetActive(!things.activeSelf);
        EndPhase(actualPhase, phases);
    }
    IEnumerator WaitToPlayerTurn(int actualPhase, List<TutotialPhases> phases)
    {
        yield return new WaitUntil(() => FindObjectOfType<Draw>().canDraw);
        EndPhase(actualPhase, phases);
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
