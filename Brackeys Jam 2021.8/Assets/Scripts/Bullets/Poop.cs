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

    private static Rigidbody2D _playerRb;

    private Transform _spawnPoop;
    private bool _isFalling = false;
    private Vector2 _explosionOffset = new Vector2(0f, 0.75f);

    private const int GRAVITY_SCALE = 3;

    private void Start() => FindSceneObjects();

    private void OnEnable()
    {
        ResetFallingState();
        SetPoopAnimator();
    }

    private void LateUpdate()
    {
        if (!_isFalling) transform.position = _spawnPoop.position;
    }

    public void SetGravity() // Invoke after animation
    {
        poopRb.gravityScale = GRAVITY_SCALE;
        poopRb.velocity = _playerRb.velocity * 0.3f;
        _isFalling = true;
    }

    private void ResetFallingState()
    {
        _isFalling = false;
        poopRb.gravityScale = 0;
    }

    private void SetPoopAnimator() => poopAnimator.runtimeAnimatorController = poopSystem.CurrentPoop.PoopAnimator;

    private void FindSceneObjects()
    {
        _spawnPoop = TagSystem.FindGameObjectWithTag(Tags.PoopSpawn).transform;

        if (_playerRb == null)
            _playerRb = TagSystem.FindGameObjectWithTag(Tags.Player).GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
