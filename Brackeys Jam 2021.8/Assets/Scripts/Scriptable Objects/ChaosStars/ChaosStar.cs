using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChaosStar", menuName = "ScriptableObjects/ChaosStar")]
public class ChaosStar : ScriptableObject
{
    public int pointsToReach;

    public int hostilesLimit;
    public float neutralSpawnRate;
    public float hostileSpawnRate;

    public int enemiesLimit;
    public float enemySpawnRate;

    public EnemyProbability[] enemyTypes;

#if UNITY_EDITOR
    public void SortEnemyTypesByProbability() => Array.Sort(enemyTypes, (x, y) => x.probability.CompareTo(y.probability));
#endif
}
