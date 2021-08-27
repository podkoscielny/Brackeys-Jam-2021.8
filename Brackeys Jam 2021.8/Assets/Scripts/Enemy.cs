using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class Enemy : MonoBehaviour
{
    [SerializeField] HumanCharacter[] humanCharacters;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator enemyAnimator;
    [SerializeField] GameObject splashEffect;

    private ObjectPooler _objectPooler;
    private GameManager _gameManager;
    private float _movementSpeed = 4f;

    void OnEnable()
    {
        splashEffect.SetActive(false);
        SetRandomSprite();
    }

    void Start()
    {   
        _objectPooler = ObjectPooler.Instance;
        _gameManager = GameManager.Instance;
    }

    void Update()
    {
        transform.Translate(Vector3.right * _movementSpeed * Time.deltaTime);
    }

    void SetRandomSprite()
    {
        if (humanCharacters.Length < 1) return;

        int characterIndex = Random.Range(0, humanCharacters.Length);

        spriteRenderer.sprite = humanCharacters[characterIndex].sprite;
        enemyAnimator.runtimeAnimatorController = humanCharacters[characterIndex].animatorController;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.Poop))
        {
            splashEffect.SetActive(true);
            _objectPooler.AddToPool(Tags.Poop, collision.gameObject);
            _gameManager.UpdateScore();
        }
    }
}
