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

    public List<EnemyProbability> enemyTypes;

    private void OnValidate() => SortEnemyTypesByProbability();

    private void SortEnemyTypesByProbability()
    {

    }
}
