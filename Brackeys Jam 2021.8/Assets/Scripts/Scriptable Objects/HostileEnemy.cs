using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HostileEnemy", menuName = "ScriptableObjects/HostileEnemy")]
public class HostileEnemy : ScriptableObject
{
    public Sprite characterSprite;
    public Gun gun;
    public Vector3 localScale;
    public RuntimeAnimatorController animatorController;
}
