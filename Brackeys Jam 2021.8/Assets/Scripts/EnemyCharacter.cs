using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class EnemyCharacter : MonoBehaviour, IExplosionHandler
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] GameObject splashEffect;
    [SerializeField] Collider2D enemyCollider;
    [SerializeField] Animator enemyAnimator;
    [SerializeField] string enemyTag;

    private IEnemyController _enemyController;
    private GameManager _gameManager;
    private ObjectPooler _objectPooler;
    private Vector3 _explodeDirection;
    private bool _canMove = true;
    private bool _hasExploded = false;
    private const float ROTATION_SPEED = 1200f;
    private const float EXPLOSION_SPEED = 14f;

    void Awake()
    {
        _gameManager = GameManager.Instance;
        _objectPooler = ObjectPooler.Instance;
        _enemyController = GetComponent<IEnemyController>();
    }

    void OnEnable()
    {
        _canMove = true;
        _hasExploded = false;
        splashEffect.SetActive(false);
        SetSpriteColor();
    }

    void Update()
    {
        if (_canMove)
        {
            Move();
        }
        else if(!_canMove && _hasExploded)
        {
            Explode();
        }
    }

    void Move() => _enemyController.Move();

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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.Poop) && _gameManager.PoopChargeLevel < 4)
        {
            _objectPooler.AddToPool(Tags.Poop, collision.gameObject);
            SetSplashEffect(collision.transform.position);

            _canMove = _enemyController.HandlePoopHit();
        }
    }
}
