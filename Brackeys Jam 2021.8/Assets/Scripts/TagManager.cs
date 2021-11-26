using System;
using System.Collections.Generic;
using UnityEngine;
using Tags = Label.Tags;

public class TagManager : MonoBehaviour
{
    [SerializeField] List<Tags> tags;

    void Awake()
    {
        Label.CacheObjectToFindWithTag(gameObject, tags);
    }

    void OnDestroy()
    {
        Label.RemoveObjectFromFindWithTag(gameObject, tags);
    }

    public bool HasTag(Tags tag) => tags.Contains(tag);
}