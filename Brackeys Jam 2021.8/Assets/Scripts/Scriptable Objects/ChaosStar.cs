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
}
