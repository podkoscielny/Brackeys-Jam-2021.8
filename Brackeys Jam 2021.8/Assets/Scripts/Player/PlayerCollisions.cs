using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] List<string> damageableTags;
    [SerializeField] Animator playerAnimator;

    private GameManager _gameManager;
    private Rigidbody2D _playerRb;
    private Vector2 _impactDirectionRight = new Vector2(1.411f, 0.637f);
    private Vector2 _impactDirectionLeft = new Vector2(-1.411f, 0.637f);
    private const float HIT_FORCE = 10f;

    void Awake() => _playerRb = GetComponent<Rigidbody2D>();

    private void Start() => _gameManager = GameManager.Instance;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (damageableTags.Contains(collision.tag) && !_gameManager.IsGameOver)
        {
            playerAnimator.SetTrigger("IsHit");

            Vector2 playersPosition = transform.position;

            Vector2 impactDir = transform.position.x > collision.transform.position.x ? _impactDirectionRight : _impactDirectionLeft;
            Vector2 direction = (impactDir - playersPosition).normalized;
            PushThePlayerOnCollision(impactDir);
        }
    }

    void PushThePlayerOnCollision(Vector3 direction)
    {
        _playerRb.velocity = Vector2.zero;
        _playerRb.AddForce(direction * HIT_FORCE, ForceMode2D.Impulse); // add specific force to specific objects

        _gameManager.GetHit(0.5f); // add specific amount to specific objects
    }
}
