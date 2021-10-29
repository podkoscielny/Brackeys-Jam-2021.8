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
    private bool _isFullyLoaded = false;
    private Vector3 _offset = new Vector3(0, -2f, 0);
    private float _explosionRange = 1.4f;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _explosionRange);
    }

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

    void SetProperties()
    {
        if (_gameManager.CurrentPoop.explosionType != null)
        {
            spriteRenderer.sprite = _gameManager.CurrentPoop.explosionType.sprite;
            explosionAnimator.runtimeAnimatorController = _gameManager.CurrentPoop.explosionType.animatorController;
            transform.localScale = _gameManager.CurrentPoop.explosionType.size;
            _explosionRange = _gameManager.CurrentPoop.explosionType.range;
        }
    }

    void ExplodeCharactersInRange()
    {
        Collider2D[] charactersInRange = Physics2D.OverlapCircleAll(transform.position, _explosionRange, layerToImpact);

        foreach (Collider2D character in charactersInRange)
        {
            Vector2 explodeInDirection = character.transform.position - (transform.position + _offset);
            character.GetComponent<IPoopHandler>()?.HandleExplosion(explodeInDirection);
        }
    }

    public void MoveExplosionToPool() => _objectPooler.AddToPool(Tags.Explosion, gameObject); // Invoke after the animation
}
