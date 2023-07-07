using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyDeck")]
public class EnemyDeck : ScriptableObject
{
    public List<Cards> deck;
}
