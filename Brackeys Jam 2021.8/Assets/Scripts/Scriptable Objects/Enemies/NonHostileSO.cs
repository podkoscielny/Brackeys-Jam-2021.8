using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tags = AoOkami.MultipleTagSystem.TagSystem.Tags;

[CreateAssetMenu(fileName = "NonHostileSO", menuName = "ScriptableObjects/NonHostileSO")]
public class NonHostileSO : EnemySO
{
    public override Tags PoolTag { get; } = Tags.Character;
}