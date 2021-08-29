using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D bulletRb;

    private float _speed = 10f;

    private void OnEnable()
    {
        GoTowardsTarget();
    }

    public void SetBulletRotation(Quaternion rotation) => transform.rotation = rotation;

    void GoTowardsTarget()
    {
        bulletRb.AddForce(transform.right * _speed, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.Player))
        {
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            Vector2 direction = collision.transform.position - transform.position;
            playerRb.AddForce(direction * 20f, ForceMode2D.Impulse);

            GameManager.Instance.GameOver();
        }
    }
}
