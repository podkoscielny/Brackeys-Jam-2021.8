using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private ObjectPooler _objectPooler;

    void Start() => _objectPooler = ObjectPooler.Instance;

    void OnTriggerEnter2D(Collider2D collision) => _objectPooler.AddToPool(collision.gameObject.tag, collision.gameObject);
}
