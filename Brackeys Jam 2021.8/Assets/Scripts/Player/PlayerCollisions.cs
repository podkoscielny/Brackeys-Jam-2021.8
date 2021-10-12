using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] List<string> damageableTags;

    private GameManager _gameManager;
    private Rigidbody2D _playerRb;
    private const float HIT_FORCE = 20f;

    void Awake() => _playerRb = GetComponent<Rigidbody2D>();

    void OnEnable() => GameManager.OnGameOver += DisableCollisionsWithCharacters;

    void OnDisable() => GameManager.OnGameOver -= DisableCollisionsWithCharacters;

    private void Start() => _gameManager = GameManager.Instance;


    void DisableCollisionsWithCharacters() => Physics2D.IgnoreLayerCollision(9, 8);


    void OnTriggerEnter2D(Collider2D collision) => PushThePlayerOnCollision(collision.tag, collision.transform.position);

    void OnCollisionEnter2D(Collision2D collision) => PushThePlayerOnCollision(collision.collider.tag, collision.transform.position);


    void PushThePlayerOnCollision(string tag, Vector3 position)
    {
        if (damageableTags.Contains(tag))
        {
            Vector2 direction = transform.position - position;
            _playerRb.velocity = Vector2.zero;
            _playerRb.AddForce(direction * HIT_FORCE, ForceMode2D.Impulse); // add specific force to specific objects

            _gameManager.GetHit(0.5f); // add specific amount to specific objects
        }
    }
}
