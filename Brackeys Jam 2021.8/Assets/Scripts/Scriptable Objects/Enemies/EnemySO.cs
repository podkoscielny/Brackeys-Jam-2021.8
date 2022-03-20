using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tags = TagSystem.Tags;

public abstract class EnemySO : ScriptableObject
{
    public abstract Tags Tag { get; }
}
