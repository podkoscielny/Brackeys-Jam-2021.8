using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] HumanCharacter[] humanCharacters;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator enemyAnimator;

    private ObjectPooler _objectPooler;
    private GameManager _gameManager;
    private float _movementSpeed = 4f;

    void OnEnable() => SetRandomSprite();

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

        int characterIndex = UnityEngine.Random.Range(0, humanCharacters.Length);

        spriteRenderer.sprite = humanCharacters[characterIndex].sprite;
        enemyAnimator.runtimeAnimatorController = humanCharacters[characterIndex].animatorController;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Poop"))
        {
            _objectPooler.AddToPool("Poop", collision.gameObject);
            _gameManager.UpdateScore();
        }
    }
}
