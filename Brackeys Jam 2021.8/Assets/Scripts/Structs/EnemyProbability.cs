using System;
using UnityEngine;

[Serializable]
public struct EnemyProbability<T>
{
    [SerializeField] T enemyType;
    [Range(0, 1)] [SerializeField] float probability;

    public T EnemyType => enemyType;
    public float Probability => probability;
}