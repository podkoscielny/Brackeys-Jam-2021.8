using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class PlayerCollisions : MonoBehaviour
{
    private Rigidbody2D _playerRb;
    private float _hitForce = 20f;

    void Start() => _playerRb = GetComponent<Rigidbody2D>();

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Tags.Bullet))
        {
            Vector2 direction = transform.position - collision.transform.position;
            _playerRb.AddForce(direction * _hitForce, ForceMode2D.Impulse);

            GameManager.Instance.GameOver();
        }
    }
}
