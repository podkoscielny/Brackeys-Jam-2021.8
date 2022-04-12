using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AoOkami.MultipleTagSystem;
using Tags = AoOkami.MultipleTagSystem.TagSystem.Tags;

public class Poop : MonoBehaviour
{
    [SerializeField] Rigidbody2D poopRb;
    [SerializeField] Animator poopAnimator;
    [SerializeField] ObjectPool objectPool;
    [SerializeField] PoopSystem poopSystem;

    private void OnEnable() => SetPoopAnimator();

    private void SetPoopAnimator() => poopAnimator.runtimeAnimatorController = poopSystem.CurrentPoop.PoopAnimator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.HasTag(Tags.HittableByPoop)) return;

        if (poopSystem.CurrentPoop.IsExplosive)
        {
            SpawnExplosionEffect();
        }
        else
        {
            collision.GetComponent<IPoopHandler>()?.HandlePoopHit(transform.position);
        }

        gameObject.SetActive(false);
    }

    private void SpawnExplosionEffect()
    {
        Vector2 explosionPosition = transform.position;
        objectPool.GetFromPool(Tags.Explosion, explosionPosition);
    }
}
