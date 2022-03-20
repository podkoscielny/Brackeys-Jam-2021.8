using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tags = TagSystem.Tags;

[CreateAssetMenu(fileName = "HostileEnemy", menuName = "ScriptableObjects/HostileEnemy")]
public class HostileEnemy : EnemySO
{
    public Sprite characterSprite;
    public Gun gun;
    public Vector3 localScale;
    public RuntimeAnimatorController animatorController;

    public override Tags Tag { get; } = Tags.Hostile;
}
