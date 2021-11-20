using System;
using UnityEngine;
using Labels = Label.Labels;

public class TagManager : MonoBehaviour
{
    [SerializeField] Labels labels;

    void Awake()
    {
        Label.CacheObjectToFindWithLabel(gameObject, labels);
    }

    void OnDestroy()
    {
        Label.RemoveObjectFromFindWithLabel(gameObject, labels);
    }

    public bool HasLabel(Labels label) => (labels & label) == label;

    //private void CheckGameObjectsAmountInDictionary()
    //{
    //    foreach (var item in Labels.LabeledObjects)
    //    {
    //        Debug.Log($"{item.Key} | {item.Value.Count}");
    //    }
    //}
}