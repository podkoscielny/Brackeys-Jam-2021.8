using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tags = TagSystem.Tags;

public class Explosion : MonoBehaviour
{
    public static event Action<float, float> OnExplosionSpawaned;

    [SerializeField] Animator explosionAnimator;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] LayerMask layerToImpact;
    [SerializeField] ObjectPool objectPool;
    [SerializeField] PoopSystem poopSystem;

    private Vector3 _offset = new Vector3(0, -2f, 0);
    private bool _isFullyLoaded = false;
    private float _explosionRange = 1.4f;
    private float _cameraShakeIntensity = 0f;
    private float _cameraShakeDuration = 0f;

    void OnEnable()
    {
        if (_isFullyLoaded)
        {
            SetProperties();
            ExplodeCharactersInRange();
            OnExplosionSpawaned?.Invoke(_cameraShakeIntensity, _cameraShakeDuration);
        }
        else
            _isFullyLoaded = true;
    }

    public void MoveExplosionToPool() => objectPool.AddToPool(Tags.Explosion, gameObject); // Invoke after the animation

    private void SetProperties()
    {
        ExplosionType currentExplosion = poopSystem.CurrentPoop.explosionType;

        if (currentExplosion != null)
        {
            spriteRenderer.sprite = currentExplosion.sprite;
            explosionAnimator.runtimeAnimatorController = currentExplosion.animatorController;
            transform.localScale = currentExplosion.size;
            _explosionRange = currentExplosion.range;
            _cameraShakeIntensity = currentExplosion.cameraShakeIntensity;
            _cameraShakeDuration = currentExplosion.cameraShakeDuration;
        }
    }

    private void ExplodeCharactersInRange()
    {
        Collider2D[] charactersInRange = Physics2D.OverlapCircleAll(transform.position, _explosionRange, layerToImpact);

        foreach (Collider2D character in charactersInRange)
        {
            if (character.TryGetComponent(out IPoopHandler poopHandler))
            {
                Vector2 explodeInDirection = character.transform.position - (transform.position + _offset);
                poopHandler.HandleExplosion(explodeInDirection);
            }
        }
    }
}
