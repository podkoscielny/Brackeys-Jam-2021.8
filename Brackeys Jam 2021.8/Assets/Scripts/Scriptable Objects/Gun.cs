using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "ScriptableObjects/Gun")]
public class Gun : ScriptableObject
{
    [SerializeField] Sprite gunSprite;
    [SerializeField] Vector2 firePoint;

    public Sprite GunSprite => gunSprite;
    public Vector2 FirePoint => firePoint;
}
