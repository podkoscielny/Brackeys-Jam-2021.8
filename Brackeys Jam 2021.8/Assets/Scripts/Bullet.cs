using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPlayerHitter
{
    [SerializeField] Rigidbody2D bulletRb;

    public float PlayerDamageAmount { get; private set; } = 1f;
    public float CameraShakeIntensity { get; private set; } = 3f;
    public float CameraShakeDuration { get; private set; } = 0.2f;

    private float _speed = 10f;

    void OnEnable() => GoTowardsTarget();

    private void GoTowardsTarget() => bulletRb.AddForce(transform.right * _speed, ForceMode2D.Impulse);
}
