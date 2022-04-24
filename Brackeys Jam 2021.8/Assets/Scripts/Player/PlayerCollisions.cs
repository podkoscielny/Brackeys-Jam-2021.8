using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    public static event Action OnHitTaken;

    [Header("Player Components")]
    [SerializeField] Animator playerAnimator;
    [SerializeField] Rigidbody2D playerRb;

    [Header("Audio")]
    [SerializeField] AudioSource playerAudio;
    [SerializeField] AudioClip impactSoundEffect;

    [Header("Scene Components")]
    [SerializeField] CameraShake cameraShake;

    [Header("Systems")]
    [SerializeField] PlayerHealth playerHealth;

    private bool _areCollisionsDisabled = false;
    private WaitForSeconds _waitForReenableCollisions;
    private Vector2 _impactDirectionRight = new Vector2(1.411f, 0.637f);
    private Vector2 _impactDirectionLeft = new Vector2(-1.411f, 0.637f);

    private const string HIT_ANIMATION_NAME = "Pigeon_hitTaken";
    private const float HIT_FORCE = 6f;
    private const float CAMERA_SHAKE_INTENSITY = 3f;
    private const float CAMERA_SHAKE_DURATION = 0.2f;

    private void OnEnable()
    {
        PlayerHealth.OnGameOver += DisableCollisions;
        SceneController.OnSceneChange += DisableCollisions;
    }

    private void OnDisable()
    {
        PlayerHealth.OnGameOver -= DisableCollisions;
        SceneController.OnSceneChange -= DisableCollisions;
    }

    private void Start() => CacheHitTakenAnimationDuration();

    private void CacheHitTakenAnimationDuration()
    {
        foreach (var animation in playerAnimator.runtimeAnimatorController.animationClips)
        {
            if (animation.name == HIT_ANIMATION_NAME)
                _waitForReenableCollisions = new WaitForSeconds(animation.averageDuration);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_areCollisionsDisabled) return;

        if (collision.TryGetComponent(out IPlayerHitter playerHitter))
        {
            Vector2 impactDirection = transform.position.x > collision.transform.position.x ? _impactDirectionRight : _impactDirectionLeft;
            HandleHitTaken(playerHitter.PlayerDamageAmount, impactDirection);
            PlayImpactSound();
            DisableCollisions();
            StartCoroutine(EnableCollisions());
        }
    }

    private IEnumerator EnableCollisions()
    {
        yield return _waitForReenableCollisions;

        if (playerHealth.HealthValue <= 0) yield break;

        _areCollisionsDisabled = false;
    }

    private void DisableCollisions() => _areCollisionsDisabled = true;

    private void PlayImpactSound()
    {
        playerAudio.clip = impactSoundEffect;
        playerAudio.Play();
    }

    private void HandleHitTaken(float damageAmount, Vector2 impactDirection)
    {
        OnHitTaken?.Invoke();
        SetHitAnimation();
        PushThePlayerOnCollision(impactDirection);
        playerHealth.TakeDamage(damageAmount);
        cameraShake.ShakeCamera(CAMERA_SHAKE_INTENSITY, CAMERA_SHAKE_DURATION);
    }

    private void SetHitAnimation()
    {
        playerAnimator.SetTrigger("IsHit");
        playerAnimator.SetBool("IsJumping", false);
    }

    private void PushThePlayerOnCollision(Vector2 impactDirection)
    {
        playerRb.velocity = Vector2.zero;
        playerRb.AddForce(impactDirection * HIT_FORCE, ForceMode2D.Impulse);
    }
}
