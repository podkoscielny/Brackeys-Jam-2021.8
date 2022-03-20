using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tags = TagSystem.Tags;

[CreateAssetMenu(fileName = "NonHostileSO", menuName = "ScriptableObjects/NonHostileSO")]
public class NonHostileSO : EnemySO
{
    public override Tags Tag { get; } = Tags.Character;
}