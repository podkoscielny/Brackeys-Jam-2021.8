using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tags = AoOkami.MultipleTagSystem.TagSystem.Tags;

[CreateAssetMenu(fileName = "HostileEnemy", menuName = "ScriptableObjects/HostileEnemy")]
public class HostileEnemy : EnemySO
{
    [SerializeField] Sprite characterSprite;
    [SerializeField] Gun gun;
    [SerializeField] Vector3 localScale;
    [SerializeField] RuntimeAnimatorController animatorController;

    public Sprite CharacterSprite => characterSprite;
    public Gun Gun => gun;
    public Vector3 LocalScale => localScale;
    public RuntimeAnimatorController AnimatorController => animatorController;

    public override Tags PoolTag { get; } = Tags.Hostile;
}
