using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    public static event Action OnHitTaken;

    [SerializeField] Animator playerAnimator;
    [SerializeField] Rigidbody2D playerRb;
    [SerializeField] AudioSource playerAudio;
    [SerializeField] AudioClip impactSoundEffect;
    [SerializeField] CameraShake cameraShake;
    [SerializeField] PlayerHealth playerHealth;

    private bool _areCollisionsDisabled = false;
    private Vector2 _impactDirectionRight = new Vector2(1.411f, 0.637f);
    private Vector2 _impactDirectionLeft = new Vector2(-1.411f, 0.637f);

    private const float HIT_FORCE = 6f;
    private const float CAMERA_SHAKE_INTENSITY = 3f;
    private const float CAMERA_SHAKE_DURATION = 0.2f;

    private void OnEnable() => PlayerHealth.OnGameOver += DisableCollisions;

    private void OnDisable() => PlayerHealth.OnGameOver -= DisableCollisions;

    private void DisableCollisions() => _areCollisionsDisabled = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_areCollisionsDisabled) return;

        bool isHit = playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Pigeon_hitTaken");

        if (collision.TryGetComponent(out IPlayerHitter playerHitter) && !isHit)
        {
            Vector2 impactDirection = transform.position.x > collision.transform.position.x ? _impactDirectionRight : _impactDirectionLeft;
            HandleHitTaken(playerHitter.PlayerDamageAmount, impactDirection);
            PlayImpactSound();
        }
    }

    private void PlayImpactSound()
    {
        playerAudio.clip = impactSoundEffect;
        playerAudio.Play();
    }

    private void HandleHitTaken(float damageAmount, Vector2 impactDirection)
    {
        OnHitTaken?.Invoke();
        SetHitAnimation();
        PushThePlayerOnCollision(impactDirection, damageAmount);
        cameraShake.ShakeCamera(CAMERA_SHAKE_INTENSITY, CAMERA_SHAKE_DURATION);
    }

    private void SetHitAnimation()
    {
        playerAnimator.SetTrigger("IsHit");
        playerAnimator.SetBool("IsJumping", false);
    }

    private void PushThePlayerOnCollision(Vector2 impactDirection, float damageAmount)
    {
        playerRb.velocity = Vector2.zero;
        playerRb.AddForce(impactDirection * HIT_FORCE, ForceMode2D.Impulse);

        playerHealth.TakeDamage(damageAmount);
    }
}
