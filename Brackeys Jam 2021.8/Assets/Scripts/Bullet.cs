using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPlayerHitter
{
    [SerializeField] Rigidbody2D bulletRb;

    public float PlayerDamageAmount => 1f;

    private const float SPEED = 10f;

    void OnEnable() => GoTowardsTarget();

    private void GoTowardsTarget() => bulletRb.AddForce(transform.right * SPEED, ForceMode2D.Impulse);
}
