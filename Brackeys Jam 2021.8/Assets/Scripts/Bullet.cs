using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPlayerHitter
{
    [SerializeField] Rigidbody2D bulletRb;

    public float PlayerDamageAmount { get; private set; } = 1f;

    private float _speed = 10f;

    private void OnEnable() => GoTowardsTarget();

    void GoTowardsTarget() => bulletRb.AddForce(transform.right * _speed, ForceMode2D.Impulse);
}
