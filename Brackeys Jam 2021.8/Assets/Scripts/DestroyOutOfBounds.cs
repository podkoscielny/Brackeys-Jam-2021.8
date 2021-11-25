using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    [SerializeField] ObjectPool objectPool;

    void OnTriggerExit2D(Collider2D collision)
    {
        if (objectPool.IsTagInDictionary(collision.tag))
        {
            objectPool.AddToPool(collision.tag, collision.gameObject);
        }
    }
}
