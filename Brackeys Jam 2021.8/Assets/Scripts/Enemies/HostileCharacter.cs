using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tags = AoOkami.MultipleTagSystem.TagSystem.Tags;

public class HostileCharacter : MonoBehaviour, IEnemyMovement
{
    [Header("Characters Components")]
    [SerializeField] Transform gun;
    [SerializeField] Transform firePoint;
    [SerializeField] Animator enemyAnimator;
    [SerializeField] SpriteRenderer gunRenderer;
    [SerializeField] SpriteRenderer spriteRenderer;

    [Header("Systems")]
    [SerializeField] ObjectPool objectPool;
    [SerializeField] ChaosStarsSystem chaosStarsSystem;

    private Vector3 _randomStopPosition;
    private bool _isFacingRight;
    private bool _hasReachedTarget = false;
    private bool _hasGameStarted = false;
    private float _movementSpeed = 40f;

    private const float MIN_POSITION_X = -7f;
    private const float MAX_POSITION_X = 7f;

    private void OnEnable()
    {
        if (!_hasGameStarted)
        {
            _hasGameStarted = true;
            return;
        }

        _hasReachedTarget = false;
        _isFacingRight = transform.right.x == 1;

        SetRandomEnemy();
        SetRandomStopPosition();
    }

    public void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _randomStopPosition, _movementSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _randomStopPosition) < 0.01f && !_hasReachedTarget)
        {
            _hasReachedTarget = true;
            ShootAnimation();
        }
    }

    public void Shoot() => objectPool.GetFromPool(Tags.Bullet, firePoint.position, gun.rotation);

    public void MoveEnemyToPool() => gameObject.SetActive(false);

    private void SetRandomEnemy()
    {
        HostileEnemy enemy = (HostileEnemy)chaosStarsSystem.CurrentChaosStar.LastEnemyPicked;

        spriteRenderer.sprite = enemy.CharacterSprite;
        gunRenderer.sprite = enemy.Gun.GunSprite;
        gun.localScale = enemy.LocalScale;
        firePoint.localPosition = enemy.Gun.FirePoint;
        _movementSpeed = enemy.MovementSpeed;
        enemyAnimator.runtimeAnimatorController = enemy.AnimatorController;
    }

    private void SetRandomStopPosition()
    {
        float randomPositionX = Random.Range(MIN_POSITION_X, MAX_POSITION_X);

        _randomStopPosition = new Vector3(randomPositionX, transform.position.y, transform.position.y);

        _hasReachedTarget = false;

        if (_randomStopPosition.x < transform.position.x && _isFacingRight || _randomStopPosition.x > transform.position.x && !_isFacingRight)
        {
            _isFacingRight = !_isFacingRight;
            FlipCharacter();
        }
    }

    private void ShootAnimation() => enemyAnimator.SetTrigger("Shoot");

    private void FlipCharacter() => transform.Rotate(0, 180, 0);
}
