using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] List<string> damageableTags;

    private GameManager _gameManager;
    private Rigidbody2D _playerRb;
    private Collider2D _playerCollider;
    private const float HIT_FORCE = 10f;

    void Awake()
    {
        _playerRb = GetComponent<Rigidbody2D>();
        _playerCollider = GetComponent<Collider2D>();
    }

    void OnEnable() => GameManager.OnGameOver += DisableCollisionsWithCharacters;

    void OnDisable() => GameManager.OnGameOver -= DisableCollisionsWithCharacters;

    private void Start() => _gameManager = GameManager.Instance;

    void DisableCollisionsWithCharacters() => Physics2D.IgnoreLayerCollision(9, 8);


    void OnTriggerEnter2D(Collider2D collision)
    {
        //if (damageableTags.Contains(collision.tag))
        //{
        //    Vector2 direction = _colliderCenter - collision.bounds.center;
        //    PushThePlayerOnCollision(direction);
        //}
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (damageableTags.Contains(collision.collider.tag))
        {
            Debug.Log(collision.collider.bounds.center);
            Vector2 direction = (_playerCollider.bounds.center - collision.collider.bounds.center).normalized;
            PushThePlayerOnCollision(direction);
        }
    }

    void PushThePlayerOnCollision(Vector3 direction)
    {
        //Debug.Log(direction);
        _playerRb.velocity = Vector2.zero;
        _playerRb.AddForce(direction * HIT_FORCE, ForceMode2D.Impulse); // add specific force to specific objects

        _gameManager.GetHit(0.5f); // add specific amount to specific objects
    }
}
