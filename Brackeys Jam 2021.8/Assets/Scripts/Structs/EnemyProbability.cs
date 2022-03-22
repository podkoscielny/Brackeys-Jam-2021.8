using System;
using UnityEngine;

[Serializable]
public struct EnemyProbability
{
    [SerializeField] EnemySO enemyType;
    [Range(0, 1)] [SerializeField] float probability;

    public EnemySO EnemyType => enemyType;
    public float Probability => probability;
}