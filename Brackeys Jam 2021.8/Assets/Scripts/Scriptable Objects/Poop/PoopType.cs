using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Poop", menuName = "ScriptableObjects/Poop")]
public class PoopType : ScriptableObject
{
    public bool isExplosive;
    public int pointsWorth;
    public RuntimeAnimatorController poopAnimator;
    public ExplosionType explosionType;
}
