using System;
using UnityEngine;

[Serializable]
public struct EnemyProbability
{
    public EnemySO enemyType;
    [Range(0, 1)] public float probability;
}