using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class RedExplosion : MonoBehaviour
{
    [SerializeField] Animator explosionAnimator;
    [SerializeField] LayerMask layerToImpact;

    private ObjectPooler _objectPooler;
    private float _explosionRange = 1.4f;
    private float _impactForce = 15f;
    private float _impactTorque = 80f;

    void OnEnable()
    {
        Collider2D[] charactersInRange = Physics2D.OverlapCircleAll(transform.position, _explosionRange, layerToImpact);

        foreach (Collider2D character in charactersInRange)
        {
            Rigidbody2D enemyRb = character.GetComponent<Rigidbody2D>();
            Animator enemyAnimator = character.GetComponent<Animator>();
            Collider2D enemyCollider = character.GetComponent<Collider2D>();
            enemyCollider.enabled = false;

            enemyAnimator.SetTrigger("Explosion");

            float toruqeDirection = enemyRb.position.x < transform.position.x ? -_impactTorque : _impactTorque;

            character.GetComponent<Enemy>()?.DisableMoving();
            character.GetComponent<HostileCharacter>()?.DisableMoving();

            enemyRb.AddForce(Vector2.up * _impactForce, ForceMode2D.Impulse);
            enemyRb.AddTorque(toruqeDirection);

            GameManager.Instance.UpdateScore();
        }
    }

    void Start()
    {
        _objectPooler = ObjectPooler.Instance;
    }

    void MoveExplosionToPool()
    {
        _objectPooler.AddToPool(Tags.ExplosionRed, gameObject);
    }
}
