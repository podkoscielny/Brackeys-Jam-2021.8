using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;
using Tags = TagSystem.Tags;

public class GroundSplash : MonoBehaviour
{
    [SerializeField] ObjectPool objectPool;

    private WaitForSeconds _waitForSplashEffectToDisappear;

    private const float DISAPPEAR_TIME = 5f;

    void Start()
    {
        _waitForSplashEffectToDisappear = new WaitForSeconds(DISAPPEAR_TIME);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.HasTag(Tags.Poop)) return;

        SpawnSplashEffect(collision.gameObject);
    }

    private void SpawnSplashEffect(GameObject poop)
    {
        GameObject splash = objectPool.GetFromPool(Helpers.Tags.SplashEffect);

        if (splash == null) return;

        splash.transform.position = poop.transform.position;
        splash.transform.SetParent(transform);
        objectPool.AddToPool(Helpers.Tags.Poop, poop);

        StartCoroutine(MoveSplashToPool(splash));
    }

    IEnumerator MoveSplashToPool(GameObject splash)
    {
        yield return new WaitForSeconds(DISAPPEAR_TIME);

        objectPool.AddToPool(Helpers.Tags.SplashEffect, splash);
    }
}
