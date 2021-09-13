using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionHandler : MonoBehaviour
{
    [SerializeField] Rigidbody2D enemyRb;
    [SerializeField] Animator enemyAnimator;
    [SerializeField] Collider2D enemyCollider;

    private GameManager _gameManager;
    private float _impactForce = 10f;

    void Start() => _gameManager = GameManager.Instance;

    public void HandleExplosion()
    {
        enemyCollider.enabled = false;

        enemyAnimator.SetTrigger("Explosion");

        GetComponent<Enemy>()?.DisableMoving();
        GetComponent<HostileCharacter>()?.DisableMoving();

        enemyRb.AddForce(Vector2.up * _impactForce, ForceMode2D.Impulse);

        _gameManager.UpdateScore();
    }
}
