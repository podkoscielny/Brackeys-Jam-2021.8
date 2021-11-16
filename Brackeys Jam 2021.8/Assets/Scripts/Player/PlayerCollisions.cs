using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] List<string> damageableTags;
    [SerializeField] Animator playerAnimator;
    [SerializeField] Rigidbody2D _playerRb;
    [SerializeField] CameraShake cameraShake;

    private GameManager _gameManager;

    private Vector2 _impactDirectionRight = new Vector2(1.411f, 0.637f);
    private Vector2 _impactDirectionLeft = new Vector2(-1.411f, 0.637f);

    private const float HIT_FORCE = 6f;

    void Start() => _gameManager = GameManager.Instance;

    void OnTriggerEnter2D(Collider2D collision)
    {
        bool isHit = playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Pigeon_hitTaken");

        if (!damageableTags.Contains(collision.tag) || _gameManager.IsGameOver || isHit) return;

        HandleHitTaken(collision);
    }

    private void HandleHitTaken(Collider2D hitter)
    {
        SetHitAnimation();

        if (hitter.TryGetComponent(out IPlayerHitter playerHitter))
        {
            Vector2 direction = transform.position.x > hitter.transform.position.x ? _impactDirectionRight : _impactDirectionLeft;
            PushThePlayerOnCollision(direction, playerHitter.PlayerDamageAmount);

            cameraShake.ShakeCamera(playerHitter.CameraShakeIntensity, playerHitter.CameraShakeDuration);
        }
    }

    private void SetHitAnimation()
    {
        playerAnimator.SetTrigger("IsHit");
        playerAnimator.SetBool("IsJumping", false);
    }

    private void PushThePlayerOnCollision(Vector2 direction, float damageAmount)
    {
        _playerRb.velocity = Vector2.zero;
        _playerRb.AddForce(direction * HIT_FORCE, ForceMode2D.Impulse); // add specific force to specific objects

        _gameManager.GetHit(damageAmount);
    }
}
