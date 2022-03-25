using System;
using System.Collections.Generic;
using UnityEngine;
using Tags = TagSystem.Tags;

public class TagManager : MonoBehaviour
{
    [SerializeField] List<Tags> tags;

    void OnEnable() => this.CacheObjectToTagSystem(gameObject, tags);

    void OnDisable() => this.RemoveObjectFromTagSystem(gameObject, tags);

    public void AddTag(Tags tagToAdd)
    {
        if (tags.Contains(tagToAdd)) return;

        tags.Add(tagToAdd);
        
    }

    public void RemoveTag(Tags tagToRemove)
    {
        if (!tags.Contains(tagToRemove)) return;

        tags.Remove(tagToRemove);
    }

    public bool HasTag(Tags tagToCompare) => tags.Contains(tagToCompare);
}