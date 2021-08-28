using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
