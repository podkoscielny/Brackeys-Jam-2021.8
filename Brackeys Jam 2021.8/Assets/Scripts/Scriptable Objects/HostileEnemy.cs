using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HostileEnemy", menuName = "ScriptableObjects/HostileEnemy")]
public class HostileEnemy : ScriptableObject
{
    public Sprite characterSprite;
    public Sprite gunSprite;
    public Vector2 firePoint;
    public Vector3 localScale;
    public RuntimeAnimatorController animatorController;
}
