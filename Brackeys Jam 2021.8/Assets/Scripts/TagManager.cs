using System;
using UnityEngine;
using Label = Labels.Label;

public class TagManager : MonoBehaviour
{
    [SerializeField] Label labels;

    void Awake()
    {
        Labels.CacheObjectToFindWithLabel(gameObject, labels);
    }

    void OnDestroy()
    {
        Labels.RemoveObjectFromFindWithLabel(gameObject, labels);
    }

    public bool HasLabel(Label label) => (labels & label) == label;

    //private void CheckGameObjectsAmountInDictionary()
    //{
    //    foreach (var item in Labels.LabeledObjects)
    //    {
    //        Debug.Log($"{item.Key} | {item.Value.Count}");
    //    }
    //}
}