using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "ScriptableObjects/Gun")]
public class Gun : ScriptableObject
{
    public Sprite gunSprite;
    public Vector2 firePoint;
}
