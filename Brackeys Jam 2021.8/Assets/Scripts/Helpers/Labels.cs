using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public static class Label
{
    [Serializable]
    public enum Tags
    {
        None = 0,
        Poop = 1,
        Corn = 2,
        Character = 3,
        Bullet = 4,
        Hostile = 5,
        SplashEffect = 6,
        Ground = 7,
        Explosion = 8,
        Player = 9,
        Life = 10,
        HittableByPoop = 11,
        PoopSpawn = 12
    }

    private static Dictionary<Tags, List<GameObject>> TaggedObjects = new Dictionary<Tags, List<GameObject>>();
    private static Dictionary<Tags, ReadOnlyCollection<GameObject>> _readonlyObjects = new Dictionary<Tags, ReadOnlyCollection<GameObject>>();

    private static ReadOnlyCollection<GameObject> _emptyList = new List<GameObject>().AsReadOnly();

    static Label()
    {
        InitializeTaggedObjectsDictionary();
    }

    private static void InitializeTaggedObjectsDictionary()
    {
        Type enumType = typeof(Tags);
        var enumValues = Enum.GetValues(enumType);

        foreach (var value in enumValues)
        {
            TaggedObjects.Add((Tags)value, new List<GameObject>());
        }

        foreach (var item in TaggedObjects)
        {
            _readonlyObjects.Add(item.Key, item.Value.AsReadOnly());
        }
    }

    public static GameObject FindGameObjectWithTag(Tags tag)
    {
        if (!TaggedObjects.ContainsKey(tag) || TaggedObjects[tag].Count < 1) return null;

        return TaggedObjects[tag][0];
    }

    public static ReadOnlyCollection<GameObject> FindAllGameObjectsWithTag(Tags tag)
    {
        if (!TaggedObjects.ContainsKey(tag) || TaggedObjects[tag].Count < 1) return _emptyList;

        return _readonlyObjects[tag];
    }

    public static void CacheObjectToTagSystem(this TagManager tagManager, GameObject gameObjectToCache, List<Tags> tags)
    {
        CacheObjectToFindWithTag(gameObjectToCache, tags);
    }

    public static void RemoveObjectFromTagSystem(this TagManager tagManager, GameObject objectToBeRemoved, List<Tags> tags)
    {
        RemoveObjectFromFindWithTag(objectToBeRemoved, tags);
    }

    private static void CacheObjectToFindWithTag(GameObject objectToCache, List<Tags> tags)
    {
        foreach (var item in TaggedObjects)
        {
            if (tags.Contains(item.Key))
            {
                TaggedObjects[item.Key].Add(objectToCache);
            }
        }
    }

    private static void RemoveObjectFromFindWithTag(GameObject objectToBeRemoved, List<Tags> tags)
    {
        foreach (var item in TaggedObjects)
        {
            if (tags.Contains(item.Key))
            {
                TaggedObjects[item.Key].Remove(objectToBeRemoved);
            }
        }
    }
}
