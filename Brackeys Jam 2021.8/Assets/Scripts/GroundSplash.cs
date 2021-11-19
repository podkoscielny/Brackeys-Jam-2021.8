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
        if (!collision.HasLabel(Labels.Poop)) return;

        SpawnSplashEffect(collision.gameObject);
    }

    private void SpawnSplashEffect(GameObject poop)
    {
        GameObject splash = ObjectPooler.Instance.GetFromPool(Tags.SplashEffect);

        if (splash == null) return;

        splash.transform.position = poop.transform.position;
        splash.transform.SetParent(transform);
        ObjectPooler.Instance.AddToPool(Tags.Poop, poop);

        StartCoroutine(MoveSplashToPool(splash));
    }

    IEnumerator MoveSplashToPool(GameObject splash)
    {
        yield return new WaitForSeconds(DISAPPEAR_TIME);

        ObjectPooler.Instance.AddToPool(Tags.SplashEffect, splash);
    }
}
