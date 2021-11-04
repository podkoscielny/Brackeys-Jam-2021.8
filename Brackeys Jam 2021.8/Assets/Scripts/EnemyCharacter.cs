using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : MonoBehaviour, IPoopHandler, IPlayerHitter
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] GameObject splashEffect;
    [SerializeField] Collider2D enemyCollider;
    [SerializeField] Animator enemyAnimator;
    [SerializeField] string enemyTag;

    public float PlayerDamageAmount { get; private set; } = 0.5f;
    public float CameraShakeIntensity { get; private set; } = 2;
    public float CameraShakeDuration { get; private set; } = 0.2f;

    private IEnemyMovement _enemyMovement;
    private GameManager _gameManager;
    private Vector3 _explodeDirection;
    private bool _canMove = true;
    private bool _isDown = false;
    private bool _hasExploded = false;
    private const float ROTATION_SPEED = 1200f;
    private const float EXPLOSION_SPEED = 14f;

    void Awake() => _enemyMovement = GetComponent<IEnemyMovement>();

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
            Move();
        }
        else if (!_canMove && _hasExploded)
        {
            Explode();
        }
    }

    void Move() => _enemyMovement.Move();

    void Explode()
    {
        transform.Rotate(Vector3.forward * ROTATION_SPEED * Time.deltaTime);
        transform.position += _explodeDirection * EXPLOSION_SPEED * Time.deltaTime;
    }

    public void SetSplashEffect(Vector2 poopPosition)
    {
        splashEffect.transform.position = poopPosition;
        splashEffect.SetActive(true);
        int poopLevel = _gameManager.PoopChargeLevel;
        float splashScaleX = splashEffect.transform.localScale.x + 0.1f * poopLevel;
        float splashScaleY = splashEffect.transform.localScale.y + 0.1f * poopLevel;
        float splashScaleZ = splashEffect.transform.localScale.z;

        splashEffect.transform.localScale = new Vector3(splashScaleX, splashScaleY, splashScaleZ);
    }

    void SetSpriteColor()
    {
        Color color = spriteRenderer.color;
        color.a = 1;

        spriteRenderer.color = color;
    }

    public void HandleExplosion(Vector2 direction)
    {
        enemyAnimator.SetTrigger("Explosion");

        _canMove = false;
        _hasExploded = true;
        _explodeDirection = new Vector3(direction.x, direction.y, 0).normalized;

        _gameManager.UpdateScore();
    }

    public void EnableMoving() => _canMove = true;

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
