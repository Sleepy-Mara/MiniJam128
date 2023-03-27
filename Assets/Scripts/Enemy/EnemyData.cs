using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData 
{
    public Strategy strategy;
    public GameObject enemyCharacter;
    public Cards[] rewards;
    [TextArea(1, 4)]
    public string introCombatMessage;
    [TextArea(1, 4)]
    public string wonCombatMessage;
    [TextArea(1, 4)]
    public string lostCombatMessage;
}
