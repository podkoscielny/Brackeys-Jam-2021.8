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
            character.GetComponent<IExplosionHandler>().HandleExplosion();
        }
    }

    public void MoveExplosionToPool() => _objectPooler.AddToPool(Tags.Explosion, gameObject); // Invoke after the animation
}
