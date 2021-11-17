using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class GroundSplash : MonoBehaviour
{
    private ObjectPooler _objectPooler;
    private WaitForSeconds _waitForSplashEffectToDisappear;

    private const float DISAPPEAR_TIME = 5f;

    void Start()
    {
        _objectPooler = ObjectPooler.Instance;
        _waitForSplashEffectToDisappear = new WaitForSeconds(DISAPPEAR_TIME);
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
        yield return _waitForSplashEffectToDisappear;

        _objectPooler.AddToPool(Tags.SplashEffect, splash);
    }
}
