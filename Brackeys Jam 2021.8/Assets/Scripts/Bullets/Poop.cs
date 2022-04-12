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

    private Vector2 _explosionOffset = new Vector2(0f, 0.75f);

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
        Vector2 explosionPosition = (Vector2)transform.position + _explosionOffset;
        objectPool.GetFromPool(Tags.Explosion, explosionPosition);
    }
}
