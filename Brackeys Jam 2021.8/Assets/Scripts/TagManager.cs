using System;
using UnityEngine;
using Label = Labels.Label;

public class TagManager : MonoBehaviour
{
    public Label labels;

    void Start()
    {
        Labels.CacheObjectToFindWithLabel(gameObject, labels);
    }

    void OnDestroy()
    {
        Labels.RemoveObjectFromFindWithLabel(gameObject, labels);
    }

    //private void CheckGameObjectsAmountInDictionary()
    //{
    //    foreach (var item in Labels.LabeledObjects)
    //    {
    //        Debug.Log($"{item.Key} | {item.Value.Count}");
    //    }
    //}
}