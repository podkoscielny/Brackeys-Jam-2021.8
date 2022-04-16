using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ChaosStar", menuName = "ScriptableObjects/ChaosStar")]
public class ChaosStar : ScriptableObject
{
    [SerializeField] int pointsToReach;
    [SerializeField] EnemyType<HostileEnemy> hostileEnemies;
    [SerializeField] EnemyType<NonHostileSO> nonhostileEnemies;

    public int PointsToReach => pointsToReach;
    public EnemyType<HostileEnemy> HostileEnemies => hostileEnemies;
    public EnemyType<NonHostileSO> NonHostileEnemies => nonhostileEnemies;
}
