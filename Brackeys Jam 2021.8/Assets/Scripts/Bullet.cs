using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPlayerHitter
{
    [SerializeField] Rigidbody2D bulletRb;

    private float _speed = 10f;
    private const float PLAYER_DAMAGE_AMOUNT = 1f;

    private void OnEnable() => GoTowardsTarget();

    void GoTowardsTarget() => bulletRb.AddForce(transform.right * _speed, ForceMode2D.Impulse);

    public float PlayerDamageAmount() => PLAYER_DAMAGE_AMOUNT;
}
