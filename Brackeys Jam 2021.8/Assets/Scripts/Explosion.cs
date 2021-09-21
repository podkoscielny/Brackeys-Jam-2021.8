using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class Explosion : MonoBehaviour
{
    [SerializeField] Animator explosionAnimator;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] LayerMask layerToImpact;

    private GameManager _gameManager;
    private ObjectPooler _objectPooler;
    private Vector3 _offset = new Vector3(0, -2f, 0);
    private float _explosionRange = 1.4f;

    void Awake()
    {
        _gameManager = GameManager.Instance;
        _objectPooler = ObjectPooler.Instance;
    }

    void OnEnable()
    {
        SetProperties();
        ExplodeCharactersInRange();
    }

    void SetProperties()
    {
        if (_gameManager.ExplosionEffect != null)
        {
            spriteRenderer.sprite = _gameManager.ExplosionEffect.sprite;
            explosionAnimator.runtimeAnimatorController = _gameManager.ExplosionEffect.animatorController;
            transform.localScale = _gameManager.ExplosionEffect.size;
        }
    }

    void ExplodeCharactersInRange()
    {
        Collider2D[] charactersInRange = Physics2D.OverlapCircleAll(transform.position, _explosionRange, layerToImpact);

        foreach (Collider2D character in charactersInRange)
        {
            Vector2 explodeInDirection = character.transform.position - (transform.position + _offset); 
            character.GetComponent<IExplosionHandler>()?.HandleExplosion(explodeInDirection);
        }
    }

    public void MoveExplosionToPool() => _objectPooler.AddToPool(Tags.Explosion, gameObject); // Invoke after the animation
}
