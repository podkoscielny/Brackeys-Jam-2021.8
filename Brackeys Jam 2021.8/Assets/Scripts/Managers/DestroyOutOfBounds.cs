using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    [SerializeField] ObjectPool objectPool;

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PooledObject pooledObject))
        {
            objectPool.AddToPool(pooledObject.PoolTag, collision.gameObject);
        }
    }
}
