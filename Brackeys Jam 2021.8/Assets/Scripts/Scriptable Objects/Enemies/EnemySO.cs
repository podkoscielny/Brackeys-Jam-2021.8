using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tags = AoOkami.MultipleTagSystem.TagSystem.Tags;

public abstract class EnemySO : ScriptableObject
{
    [SerializeField] float movementSpeed = 4f;

    public float MovementSpeed => movementSpeed;
    public abstract Tags PoolTag { get; }
}
