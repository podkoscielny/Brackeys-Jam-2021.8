using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class HostileCharacter : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Collider2D enemyCollider;
    [SerializeField] Rigidbody2D enemyRb;
    [SerializeField] Transform gun;
    [SerializeField] Transform firePoint;
    [SerializeField] Animator enemyAnimator;
    [SerializeField] SpriteRenderer splashRenderer;
    [SerializeField] EnemyCharacter baseFunctionalityController;

    private ObjectPooler _objectPooler;
    private GameManager _gameManager;
    private float _movementSpeed = 4f;
    private float _minPositionX = -7f;
    private float _maxPositionX = 7f;
    private bool _hasReachedTarget = false;
    private bool _isFacingRight = true;
    private Vector2 _randomStopPosition;

    void OnEnable()
    {
        _hasReachedTarget = false;
    }

    void OnDisable()
    {
        _hasReachedTarget = false;
        CancelInvoke(nameof(Shoot));
    }

    void Start()
    {
        _objectPooler = ObjectPooler.Instance;
        _gameManager = GameManager.Instance;

        SetRandomStopPosition();
    }

    void Update()
    {
        if (baseFunctionalityController.CanMove)
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
            ShootAnimation();
        }
    }

    void FlipCharacter() => transform.Rotate(0, 180, 0);

    void ShootAnimation() => enemyAnimator.SetTrigger("Shoot");

    void Shoot()
    {
        GameObject bullet = _objectPooler.GetFromPoolInActive(Tags.Bullet);

        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = gun.rotation;
        bullet.SetActive(true);
    }

    void SetRandomStopPosition()
    {
        float randomPositionX = Random.Range(_minPositionX, _maxPositionX);
        _randomStopPosition = new Vector2(randomPositionX, transform.position.y);

        _hasReachedTarget = false;
    }

    public void MoveEnemyToPool() // Invoke in animation
    {
        enemyRb.velocity = new Vector2(0f, 0f);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        _objectPooler.AddToPool(Tags.Hostile, gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.Poop) && _gameManager.PoopChargeLevel < 4)
        {
            _objectPooler.AddToPool(Tags.Poop, collision.gameObject);
            baseFunctionalityController.SetSplashEffect(collision.transform.position);

            if (splashRenderer.bounds.size.y / 2 >= spriteRenderer.bounds.size.y)
            {
                baseFunctionalityController.DisableMoving();
                _gameManager.UpdateScore();
                enemyAnimator.SetTrigger("Death");
                enemyCollider.enabled = false;
            }
        }
    }
}
