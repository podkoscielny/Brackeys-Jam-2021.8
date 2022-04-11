using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tags = AoOkami.MultipleTagSystem.TagSystem.Tags;

public abstract class EnemySO : ScriptableObject
{
    public abstract Tags Tag { get; }
}
