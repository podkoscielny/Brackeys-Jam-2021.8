using System;
using UnityEngine;
using Label = Labels.Label;

public class TagManager : MonoBehaviour
{
    public Label labels;

    void Start()
    {
        CacheObjectToFindByLabel();
    }

    void OnDestroy()
    {
        RemoveObjectFromFindByLabel();
    }

    private void CacheObjectToFindByLabel()
    {
        foreach (Enum value in Enum.GetValues(labels.GetType()))
        {
            if (labels.HasFlag(value))
            {
                Labels.LabeledObjects[(Label)value].Add(gameObject);
            }

        }
    }

    private void RemoveObjectFromFindByLabel()
    {
        foreach (Enum value in Enum.GetValues(labels.GetType()))
        {
            if (labels.HasFlag(value))
            {
                Labels.LabeledObjects[(Label)value].Remove(gameObject);
            }

        }
    }

    private void CheckGameObjectsAmountInDictionary()
    {
        foreach (var item in Labels.LabeledObjects)
        {
            Debug.Log($"{item.Key} | {item.Value.Count}");
        }
    }
}