using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class Poop : MonoBehaviour
{
    [SerializeField] Rigidbody2D poopRb;
    [SerializeField] Animator poopAnimator;
    [SerializeField] List<string> hitableTags;

    private ObjectPooler _objectPooler;
    private GameManager _gameManager;
    private GameObject _spawnPoop;
    private List<string> collidables;
    private Vector2 _explosionOffset = new Vector2(0f, 0.75f);
    private bool _isFalling = false;
    private bool _isFullyLoaded = false;
    private const int GRAVITY_SCALE = 3;

    void Awake()
    {
        _spawnPoop = GameObject.FindGameObjectWithTag("SpawnPoop");
        collidables = new List<string>();
    }

    void OnEnable()
    {
        _isFalling = false;
        poopRb.gravityScale = 0;

        if (_isFullyLoaded)
        {
            poopAnimator.runtimeAnimatorController = _gameManager.CurrentPoop.poopAnimator;
            SetCollidableTags();
        }
        else
        {
            _isFullyLoaded = true;
            _gameManager = GameManager.Instance;
            _objectPooler = ObjectPooler.Instance;
        }

    }

    void LateUpdate()
    {
        if (!_isFalling)
        {
            transform.position = _spawnPoop.transform.position;
        }
    }

    public void SetGravity() // Invoke after animation
    {
        poopRb.gravityScale = GRAVITY_SCALE;
        _isFalling = true;
    }

    private void SetCollidableTags()
    {
        collidables.Clear();

        for (int i = 0; i < hitableTags.Count; i++)
        {
            collidables.Add(hitableTags[i]);
        }

        if (_gameManager.CurrentPoop.isExplosive) collidables.Add(Tags.Ground);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collidables.Contains(collision.tag))
        {
            if (_gameManager.CurrentPoop.isExplosive)
            {
                GameObject explosion = _objectPooler.GetFromPoolInActive(Tags.Explosion);
                explosion.transform.position = (Vector2)transform.position + _explosionOffset;
                explosion.SetActive(true);
            }
            else
            {
                collision.GetComponent<IPoopHandler>()?.HandlePoopHit(transform.position);
            }

            gameObject.SetActive(false);
        }
    }
}
