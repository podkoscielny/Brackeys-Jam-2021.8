using System;
using System.Collections.Generic;
using UnityEngine;
using Tags = TagSystem.Tags;

public class TagManager : MonoBehaviour
{
    [SerializeField] List<Tags> tags;

    void Awake()
    {
        this.CacheObjectToTagSystem(gameObject, tags);
    }

    void OnDestroy()
    {
        this.RemoveObjectFromTagSystem(gameObject, tags);
    }

    public bool HasTag(Tags tag) => tags.Contains(tag);
}