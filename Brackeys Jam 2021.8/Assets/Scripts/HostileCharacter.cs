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

    private ObjectPooler _objectPooler;
    private GameManager _gameManager;
    private Vector2 _randomStopPosition;
    private bool _isFacingRight;
    private bool _isDown = false;
    private bool _hasReachedTarget = false;
    private const float MOVEMENT_SPEED = 4f;
    private const float MIN_POSITION_X = -7f;
    private const float MAX_POSITION_X = 7f;

    void Awake()
    {
        _objectPooler = ObjectPooler.Instance;
        _gameManager = GameManager.Instance;
    }

    void OnEnable()
    {
        _hasReachedTarget = false;
        _isDown = false;
        _isFacingRight = transform.right.x == 1;
        SetRandomStopPosition();
    }

    public void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, _randomStopPosition, MOVEMENT_SPEED * Time.deltaTime);

        if (Vector2.Distance(transform.position, _randomStopPosition) < 0.01f && !_hasReachedTarget)
        {
            _hasReachedTarget = true;
            ShootAnimation();
        }
    }

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
        float randomPositionX = Random.Range(MIN_POSITION_X, MAX_POSITION_X);
        _randomStopPosition = new Vector2(randomPositionX, transform.position.y);

        _hasReachedTarget = false;

        if (_randomStopPosition.x < transform.position.x && _isFacingRight || _randomStopPosition.x > transform.position.x && !_isFacingRight)
        {
            _isFacingRight = !_isFacingRight;
            FlipCharacter();
        }
    }

    void FlipCharacter() => transform.Rotate(0, 180, 0);

    public bool HandlePoopHit()
    {
        if (splashRenderer.bounds.size.y / 2 >= spriteRenderer.bounds.size.y && !_isDown)
        {
            _gameManager.UpdateScore();
            enemyAnimator.SetTrigger("Death");
            _isDown = true;

            return false;
        }

        return true;
    }
}
