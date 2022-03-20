using System;
using Tags = TagSystem.Tags;
using UnityEngine;

[Serializable]
public struct EnemyProbability
{
    public EnemySO enemyType;
    [Range(0, 1)] public float probability;
}