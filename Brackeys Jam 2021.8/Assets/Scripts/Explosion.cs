using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class Explosion : MonoBehaviour
{
    [SerializeField] Animator explosionAnimator;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] LayerMask layerToImpact;

    private ObjectPooler _objectPooler;
    private float _explosionRange = 1.4f;
    private float _impactForce = 10f;

    void OnEnable()
    {
        SetProperties();
        ExplodeCharactersInRange();
    }

    void Start() => _objectPooler = ObjectPooler.Instance;

    void SetProperties()
    {
        if (GameManager.Instance.ExplosionEffect != null)
        {
            spriteRenderer.sprite = GameManager.Instance.ExplosionEffect.sprite;
            explosionAnimator.runtimeAnimatorController = GameManager.Instance.ExplosionEffect.animatorController;
            transform.localScale = GameManager.Instance.ExplosionEffect.size;
        }
    }

    void ExplodeCharactersInRange()
    {
        Collider2D[] charactersInRange = Physics2D.OverlapCircleAll(transform.position, _explosionRange, layerToImpact);

        foreach (Collider2D character in charactersInRange)
        {
            Rigidbody2D enemyRb = character.GetComponent<Rigidbody2D>();
            Animator enemyAnimator = character.GetComponent<Animator>();
            Collider2D enemyCollider = character.GetComponent<Collider2D>();
            enemyCollider.enabled = false;

            enemyAnimator.SetTrigger("Explosion");

            character.GetComponent<Enemy>()?.DisableMoving();
            character.GetComponent<HostileCharacter>()?.DisableMoving();

            enemyRb.AddForce(Vector2.up * _impactForce, ForceMode2D.Impulse);

            GameManager.Instance.UpdateScore();
        }
    }

    void MoveExplosionToPool() => _objectPooler.AddToPool(Tags.Explosion, gameObject); // Invoke after the animation
}
