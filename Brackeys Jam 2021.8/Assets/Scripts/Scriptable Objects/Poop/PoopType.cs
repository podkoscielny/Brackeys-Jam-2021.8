using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Poop", menuName = "ScriptableObjects/Poop")]
public class PoopType : ScriptableObject
{
    [SerializeField] bool isExplosive;
    [SerializeField] int pointsWorth;
    [SerializeField] RuntimeAnimatorController poopAnimator;
    [SerializeField] ExplosionType explosionType;

    public bool IsExplosive => isExplosive;
    public int PointsWorth => pointsWorth;
    public RuntimeAnimatorController PoopAnimator => poopAnimator;
    public ExplosionType ExplosionType => explosionType;
}
