using System;
using UnityEngine;

[Serializable]
public struct EnemyType<T>
{
    [SerializeField] int enemyLimit;
    [SerializeField] float spawnRate;
    [SerializeField] EnemyProbability<T>[] enemyVariants;

    public int EnemyLimit => enemyLimit;
    public float SpawnRate => spawnRate;
    public EnemyProbability<T>[] EnemyVariants => enemyVariants;
}