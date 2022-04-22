using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : MonoBehaviour, IPoopHandler, IPlayerHitter
{
    [Header("Enemy Components")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator enemyAnimator;
    [SerializeField] GameObject splashEffect;

    [Header("Systems")]
    [SerializeField] PoopSystem poopSystem;
    [SerializeField] Score score;

    public float PlayerDamageAmount { get; private set; } = 0.5f;

    private Transform _splashTransform;
    private IEnemyMovement _enemyMovement;
    private Vector3 _explodeDirection;

    private bool _canMove = true;
    private bool _isDown = false;
    private bool _hasExploded = false;

    private const float SPLASH_SIZE_FACTOR = 0.05f;
    private const float ROTATION_SPEED = 1200f;
    private const float EXPLOSION_SPEED = 14f;

    private void Awake() => CacheComponents();

    private void OnEnable() => ResetEnemyState();

    private void Update()
    {
        if (_canMove)
        {
            _enemyMovement.Move();
        }
        else if (!_canMove && _hasExploded)
        {
            Explode();
        }
    }

    public void EnableMoving() => _canMove = true; //Invoke in animation event

    public void HandleExplosion(Vector2 direction)
    {
        if (_isDown) return;

        _isDown = true;
        _canMove = false;
        _hasExploded = true;
        _explodeDirection = new Vector3(direction.x, direction.y, 0).normalized;

        enemyAnimator.SetTrigger("Explosion");
        score.AddPoints(poopSystem.CurrentPoop.PointsWorth);
    }

    public void HandlePoopHit(Vector2 splashPosition)
    {
        if (_isDown) return;

        _isDown = true;
        _canMove = false;

        SetSplashEffect(splashPosition);
        enemyAnimator.SetTrigger("IsDown");
        score.AddPoints(poopSystem.CurrentPoop.PointsWorth);
    }

    private void Explode()
    {
        transform.Rotate(ROTATION_SPEED * Time.deltaTime * Vector3.forward);
        transform.position += EXPLOSION_SPEED * Time.deltaTime * _explodeDirection;
    }

    private void ResetEnemyState()
    {
        _canMove = true;
        _isDown = false;
        _hasExploded = false;
        splashEffect.SetActive(false);
        SetSpriteColor();
    }

    private void SetSpriteColor()
    {
        Color color = spriteRenderer.color;
        color.a = 1;

        spriteRenderer.color = color;
    }

    private void SetSplashEffect(Vector2 poopPosition)
    {
        int poopLevel = poopSystem.PoopChargeLevel;

        _splashTransform.position = poopPosition;

        float splashSizeFactor = SPLASH_SIZE_FACTOR * poopLevel;

        float splashScaleX = _splashTransform.localScale.x + splashSizeFactor;
        float splashScaleY = _splashTransform.localScale.y + splashSizeFactor;
        float splashScaleZ = _splashTransform.localScale.z;

        _splashTransform.localScale = new Vector3(splashScaleX, splashScaleY, splashScaleZ);
        
        splashEffect.SetActive(true);
    }

    private void CacheComponents()
    {
        _enemyMovement = GetComponent<IEnemyMovement>();
        _splashTransform = splashEffect.transform;
    }
}
