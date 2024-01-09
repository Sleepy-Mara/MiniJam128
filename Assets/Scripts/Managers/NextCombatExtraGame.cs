using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextCombatExtraGame : NextCombat
{
    private bool endless;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void StartCombat()
    {
        if (endless)
        {
            if (FindObjectOfType<EnemyAI>())
            {
                var num = enemyNum;
                while (num >= enemies.Length)
                    num -= enemies.Length;
                _enemy.GetComponent<EnemyAI>().StartCombat(enemies[num].enemyDeck.deck);
            }
            else
                _enemy.strategy = enemies[enemyNum].strategy;
            _audioPlayer.Play("Music" + enemyNum);
            _turnManager.StartBattle();
            introCombat.SetActive(false);
            endTurnButton.SetActive(true);
        }
        else
            base.StartCombat();
    }
}
