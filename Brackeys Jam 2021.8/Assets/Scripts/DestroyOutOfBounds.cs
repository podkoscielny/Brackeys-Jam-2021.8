using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class DestroyOutOfBounds : MonoBehaviour
{
    private ObjectPooler _objectPooler;

    void Awake() => _objectPooler = ObjectPooler.Instance;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (_objectPooler.IsTagInDictionary(collision.tag))
        {
            _objectPooler.AddToPool(collision.tag, collision.gameObject);
        }
    }
}
