using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : MonoBehaviour, IExplosionHandler
{
    public bool CanMove { get; private set; } = true;

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] GameObject splashEffect;
    [SerializeField] Rigidbody2D enemyRb;
    [SerializeField] Collider2D enemyCollider;
    [SerializeField] Animator enemyAnimator;

    private GameManager _gameManager;
    private float _impactForce = 10f;
    private const int LAYER_TO_IGNORE = 8;

    void OnEnable()
    {
        enemyCollider.enabled = true;
        CanMove = true;
        splashEffect.SetActive(false);
        SetSpriteColor();
    }

    void Start()
    {
        _gameManager = GameManager.Instance;
        Physics2D.IgnoreLayerCollision(LAYER_TO_IGNORE, LAYER_TO_IGNORE);
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

    public void HandleExplosion()
    {
        enemyCollider.enabled = false;

        enemyAnimator.SetTrigger("Explosion");

        CanMove = false;

        enemyRb.AddForce(Vector2.up * _impactForce, ForceMode2D.Impulse);

        _gameManager.UpdateScore();
    }

    public void DisableMoving() => CanMove = false;

    public void EnableMoving() => CanMove = true;
}
