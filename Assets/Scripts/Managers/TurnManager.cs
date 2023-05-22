using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [HideInInspector] public int turn;
    [SerializeField] private Player player;
    public Enemy enemy;
    private Draw draw;
    public Table table;
    private EffectManager effectManager;
    public bool canPlayCards;
    public bool canEndTurn;
    [SerializeField] private int cardsInHandStart;
    public Animator drawCardWindow;
    public Animator drawBeforePlayWindow;

    private void Start()
    {
        draw = FindObjectOfType<Draw>();
        table = FindObjectOfType<Table>();
        effectManager = FindObjectOfType<EffectManager>();
        //StartBattle();
    }

    public void StartBattle()
    {
        enemy.RestoreHealth(10);
        Debug.Log("Empieza el combate");
        enemy.MoveBackCards(turn);
        for (int i = 0; i < cardsInHandStart; i++)
            draw.DrawACard(Draw.DeckType.Mana, true, true);
        //hacer cosas como agarrar cartas iniciales, setear enemigo? y eso
    }
    public void StartTurn()
    {
        foreach (MapPosition card in table.playerPositions)
            if (card.card != null)
                effectManager.CheckConditionStartOfTurn(card.card);
        FindObjectOfType<CameraManager>().HandCamera();
        canEndTurn = false;
        turn++;
        Debug.Log("Empieza el turno " + turn);
        player.RestoreMana();
        draw.CanDraw();
        //algun efecto de comienzo de turno
    }
    public void PlayableTurn()
    {
        Debug.Log("deberias poder jugar");
        canPlayCards = true;
        //var cards = FindObjectsOfType<Card>();
        //foreach (Card card in cards)
        //    card.canPlay = true;
    }
    public void EndTurn()
    {
        if (canEndTurn)
        {
            Debug.Log("puta");
            FindObjectOfType<CameraManager>().PlaceCardCamera();
            canPlayCards = false;
            StartCoroutine(AttackPhase());
        }
        else
        {
            drawCardWindow.SetTrigger("Activate");
            //abrir ventanita que diga que tenes que robar antes de terminar
        }
        //var cards = FindObjectsOfType<Card>();
        //foreach (Card card in cards)
        //    card.canPlay = false;
        //foreach (MapPosition position in battleMap.playerPositions)
        //{
        //    if(position.card != null)
        //        position.card.Attack();
        //}
        //algun efecto de fin de turno quizas
        //que actue el oponente
    }
    public bool CanPlayCards()
    {
        if (!canPlayCards)
            drawBeforePlayWindow.SetTrigger("Activate");
        return canPlayCards;
    }

    IEnumerator AttackPhase()
    {
        canEndTurn = false;
        bool wait = false;
        foreach (MapPosition card in table.playerPositions)
            if (card.card != null)
            {
                card.card.attacking = true;
                card.card.Attack();
                if (card.card.ActualAttack > 0)
                {
                    yield return new WaitUntil(() => card.card.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("AttackPlayer"));
                    yield return new WaitUntil(() => !card.card.attacking);
                }
            }
        foreach (MapPosition card in table.playerPositions)
            if (card.card != null)
            {
                card.card.checkingEffect = true;
                effectManager.CheckConditionEndOfTurn(card.card);
                yield return new WaitUntil(() => !card.card.checkingEffect);
            }
        if (enemy.actualHealth > 0)
            enemy.MoveBackCards(turn);
    }
}
