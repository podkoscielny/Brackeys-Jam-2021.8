using System;
using UnityEngine;
using Tags = TagSystem.Tags;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "ChaosStar", menuName = "ScriptableObjects/ChaosStar")]
public class ChaosStar : ScriptableObject
{
    public int pointsToReach;

    public int hostilesLimit;
    public float neutralSpawnRate;
    public float hostileSpawnRate;

    [SerializeField] int enemiesLimit;
    [SerializeField] float enemySpawnRate;
    [SerializeField] EnemyProbability[] enemyTypes;

    public int EnemiesLimit => enemiesLimit;
    public float EnemySpawnRate => enemySpawnRate;
    public EnemySO LastEnemyPicked { get; private set; }
    public EnemyProbability[] EnemyTypes => enemyTypes;

    private float _probabilitySum;

    private void OnValidate() => CalculateProbabilitySum();

    private void OnEnable() => CalculateProbabilitySum();

    public Tags GetRandomEnemy()
    {
        EnemySO enemyToBeSpawned = enemyTypes[enemyTypes.Length - 1].enemyType;

        float randomProbability = Random.Range(0, _probabilitySum);
        float subtractFromSum = 0;

        foreach (EnemyProbability enemy in enemyTypes)
        {
            if (randomProbability - subtractFromSum <= enemy.probability)
            {
                LastEnemyPicked = enemy.enemyType;
                return enemyToBeSpawned.Tag;
            }

            subtractFromSum += enemy.probability;
        }

        LastEnemyPicked = enemyToBeSpawned;
        return enemyToBeSpawned.Tag;
    }

    private void CalculateProbabilitySum()
    {
        float probability = 0;

        foreach (EnemyProbability enemyType in enemyTypes)
        {
            probability += enemyType.probability;
        }

        _probabilitySum = probability;
    }

#if UNITY_EDITOR
    public void SortEnemyTypesByProbability() => Array.Sort(enemyTypes, (x, y) => x.probability.CompareTo(y.probability));
#endif
}
