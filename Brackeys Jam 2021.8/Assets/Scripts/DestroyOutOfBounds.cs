using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private ObjectPooler _objectPooler;

    void Start() => _objectPooler = ObjectPooler.Instance;

    void OnTriggerExit2D(Collider2D collision)
    {
        if (_objectPooler.IsTagInDictionary(collision.tag))
        {
            _objectPooler.AddToPool(collision.tag, collision.gameObject);
        }
    }
}
