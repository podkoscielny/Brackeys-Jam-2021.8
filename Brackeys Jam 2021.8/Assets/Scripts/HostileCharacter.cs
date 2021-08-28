using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class HostileCharacter : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Transform firePoint;
    [SerializeField] Animator enemyAnimator;
    [SerializeField] GameObject splashEffect;
    [SerializeField] SpriteRenderer splashRenderer;
    [SerializeField] LayerMask layerToIgnoreCollision;

    private ObjectPooler _objectPooler;
    private GameManager _gameManager;
    private float _movementSpeed = 4f;
    private float _minPositionX = -7f;
    private float _maxPositionX = 7f;
    private bool _hasReachedTarget = false;
    private bool _isFacingRight = true;
    private Vector2 _randomStopPosition;
    private int layerIgnore = 8;

    void OnEnable()
    {
        splashEffect.SetActive(false);
    }

    void OnDisable() => CancelInvoke(nameof(Shoot));

    void Start()
    {
        _objectPooler = ObjectPooler.Instance;
        _gameManager = GameManager.Instance;

        float randomPositionX = Random.Range(_minPositionX, _maxPositionX);
        _randomStopPosition = new Vector2(randomPositionX, transform.position.y);

        Physics2D.IgnoreLayerCollision(layerIgnore, layerIgnore);
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, _randomStopPosition, _movementSpeed * Time.deltaTime);

        if (_randomStopPosition.x < transform.position.x && _isFacingRight || _randomStopPosition.x > transform.position.x && !_isFacingRight)
        {
            _isFacingRight = !_isFacingRight;
            FlipCharacter();
        }


        if (Vector2.Distance(transform.position, _randomStopPosition) < 0.01f && !_hasReachedTarget)
        {
            _hasReachedTarget = true;
            InvokeRepeating(nameof(ShootAnimation), 0f, 2f);
        }
    }

    void FlipCharacter() => transform.Rotate(0, 180, 0);

    void ShootAnimation() => enemyAnimator.SetTrigger("Shoot");

    void Shoot()
    {
        GameObject bullet = _objectPooler.GetFromPoolInActive(Tags.Bullet);
        bullet.transform.position = firePoint.position;
        bullet.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.Poop))
        {
            _objectPooler.AddToPool(Tags.Poop, collision.gameObject);
            _gameManager.UpdateScore();

            SetSplashEffect(collision.transform.position);
        }
    }

    void SetSplashEffect(Vector2 poopPosition)
    {
        splashEffect.transform.position = poopPosition;
        splashEffect.SetActive(true);
        int poopLevel = _gameManager.PoopChargeLevel;
        float splashScaleX = splashEffect.transform.localScale.x + 0.1f * poopLevel;
        float splashScaleY = splashEffect.transform.localScale.y + 0.1f * poopLevel;
        float splashScaleZ = splashEffect.transform.localScale.z;

        splashEffect.transform.localScale = new Vector3(splashScaleX, splashScaleY, splashScaleZ);

        if (splashRenderer.bounds.size.y / 2 >= spriteRenderer.bounds.size.y)
        {
            _objectPooler.AddToPool(Tags.Hostile, gameObject);
        }
    }
}
