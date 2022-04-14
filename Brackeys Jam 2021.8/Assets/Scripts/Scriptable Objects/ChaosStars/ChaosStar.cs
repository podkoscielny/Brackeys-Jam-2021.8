using System;
using UnityEngine;
using Tags = AoOkami.MultipleTagSystem.TagSystem.Tags;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "ChaosStar", menuName = "ScriptableObjects/ChaosStar")]
public class ChaosStar : ScriptableObject
{
    [SerializeField] int pointsToReach;
    [SerializeField] int enemiesLimit;
    [SerializeField] float enemySpawnRate;
    [SerializeField] EnemyProbability[] enemyTypes;

    public int PointsToReach => pointsToReach;
    public int EnemiesLimit => enemiesLimit;
    public float EnemySpawnRate => enemySpawnRate;
    public EnemySO LastEnemyPicked { get; private set; }

    private float _probabilitySum;

    private void OnValidate() => CalculateProbabilitySum();

    private void OnEnable() => CalculateProbabilitySum();

    public Tags GetRandomEnemy()
    {
        EnemySO enemyToBeSpawned = enemyTypes[enemyTypes.Length - 1].EnemyType;

        float randomProbability = Random.Range(0, _probabilitySum);
        float subtractFromSum = 0;

        foreach (EnemyProbability enemy in enemyTypes)
        {
            if (randomProbability - subtractFromSum <= enemy.Probability)
            {
                enemyToBeSpawned = enemy.EnemyType;

                LastEnemyPicked = enemyToBeSpawned;
                Debug.Log(LastEnemyPicked);
                return enemyToBeSpawned.Tag;
            }

            subtractFromSum += enemy.Probability;
        }

        LastEnemyPicked = enemyToBeSpawned;
        return enemyToBeSpawned.Tag;
    }

    private void CalculateProbabilitySum()
    {
        float probability = 0;

        foreach (EnemyProbability enemyType in enemyTypes)
        {
            probability += enemyType.Probability;
        }

        _probabilitySum = probability;
    }

#if UNITY_EDITOR
    public void SortEnemyTypesByProbability() => Array.Sort(enemyTypes, (x, y) => x.Probability.CompareTo(y.Probability));
#endif
}
