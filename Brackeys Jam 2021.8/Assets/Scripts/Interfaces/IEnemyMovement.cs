using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyMovement
{
    public float MovementSpeed { get; }

    public void Move();
}
