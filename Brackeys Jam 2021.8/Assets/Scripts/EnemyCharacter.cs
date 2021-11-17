using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : MonoBehaviour, IPoopHandler, IPlayerHitter
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] GameObject splashEffect;
    [SerializeField] Animator enemyAnimator;

    public float PlayerDamageAmount { get; private set; } = 0.5f;

    private GameManager _gameManager;
    private Transform _splashTransform;
    private IEnemyMovement _enemyMovement;
    private Vector3 _explodeDirection;

    private bool _canMove = true;
    private bool _isDown = false;
    private bool _hasExploded = false;

    private const float ROTATION_SPEED = 1200f;
    private const float EXPLOSION_SPEED = 14f;

    void Awake()
    {
        _enemyMovement = GetComponent<IEnemyMovement>();
        _splashTransform = splashEffect.transform;
    }

    void OnEnable()
    {
        _canMove = true;
        _isDown = false;
        _hasExploded = false;
        splashEffect.SetActive(false);
        SetSpriteColor();
    }

    void Start() => _gameManager = GameManager.Instance;

    void Update()
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

    private void Explode()
    {
        transform.Rotate(Vector3.forward * ROTATION_SPEED * Time.deltaTime);
        transform.position += _explodeDirection * EXPLOSION_SPEED * Time.deltaTime;
    }
    
    private void SetSpriteColor()
    {
        Color color = spriteRenderer.color;
        color.a = 1;

        spriteRenderer.color = color;
    }

    private void SetSplashEffect(Vector2 poopPosition)
    {
        int poopLevel = _gameManager.PoopChargeLevel;

        _splashTransform.position = poopPosition;

        float splashScaleX = _splashTransform.localScale.x + 0.1f * poopLevel;
        float splashScaleY = _splashTransform.localScale.y + 0.1f * poopLevel;
        float splashScaleZ = _splashTransform.localScale.z;

        _splashTransform.localScale = new Vector3(splashScaleX, splashScaleY, splashScaleZ);

        splashEffect.SetActive(true);
    }

    public void EnableMoving() => _canMove = true;

    public void HandleExplosion(Vector2 direction)
    {
        enemyAnimator.SetTrigger("Explosion");

        _canMove = false;
        _hasExploded = true;
        _explodeDirection = new Vector3(direction.x, direction.y, 0).normalized;

        _gameManager.UpdateScore();
    }

    public void HandlePoopHit(Vector2 splashPosition)
    {
        if (!_isDown)
        {
            SetSplashEffect(splashPosition);

            _gameManager.UpdateScore();
            enemyAnimator.SetTrigger("IsDown");

            _isDown = true;
            _canMove = false;
        }
    }
}
