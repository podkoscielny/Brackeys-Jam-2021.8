using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Labels
{
    private static Dictionary<Label, List<GameObject>> LabeledObjects = new Dictionary<Label, List<GameObject>>();

    [Serializable]
    [Flags]
    public enum Label
    {
        //None = 0,
        Poop = 1 << 0,
        Corn = 1 << 1,
        Character = 1 << 2,
        Bullet = 1 << 3,
        Hostile = 1 << 4,
        SplashEffect = 1 << 5,
        Ground = 1 << 6,
        Explosion = 1 << 7,
        Player = 1 << 8,
        Life = 1 << 9,
        HittableByPoop = 1 << 10
    }

    static Labels()
    {
        InitializeLabeledObjectsDictionary();
    }

    private static void InitializeLabeledObjectsDictionary()
    {
        Type enumType = typeof(Label);
        var enumValues = Enum.GetValues(enumType);

        foreach (var value in enumValues)
        {
            LabeledObjects.Add((Label)value, new List<GameObject>());
        }
    }

    public static GameObject FindGameObjectWithLabel(Label label)
    {
        if (!LabeledObjects.ContainsKey(label) || LabeledObjects[label].Count < 1) return null;

        return LabeledObjects[label][0];
    }

    public static GameObject[] FindAllGameObjecstWithLabel(Label label)
    {
        if (!LabeledObjects.ContainsKey(label) || LabeledObjects[label].Count < 1) return null;

        GameObject[] labeledGameObjects = LabeledObjects[label].ToArray();

        return labeledGameObjects;
    }

    public static void CacheObjectToFindWithLabel(GameObject objectToCache, Label labels)
    {
        foreach (Enum value in Enum.GetValues(labels.GetType()))
        {
            if (labels.HasFlag(value))
            {
                LabeledObjects[(Label)value].Add(objectToCache);
            }
        }
    }

    public static void RemoveObjectFromFindWithLabel(GameObject objectToBeRemoved, Label labels)
    {
        foreach (Enum value in Enum.GetValues(labels.GetType()))
        {
            if (labels.HasFlag(value))
            {
                LabeledObjects[(Label)value].Remove(objectToBeRemoved);
            }
        }
    }
}

