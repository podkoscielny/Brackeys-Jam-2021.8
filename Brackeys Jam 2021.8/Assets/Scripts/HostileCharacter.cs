using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class HostileCharacter : MonoBehaviour, IEnemyController
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Collider2D enemyCollider;
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
    private bool _isDown = false;
    private Vector2 _randomStopPosition;

    void Awake()
    {
        _objectPooler = ObjectPooler.Instance;
        _gameManager = GameManager.Instance;
    }

    void OnEnable()
    {
        _hasReachedTarget = false;
        _isDown = false;
    }

    void Start() => SetRandomStopPosition();

    public void Move()
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

    public void Shoot() // Invoke in Shoot animation
    {
        GameObject bullet = _objectPooler.GetFromPoolInActive(Tags.Bullet);

        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = gun.rotation;
        bullet.SetActive(true);
    }

    public void MoveEnemyToPool() => _objectPooler.AddToPool(Tags.Hostile, gameObject);

    void SetRandomStopPosition()
    {
        float randomPositionX = Random.Range(_minPositionX, _maxPositionX);
        _randomStopPosition = new Vector2(randomPositionX, transform.position.y);

        _hasReachedTarget = false;
    }

    public void HandlePoopHit()
    {
        if (splashRenderer.bounds.size.y / 2 >= spriteRenderer.bounds.size.y && !_isDown)
        {
            baseFunctionalityController.DisableMoving();
            _gameManager.UpdateScore();
            enemyAnimator.SetTrigger("Death");
            _isDown = true;
        }
    }
}
