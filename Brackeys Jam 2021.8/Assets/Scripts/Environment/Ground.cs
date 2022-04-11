using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AoOkami.MultipleTagSystem;
using Tags = AoOkami.MultipleTagSystem.TagSystem.Tags;

public class Ground : MonoBehaviour
{
    [SerializeField] TagManager tagManager;
    [SerializeField] PoopSystem poopSystem;

    private void OnEnable() => PoopSystem.OnPoopUpgrade += SwitchHittableByPoopTag;

    private void OnDisable() => PoopSystem.OnPoopUpgrade += SwitchHittableByPoopTag;

    private void SwitchHittableByPoopTag()
    {
        if (poopSystem.CurrentPoop.IsExplosive)
        {
            tagManager.AddTag(Tags.HittableByPoop);
        }
        else
        {
            tagManager.RemoveTag(Tags.HittableByPoop);
        }
    }
}