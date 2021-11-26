using System;
using System.Collections.Generic;
using UnityEngine;
using Labels = Label.Labels;
using Tags = Label.Tags;

public class TagManager : MonoBehaviour
{
    [SerializeField] Labels labels;
    [SerializeField] List<Tags> tags;

    void Awake()
    {
        Label.CacheObjectToFindWithLabel(gameObject, labels);
    }

    void OnDestroy()
    {
        Label.RemoveObjectFromFindWithLabel(gameObject, labels);
    }

    public bool HasLabel(Labels label) => (labels & label) == label;
}