using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AoOkami.MultipleTagSystem;
using Tags = AoOkami.MultipleTagSystem.TagSystem.Tags;

public class Poop : MonoBehaviour
{
    [SerializeField] Rigidbody2D poopRb;
    [SerializeField] Animator poopAnimator;
    [SerializeField] ObjectPool objectPool;
    [SerializeField] PoopSystem poopSystem;

    private Transform _spawnPoop;
    private bool _isFalling = false;
    private bool _isFullyLoaded = false;
    private Vector2 _explosionOffset = new Vector2(0f, 0.75f);

    private const int GRAVITY_SCALE = 3;

    private void Start() => _spawnPoop = TagSystem.FindGameObjectWithTag(Tags.PoopSpawn).transform;

    private void OnEnable()
    {
        _isFalling = false;
        poopRb.gravityScale = 0;

        if (_isFullyLoaded)
        {
            poopAnimator.runtimeAnimatorController = poopSystem.CurrentPoop.PoopAnimator;
        }
        else
        {
            _isFullyLoaded = true;
        }
    }

    private void LateUpdate()
    {
        if (!_isFalling) transform.position = _spawnPoop.position;
    }

    public void SetGravity() // Invoke after animation
    {
        poopRb.gravityScale = GRAVITY_SCALE;
        _isFalling = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.HasTag(Tags.HittableByPoop)) return;

        if (poopSystem.CurrentPoop.IsExplosive)
        {
            SpawnExplosionEffect();
        }
        else
        {
            collision.GetComponent<IPoopHandler>()?.HandlePoopHit(transform.position);
        }

        gameObject.SetActive(false);
    }

    private void SpawnExplosionEffect()
    {
        Vector2 explosionPosition = (Vector2)transform.position + _explosionOffset;
        objectPool.GetFromPool(Tags.Explosion, explosionPosition);
    }
}
