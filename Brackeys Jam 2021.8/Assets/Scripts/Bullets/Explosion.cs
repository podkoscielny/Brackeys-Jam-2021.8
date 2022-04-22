using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tags = AoOkami.MultipleTagSystem.TagSystem.Tags;

public class Explosion : MonoBehaviour
{
    public static event Action<float, float> OnExplosionSpawaned;

    [Header("Explosion Components")]
    [SerializeField] Animator explosionAnimator;
    [SerializeField] AudioSource explosionAudio;

    [Header("Layer")]
    [SerializeField] LayerMask layerToImpact;

    [Header("Systems")]
    [SerializeField] ObjectPool objectPool;
    [SerializeField] PoopSystem poopSystem;

    private bool _isBeingInitialized = true;
    private float _explosionRange = 1.4f;
    private float _cameraShakeIntensity = 0f;
    private float _cameraShakeDuration = 0f;
    private Vector3 _offset = new Vector3(0, -2f, 0);

    private void OnEnable()
    {
        if (!_isBeingInitialized)
        {
            SetProperties();
            ExplodeCharactersInRange();
            PlaySoundEffect();
            OnExplosionSpawaned?.Invoke(_cameraShakeIntensity, _cameraShakeDuration);
        }
        else
            _isBeingInitialized = false;
    }

    public void MoveExplosionToPool() => objectPool.AddToPool(Tags.Explosion, gameObject); // Invoke after the animation

    private void SetProperties()
    {
        ExplosionType currentExplosion = poopSystem.CurrentPoop.ExplosionType;

        if (currentExplosion != null)
        {
            explosionAnimator.runtimeAnimatorController = currentExplosion.AnimatorController;
            transform.localScale = currentExplosion.Size;
            explosionAudio.clip = currentExplosion.SoundEffect;
            _explosionRange = currentExplosion.Range;
            _cameraShakeIntensity = currentExplosion.CameraShakeIntensity;
            _cameraShakeDuration = currentExplosion.CameraShakeDuration;
        }
    }

    private void PlaySoundEffect() => explosionAudio.Play();

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
