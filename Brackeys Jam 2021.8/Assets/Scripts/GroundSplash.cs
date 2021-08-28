using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class GroundSplash : MonoBehaviour
{
    private ObjectPooler _objectPooler;
    private float _disappearTime = 5f;

    void Start()
    {
        _objectPooler = ObjectPooler.Instance;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.Poop))
        {
            GameObject splash = _objectPooler.GetFromPool(Tags.SplashEffect);
            splash.transform.position = collision.transform.position;

            StartCoroutine(MoveSplashToPool(splash));

            _objectPooler.AddToPool(Tags.Poop, collision.gameObject);
        }
    }

    IEnumerator MoveSplashToPool(GameObject splash)
    {
        yield return new WaitForSeconds(_disappearTime);

        _objectPooler.AddToPool(Tags.SplashEffect, splash);
    }
}
