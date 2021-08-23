using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _movementSpeed = 4f;

    void Update()
    {
        transform.Translate(Vector3.right * _movementSpeed * Time.deltaTime);
    }
}
