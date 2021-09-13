using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionHandler : MonoBehaviour
{
    [SerializeField] Rigidbody2D enemyRb;
    [SerializeField] Animator enemyAnimator;
    [SerializeField] Collider2D enemyCollider;

    private GameManager _gameManager;
    private IMovementDisabler _movementController;
    private float _impactForce = 10f;

    void Start()
    {
        _gameManager = GameManager.Instance;
        _movementController = GetComponent<IMovementDisabler>();
    }

    public void HandleExplosion()
    {
        enemyCollider.enabled = false;

        enemyAnimator.SetTrigger("Explosion");

        _movementController.DisableMoving();

        enemyRb.AddForce(Vector2.up * _impactForce, ForceMode2D.Impulse);

        _gameManager.UpdateScore();
    }
}
