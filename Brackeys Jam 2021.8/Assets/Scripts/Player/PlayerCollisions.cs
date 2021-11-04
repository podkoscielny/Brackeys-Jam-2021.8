using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] List<string> damageableTags;
    [SerializeField] Animator playerAnimator;

    private GameManager _gameManager;
    private CameraShake _cameraShake;
    private Rigidbody2D _playerRb;
    private Vector2 _impactDirectionRight = new Vector2(1.411f, 0.637f);
    private Vector2 _impactDirectionLeft = new Vector2(-1.411f, 0.637f);
    private const float HIT_FORCE = 6f;

    void Awake() => _playerRb = GetComponent<Rigidbody2D>();

    void Start()
    {
        _gameManager = GameManager.Instance;
        _cameraShake = CameraShake.Instance;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        bool isHit = playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Pigeon_hitTaken");

        if (damageableTags.Contains(collision.tag) && !_gameManager.IsGameOver && !isHit)
        {
            playerAnimator.SetTrigger("IsHit");
            playerAnimator.SetBool("IsJumping", false);

            Vector2 direction = transform.position.x > collision.transform.position.x ? _impactDirectionRight : _impactDirectionLeft;

            float damageAmount = 0;
            float cameraShakeIntensity = 0;
            float cameraShakeDuration = 0;

            IPlayerHitter playerHitter = collision.GetComponent<IPlayerHitter>();

            if (playerHitter != null)
            {
                damageAmount = playerHitter.PlayerDamageAmount;
                cameraShakeIntensity = playerHitter.CameraShakeIntensity;
                cameraShakeDuration = playerHitter.CameraShakeDuration;
            }

            PushThePlayerOnCollision(direction, damageAmount);
            _cameraShake.ShakeCamera(cameraShakeIntensity, cameraShakeDuration);
        }
    }

    private void PushThePlayerOnCollision(Vector3 direction, float damageAmount)
    {
        _playerRb.velocity = Vector2.zero;
        _playerRb.AddForce(direction * HIT_FORCE, ForceMode2D.Impulse); // add specific force to specific objects

        _gameManager.GetHit(damageAmount);
    }
}
