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
    private CameraShake _cameraShake;
    private Vector3 _offset = new Vector3(0, -2f, 0);
    private bool _isFullyLoaded = false;
    private float _explosionRange = 1.4f;
    private float _cameraShakeIntensity = 0f;
    private float _cameraShakeDuration = 0f;

    void Awake() => _cameraShake = Camera.main.GetComponent<CameraShake>();

    void OnEnable()
    {
        if (_isFullyLoaded)
        {
            SetProperties();
            ExplodeCharactersInRange();
        }
        else
        {
            _gameManager = GameManager.Instance;
            _objectPooler = ObjectPooler.Instance;

            _isFullyLoaded = true;
        }
    }

    private void SetProperties()
    {
        ExplosionType currentExplosion = _gameManager.CurrentPoop.explosionType;

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
            Vector2 explodeInDirection = character.transform.position - (transform.position + _offset);
            character.GetComponent<IPoopHandler>()?.HandleExplosion(explodeInDirection);
        }

        _cameraShake.ShakeCamera(_cameraShakeIntensity, _cameraShakeDuration);
    }

    public void MoveExplosionToPool() => _objectPooler.AddToPool(Tags.Explosion, gameObject); // Invoke after the animation
}
