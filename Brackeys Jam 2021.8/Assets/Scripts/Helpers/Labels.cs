using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Label
{
    private static Dictionary<Labels, List<GameObject>> LabeledObjects = new Dictionary<Labels, List<GameObject>>();

    [Serializable]
    [Flags]
    public enum Labels
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
        HittableByPoop = 1 << 10,
        PoopSpawn = 1 << 11
    }

    static Label()
    {
        InitializeLabeledObjectsDictionary();
    }

    private static void InitializeLabeledObjectsDictionary()
    {
        Type enumType = typeof(Labels);
        var enumValues = Enum.GetValues(enumType);

        foreach (var value in enumValues)
        {
            LabeledObjects.Add((Labels)value, new List<GameObject>());
        }
    }

    public static GameObject FindGameObjectWithLabel(Labels label)
    {
        if (!LabeledObjects.ContainsKey(label) || LabeledObjects[label].Count < 1) return null;

        return LabeledObjects[label][0];
    }

    public static GameObject[] FindAllGameObjecstWithLabel(Labels label)
    {
        if (!LabeledObjects.ContainsKey(label) || LabeledObjects[label].Count < 1) return null;

        GameObject[] labeledGameObjects = LabeledObjects[label].ToArray();

        return labeledGameObjects;
    }

    public static void CacheObjectToFindWithLabel(GameObject objectToCache, Labels labels)
    {
        foreach (Enum value in Enum.GetValues(labels.GetType()))
        {
            if (labels.HasFlag(value))
            {
                LabeledObjects[(Labels)value].Add(objectToCache);
            }
        }
    }

    public static void RemoveObjectFromFindWithLabel(GameObject objectToBeRemoved, Labels labels)
    {
        foreach (Enum value in Enum.GetValues(labels.GetType()))
        {
            if (labels.HasFlag(value))
            {
                LabeledObjects[(Labels)value].Remove(objectToBeRemoved);
            }
        }
    }
}

