using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Explosion", menuName = "ScriptableObjects/Explosion")]
public class ExplosionType : ScriptableObject
{
    public Vector3 size;
    public Sprite sprite;
    public RuntimeAnimatorController animatorController;
}
