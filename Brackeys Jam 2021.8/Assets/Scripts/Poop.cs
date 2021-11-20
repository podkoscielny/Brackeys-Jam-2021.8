using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;
using Labels = Label.Labels;

public class Poop : MonoBehaviour
{
    [SerializeField] Rigidbody2D poopRb;
    [SerializeField] Animator poopAnimator;

    private ObjectPooler _objectPooler;
    private GameManager _gameManager;
    private Transform _spawnPoop;

    private bool _isFalling = false;
    private bool _isFullyLoaded = false;
    private Vector2 _explosionOffset = new Vector2(0f, 0.75f);

    private const int GRAVITY_SCALE = 3;

    void Start() => _spawnPoop = Label.FindGameObjectWithLabel(Labels.PoopSpawn).transform;

    void OnEnable()
    {
        _isFalling = false;
        poopRb.gravityScale = 0;

        if (_isFullyLoaded)
        {
            poopAnimator.runtimeAnimatorController = _gameManager.CurrentPoop.poopAnimator;
        }
        else
        {
            _isFullyLoaded = true;
            _gameManager = GameManager.Instance;
            _objectPooler = ObjectPooler.Instance;
        }
    }

    void LateUpdate()
    {
        if (!_isFalling)
        {
            transform.position = _spawnPoop.position;
        }
    }

    public void SetGravity() // Invoke after animation
    {
        poopRb.gravityScale = GRAVITY_SCALE;
        _isFalling = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.HasLabel(Labels.HittableByPoop)) return;

        if (_gameManager.CurrentPoop.isExplosive)
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
        GameObject explosion = _objectPooler.GetFromPoolInActive(Tags.Explosion);
        explosion.transform.position = (Vector2)transform.position + _explosionOffset;
        explosion.SetActive(true);
    }
}
